/*
 * Library:         EF_UI
 * Module:          Program.cs
 * Date:            2020/07/01
 * Author:          Edward Fischer
 * Description:     A simple demo program illustrating the use of the EF_UI.UIHelper and Menu
 */

using System.Collections.Generic;
using EF_UI;
using UI = EF_UI.UIHelper; //Bind EF_UI.UIHelper.* to UI.*

namespace EF_UserInterface {
    class Program {
        static void Main() {
            Menu mainMenu = new Menu();
            mainMenu.SetTitle("Main Menu", MenuColours.BLUE);
            mainMenu.AddLine("This is a test line");
            mainMenu.AddLine("This is a second test line");
            mainMenu.AddLine("This is a much longer, much more AWESOME, third line!", MenuColours.RED);
            mainMenu.AddMenuSelection(new MenuSelection("A", "Awesomesauce!", MenuColours.GREEN));
            mainMenu.AddMenuSelection(new MenuSelection("B", "Bawesome!", MenuColours.BLUE));
            mainMenu.AddMenuSelection(new MenuSelection("C", "Coolio!"));
            List<string> errors = new List<string>();
            List<string> successMessages = new List<string>();
            while (true) {
                UI.DrawMenu(mainMenu, ref errors, ref successMessages);
                UI.GetValidUserSelection(mainMenu, out errors);
            }
            
        }
    }
}
