# BCEdit180
A java classfile viewer and editor, written in C#. 

Similar to jclasslib but will soon support many more features, e.g copy and pasting bytecode, adding and removing methods and fields, etc

See the bottom of this page for a feature list... and also some bugs :(

## Preview
### Bytecode editor
![](z17dYi6K4J.png)
### Class info
![](ydGC874C0X.png)

# Installing
You can download the release.zip file from releases, which includes the .exe with all the dependencies

You will also need to build the projects REghZy.MVVM and REghZy.WPF from https://github.com/AngryCarrot789/REghZyUtilsCS

That leaves you with 3 DLL files you need to reference in this project. Once you reference them, you should be able to build

You don't need to download my fork of the JavaAsm library, as i moved it into this solution. But it can be found here: https://github.com/AngryCarrot789/java-asm
My fork of java-asm targets .NET Standard 2.0 (instead of 2.1 which the original creator used), as well as my MVVM and WPF libraries, which works fine with .NET Framework 4.7.2 which this project uses (i think... i didn't really look)

# Features
- Class info viewer
- Interfaces list (can edit, add and remove interfaces)
- Editable class attributes (apart from bootstrap methods; they are contained in the method instructions)
- Method list, + general method info editor (descriptor, name, max stack/locals, etc)
- Method instruction editor (in bytecode form) which is (hopefully) fully functional, allowing almost everything to be modified (apart from labels). The bytecode editor also has colours, which helps the details stand out
- Exception table and local variable table editor. Cannot add/remove exceptions or local variables currently (coming soon)
- Field list + general field info editor (name, descriptor, signature, etc)
- A source code generator (does not actually generate method source code, only the structure of the class (all methods will look 'abstract'))
- You can create and remove methods too. But you can't actually add instructions to the methods yet, so it's pretty much pointless
- You can create and remove fields, which might have some use with reflection or when using custom ASM libraries
- You can view annotations, but you can only edit the annotation type, and the name/type of the annotation's entries. Will add more to this soon though

There's probably more that i've missed, but this is generally what this program can do

## Slight lag issues
When loading big class files (with 100s of methods), it may lag for a split second. The bytecode editor will be the most laggiest (scrolling isn't laggy at all); when you select a new method, it has to clear a list of instructions and create new list items for each instruction. WPF controls, like ListBoxItems (which the bytecode editor use for every instruction) usually takes about half a millisecond (0.5ms~) to create and add to the bytecode editor list. Meaning when you select a method with, say, 1000 instructions, there will be a half second lag spike. This is a limit in the WPF framework and i'm 99% certain there's no way to decrease this lag :( (unless i switch to a text editor... but i don't work at JetBrains so :<   )
