/*
 * Library:         EF_UI
 * Module:          EF_UI.cs
 * Date:            2020/07/01
 * Author:          Edward Fischer
 * Description:     Contains the abstract EF_UI class and related components.
 */

using System;
using System.Collections.Generic;

namespace EF_UI {
    /// <summary>
    /// Enum containing all colours for each type of message.
    /// </summary>
    public enum MenuColours {
        RED = 12,                   //Red
        DARK_RED = 4,               //DarkRed
        GREEN = 10,                 //Green
        DARK_GREEN = 2,             //DarkGreen
        MAGENTA = 13,               //Magenta
        BLUE = 1,                   //Blue
        CYAN = 11,                  //Cyan
        DEFAULT = 15,               //White
    }
    
    /// <summary>
    /// Contains methods that involve drawing and writing UI elements and prompting for user input.
    /// </summary>
    public abstract class UIHelper {
        /// <summary>
        /// Draws a UI divider spanning the width of the console window
        /// </summary>
        private static void DrawDivider() {
            Console.Write("+"); //left side
            int WORKING_AREA = Console.WindowWidth - 3;
            for (int i = 1; i <= WORKING_AREA; ++i) {
                Console.Write("-");
            }
            Console.WriteLine("+"); //right side
        }

        /// <summary>
        /// Draws the passed Menu to the screen.
        /// </summary>
        /// <param name="menu">The Menu to draw to the screen</param>
        public static void DrawMenu(Menu menu, ref List<string> errors, ref List<string> successMessages) {
            Console.Clear();
            //Write out any errors before drawing the Menu
            if (errors != null && errors.Count > 0) {
                foreach (string error in errors) {
                    WriteError(error);
                }
                errors.Clear();
            }
            //Write out any success messages before drawing the Menu
            if (successMessages != null && successMessages.Count > 0) {
                foreach (string success in successMessages) {
                    WriteSuccess(success);
                }
                successMessages.Clear();
            }
            DrawDivider();
            MenuTitle title = menu.GetTitle();
            if (title.Text != "" && title.Text != null) {
                DrawTitle(title);
                DrawDivider();
            }
            List<MenuLine> lines = menu.GetLines();
            if (lines.Count > 0) {
                DrawMenuLines(lines);
                DrawDivider();
            }
            List<MenuSelection> selections = menu.GetMenuSelections();   
            if (selections.Count > 0) {
                DrawSelections(selections);
                DrawDivider();
            }
            //DrawPrompt(menu.GetPrompt());
        }

        /// <summary>
        /// Gets a valid selection from the user for the passed Menu.
        /// Any error messages which should be displayed to the user are returned in errorMessages.
        /// Returns a string with the user's valid selection, or an empty string if user's choice was invalid.
        /// </summary>
        /// <param name="menu">The Menu containing the valid user choices</param>
        /// <param name="errorMessages">A List containing any error messages with the user's selection</param>
        /// <returns>A string containing the user's selection, or an empty string if the user did not make a valid selection</returns>
        public static string GetValidUserSelection(Menu menu, out List<string> errorMessages) {
            DrawPrompt(menu.GetPrompt());
            string userSelection = Console.ReadLine().ToLower();
            List<MenuSelection> validSelections = menu.GetMenuSelections();
            errorMessages = new List<string>();
            foreach (MenuSelection selection in validSelections) {
                if (userSelection.ToLower() == selection.Option.ToLower()) {
                    return userSelection;
                }
            }
            errorMessages.Add($"Invalid choice: \"{userSelection}\"");
            return string.Empty;
        }

        /// <summary>
        /// Draws the passed MenuTitle to the screen centered.
        /// </summary>
        /// <param name="title">The MenuTitle to draw</param>
        private static void DrawTitle(MenuTitle title) {
            WriteLine(title.Text, title.Colour, true);
        }

        /// <summary>
        /// Draws the passed List of MenuLines to the screen
        /// </summary>
        /// <param name="lines">The List of MenuLines to draw</param>
        public static void DrawMenuLines(List<MenuLine> lines) {
            foreach (MenuLine line in lines) {
                WriteLine(line.Text, line.Colour);
            }
        }

        /// <summary>
        /// Draws the passed List of MenuSelections to the screen
        /// </summary>
        /// <param name="selections">The List of MenuSelections to draw to the screen</param>
        public static void DrawSelections(List<MenuSelection> selections) {
            foreach (MenuSelection selection in selections) {
                WriteLine(selection.Option.PadRight(5) + selection.Text);
            }
        }

