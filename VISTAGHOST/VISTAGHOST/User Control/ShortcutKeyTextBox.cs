using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using EnvDTE80;

namespace Vistaghost.VISTAGHOST.User_Control
{
    public partial class ShortcutKeyTextBox : TextBox
    {
        public DTE2 DTE2Object { get; set; }

        public List<object> ExistKeyBindings { get; set; }

        public ShortcutKeyTextBox()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(ShortcutKeyTextBox_KeyDown);
        }

        void ShortcutKeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.ShiftKey && (e.Modifiers == Keys.Alt | e.Modifiers == Keys.Control | e.Modifiers == Keys.Shift))
            {
                Text = e.Modifiers.ToString() + "+" + e.KeyCode.ToString();
            }

            e.SuppressKeyPress = true;
        }
    }
}
