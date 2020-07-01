/*
 * Library:         EF_UI
 * Module:          EF_UI.cs
 * Date:            2020/06/16
 * Author:          Edward Fischer 0440835
 * Description:     Contains the abstract EF_UI class and related components.
 */

using System;
using System.IO;
using System.Collections.Generic;

namespace EF_UI {
    /// <summary>
    /// Enum containing all colours for each type of message.
    /// </summary>
    public enum MenuColours {
        ERROR_MESSAGE = 12,         //Red
        EXCEPTION_MESSAGE = 4,      //DarkRed
        SUCCESS_MESSAGE = 10,       //Green
        INSTRUCTIONS_MESSAGE = 2,   //DarkGreen
        ACCOUNT = 13,               //Magenta
        TITLE = 1,                  //Blue
        VERY_WEAK = 4,              //DarkRed
        WEAK = 12,                  //Red
        GOOD = 11,                  //Cyan
        STRONG = 2,                 //DarkGreen
        VERY_STRONG = 10,           //Green
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

        public static void DrawMenu(Menu menu) {
            Console.Clear();
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
            DrawPrompt(menu.GetPrompt());
        }

        private static void DrawTitle(MenuTitle title) {
            WriteLine(title.Text, title.Colour, true);
        }

        public static void DrawMenuLines(List<MenuLine> lines) {
            foreach (MenuLine line in lines) {
                WriteLine(line.Text, line.Colour);
            }
        }

        public static void DrawSelections(List<MenuSelection> selections) {
            foreach (MenuSelection selection in selections) {
                WriteLine(selection.Option.PadRight(5) + selection.Text);
            }
        }

