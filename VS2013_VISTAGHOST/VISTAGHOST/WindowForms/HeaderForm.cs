using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.Lib;

namespace Vistaghost.VISTAGHOST
{
    public partial class HeaderForm : Form
    {
        public HeaderInputEventHandler OnSendHeaderInfo;
        public HeaderNotifyEventHandler OnNotify;

        public List<LVFuncInfo> Non_AddFunc { get; set; }

        private List<LVFuncInfo> funcinfo = new List<LVFuncInfo>();

        public HeaderForm()
        {
            InitializeComponent();
        }

        private void HeaderForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(496, 155);
            this.Text = Non_AddFunc[0].FuncString;

            if (OnNotify != null)
                OnNotify(Non_AddFunc[0].Index);
        }

        private void HeaderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (!e.Shift && e.KeyCode == Keys.Enter)
            {
                funcinfo.Add(new LVFuncInfo { Index = Non_AddFunc[0].Index, FuncString = txtFunction.Text });

                /*remove used info*/
                Non_AddFunc.RemoveAt(0);

                if (Non_AddFunc.Count == 0)
                {
                    if (OnSendHeaderInfo != null)
                    {
                        OnSendHeaderInfo(funcinfo);
                    }
                    this.Close();
                }
                else
                {
                    if (OnNotify != null)
                    {
                        OnNotify(Non_AddFunc[0].Index);
                    }

                    this.Text = Non_AddFunc[0].FuncString;
                    txtFunction.Text = String.Empty;
                    txtFunction.SelectionStart = 0;
                }
            }
        }

        private void txtFunction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (e.Shift)
                {
                    int pos = txtFunction.SelectionStart;
                    txtFunction.Text = txtFunction.Text.Insert(pos, Environment.NewLine);
                    txtFunction.SelectionStart = pos + 2;
                    txtFunction.ScrollToCaret();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (var f in Non_AddFunc)
            {
                funcinfo.Add(new LVFuncInfo { Index = f.Index, FuncString = String.Empty });
            }

            if (OnSendHeaderInfo != null)
            {
                OnSendHeaderInfo(funcinfo);
            }

            this.Close();
        }
    }
}
