using System;
using EF_UI;
using UI = EF_UI.UIHelper;

namespace EF_UserInterface {
    class Program {
        static void Main(string[] args) {
            Menu test1 = new Menu();
            test1.SetTitle("Test", MenuColours.TITLE);
            test1.AddLine("This is a test line");
            test1.AddLine("This is a second test line");
            test1.AddLine("This is a much longer, much more AWESOME, third line!", MenuColours.WEAK);
            test1.AddMenuItem(new MenuSelection("A", "Awesomesauce!", MenuColours.STRONG, MenuColours.STRONG));
            test1.AddMenuItem(new MenuSelection("B", "Bawesome!", MenuColours.TITLE, MenuColours.TITLE));
            test1.AddMenuItem(new MenuSelection("C", "Coolio!"));
            UI.DrawMenu(test1);
        }
    }
}
