// Copyright (c) Microsoft. All rights reserved.
// Based on code from: https://github.com/facebook/lexical/blob/main/packages/lexical-react/src/shared/LexicalMenu.ts

import { useLexicalComposerContext } from '@fluentui-copilot/react-copilot';
import { mergeRegister } from '@lexical/utils';
import {
    $getSelection,
    $isRangeSelection,
    COMMAND_PRIORITY_LOW,
    CommandListenerPriority,
    KEY_ARROW_DOWN_COMMAND,
    KEY_ARROW_UP_COMMAND,
    KEY_ENTER_COMMAND,
    KEY_ESCAPE_COMMAND,
    KEY_TAB_COMMAND,
    LexicalEditor,
    TextNode,
} from 'lexical';
import React from 'react';
import { SCROLL_TYPEAHEAD_OPTION_INTO_VIEW_COMMAND } from './TypeaheadMenuPlugin';

export type MenuTextMatch = {
    leadOffset: number;
    matchingString: string;
    replaceableString: string;
};

export type MenuResolution = {
    match?: MenuTextMatch;
    getRect: () => DOMRect;
};

export const punctuationCharacters = '\\.,\\+\\*\\?\\$\\@\\|#{}\\(\\)\\^\\-\\[\\]\\\\/!%\'"~=<>_:;';

export class MenuOption {
    key: string;
    ref?: React.MutableRefObject<HTMLElement | null>;

    constructor(key: string) {
        this.key = key;
        this.ref = { current: null };
        this.setRefElement = this.setRefElement.bind(this);
    }

    setRefElement(element: HTMLElement | null) {
        this.ref = { current: element };
    }
}

export type MenuRenderFn<TOption extends MenuOption> = (
    anchorElementRef: React.MutableRefObject<HTMLElement | null>,
    itemProps: {
        selectedIndex: number | null;
        selectOptionAndCleanUp: (option: TOption) => void;
        setHighlightedIndex: (index: number) => void;
        options: Array<TOption>;
    },
    matchingString: string | null,
) => React.ReactPortal | JSX.Element | null;

const scrollIntoViewIfNeeded = (target: HTMLElement) => {
    const typeaheadContainerNode = document.getElementById('typeahead-menu');
    if (!typeaheadContainerNode) {
        return;
    }

    const typeaheadRect = typeaheadContainerNode.getBoundingClientRect();

    if (typeaheadRect.top + typeaheadRect.height > window.innerHeight) {
        typeaheadContainerNode.scrollIntoView({
            block: 'center',
        });
    }

    if (typeaheadRect.top < 0) {
        typeaheadContainerNode.scrollIntoView({
            block: 'center',
        });
    }

    target.scrollIntoView({ block: 'nearest' });
};

/**
 * Walk backwards along user input and forward through entity title to try
 * and replace more of the user's text with entity.
 */
const getFullMatchOffset = (documentText: string, entryText: string, offset: number): number => {
    let triggerOffset = offset;
    for (let i = triggerOffset; i <= entryText.length; i++) {
        if (documentText.slice(-i) === entryText.slice(0, i)) {
            triggerOffset = i;
        }
    }
    return triggerOffset;
};

/**
 * Split Lexical TextNode and return a new TextNode only containing matched text.
 * Common use cases include: removing the node, replacing with a new node.
 */
const $splitNodeContainingQuery = (match: MenuTextMatch): TextNode | null => {
    const selection = $getSelection();
    if (!$isRangeSelection(selection) || !selection.isCollapsed()) {
        return null;
    }
    const anchor = selection.anchor;
    if (anchor.type !== 'text') {
        return null;
    }
    const anchorNode = anchor.getNode();
    if (!anchorNode.isSimpleText()) {
        return null;
    }
    const selectionOffset = anchor.offset;
    const textContent = anchorNode.getTextContent().slice(0, selectionOffset);
    const characterOffset = match.replaceableString.length;
    const queryOffset = getFullMatchOffset(textContent, match.matchingString, characterOffset);
    const startOffset = selectionOffset - queryOffset;
    if (startOffset < 0) {
        return null;
    }
    let newNode;
    if (startOffset === 0) {
        [newNode] = anchorNode.splitText(selectionOffset);
    } else {
        [, newNode] = anchorNode.splitText(startOffset, selectionOffset);
    }

    return newNode;
};

