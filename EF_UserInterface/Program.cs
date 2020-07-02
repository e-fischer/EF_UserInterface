/*
 * Library:         EF_UI
 * Module:          Program.cs
 * Date:            2020/07/01
 * Author:          Edward Fischer
 * Description:     A simple demo program illustrating the use of the EF_UI.UIHelper and Menu
 */

using System;
using EF_UI;
using UI = EF_UI.UIHelper;

namespace EF_UserInterface {
    class Program {
        static void Main() {
            Menu test1 = new Menu();
            test1.SetTitle("Test", MenuColours.TITLE);
            test1.AddLine("This is a test line");
            test1.AddLine("This is a second test line");
            test1.AddLine("This is a much longer, much more AWESOME, third line!", MenuColours.WEAK);
            test1.AddMenuSelection(new MenuSelection("A", "Awesomesauce!", MenuColours.STRONG));
            test1.AddMenuSelection(new MenuSelection("B", "Bawesome!", MenuColours.TITLE));
            test1.AddMenuSelection(new MenuSelection("C", "Coolio!"));
            UI.DrawMenu(test1);
        }
    }
}
