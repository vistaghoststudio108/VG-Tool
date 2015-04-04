using EnvDTE;
using EnvDTE80;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Vistaghost.VISTAGHOST.Helper;
using Vistaghost.VISTAGHOST.Lib;
using System.Globalization;

namespace Vistaghost.VISTAGHOST.WindowForms
{
    public partial class SingleHeader : Form
    {
        public AddHeaderEventHandler OnSendData;

        private ObjectType func;

        private DTE2 dte2Object;

        public SingleHeader(DTE2 dte2Obj)
        {
            this.dte2Object = dte2Obj;

            InitializeComponent();

            lblParaNumber.Text = String.Empty;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtParamView.Rows.Count; i++)
            {
                func.Parameters.Add(new IOType
                {
                    Name = (string)dtParamView.Rows[i].Cells[0].Value,
                    Input = (bool)dtParamView.Rows[i].Cells[1].Value,
                    Output = (bool)dtParamView.Rows[i].Cells[2].Value
                });

            }

            if (OnSendData != null)
                OnSendData(func);

            this.Close();
        }

        private void InputHeader_Load(object sender, EventArgs e)
        {
            try
            {
                var paramList = new List<string>();
                // Retrieve the CodeFunction at the insertion point.
                TextSelection selected = (TextSelection)this.dte2Object.ActiveDocument.Selection;
                CodeFunction codeFunc = (CodeFunction)selected.ActivePoint.get_CodeElement(vsCMElement.vsCMElementFunction);

                if (codeFunc == null)
                {
                    this.Text = "No function matched";
                    lblParaNumber.Text = String.Empty;
                    func = null;
                    return;
                }

                func = new ObjectType();
                func.Name = codeFunc.Name;
                func.Prototype = codeFunc.get_Prototype((int)(vsCMPrototype.vsCMPrototypeParamNames | vsCMPrototype.vsCMPrototypeParamTypes | vsCMPrototype.vsCMPrototypeType));
                func.Line = codeFunc.StartPoint.Line;
                func.Description = String.Empty;

                /*Set form's title*/
                this.Text = codeFunc.FullName;

                // Display the function's parameters.
                foreach (CodeParameter param in codeFunc.Parameters)
                {
                    var codeType = param.Type;

                    var fullParam = codeType.AsString + " " + param.Name;
                    paramList.Add(fullParam);
                }

                foreach (var pr in paramList)
                {
                    if (!String.IsNullOrEmpty(pr))
                        dtParamView.Rows.Add(pr, true, false);
                }

                lblParaNumber.Text = paramList.Count.ToString(new CultureInfo("en-US")) + (paramList.Count > 1 ? " parameters" : " parameter");
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex.Message);
            }
        }

        private void SingleHeader_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