// Got from https://stackoverflow.com/a/42543908/2013580
export const getScrollParent = (element: HTMLElement, includeHidden: boolean): HTMLElement | HTMLBodyElement => {
    let style = getComputedStyle(element);
    const excludeStaticParent = style.position === 'absolute';
    const overflowRegex = includeHidden ? /(auto|scroll|hidden)/ : /(auto|scroll)/;
    if (style.position === 'fixed') {
        return document.body;
    }
    for (let parent: HTMLElement | null = element; (parent = parent.parentElement); ) {
        style = getComputedStyle(parent);
        if (excludeStaticParent && style.position === 'static') {
            continue;
        }
        if (overflowRegex.test(style.overflow + style.overflowY + style.overflowX)) {
            return parent;
        }
    }
    return document.body;
};

const isTriggerVisibleInNearestScrollContainer = (
    targetElement: HTMLElement,
    containerElement: HTMLElement,
): boolean => {
    const tRect = targetElement.getBoundingClientRect();
    const cRect = containerElement.getBoundingClientRect();
    return tRect.top > cRect.top && tRect.top < cRect.bottom;
};

// Reposition the menu on scroll, window resize, and element resize.
export const useDynamicPositioning = (
    resolution: MenuResolution | null,
    targetElement: HTMLElement | null,
    onReposition: () => void,
    onVisibilityChange?: (isInView: boolean) => void,
) => {
    const [editor] = useLexicalComposerContext();
    React.useEffect(() => {
        if (targetElement != null && resolution != null) {
            const rootElement = editor.getRootElement();
            const rootScrollParent = rootElement != null ? getScrollParent(rootElement, false) : document.body;
            let ticking = false;
            let previousIsInView = isTriggerVisibleInNearestScrollContainer(targetElement, rootScrollParent);
            const handleScroll = () => {
                if (!ticking) {
                    window.requestAnimationFrame(() => {
                        onReposition();
                        ticking = false;
                    });
                    ticking = true;
                }
                const isInView = isTriggerVisibleInNearestScrollContainer(targetElement, rootScrollParent);
                if (isInView !== previousIsInView) {
                    previousIsInView = isInView;
                    if (onVisibilityChange != null) {
                        onVisibilityChange(isInView);
                    }
                }
            };
            const resizeObserver = new ResizeObserver(onReposition);
            window.addEventListener('resize', onReposition);
            document.addEventListener('scroll', handleScroll, {
                capture: true,
                passive: true,
            });
            resizeObserver.observe(targetElement);
            return () => {
                resizeObserver.unobserve(targetElement);
                window.removeEventListener('resize', onReposition);
                document.removeEventListener('scroll', handleScroll, true);
            };
        }

        return;
    }, [targetElement, editor, onVisibilityChange, onReposition, resolution]);
};

