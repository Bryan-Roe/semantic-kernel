package com.microsoft.semantickernel.samples.syntaxexamples;

import com.microsoft.semantickernel.coreskills.TextSkill;

<<<<<<< HEAD
/**
 * Demonstrates a native function from the {@code com.microsoft.semantickernel.coreskills} package.
 */
=======
>>>>>>> main
public class Example01_NativeFunctions {

    public static void main(String[] args) {

        // Load native skill
        TextSkill text = new TextSkill();

        // Use function without kernel
        String result = text.uppercase("ciao!");

        System.out.println(result);
    }
}