        /// <summary>
        /// Draws the passed prompt text to the screen
        /// </summary>
        /// <param name="promptText">The text to prompt the user with</param>
        public static void DrawPrompt(string promptText = "") {
            if (promptText == "") {
                Write("Enter an option: ");
            }
            else {
                Write(promptText);
            }
        }
        
        /// <summary>
        /// Writes the passed message to the console.
        /// Does not prepend or append the box-edge UI elements
        /// </summary>
        /// <param name="message">The message to print to the console</param>
        private static void Write(string message) {
            Console.Write(message);
        }

        /// <summary>
        /// Writes the passed message to the console coloured with the passed colour.
        /// Does not prepend or append the box-edge UI elements
        /// </summary>
        /// <param name="message">The message to be coloured and written to the console</param>
        /// <param name="colour">The colour of the message text</param>
        private static void Write(string message, MenuColours colour) {
            Console.ForegroundColor = (ConsoleColor)colour;
            Console.Write(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes the passed line to the console coloured with the passed colour.
        /// Does not prepend or append the box-edge UI elements
        /// </summary>
        /// <param name="line">The line to write</param>
        /// <param name="colour">The colour of the line text</param>
        private static void WriteColouredLine(string line, MenuColours colour) {
            Console.ForegroundColor = (ConsoleColor)colour;
            Console.WriteLine(line);
            Console.ResetColor();
        }

        /// <summary>
        /// Writes a line to the console.
        /// Encloses the line in box-edge UI elements
        /// </summary>
        /// <param name="line">The line to write to the console</param>
        public static void WriteLine(string line) {
            int WORKING_AREA = Console.WindowWidth - 2;
            line = line.PadLeft(line.Length + 1);
            Write("|" + line + "|".PadLeft(WORKING_AREA - line.Length));
            Console.WriteLine();
        }

        /// <summary>
        /// Writes the passed line to the console window, optionally center-justified.
        /// Encloses the line in box-edge UI elements.
        /// </summary>
        /// <param name="line">The line to write to the console window</param>
        /// <param name="centered">True if the line should be center-justified within the console, false if the line should be left-justified</param>
        public static void WriteLine(string line, bool centered = false) {
            if (!centered)
                WriteLine(line);
            else {
                int WORKING_AREA = Console.WindowWidth - 2;
                Write("|" + line.PadLeft(WORKING_AREA / 2 + line.Length / 2) + "|".PadLeft(WORKING_AREA / 2 - line.Length / 2));
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Writes the passed line to the console window with the passed colour.
        /// Encloses the line in box-end UI elements.
        /// </summary>
        /// <param name="line">The line to write to the console</param>
        /// <param name="colour">The colour of the text to write</param>
        public static void WriteLine(string line, MenuColours colour) {
            int WORKING_AREA = Console.WindowWidth - 2;
            Write("|");
            Write(line, colour);
            Console.WriteLine("|".PadLeft(WORKING_AREA - line.Length));
        }

        /// <summary>
        /// Writes the passed line to the console window with the passed colour, optionally centered.
        /// </summary>
        /// <param name="line">The line to write to the console</param>
        /// <param name="colour">The colour of the text to write.</param>
        /// <param name="centered">True if the line should be center-justified, false if the line should be left-aligned</param>
        public static void WriteLine(string line, MenuColours colour, bool centered = false) {
            if (!centered)
                WriteLine(line, colour);
            else {
                int WORKING_AREA = Console.WindowWidth - 2;
                Console.Write("|");

                Write(line.PadLeft(WORKING_AREA / 2 + line.Length / 2), colour);
                //if working area is odd
                if (WORKING_AREA % 2 != 0)
                    Console.WriteLine("|".PadLeft(WORKING_AREA / 2 - line.Length / 2 + 1)); //add an extra padding char
                else
                    Console.WriteLine("|".PadLeft(WORKING_AREA / 2 - line.Length / 2));
            }
            
        }


        ///////////////////////
        //// Error Writing ////
        ///////////////////////

        /// <summary>
        /// Writes the passed error message to the console, coloured Red.
        /// </summary>
        /// <param name="error">The message to write</param>
        public static void WriteError(string error) {
            WriteColouredLine($"[ERROR]: {error}", MenuColours.RED);
        }

        /// <summary>
        /// Writes the passed exception message to the console, coloured DarkRed.
        /// </summary>
        /// <param name="exception">The exception message to write</param>
        public static void WriteException(string exception) {
            WriteColouredLine(exception, MenuColours.DARK_RED);
        }

        /// <summary>
        /// Writes the passed success message to the console, coloured Green.
        /// </summary>
        /// <param name="message">The success message to write</param>
        public static void WriteSuccess(string message) {
            WriteColouredLine($"[SUCCESS]: {message}", MenuColours.GREEN);
        }
    }//end UIHelper
}