        public static void DrawPrompt(string promptText = "") {
            if (promptText == "") {
                Write("Enter an option: ");
            }
            else {
                Write(promptText);
            }
                
            //WriteLine(promptText);
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



        ///// <summary>
        ///// Draws the main menu to the screen including prompt options.
        ///// </summary>
        ///// <param name="accounts">The populated List of accounts.</param>
        //public static void DrawMainMenu(List<Account> accounts) {
        //    //Draw the title
        //    DrawTitle("Password Manager (Edward Fischer 0440835)");
        //    //if there are accounts, draw them
        //    if (accounts.Count > 0)
        //        DrawAccounts(accounts);
        //    //draw the prompts
        //    WriteLine("Please choose from the following options: ", Colours.INSTRUCTIONS_MESSAGE);
        //    //Only allow user to pick an account if there are accounts
        //    if (accounts.Count > 0)
        //        WriteLine(" #. Inspect the chosen number's account details", Colours.INSTRUCTIONS_MESSAGE);
        //    WriteLine(" A. Add a new account", Colours.INSTRUCTIONS_MESSAGE);
        //    WriteLine(" X. Save accounts to JSON and exit the application", Colours.INSTRUCTIONS_MESSAGE);
        //    DrawDivider();
        //}

        ///// <summary>
        ///// Draws the Account Details menu to the screen including prompt options.
        ///// </summary>
        ///// <param name="account">The Account the user has selected</param>
        ///// <param name="selectedAccountIndex">The selected account's index in the List of populated accounts.</param>
        //public static void DrawAccountMenu(Account account, int selectedAccountIndex) {
        //    const int LEFT_PADDING = 20;

        //    DrawTitle($"{selectedAccountIndex + 1}. {account.Description}", Colours.ACCOUNT);
        //    //Write account details
        //    WriteLine("User ID: ".PadLeft(LEFT_PADDING) + account.UserID);
        //    WriteLine("Password: ".PadLeft(LEFT_PADDING) + account.Password.Value);
        //    DrawPasswordStrength($"{account.Password.StrengthText} ({account.Password.StrengthNum}%)", GetColourForStrength(account.Password.StrengthNum));

        //    //Only display LastReset if LastReset is not blank
        //    if (!account.Password.LastReset.Equals("")) {
        //        //Determine the difference between now and when the password was last reset
        //        DateTime now = DateTime.Now;
        //        DateTime lastReset = DateTime.Parse(account.Password.LastReset);
        //        var diff = now.Subtract(lastReset);
        //        string days;
        //        if (diff.Days == 1)
        //            days = $"({diff.Days} day ago)";
        //        else if (diff.Days == 0)
        //            days = "(today)";
        //        else
        //            days = $"({diff.Days} days ago)";

        //        WriteLine("Last Reset: ".PadLeft(LEFT_PADDING) + account.Password.LastReset + $" {days}");
        //    }

        //    //Only display LoginURL if LoginURL is not blank
        //    //IMPORTANT NOTE:
        //    //Due to the JSON schema LoginURL must validate as URI, however a blank URI will not validate
        //    //despite LoginURL being an optional property in the schema.
        //    if (!account.LoginURL.Equals(""))
        //        WriteLine("Login URL: ".PadLeft(LEFT_PADDING) + account.LoginURL);

        //    //Only display AccountNum if AccountNum is not blank
        //    if (!account.AccountNum.Equals(""))
        //        WriteLine("Account #: ".PadLeft(LEFT_PADDING) + account.AccountNum);
        //    DrawDivider();
        //    //User prompt options
        //    WriteLine("Please choose from the following options: ", Colours.INSTRUCTIONS_MESSAGE);
        //    WriteLine(" P. Change this account's password", Colours.INSTRUCTIONS_MESSAGE);
        //    WriteLine(" D. Delete this account", Colours.INSTRUCTIONS_MESSAGE);
        //    WriteLine(" M. Save account and return to main menu", Colours.INSTRUCTIONS_MESSAGE);
        //    DrawDivider();
        //}



        ///// <summary>
        ///// Draws the accounts in the passed List to the console.
        ///// Prepends the ordinal number of each account in each written line.
        ///// Encloses each account within box-edge UI elements.
        ///// </summary>
        ///// <param name="accounts">A List containing the accounts to print to the console</param>
        //private static void DrawAccounts(List<Account> accounts) {
        //    WriteLine("Accounts:", Colours.ACCOUNT);
        //    foreach (Account acc in accounts)
        //        WriteLine($" {accounts.IndexOf(acc) + 1}. {acc.Description}", Colours.ACCOUNT);
        //    DrawDivider();
        //}

        ///// <summary>
        ///// Draws the password strength field to the console, coloured appropriately.
        ///// </summary>
        ///// <param name="strength">The strength string to print to the console</param>
        ///// <param name="colour">The colour of the strength of the password</param>
        //private static void DrawPasswordStrength(string strength, Colours colour) {
        //    int WORKING_AREA = Console.WindowWidth - 3;
        //    const int LEFT_PADDING = 20;
        //    Write("| " + "Password Strength: ".PadLeft(LEFT_PADDING));
        //    Write(strength, colour);
        //    Console.WriteLine("|".PadLeft(WORKING_AREA - strength.Length - LEFT_PADDING));
        //}


        ///////////////////////
        //// Error Writing ////
        ///////////////////////

        /// <summary>
        /// Writes the passed error message to the console, coloured Red.
        /// </summary>
        /// <param name="error">The message to write</param>
        public static void WriteError(string error) {
            WriteColouredLine($"[ERROR]: {error}", MenuColours.ERROR_MESSAGE);
        }

        /// <summary>
        /// Writes the passed exception message to the console, coloured DarkRed.
        /// </summary>
        /// <param name="exception">The exception message to write</param>
        public static void WriteException(string exception) {
            WriteColouredLine(exception, MenuColours.EXCEPTION_MESSAGE);
        }

        /// <summary>
        /// Writes the passed success message to the console, coloured Green.
        /// </summary>
        /// <param name="message">The success message to write</param>
        public static void WriteSuccess(string message) {
            WriteColouredLine($"[SUCCESS]: {message}", MenuColours.SUCCESS_MESSAGE);
        }



    }//end UIHelper
}
