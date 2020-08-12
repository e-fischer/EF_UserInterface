/*
 * Library:         EF_UI
 * Module:          Menu.cs
 * Date:            2020/07/01
 * Author:          Edward Fischer
 * Description:     Contains the Menu class and related immutable structs.
 */

using System;
using System.Collections.Generic;
namespace EF_UI {
    /// <summary>
    /// Menu class.
    /// Contains a MenuTitle to be displayed at the top of the screen
    /// MenuLines which represent strings to be displayed in the middle portion of the menu
    /// MenuSelections which represent strings that should be displayed to the user as valid selections
    /// </summary>
    public class Menu {
        private MenuTitle Title;
        private List<MenuLine> MenuLines;
        private List<MenuSelection> MenuSelections;
        private string Prompt;

        //Constructors
        public Menu() {
            Title = new MenuTitle();
            MenuLines = new List<MenuLine>();
            MenuSelections = new List<MenuSelection>();
            Prompt = string.Empty;
        }

        public Menu(MenuTitle title, List<MenuLine> lines, List<MenuSelection> selections, string prompt) {
            Title = title;
            MenuLines = lines;
            MenuSelections = selections;
            Prompt = prompt;
        }

        //Accessors + Mutators
        public void SetPrompt(string prompt) {
            Prompt = prompt;
        }

        public string GetPrompt() {
            return Prompt;
        }
        public MenuTitle GetTitle() {
            return Title; 
        }
        public void SetTitle(MenuTitle title) {
            Title = title; 
        }
        public void SetTitle(string title, MenuColours colour = MenuColours.DEFAULT) {
            MenuTitle newTitle = new MenuTitle(title, colour);
            Title = newTitle;
        }
        public void SetTitle(string title, ConsoleColor colour = ConsoleColor.White) {
            MenuTitle newTitle = new MenuTitle(title, (MenuColours) colour);
            Title = newTitle;
        }
        public List<MenuLine> GetLines() { 
            return MenuLines; 
        }
        public void SetLines(List<MenuLine> lines) { 
            MenuLines = lines; 
        }
        public void SetMenuItems(List<MenuSelection> menuItems) { 
            MenuSelections = menuItems;
        }
        public List<MenuSelection> GetMenuSelections() { 
            return MenuSelections; 
        }
        public void SetMenuSelections(List<MenuSelection> menuSelections) { 
            MenuSelections = menuSelections; 
        }

        /// <summary>
        /// Constructs a new MenuItem and adds it to this Menu's list of MenuLines
        /// </summary>
        /// <param name="line">The string to be displayed</param>
        /// <param name="colour">Optional MenuColour for the line</param>
        public void AddLine(string line, MenuColours colour = MenuColours.DEFAULT) {
            MenuLine newLine = new MenuLine(line, colour);
            MenuLines.Add(newLine);
        }

        /// <summary>
        /// Constructs new default-coloured MenuLines for each string in the passed list 
        /// and adds them to this Menu's list of MenuLines
        /// </summary>
        /// <param name="lines">A list of strings to be displayed</param>
        public void AddLines(List<string> lines) {
            foreach (string line in lines) {
                MenuLine newLine = new MenuLine(line);
                MenuLines.Add(newLine);
            }
        }

        /// <summary>
        /// Returns the number of MenuLines added to this Menu
        /// </summary>
        /// <returns>The number of MenuLines contained by this Menu</returns>
        public int CountLines() {
            return MenuLines.Count;
        }

        /// <summary>
        /// Removes the MenuLine at the passed position, pos.
        /// Throws an ArgumentOutOfRangeException if pos represents an invalid position.
        /// </summary>
        /// <param name="pos">The position of the MenuLine to remove</param>
        public void RemoveLine(int pos) {
            try {
                MenuLines.RemoveAt(pos);
            }
            catch (ArgumentOutOfRangeException ex) {
                throw ex;
            }
        }

        /// <summary>
        /// Removes a sequence of MenuLines starting at startPos for count times.
        /// Throws ArgumentOutOfRangeException if startPos does not represent a valid position
        /// </summary>
        /// <param name="startPos">The starting position of the MenuLines to remove</param>
        /// <param name="count">The number of MenuLines to remove from this Menu counting from startPos</param>
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

        /// <summary>
        /// Adds a new MenuSelection to this Menu
        /// </summary>
        /// <param name="selection">The new MenuSelection to add to this Menu</param>
        public void AddMenuSelection(MenuSelection selection) {
            MenuSelections.Add(selection);
        }

        /// <summary>
        /// Constructs a new MenuSelection and adds it to this Menu
        /// </summary>
        /// <param name="option">Represents an expected user input for this MenuSelection</param>
        /// <param name="text">A string which should be displayed beside the option that describes what the option should accomplish</param>
        /// <param name="colour">The colour the new MenuSelection should be, defaults to white</param>
        public void AddMenuSelection(string option, string text, MenuColours colour = MenuColours.DEFAULT) {
            MenuSelection newItem = new MenuSelection(option, text, colour);
            MenuSelections.Add(newItem);
        }

        /// <summary>
        /// Appends a list of MenuSelections to this Menu
        /// </summary>
        /// <param name="selections">The List of MenuSelections to add to this Menu</param>
        public void AddMenuItems(List<MenuSelection> selections) {
            MenuSelections.AddRange(selections);
        }

        /// <summary>
        /// Returns the count of MenuSelections in this Menu
        /// </summary>
        /// <returns>The count of MenuSelections in this Menu</returns>
        public int CountMenuItems() {
            return MenuSelections.Count;
        }
    }

    /// <summary>
    /// Struct representing a MenuTitle.
    /// </summary>
    public readonly struct MenuTitle {
        public MenuColours Colour { get; }
        public string Text { get; }
        public MenuTitle(string text, MenuColours colour = MenuColours.DEFAULT) {
            Text = text;
            Colour = colour;
        }
    }

    /// <summary>
    /// Struct representing a MenuLine
    /// </summary>
    public readonly struct MenuLine {
        public MenuColours Colour { get; }
        public string Text { get; }
        public MenuLine(string line, MenuColours colour = MenuColours.DEFAULT) {
            Text = line;
            Colour = colour;
        }
    }

    /// <summary>
    /// Struct representing a MenuSelection
    /// </summary>
    public readonly struct MenuSelection {
        public string Option { get; }
        public string Text { get; }
        public MenuColours TextColour { get; }

        public MenuSelection(
            string option,
            string text,
            MenuColours colour = MenuColours.DEFAULT) {
            Option = option;
            Text = text;
            TextColour = colour;
        }
    }
}