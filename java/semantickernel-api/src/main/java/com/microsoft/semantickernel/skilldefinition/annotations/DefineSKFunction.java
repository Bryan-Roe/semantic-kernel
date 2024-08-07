// Copyright (c) Microsoft. All rights reserved.
package com.microsoft.semantickernel.skilldefinition.annotations;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/** Annotation that defines a method that can be invoked as a native function */
@Retention(RetentionPolicy.RUNTIME)
@Target(ElementType.METHOD)
public @interface DefineSKFunction {
    String description() default "";

    String name() default "";
<<<<<<< AI
=======
<<<<<<< HEAD
>>>>>>> main

    String returnType() default "void";

    String returnDescription() default "";
<<<<<<< AI
=======
=======
>>>>>>> beeed7b7a795d8c989165740de6ddb21aeacbb6f
>>>>>>> main
}
