# EF_UserInterface
Simple UI/Menu system for C# console applications
EF_UserInterface is a simple Menu system for console applications written in C#.
It provides an abstract object, `Menu`, through which programmers can easily create a simple interface supporting multiple information lines, user options and data validation.

## `Menu`
The format of a `Menu` is laid out like this:
|Title|
|:----|
| |
|Lines|
| |
|MenuSelections|

Title is a short description of the Menu, like "Main Menu". Titles are center-justified in the Title bar, and can be optionally coloured.

Lines represent informational lines to be displayed as part of the Menu, for instance, an Accounts Menu might display things such as a username and password here.
Lines are left-justified, and can be optionally coloured.

MenuSelections are objects which represent valid choices for the user, they consist of an Option and Text.
Option represents the text the user must type to indicate their choice. Important Note: Options are case-insensitive!
Text is displayed next to Option to indicate to the user what this choice should do.
For example:
|Option|Text|
|:-----|:---|
|A|Add a new account|
|R|Remove an account|
|X|Exit the application|
MenuSelections are left-justified, and can be optionally coloured in a future update.

## UI_Helper
UI_Helper is an abstract class which contains useful methods for interacting with `Menu`s, specifically UI_Helper contains `DrawMenu()` and `GetValidUserSelection()`,
which draw the passed `Menu` to the console and get a valid user selection for the passed `Menu`, respectfully.

Included in the repository is a (very) simple demo C# program which demonstrates the use of the `Menu` class and the UI_Helper `DrawMenu` and `GetValidUserSelection` methods.
