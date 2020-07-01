/*
 * Library:         EF_UI
 * Module:          Menu.cs
 * Date:            2020/06/29
 * Author:          Edward Fischer 0440835
 * Description:     Contains the Menu class and related MenuItem struct.
 */

using System;
using System.Collections.Generic;
using System.Text;
namespace EF_UI {
    /// <summary>
    /// Menu class.
    /// Contains a title to be displayed at the top of the screen
    /// MenuLines which represent strings to be displayed in the middle portion of the menu
    /// MenuSelections which represent strings that should be displayed to the user as valid selections
    /// </summary>
    public class Menu {
        private MenuTitle Title;
        //private List<string> MenuLines;
        private List<MenuLine> MenuLines;
        private List<MenuSelection> MenuSelections;
        //private List<string> ValidUserSelections;
        private string Prompt;

        public void SetPrompt(string prompt) {
            Prompt = prompt;
        }

        public string GetPrompt() {
            return Prompt;
        }

        public Menu() {
            Title = new MenuTitle();
            MenuLines = new List<MenuLine>();
            MenuSelections = new List<MenuSelection>();
            Prompt = string.Empty;
        }

        public MenuTitle GetTitle() { return Title; }
        public void SetTitle(MenuTitle title) { Title = title; }
        public void SetTitle(string title, MenuColours colour = MenuColours.DEFAULT) {
            MenuTitle newTitle = new MenuTitle(title, colour);
            Title = newTitle;
        }
        public List<MenuLine> GetLines() { return MenuLines; }
        public void SetLines(List<MenuLine> lines) { MenuLines = lines; }
        //public List<string> GetMenuItems() { return MenuItems_; }
        public void SetMenuItems(List<MenuSelection> menuItems) { MenuSelections = menuItems; }
        public List<MenuSelection> GetMenuSelections() { return MenuSelections; }
        public void SetMenuSelections(List<MenuSelection> menuSelections) { MenuSelections = menuSelections; }
        //public List<string> GetValidUserSelections() { return ValidUserSelections; }
        //public void SetValidUserSelections(List<string> validUserSelections) { ValidUserSelections = validUserSelections; }

        public int AddLine(string line, MenuColours colour = MenuColours.DEFAULT) {
            MenuLine newLine = new MenuLine(line, colour);
            MenuLines.Add(newLine);
            return MenuLines.Count;
        }

        public int AddLines(List<string> lines) {
            foreach (string line in lines) {
                MenuLine newLine = new MenuLine(line);
                MenuLines.Add(newLine);
            }
            //MenuLines.AddRange(lines);
            return MenuLines.Count;
        }

        public int CountLines() {
            return MenuLines.Count;
        }

        public void RemoveLine(int pos) {
            try {
                MenuLines.RemoveAt(pos);
            }
            catch (ArgumentOutOfRangeException ex) {
                throw ex;
            }
        }

        public void RemoveLines(int startPos, int count) {
            try {
                MenuLines.RemoveRange(startPos, count);
            }
            catch (ArgumentOutOfRangeException rex) {
                throw rex;
            }
            catch (ArgumentException aex) {
                throw aex;
            }
        }

        public int AddMenuItem(MenuSelection item) {
            MenuSelections.Add(item);
            return MenuSelections.Count;
        }

        public int AddMenuItem(string option, string text) {
            MenuSelection newItem = new MenuSelection(option, text);
            MenuSelections.Add(newItem);
            return MenuSelections.Count;
        }

        public int AddMenuItems(List<MenuSelection> items) {
            MenuSelections.AddRange(items);
            return MenuSelections.Count;
        }
        public int CountMenuItems() {
            return MenuSelections.Count;
        }
    }

    public readonly struct MenuTitle {
        public MenuColours Colour { get; }
        public string Text { get; }
        public MenuTitle(string text, MenuColours colour = MenuColours.DEFAULT) {
            Text = text;
            Colour = colour;
        }
    }

    public readonly struct MenuLine {
        public MenuColours Colour { get; }
        public string Text { get; }
        public MenuLine(string line, MenuColours colour = MenuColours.DEFAULT) {
            Text = line;
            Colour = colour;
        }
    }

    public readonly struct MenuSelection {
        public string Option { get; }
        public string Text { get; }
        public MenuColours OptionColour { get; }
        public MenuColours TextColour { get; }

        public MenuSelection(
            string option,
            string text,
            MenuColours optionColour = MenuColours.DEFAULT,
            MenuColours textColour = MenuColours.DEFAULT) {
            Option = option;
            Text = text;
            OptionColour = optionColour;
            TextColour = textColour;
        }
    }
}