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
    public partial class ViewFunction : Form
    {
        private DTE dteObject;

        private DTE2 dte2Object;

        private List<ObjectType> objList = new List<ObjectType>();

        private int offsetLine = 0;
        private int activeLine = 0;

        private bool Added = false;

        private SearchType searchType;

        private List<LVFuncInfo> non_AddFunc = new List<LVFuncInfo>();

        public ViewFunction(DTE dte, DTE2 dte2)
        {
            this.dte2Object = dte2;
            this.dteObject = dte;

            /*Search all functions by default*/
            searchType = SearchType.AllFunction;

            InitializeComponent();
        }

        string GetString(SearchType sType, int foundNum)
        {
            string returnString = String.Empty;

            switch (sType)
            {
                case SearchType.NoneHeaderFunction:
                    {
                        returnString = "Found " + foundNum + (foundNum > 1 ? " functions" : " function") + " without header";
                    }
                    break;
                case SearchType.AllFunction:
                    {
                        returnString = "Found " + foundNum + (foundNum > 1 ? " functions" : " function");
                    }
                    break;
                case SearchType.Class:
                    {
                        returnString = "Found " + foundNum + " class";
                    }
                    break;
                case SearchType.Enumerable:
                    {
                        returnString = "Found " + foundNum + " defined enum";
                    }
                    break;
                case SearchType.Structure:
                    {
                        returnString = "Found " + foundNum + " defined structure";
                    }
                    break;
                case SearchType.Union:
                    {
                        returnString = "Found " + foundNum + " defined union";
                    }
                    break;
                case SearchType.TypeDef:
                    {
                        returnString = "Found " + foundNum + " defined typedef";
                    }
                    break;
                case SearchType.None:
                default:
                    returnString = "Not found";
                    break;
            }

            return returnString;
        }

        /// <summary>
        /// search all functions in document
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFindAll_Click(object sender, EventArgs e)
        {
            if (this.dte2Object.ActiveDocument == null ||
                this.dte2Object.ActiveDocument.Selection == null)
            {
                return;
            }

            var pri = dte2Object.ActiveDocument.ProjectItem;
            if (pri == null)
            {
                lblStatus.Text = "Unknown scope. Move to the project that contains this file and try again.";
                return;
            }

            /*reset to zero*/
            offsetLine = 0;
            activeLine = 0;
            Added = false;
            objList.Clear();

            var fcm = dte2Object.ActiveDocument.ProjectItem.FileCodeModel;

            /*open a file that not included in project*/
            if (fcm == null)
            {
                lblStatus.Text = "Unknown scope. Move to the project that contains this file and try again.";
                return;
            }

            dtFunctions.Rows.Clear();
            dtParamView.Rows.Clear();
            lblStatus.Text = "Searching...";

            this.btnSearch.Enabled = false;

            foreach (CodeElement ce in fcm.CodeElements)
            {
                switch (searchType)
                {
                    case SearchType.AllFunction:
                    case SearchType.NoneHeaderFunction:
                        {
                            /*Get CodeFunction ojbect*/
                            if (ce.Kind == vsCMElement.vsCMElementFunction)
                            {
                                var codeFunc = (CodeFunction)ce;
                                if (!String.IsNullOrEmpty(codeFunc.Comment))
                                {
                                    if (searchType == SearchType.AllFunction)
                                        Added = true;
                                    else
                                        continue;
                                }

                                var func = new ObjectType();

                                /*Get CodeParameters object*/
                                foreach (CodeParameter codeParam in codeFunc.Parameters)
                                {
                                    var codeType = codeParam.Type;
                                    var param = new IOType();

                                    var fullParam = codeType.AsString + " " + codeParam.Name;
                                    param.Name = fullParam;

                                    func.Parameters.Add(param);
                                }

                                func.Name = codeFunc.Name;
                                func.Comment = codeFunc.Comment;

                                func.Prototype = codeFunc.get_Prototype((int)(vsCMPrototype.vsCMPrototypeParamNames | vsCMPrototype.vsCMPrototypeParamTypes | vsCMPrototype.vsCMPrototypeType));
                                func.Line = codeFunc.StartPoint.Line;

                                objList.Add(func);

                                dtFunctions.Rows.Add(Added, ce.FullName);

                                Added = false;
                            }
                        }
                        break;
                    case SearchType.Class:
                        {
                            /*Get CodeClass objects*/
                            if (ce.Kind == vsCMElement.vsCMElementClass)
                            {
                                CodeClass codeClass = (CodeClass)ce;

                                var clas = new ObjectType();
                                clas.Name = codeClass.FullName;
                                clas.Comment = codeClass.Comment;
                                clas.Line = codeClass.StartPoint.Line;
                                clas.Count = codeClass.Bases.Count;

                                foreach (CodeElement baseClass in codeClass.Bases)
                                {
                                    var bc = new IOType();
                                    bc.Name = baseClass.FullName;

                                    clas.Parameters.Add(bc);
                                }

                                objList.Add(clas);

                                dtFunctions.Rows.Add(false, ce.FullName);
                            }
                        }
                        break;
                    case SearchType.Enumerable:
                        {
                            /*Get CodeEnum objects*/
                            if (ce.Kind == vsCMElement.vsCMElementEnum)
                            {
                                CodeEnum codeEnum = (CodeEnum)ce;

                                var enu = new ObjectType();

                                enu.Name = codeEnum.FullName;
                                enu.Comment = codeEnum.Comment;
                                enu.Line = codeEnum.StartPoint.Line;
                                enu.Count = codeEnum.Members.Count;

                                foreach (CodeElement member in codeEnum.Members)
                                {
                                    var memb = new IOType();
                                    memb.Name = member.Name;

                                    enu.Parameters.Add(memb);
                                }

                                objList.Add(enu);

                                dtFunctions.Rows.Add(false, ce.FullName);
                            }
                        }
                        break;
                    /*for the future update*/
                    case SearchType.Structure:
                        {
                            if (ce.Kind == vsCMElement.vsCMElementStruct)
                            {
                                CodeStruct codeStru = (CodeStruct)ce;

                                var stru = new ObjectType();
                                stru.Name = codeStru.FullName;
                                stru.Comment = codeStru.Comment;
                                stru.Line = codeStru.StartPoint.Line;
                                stru.Count = codeStru.Members.Count;

                                foreach (CodeElement member in codeStru.Members)
                                {
                                    var mem = new IOType();
                                    switch (member.Kind)
                                    {
                                        case vsCMElement.vsCMElementVariable:
                                            mem.Name = ((CodeVariable)member).get_Prototype((int)vsCMPrototype.vsCMPrototypeType);
                                            break;
                                        case vsCMElement.vsCMElementFunction:
                                            mem.Name = ((CodeFunction)member).get_Prototype((int)vsCMPrototype.vsCMPrototypeParamNames | (int)vsCMPrototype.vsCMPrototypeParamTypes | (int)vsCMPrototype.vsCMPrototypeType);
                                            break;
                                        default:
                                            break;
                                    }

                                    if (!String.IsNullOrEmpty(mem.Name))
                                        stru.Parameters.Add(mem);
                                }

                                objList.Add(stru);

                                dtFunctions.Rows.Add(false, ce.FullName);
                            }
                        }
                        break;
                    case SearchType.Union:
                        break;
                    case SearchType.TypeDef:
                        break;
                    default:
                        break;
                }
            }

            this.btnSearch.Enabled = true;

            if (dtFunctions.Rows.Count > 0)
            {
                dtFunctions.Focus();
                btnSearch.Enabled = false;
                if (dtFunctions.Rows.Count == 1)
                {
                    switch (searchType)
                    {
                        case SearchType.AllFunction:
                        case SearchType.NoneHeaderFunction:
                            {
                                /*detail of selected function*/
                                lblStatus.Text = objList[0].Name + ", at line " + objList[0].Line + (((objList[0].Parameters.Count > 0) ? (", contains " + objList[0].Parameters.Count) : (", no")) + " parameters");
                            }
                            break;
                        case SearchType.Class:
                            {
                                /*detail of selected class*/
                                lblStatus.Text = objList[0].Name + ", at line " + objList[0].Line + (((objList[0].Count > 0) ? (", devired from " + objList[0].Count) : (", no base ")) + " class");
                            }
                            break;
                        case SearchType.Enumerable:
                        case SearchType.Structure:
                            {
                                /*detail of selected enum*/
                                lblStatus.Text = objList[0].Name + ", at line " + objList[0].Line + (((objList[0].Count > 0) ? (", contains " + objList[0].Count) : (", no")) + " members");
                            }
                            break;
                        case SearchType.Union:
                            break;
                        case SearchType.TypeDef:
                            break;
                        default:
                            break;
                    }
                }
                else
                    lblStatus.Text = GetString(searchType, objList.Count);
            }
            else
            {
                lblStatus.Text = "Not found";
            }
        }

        void UpdateListView(SearchType type)
        {
            switch (type)
            {
                case SearchType.AllFunction:
                case SearchType.NoneHeaderFunction:
                    {
                        dtParamView.Columns[0].HeaderText = "Arguments";
                        dtParamView.Columns[1].HeaderText = "Input";
                        dtParamView.Columns[2].HeaderText = "Output";
                        dtParamView.Columns[1].Visible = true;
                        dtParamView.Columns[2].Visible = true;
                        dtParamView.Columns[3].Visible = false;
                    }
                    break;
                case SearchType.Class:
                    {
                        dtParamView.Columns[0].HeaderText = "Base class";
                        dtParamView.Columns[1].Visible = false;
                        dtParamView.Columns[2].Visible = false;
                        dtParamView.Columns[3].Visible = false;
                    }
                    break;
                case SearchType.Enumerable:
                    {
                        dtParamView.Columns[0].HeaderText = "Members";
                        dtParamView.Columns[1].Visible = false;
                        dtParamView.Columns[2].Visible = false;
                        dtParamView.Columns[3].Visible = true;
                    }
                    break;
                case SearchType.Structure:
                    {
                        dtParamView.Columns[0].HeaderText = "Members";
                        dtParamView.Columns[1].Visible = false;
                        dtParamView.Columns[2].Visible = false;
                        dtParamView.Columns[3].Visible = false;
                    }
                    break;
                case SearchType.Union:
                    break;
                case SearchType.TypeDef:
                    break;
                default:
                    break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        /// <summary>
        /// update linenumber of functions after changed
        /// </summary>
        /// <param name="offsetLine">the offset line</param>
        void UpdateLineNumber(int offset)
        {
            foreach (var func in objList)
            {
                if (func.Line > activeLine)
                {
                    func.Line += offset;
                }
            }
        }

        bool CheckFunction()
        {
            var count = dtFunctions.SelectedRows.Count;

            if (count == 0)
                return false;

            foreach (DataGridViewRow row in dtFunctions.SelectedRows)
            {
                if ((bool)row.Cells[0].Value == false)
                {
                    non_AddFunc.Add(new LVFuncInfo { Index = row.Index, FuncString = (string)row.Cells[1].Value });
                }
            }

            return non_AddFunc.Count > 0;
        }

        private void hif_Notify(int index)
        {
            var pointer = this.dteObject.ActiveDocument.Selection as TextSelection;
            pointer.GotoLine(objList[index].Line, false);
        }

        private void hif_OnSendHeaderInfo(List<LVFuncInfo> funcinfo)
        {
            var pointer = this.dteObject.ActiveDocument.Selection as TextSelection;
            Added = true;

            foreach (var f in funcinfo)
            {
                activeLine = objList[f.Index].Line;
                objList[f.Index].Description = f.FuncString;

                if (VGOperations.ProcessStringForAddHeader(this.dteObject, objList[f.Index], out offsetLine))
                {
                    UpdateLineNumber(offsetLine);

                    /*update line*/
                    objList[f.Index].Line += offsetLine;
                    dtFunctions.Rows[f.Index].Cells[0].Value = true;
                }
                else
                {
                    lblStatus.Text = "Add header failed";
                }
            }

            Added = false;
        }

        private void ViewFunction_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Ready";
            this.Text = this.dteObject.ActiveDocument.FullName;

            cbSearchType.SelectedIndex = 0;
            cbSearchType.SelectedIndexChanged += new EventHandler(cbSearchType_SelectedIndexChanged);
        }

        private void ViewFunction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Combobox event, check and assigned search type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var curType = searchType;

            searchType = (SearchType)cbSearchType.SelectedIndex;

            /*No change*/
            if (curType == searchType)
                return;

            dtParamView.Rows.Clear();
            dtFunctions.Rows.Clear();
            lblStatus.Text = "Ready";

            /*re-update list view*/
            UpdateListView(searchType);

            if (!btnSearch.Enabled)
            {
                btnSearch.Enabled = true;
                btnSearch.Focus();
            }
        }

        /// <summary>
        /// Process key down event for check Enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtFunctions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter &&
                (searchType == SearchType.AllFunction ||
                searchType == SearchType.NoneHeaderFunction) &&
                dtFunctions.Rows.Count > 0)
            {
                non_AddFunc.Clear();
                if (CheckFunction())
                {
                    HeaderForm hif = new HeaderForm();
                    hif.Non_AddFunc = non_AddFunc;
                    hif.OnNotify += new HeaderNotifyEventHandler(hif_Notify);
                    hif.OnSendHeaderInfo += new HeaderInputEventHandler(hif_OnSendHeaderInfo);
                    hif.ShowDialog();
                }
                else
                    lblStatus.Text = "Selected functions already have header. Try again";

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.C) // repesent for Ctrl+C action
            {
                /*Copy selected row information*/
                string copiedText = String.Empty;
                foreach (DataGridViewRow row in dtFunctions.SelectedRows)
                {
                    switch (searchType)
                    {
                        case SearchType.AllFunction:
                        case SearchType.NoneHeaderFunction:
                            {
                                copiedText += objList[row.Index].Prototype + "\n";
                            }
                            break;
                        case SearchType.Class:
                        case SearchType.Enumerable:
                        case SearchType.Structure:
                        case SearchType.Union:
                        case SearchType.TypeDef:
                            {
                                copiedText += objList[row.Index].Name + "\n";
                            }
                            break;
                        default:
                            break;
                    }
                }

                /*Set text to the clipboard*/
                if (!String.IsNullOrEmpty(copiedText))
                {
                    copiedText = copiedText.Remove(copiedText.Length - 1, 1);
                    Clipboard.SetText(copiedText, TextDataFormat.UnicodeText);
                }

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        /// <summary>
        /// This function occure when the selection row changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtFunctions_SelectionChanged(object sender, EventArgs e)
        {
            dtParamView.Rows.Clear();

            var count = dtFunctions.SelectedRows.Count;
            if (count > 1)
            {
                lblStatus.Text = count.ToString(new CultureInfo("en-US"));
                switch (searchType)
                {
                    case SearchType.AllFunction:
                    case SearchType.NoneHeaderFunction:
                        {
                            lblStatus.Text += " functions are selected";
                        }
                        break;
                    case SearchType.Class:
                        {
                            lblStatus.Text += " class are selected";
                        }
                        break;
                    case SearchType.Enumerable:
                        {
                            lblStatus.Text += " enums are selected";
                        }
                        break;
                    case SearchType.Structure:
                        {
                            lblStatus.Text += " structures are selected";
                        }
                        break;
                    case SearchType.Union:
                        {
                            lblStatus.Text += " union are selected";
                        }
                        break;
                    case SearchType.TypeDef:
                        {
                            lblStatus.Text += " typedef structure are selected";
                        }
                        break;
                    default:
                        break;
                }
            }
            else if (count == 0)
            {
                lblStatus.Text = "No items are selected";
            }
            else
            {
                int index = dtFunctions.SelectedRows[0].Index;

                var pointer = this.dteObject.ActiveDocument.Selection as TextSelection;
                pointer.GotoLine(objList[index].Line, false);
                pointer.SelectLine();

                switch (searchType)
                {
                    case SearchType.AllFunction:
                    case SearchType.NoneHeaderFunction:
                        {
                            /*detail of selected function*/
                            lblStatus.Text = objList[index].Name + ", at line " + objList[index].Line + (((objList[index].Parameters.Count > 0) ? (", contains " + objList[index].Parameters.Count) : (", no")) + " parameters");

                            foreach (var pr in objList[index].Parameters)
                            {
                                dtParamView.Rows.Add(pr.Name, pr.Input, pr.Output);
                            }
                        }
                        break;
                    case SearchType.Class:
                        {
                            /*detail of selected class*/
                            lblStatus.Text = objList[index].Name + ", at line " + objList[index].Line + (((objList[index].Count > 0) ? (", devired from " + objList[index].Count) : (", no base ")) + " class");

                            foreach (var pr in objList[index].Parameters)
                            {
                                dtParamView.Rows.Add(pr.Name, false, false);
                            }
                        }
                        break;
                    case SearchType.Enumerable:
                    case SearchType.Structure:
                        {
                            /*detail of selected enum*/
                            lblStatus.Text = objList[index].Name + ", at line " + objList[index].Line + (((objList[index].Count > 0) ? (", contains " + objList[index].Count) : (", no")) + " members");

                            foreach (var pr in objList[index].Parameters)
                            {
                                dtParamView.Rows.Add(pr.Name, false, false);
                            }
                        }
                        break;
                    case SearchType.Union:
                        break;
                    case SearchType.TypeDef:
                        break;
                    default:
                        break;
                }
            }
        }

        private void dtParamView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (searchType)
            {
                case SearchType.Enumerable:
                    {
                        int beginValue = 0;
                        int index = e.RowIndex;
                        if (int.TryParse((string)dtParamView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, out beginValue))
                        {
                            for (int i = index + 1; i < dtParamView.Rows.Count; i++)
                            {
                                beginValue++;
                                dtParamView.Rows[i].Cells[e.ColumnIndex].Value = beginValue;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void dtParamView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (searchType)
            {
                case SearchType.AllFunction:
                case SearchType.NoneHeaderFunction:
                    {
                        int selectedFunc = dtFunctions.SelectedRows[0].Index;
                        var val = (bool)dtParamView.SelectedRows[0].Cells[e.ColumnIndex].Value;
                    }
                    break;
                default:
                    break;
            }

            //objList[selectedFunc].Parameters[e.RowIndex].Input = (bool)dtParamView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //objList[selectedFunc].Parameters[e.RowIndex].Output = (bool)dtParamView.Rows[e.RowIndex].Cells[2].Value;
        }
    }
}