export const LexicalMenu = <TOption extends MenuOption>({
    close,
    editor,
    anchorElementRef,
    resolution,
    options,
    menuRenderFn,
    onSelectOption,
    shouldSplitNodeWithQuery = false,
    commandPriority = COMMAND_PRIORITY_LOW,
}: {
    close: () => void;
    editor: LexicalEditor;
    anchorElementRef: React.MutableRefObject<HTMLElement>;
    resolution: MenuResolution;
    options: Array<TOption>;
    shouldSplitNodeWithQuery?: boolean;
    menuRenderFn: MenuRenderFn<TOption>;
    onSelectOption: (
        option: TOption,
        textNodeContainingQuery: TextNode | null,
        closeMenu: () => void,
        matchingString: string,
    ) => void;
    commandPriority?: CommandListenerPriority;
}): JSX.Element | null => {
    const [selectedIndex, setHighlightedIndex] = React.useState<null | number>(null);

    const matchingString = resolution.match && resolution.match.matchingString;

    React.useEffect(() => {
        setHighlightedIndex(0);
    }, [matchingString]);

    const selectOptionAndCleanUp = React.useCallback(
        (selectedEntry: TOption) => {
            editor.update(() => {
                const textNodeContainingQuery =
                    resolution.match != null && shouldSplitNodeWithQuery
                        ? $splitNodeContainingQuery(resolution.match)
                        : null;

                onSelectOption(
                    selectedEntry,
                    textNodeContainingQuery,
                    close,
                    resolution.match ? resolution.match.matchingString : '',
                );
            });
        },
        [editor, shouldSplitNodeWithQuery, resolution.match, onSelectOption, close],
    );

    const updateSelectedIndex = React.useCallback(
        (index: number) => {
            const rootElem = editor.getRootElement();
            if (rootElem !== null) {
                rootElem.setAttribute('aria-activedescendant', 'typeahead-item-' + index);
                setHighlightedIndex(index);
            }
        },
        [editor],
    );

    React.useEffect(() => {
        return () => {
            const rootElem = editor.getRootElement();
            if (rootElem !== null) {
                rootElem.removeAttribute('aria-activedescendant');
            }
        };
    }, [editor]);

    React.useLayoutEffect(() => {
        if (options === null) {
            setHighlightedIndex(null);
        } else if (selectedIndex === null) {
            updateSelectedIndex(0);
        }
    }, [options, selectedIndex, updateSelectedIndex]);

    React.useEffect(() => {
        return mergeRegister(
            editor.registerCommand(
                SCROLL_TYPEAHEAD_OPTION_INTO_VIEW_COMMAND,
                ({ option }) => {
                    if (option.ref && option.ref.current != null) {
                        scrollIntoViewIfNeeded(option.ref.current);
                        return true;
                    }

                    return false;
                },
                commandPriority,
            ),
        );
    }, [editor, updateSelectedIndex, commandPriority]);

    React.useEffect(() => {
        return mergeRegister(
            editor.registerCommand<KeyboardEvent>(
                KEY_ARROW_DOWN_COMMAND,
                (payload) => {
                    const event = payload;
                    if (options !== null && options.length && selectedIndex !== null) {
                        const newSelectedIndex = selectedIndex !== options.length - 1 ? selectedIndex + 1 : 0;
                        updateSelectedIndex(newSelectedIndex);
                        const option = options[newSelectedIndex];
                        if (option.ref != null && option.ref.current) {
                            editor.dispatchCommand(SCROLL_TYPEAHEAD_OPTION_INTO_VIEW_COMMAND, {
                                index: newSelectedIndex,
                                option,
                            });
                        }
                        event.preventDefault();
                        event.stopImmediatePropagation();
                    }
                    return true;
                },
                commandPriority,
            ),
            editor.registerCommand<KeyboardEvent>(
                KEY_ARROW_UP_COMMAND,
                (payload) => {
                    const event = payload;
                    if (options !== null && options.length && selectedIndex !== null) {
                        const newSelectedIndex = selectedIndex !== 0 ? selectedIndex - 1 : options.length - 1;
                        updateSelectedIndex(newSelectedIndex);
                        const option = options[newSelectedIndex];
                        if (option.ref != null && option.ref.current) {
                            scrollIntoViewIfNeeded(option.ref.current);
                        }
                        event.preventDefault();
                        event.stopImmediatePropagation();
                    }
                    return true;
                },
                commandPriority,
            ),
            editor.registerCommand<KeyboardEvent>(
                KEY_ESCAPE_COMMAND,
                (payload) => {
                    const event = payload;
                    event.preventDefault();
                    event.stopImmediatePropagation();
                    close();
                    return true;
                },
                commandPriority,
            ),
            editor.registerCommand<KeyboardEvent>(
                KEY_TAB_COMMAND,
                (payload) => {
                    const event = payload;
                    if (options === null || selectedIndex === null || options[selectedIndex] == null) {
                        return false;
                    }
                    event.preventDefault();
                    event.stopImmediatePropagation();
                    selectOptionAndCleanUp(options[selectedIndex]);
                    return true;
                },
                commandPriority,
            ),
            editor.registerCommand(
                KEY_ENTER_COMMAND,
                (event: KeyboardEvent | null) => {
                    if (options === null || selectedIndex === null || options[selectedIndex] == null) {
                        return false;
                    }
                    if (event !== null) {
                        event.preventDefault();
                        event.stopImmediatePropagation();
                    }
                    selectOptionAndCleanUp(options[selectedIndex]);
                    return true;
                },
                commandPriority,
            ),
        );
    }, [selectOptionAndCleanUp, close, editor, options, selectedIndex, updateSelectedIndex, commandPriority]);

    const listItemProps = React.useMemo(
        () => ({
            options,
            selectOptionAndCleanUp,
            selectedIndex,
            setHighlightedIndex,
        }),
        [selectOptionAndCleanUp, selectedIndex, options],
    );

    return menuRenderFn(anchorElementRef, listItemProps, resolution.match ? resolution.match.matchingString : '');
};

