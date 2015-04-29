using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vistaghost.VISTAGHOST.User_Control
{
    public partial class ExListView : ListView
    {

        private bool contextMenuAllowed = true;

        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
                if (value != null)
                    base.ContextMenuStrip.Opening += new CancelEventHandler(ContextMenuStrip_Opening);
            }
        }

        public int CurrentIndex { get; private set; }

        public ExListView()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            contextMenuAllowed = true;
            if (e.Button == MouseButtons.Right)
            {
                ListViewHitTestInfo lvhti = HitTest(e.X, e.Y);
                if (lvhti.Item == null)
                {
                    contextMenuAllowed = false;
                }
                else
                    CurrentIndex = lvhti.Item.Index;
            }
            base.OnMouseDown(e);
        }

        void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (!contextMenuAllowed)
                e.Cancel = true;
        }
    }
}
