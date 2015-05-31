using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using EnvDTE80;

namespace Vistaghost.VISTAGHOST.VGUserControl
{
    public partial class ShortcutKeyTextBox : TextBox
    {
        public DTE2 DTE2Object { get; set; }

        public List<object> ExistKeyBindings { get; set; }

        private List<string> key = new List<string>() { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D0", "NumPad1", 
                                                        "NumPad2", "NumPad3", "NumPad4", "NumPad5", "NumPad6", "NumPad7", "NumPad8", "NumPad9", "NumPad0",
                                                        "Return", "Add", "Decimal", "Subtract", "Divide", "Multiply", "Insert", "PageUp", "Delete", "Next",
                                                        "Up", "Left", "Right", "Down", "Pause", "Oemtilde", "Oemcomma", "OemPeriod", "OemQuestion", "Oem1",
                                                        "Oem7", "OemMinus", "Oemplus", "Oem5", "Oem6", "OemOpenBrackets", "Back" };

        private List<string> value = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "Num 1", 
                                                        "Num 2", "Num 3", "Num 4", "Num 5", "Num 6", "Num 7", "Num 8", "Num 9", "Num 0",
                                                        "Enter", "Num +", "Num .", "Num -", "Num /", "Num *", "Ins", "PgUp", "Del", "PgDn",
                                                        "Up Arrow", "Left Arrow", "Right Arrow", "Down Arrow", "Break", "`", ",", ".", "/", ";",
                                                        "'", "-", "=", "\\", "]", "[", "Bkspce" };

        public ShortcutKeyTextBox()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(ShortcutKeyTextBox_KeyDown);
        }

        void ShortcutKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode != Keys.ControlKey)
                {
                    if (e.KeyCode.ToString() == "Capital")
                    { }
                    else
                    {
                        int index = key.IndexOf(e.KeyCode.ToString());
                        Text = "Ctrl+" + (index != -1 ? value[index] : e.KeyCode.ToString());
                    }
                }
            }
            else if (e.Modifiers == Keys.Alt)
            {
                if (e.KeyCode != Keys.Menu)
                {
                    if (e.KeyCode.ToString() == "NumLock" || e.KeyCode.ToString() == "Capital")
                    {

                    }
                    else
                    {
                        int index = key.IndexOf(e.KeyCode.ToString());
                        Text = "Alt+" + (index != -1 ? value[index] : e.KeyCode.ToString());
                    }
                }
            }
            else if (e.Control && e.Alt)
            {
                if (e.KeyCode != Keys.Menu && e.KeyCode != Keys.ControlKey)
                {
                    if (e.KeyCode.ToString() == "NumLock" || e.KeyCode.ToString() == "Capital")
                    {

                    }
                    else
                    {
                        int index = key.IndexOf(e.KeyCode.ToString());
                        Text = "Ctrl+Alt+" + (index != -1 ? value[index] : e.KeyCode.ToString());
                    }
                }
            }
            else if (e.Control && e.Shift)
            {
                if (e.KeyCode != Keys.ShiftKey && e.KeyCode != Keys.ControlKey)
                {
                    if (e.KeyCode.ToString() == "NumLock" || e.KeyCode.ToString() == "Capital")
                    {

                    }
                    else
                    {
                        int index = key.IndexOf(e.KeyCode.ToString());
                        Text = "Ctrl+Shift+" + (index != -1 ? value[index] : e.KeyCode.ToString());
                    }
                }
            }

            e.SuppressKeyPress = true;
        }
    }
}
