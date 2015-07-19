using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GhostExcel.UserControls
{
    public partial class AttachmentItem : UserControl
    {
        public EventHandler OnClosed;

        public int Index { get; set; }

        public override string Text
        {
            get
            {
                return this.lblFilePath.Text;
            }
            set
            {
                string newText = GetVisibleString(value, this.Width - this.btnClose.Width - 10);
                this.lblFilePath.Text = newText;

                this.toolTip.SetToolTip(this, value);
                this.toolTip.SetToolTip(lblFilePath, value);
            }
        }

        public AttachmentItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// If text's length > this width value then make it visible
        /// </summary>
        /// <param name="inputText">old text</param>
        /// <param name="maxSize">max size to visible text</param>
        /// <returns>new text</returns>
        private string GetVisibleString(string inputText, int maxSize)
        {
            string tempStr = inputText;
            int len = tempStr.Length;
            int textWidth = TextRenderer.MeasureText(tempStr, this.Font).Width;
            bool IsTooLong = textWidth > maxSize;

            while (textWidth > maxSize)
            {
                tempStr = tempStr.Remove(len - 1);
                len--;

                textWidth = TextRenderer.MeasureText(tempStr, this.Font).Width;
            }

            if (IsTooLong)
                tempStr = string.Format("{0}...", tempStr);

            return tempStr;
        }

        private void AttachmentItem_Load(object sender, EventArgs e)
        {
            this.Height = this.lblFilePath.Height;
            this.btnClose.Height = this.Height;

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (OnClosed != null)
                OnClosed(this, EventArgs.Empty);

            this.Dispose();
        }

        private void AttachmentItem_SizeChanged(object sender, EventArgs e)
        {
            this.btnClose.Location = new Point(this.Width - this.btnClose.Width - 2, this.btnClose.Location.Y);
        }
    }
}