export const useMenuAnchorRef = (
    resolution: MenuResolution | null,
    setResolution: (r: MenuResolution | null) => void,
    className?: string,
    parent: HTMLElement = document.body,
): React.MutableRefObject<HTMLElement> => {
    const [editor] = useLexicalComposerContext();
    const anchorElementRef = React.useRef<HTMLElement>(document.createElement('div'));
    const positionMenu = React.useCallback(() => {
        anchorElementRef.current.style.top = anchorElementRef.current.style.bottom;
        const rootElement = editor.getRootElement();
        const containerDiv = anchorElementRef.current;

        const menuEle = containerDiv.firstChild as HTMLElement;
        if (rootElement !== null && resolution !== null) {
            const { left, top, width, height } = resolution.getRect();
            containerDiv.style.bottom = `${top + window.scrollY}px`;
            containerDiv.style.left = `${left + window.scrollX}px`;
            containerDiv.style.height = `${height}px`;
            containerDiv.style.width = `${width}px`;
            if (menuEle !== null) {
                menuEle.style.top = `${top}`;
                const menuRect = menuEle.getBoundingClientRect();
                const menuHeight = menuRect.height;
                const menuWidth = menuRect.width;

                const rootElementRect = rootElement.getBoundingClientRect();

                if (left + menuWidth > rootElementRect.right) {
                    containerDiv.style.left = `${rootElementRect.right - menuWidth + window.scrollX}px`;
                }
                if (
                    (top + menuHeight > window.innerHeight || top + menuHeight > rootElementRect.bottom) &&
                    top - rootElementRect.top > menuHeight + height
                ) {
                    containerDiv.style.top = `${top - menuHeight + window.scrollY - height}px`;
                }
            }

            if (!containerDiv.isConnected) {
                if (className != null) {
                    containerDiv.className = className;
                }
                containerDiv.setAttribute('aria-label', 'Typeahead menu');
                containerDiv.setAttribute('id', 'typeahead-menu');
                containerDiv.setAttribute('role', 'listbox');
                containerDiv.style.display = 'block';
                containerDiv.style.position = 'absolute';
                parent.append(containerDiv);
            }
            anchorElementRef.current = containerDiv;
            rootElement.setAttribute('aria-controls', 'typeahead-menu');
        }
    }, [editor, resolution, className, parent]);

    React.useEffect(() => {
        const rootElement = editor.getRootElement();
        if (resolution !== null) {
            positionMenu();
            return () => {
                if (rootElement !== null) {
                    rootElement.removeAttribute('aria-controls');
                }

                const containerDiv = anchorElementRef.current;
                if (containerDiv !== null && containerDiv.isConnected) {
                    containerDiv.remove();
                }
            };
        }

        return;
    }, [editor, positionMenu, resolution]);

    const onVisibilityChange = React.useCallback(
        (isInView: boolean) => {
            if (resolution !== null) {
                if (!isInView) {
                    setResolution(null);
                }
            }
        },
        [resolution, setResolution],
    );

    useDynamicPositioning(resolution, anchorElementRef.current, positionMenu, onVisibilityChange);

    return anchorElementRef;
};

export type TriggerFn = (text: string, editor: LexicalEditor) => MenuTextMatch | null;