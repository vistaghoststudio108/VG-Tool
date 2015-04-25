using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Vistaghost.VISTAGHOST.Helper;
using EnvDTE80;

namespace Vistaghost.VISTAGHOST.Lib
{
    public class VGOperations
    {

        #region user definition

        public const string LeftBracket = "(";

        public const string RightBracket = ")";

        public const string DefaultTag = "//<Not set> ";

        public const char Comma = ',';

        public const string NoIndent = "";

        public const string WhiteSpace4 = "    ";           // 4x space

        public const string WhiteSpace3 = "   ";          // 3x space

        public const string EndOfLine1 = "\n";

        public const string EndOfLine2 = "\n\n";

        public const string EndOfLineStar = "\n*\n";

        public const string StarSymbol = "*" + WhiteSpace3;

        public const string WhiteSpace6 = WhiteSpace3 + WhiteSpace3;

        public const string NoneJP = "なし";

        public const string NoneEn = "None";

        #endregion

        public static string[] langType = { 
                                             "//",      // C++ and C#
                                             "/*",      // C++ and C#
                                             "'",       // Visual Basic
                                             "<!--"     // Xml file
                                         };

        #region Common methods
        public static string GetDateString(DateFormat format)
        {
            string date = String.Empty;

            string month = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString().Length == 1 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();

            string year = DateTime.Now.Year.ToString();

            // default format
            date = year + month + day;

            switch (format)
            {
                case DateFormat.yyyymmdd:
                    date = year + month + day;
                    break;
                case DateFormat.yyyyddmm:
                    date = year + day + month;
                    break;
                case DateFormat.ddmmyyyy:
                    date = day + month + year;
                    break;
                case DateFormat.mmddyyyy:
                    date = month + day + year;
                    break;
                case DateFormat.DateFormat1:
                    date = day + "-" + month + "-" + year;
                    break;
                case DateFormat.DateFormat2:
                    date = year + "/" + month + "/" + day;
                    break;
                case DateFormat.FullDate:
                    {
                        date = DateTime.Now.ToString() + " " + DateTime.Now.DayOfWeek;
                    }
                    break;
                default:
                    break;
            }

            return date;
        }

        /// <summary>
        /// check valid lines
        /// </summary>
        /// <param name="line">line to check</param>
        /// <returns></returns>
        static bool ValidLine(string line)
        {
            var arr = line.ToCharArray();
            foreach (var ch in arr)
            {
                if (ch != '\0' && ch != '\r' && ch != '\n' && ch != '\t')
                    return true;
            }

            return false;
        }

        public static string GetFuncPrototype(DTE2 dte2, PrototypeType proType)
        {
            TextSelection selected = (TextSelection)dte2.ActiveDocument.Selection;
            CodeFunction codeFunc = (CodeFunction)selected.ActivePoint.get_CodeElement(vsCMElement.vsCMElementFunction);
            string _prototype = String.Empty;

            if (codeFunc != null)
            {
                switch (proType)
                {
                    case PrototypeType.FuncName:
                        _prototype = codeFunc.FullName;
                        break;
                    case PrototypeType.FullProt:
                        _prototype = codeFunc.get_Prototype((int)(vsCMPrototype.vsCMPrototypeParamNames | vsCMPrototype.vsCMPrototypeParamTypes | vsCMPrototype.vsCMPrototypeType | vsCMPrototype.vsCMPrototypeFullname));
                        break;
                    default:
                        break;
                }
            }

            return _prototype;
        }

        /// <summary>
        /// pre-processing selection text block
        /// </summary>
        /// <param name="dte">DTE object</param>
        /// <param name="addBookMark">Add postion to bookmark</param>
        /// <returns></returns>
        static TextSelection PreProcessSelectionText(DTE dte, bool addBookMark)
        {
            TextSelection selected = dte.ActiveDocument.Selection as TextSelection;
            var storePos = selected.TopPoint.VirtualCharOffset;
            var atLine = selected.TopPoint.Line;

            // selection is empty
            if (selected.IsEmpty)
            {
                selected.SelectLine();
                if (!ValidLine(selected.Text))
                {
                    selected.MoveToLineAndOffset(atLine, storePos, false);
                    return selected;
                }
                selected.CharLeft(true, 1);
                return selected;
            }

            // get number of line inside selection text
            int numLine = selected.TextRanges.Count;

            if (selected.Text.Length != 1 &&
                selected.Text.IndexOf("\r\n") == selected.Text.Length - 2)
            {
                numLine -= 1;
            }

            selected.MoveToPoint(selected.TopPoint, false);

            selected.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, false);
            selected.LineDown(true, numLine - 1);
            selected.EndOfLine(true);

            return selected;
        }

        static string GetTags(string tag, ActionType mode, bool bJustOne)
        {
            if (String.IsNullOrEmpty(tag))
            {
                return DefaultTag;
            }
            var t = tag.Split(new char[] { ']' });

            if (t.Count() == 1)
                return DefaultTag;

            int p1 = tag.IndexOf("[");
            int p2 = tag.IndexOf("]");

            if (p1 == -1 || p2 == -1)
                return DefaultTag;

            var tagstr = tag.Substring(p1 + 1, p2 - p1 - 1);
            var tlist = tagstr.Split(new char[] { Comma });

            if (tlist.Count() < 3)
                return DefaultTag;

            if(bJustOne)
                return ("//" + tlist[(int)mode - 1] + " ");

            return ("//" + tlist[(int)mode - 1] + t[1]);
        }

        /// <summary>
        /// Processing text for add single comments
        /// </summary>
        /// <param name="dte">DTE object, use this object to get selection text block</param>
        /// <param name="dateNow">date information</param>
        /// <param name="content">tag's content</param>
        /// <param name="account">user's account</param>
        /// <param name="devid">development ID</param>
        /// <param name="mode">mode of action(delete, add, modify)</param>
        /// <param name="keep_comments">keep old comments</param>
        /// <returns></returns>
        public static bool ProcessTextForAddSingle(DTE dte,
                                                    string dateNow,
                                                    string content,
                                                    string account,
                                                    string devid,
                                                    ActionType mode,
                                                    bool keep_comments,
                                                    ref ActionInfo info)
        {
            if (dte.ActiveDocument == null ||
                dte.ActiveDocument.Selection == null)
            {
                return false;
            }

            var strSelected = String.Empty;
            string opentag = "", closetag = "", blank = "";
            int pos = 0;
            int numLines = 0;
            int index = 1;
            bool result = false;
            TextRanges dummy = null;

            UndoContext undoObj = dte.UndoContext;

            if (!undoObj.IsOpen)
            {
                undoObj.Open("add_com", false);    /*add comments undo object*/
            }

            opentag = GetTags(VGSetting.SettingData.CommentInfo.OpenTagBegin, mode, false) + devid + " (" + dateNow + " " + account + ") " + content + VGSetting.SettingData.CommentInfo.OpenTagEnd;
            closetag = GetTags(VGSetting.SettingData.CommentInfo.CloseTagBegin, mode, false) + devid + " (" + dateNow + " " + account + ")" + VGSetting.SettingData.CommentInfo.CloseTagEnd;

            var selected = PreProcessSelectionText(dte, true);

            if (selected.IsEmpty)
            {
                switch (mode)
                {
                    case ActionType.Add:
                        info.SetInfo(dte.ActiveDocument.FullName, selected.TopPoint.Line);
                        selected.Insert(opentag, 1);
                        selected.NewLine(2);
                        selected.Insert(closetag, 1);
                        selected.LineUp(false, 1);

                        result = true;
                        break;

                    case ActionType.Modify:
                    case ActionType.Delete:
                    default:
                        result = false;
                        break;
                }
            }
            else
            {
                numLines = selected.TextRanges.Count;
                int selectionStartAbsoluteOffset = selected.TopPoint.AbsoluteCharOffset;
                int selectionEndAbsoluteOffset = selected.BottomPoint.AbsoluteCharOffset;

                if (selected.IsActiveEndGreater)
                {
                    selected.MoveToAbsoluteOffset(selectionEndAbsoluteOffset, false);
                    selected.MoveToAbsoluteOffset(selectionStartAbsoluteOffset, true);
                }

                selected.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstColumn, true);

                strSelected = selected.Text;

                if (mode != ActionType.Add)
                {
                    try
                    {
                        dte.ExecuteCommand("Edit.CommentSelection", "");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                        return false;
                    }

                    if (!selected.TopPoint.AtStartOfLine)
                        selected.MoveToAbsoluteOffset(selectionStartAbsoluteOffset, true);

                    pos = selected.Text.IndexOf(langType[0]);

                    while (pos == -1)
                    {
                        pos = selected.Text.IndexOf(langType[++index]);
                    }

                    blank = (pos != -1) ? selected.Text.Substring(0, pos) : WhiteSpace4;
                }

                info.SetInfo(dte.ActiveDocument.FullName, selected.TopPoint.Line);

                switch (mode)
                {
                    case ActionType.Modify:
                        result = selected.ReplacePattern(selected.Text, blank + opentag + "\n" + selected.Text + "\n" + strSelected + "\n" + blank + closetag, (int)vsFindOptions.vsFindOptionsNone, ref dummy);
                        if (result)
                        {
                            selected.EndOfLine(false);
                            selected.LineUp(false, 1);
                            selected.EndOfLine(false);
                            selected.LineUp(true, numLines - 1);
                            selected.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText, true);

                            if (keep_comments)
                            {
                                result = ClearCommentWithSelectedText(dte, VGDelCommentsType.DeleteBoth, VGDelCommentsOptions.DeleteAllBreakLine | VGDelCommentsOptions.SmartFormat);
                            }
                        }
                        break;

                    case ActionType.Add:
                        {
                            if (VGSetting.SettingData.CommentInfo.JustOneLine && selected.TextRanges.Count == 1)
                            {
                                opentag = GetTags(VGSetting.SettingData.CommentInfo.OpenTagBegin, mode, true) + devid + " (" + dateNow + " " + account + ") " + content + VGSetting.SettingData.CommentInfo.OpenTagEnd;
                                selected.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText, false);
                                selected.Insert(opentag, 1);
                                selected.NewLine(1);
                            }
                            else
                            {
                                //Add new comments tag here without content
                                selected.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText, false);
                                selected.Insert(opentag, 1);
                                selected.NewLine(1);
                                selected.LineDown(false, numLines - 1);
                                selected.EndOfLine(false);
                                selected.NewLine(1);
                                selected.Insert(closetag, 1);
                            }
                            result = true;
                        }
                        break;

                    case ActionType.Delete:
                        {
                            if (VGSetting.SettingData.CommentInfo.JustOneLine && selected.TextRanges.Count == 1)
                            {
                                opentag = GetTags(VGSetting.SettingData.CommentInfo.OpenTagBegin, mode, true) + devid + " (" + dateNow + " " + account + ") " + content + VGSetting.SettingData.CommentInfo.OpenTagEnd;
                                result = selected.ReplacePattern(selected.Text, blank + opentag + "\n" + selected.Text, (int)vsFindOptions.vsFindOptionsNone, ref dummy);
                            }
                            else
                                result = selected.ReplacePattern(selected.Text, blank + opentag + "\n" + selected.Text + "\n" + blank + closetag, (int)vsFindOptions.vsFindOptionsNone, ref dummy);
                        }
                        break;

                    default:
                        break;
                }
            }

            if (undoObj.IsOpen)
                undoObj.Close();

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public static int ProcessLinesForCount(string selected)
        {
            var pos = 0;
            string tmp = String.Empty;
            var count = 0;
            var isComment = false;

            while (pos != -1)
            {
                pos = selected.IndexOf('\n');

                if (pos != -1)
                {
                    tmp = selected.Substring(0, pos + 1).Trim().TrimStart('\t');
                    selected = selected.Substring(pos + 1, selected.Length - 1 - pos);
                }
                else
                    tmp = selected.Trim().TrimStart('\t');

                if (isComment)
                {
                    if (tmp.Contains("*/"))
                        isComment = false;
                    continue;
                }

                if (tmp.IndexOf("//") == 0 ||
                   (tmp.IndexOf("/*") == 0 && tmp.IndexOf("*/") == tmp.Length - 2) ||
                    String.IsNullOrEmpty(tmp.Trim().Trim('\t')))
                {
                    continue;
                }

                if (tmp.IndexOf("/*") == 0 && !tmp.Contains("*/"))
                {
                    isComment = true;
                    continue;
                }

                count++;
            }
            return count;
        }

        public static int Replace(_DTE dte, string find, string replacedby)
        {
            if (String.IsNullOrEmpty(find) || String.IsNullOrEmpty(replacedby))
                return -1;

            int count = 0;
            TextRanges dummy = null;
            var selected = dte.ActiveDocument.Selection as TextSelection;
            var ts = selected.TopPoint.CreateEditPoint();

            while (ts.LessThan(selected.BottomPoint))
            {
                if (ts.GetText(ts.LineLength).Contains(find))
                {
                    count++;
                }
                ts.LineDown(1);
            }
            if (count > 0)
            {
                selected.ReplacePattern(find, replacedby, (int)vsFindOptions.vsFindOptionsMatchCase | (int)vsFindOptions.vsFindOptionsMatchWholeWord, ref dummy);
                return count;
            }

            return -1;
        }

        static void AlignParamList(List<IOType> paramList)
        {
            int maxSpacePos = 0;

            foreach (var param in paramList)
            {
                int pos = param.Name.IndexOf(' ');
                if (pos > maxSpacePos)
                    maxSpacePos = pos;
            }

            foreach (var param in paramList)
            {
                int curPos = param.Name.IndexOf(' ');
                int offset = maxSpacePos - curPos;

                if (offset > 0)
                {
                    for (int i = 0; i < offset + 1; i++)
                    {
                        param.Name = param.Name.Insert(curPos, " ");
                    }
                }
                else if (offset == 0)
                {
                    param.Name = param.Name.Insert(curPos, " ");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="checklist"></param>
        /// <returns></returns>
        public static bool ProcessStringForAddHeader(DTE dte, ObjectType func, out int offsetLine)
        {
            if (dte.ActiveDocument == null ||
                dte.ActiveDocument.Selection == null ||
                func == null)
            {
                offsetLine = 0;
                return false;
            }

            try
            {
                var selected = dte.ActiveDocument.Selection as TextSelection;

                AlignParamList(func.Parameters);

                var headerText = MakeHeaderString(func.Name, func, func.Parameters);

                if (String.IsNullOrEmpty(headerText))
                {
                    offsetLine = 0;
                    return false;
                }

                UndoContext undoObj = dte.UndoContext;

                if (!undoObj.IsOpen)
                {
                    undoObj.Open("add_header", false);
                }

                selected.GotoLine(func.Line, false);
                selected.NewLine(1);
                selected.LineUp(false, 1);
                selected.Insert(headerText, 1);

                if (undoObj.IsOpen)
                {
                    undoObj.Close();
                }

                /*calculate offset line*/
                offsetLine = selected.ActivePoint.Line - func.Line + 1;
            }
            catch (Exception ex)
            {
                offsetLine = 0;
                Logger.LogError(ex, false);

                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string PreProcessString(string str, string _indent)
        {
            return str.Replace("\n", "\n   ");
        }

        static string ProcessDescription(string des, string tab)
        {
            if (String.IsNullOrEmpty(des))
                return String.Empty;

            string tempStr = des;
            int pos = 0;

            while (tempStr.IndexOf(Environment.NewLine, pos) != -1)
            {
                pos = tempStr.IndexOf(Environment.NewLine, pos);
                tempStr = tempStr.Insert(pos + 2, tab + WhiteSpace3);

                pos += 2;
            }

            return tempStr;
        }

        static string ProcessHistory(string tab)
        {
            string tempStr = String.Empty;

            string model = "-- " + VGSetting.SettingData.HeaderInfo.XAModel + " --\n" + tab + WhiteSpace3;
            tempStr = model + GetDateString(DateFormat.DateFormat2) + "     " + VGSetting.SettingData.CommentInfo.Account + "     [" + VGSetting.SettingData.HeaderInfo.History + "]    Create new";

            return tempStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modulName"></param>
        /// <param name="callSequense"></param>
        /// <param name="paramlist"></param>
        /// <param name="checklist"></param>
        /// <returns></returns>
        static string MakeHeaderString(string modulName, ObjectType func, List<IOType> paramlist)
        {
            if (VGSetting.SettingData.HeaderInfo.HeaderComponents == null ||
                VGSetting.SettingData.HeaderInfo.HeaderComponents.Count == 0 ||
                String.IsNullOrEmpty(VGSetting.SettingData.HeaderInfo.BeginHeader) ||
                String.IsNullOrEmpty(VGSetting.SettingData.HeaderInfo.EndHeader))
            {
                return String.Empty;
            }

            string header = String.Empty;
            bool none = false;
            var headerStyle = (HeaderStyle)VGSetting.SettingData.HeaderInfo.Style;
            var tab = String.Empty;
            var _EOF = EndOfLine1;

            switch (headerStyle)
            {
                case HeaderStyle.Aloka1:
                    {
                        tab = NoIndent;

                        if (VGSetting.SettingData.HeaderInfo.AddBreakLine)
                            _EOF = EndOfLine2;
                    }
                    break;
                case HeaderStyle.Aloka2:
                    {
                        tab = WhiteSpace3;

                        if (VGSetting.SettingData.HeaderInfo.AddBreakLine)
                            _EOF = EndOfLine2;
                    }
                    break;
                case HeaderStyle.Aloka3:
                    {
                        tab = StarSymbol;

                        if (VGSetting.SettingData.HeaderInfo.AddBreakLine)
                            _EOF = EndOfLineStar;
                    }
                    break;
                case HeaderStyle.Aloka4:
                    break;
                case HeaderStyle.Doxygen:
                    break;
                case HeaderStyle.Vistaghost:
                    break;
                default:
                    break;
            }

            header += VGSetting.SettingData.HeaderInfo.BeginHeader + "\n" + tab;

            if (VGSetting.SettingData.HeaderInfo.HeaderComponents[0].Checked)
                header += VGSetting.SettingData.HeaderInfo.HeaderComponents[0].Name + "\n" + tab + WhiteSpace3 + modulName + _EOF;             // module name

            if (VGSetting.SettingData.HeaderInfo.HeaderComponents[1].Checked)
                header += tab + VGSetting.SettingData.HeaderInfo.HeaderComponents[1].Name + "\n" + tab + WhiteSpace3 + func.Prototype + _EOF;        // calling sequence

            if (VGSetting.SettingData.HeaderInfo.HeaderComponents[2].Checked)
                header += tab + VGSetting.SettingData.HeaderInfo.HeaderComponents[2].Name + "\n" + tab + WhiteSpace3 + ProcessDescription(func.Description, tab) + _EOF;      // Function

            /*input/ouput parameters*/
            if (VGSetting.SettingData.HeaderInfo.HeaderComponents[3].Checked)
            {
                header += tab + VGSetting.SettingData.HeaderInfo.HeaderComponents[3].Name + "\n";                                         // Arguments

                header += tab + "   [Input]\n";

                for (int i = 0; i < paramlist.Count; i++)
                {
                    if (paramlist[i].Input)
                    {
                        none = true;
                        header += tab + "      ";
                        header += paramlist[i].Name + "\n";
                    }
                }

                if (!none)
                    header += tab + "      " + NoneJP + _EOF;

                // reset none
                none = false;

                header += tab + "   [Output]\n";

                for (int i = 0; i < paramlist.Count; i++)
                {
                    if (paramlist[i].Output)
                    {
                        none = true;
                        header += tab + "      ";
                        header += paramlist[i].Name + _EOF;
                    }
                }

                if (!none)
                    header += tab + "      " + NoneJP + _EOF;
            }
            if (VGSetting.SettingData.HeaderInfo.HeaderComponents[4].Checked)
                header += tab + VGSetting.SettingData.HeaderInfo.HeaderComponents[4].Name + "\n" + tab + WhiteSpace3 + EndOfLine1;

            if (VGSetting.SettingData.HeaderInfo.HeaderComponents[5].Checked)
                header += tab + VGSetting.SettingData.HeaderInfo.HeaderComponents[5].Name + "\n" + tab + WhiteSpace3 + EndOfLine1;

            if (VGSetting.SettingData.HeaderInfo.HeaderComponents[6].Checked)
                header += tab + VGSetting.SettingData.HeaderInfo.HeaderComponents[6].Name + "\n" + tab + ProcessHistory(tab) + "\n";

            header += VGSetting.SettingData.HeaderInfo.EndHeader;

            return header;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dte"></param>
        /// <param name="rangeLOC"></param>
        /// <param name="dType"></param>
        /// <param name="dOpt"></param>
        /// <returns></returns>
        public static bool ClearCommentWithoutSelection(DTE dte,
                                                        int LocRange,
                                                        VGDelCommentsType dType,
                                                        VGDelCommentsOptions dOpt)
        {
            if (dte.ActiveDocument == null || dte.ActiveDocument.Selection == null)
                return false;

            if (LocRange <= 0)
                return false;

            var selected = dte.ActiveDocument.Selection as TextSelection;
            if (!selected.IsEmpty)
            {
                selected.LineDown(false, 1);
            }

            selected.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText, false);
            selected.LineDown(true, LocRange);
            selected.EndOfLine(true);

            /*Now there was selection text and begin clear comments*/
            return ClearCommentWithSelectedText(dte, dType, dOpt);
        }

        /// <summary>
        /// Clear comments in selection code block
        /// </summary>
        /// <param name="dte">DTE object</param>
        /// <param name="dType">Delete type</param>
        /// <param name="dOpt">More options for delete</param>
        /// <returns></returns>
        public static bool ClearCommentWithSelectedText(DTE dte,
                                                        VGDelCommentsType dType,
                                                        VGDelCommentsOptions dOpt)
        {
            if (dte.ActiveDocument == null ||
                dte.ActiveDocument.Selection == null)
            {
                return false;
            }

            var selected = dte.ActiveDocument.Selection as TextSelection;

            /*there is no text selected*/
            if (selected.IsEmpty)
            {
                return false;
            }

            UndoContext undoObj = dte.UndoContext;
            string expCode = String.Empty;

            switch (dType)
            {
                case VGDelCommentsType.DeleteDoubleSlash:
                    expCode += VGSettingConstants.LineCode;
                    break;
                case VGDelCommentsType.DeleteSlashStar:
                    expCode += VGSettingConstants.BlockCode;
                    break;
                case VGDelCommentsType.DeleteBoth:
                    expCode += VGSettingConstants.LineCode;
                    expCode += "|";
                    expCode += VGSettingConstants.BlockCode;
                    break;
                case VGDelCommentsType.None:
                default:
                    break;
            }

            if (!undoObj.IsOpen)
            {
                undoObj.Open("del_com", false); /*delete comment undo object*/
            }

            if (!String.IsNullOrEmpty(expCode))
            {
                /*final code*/
                expCode = VGSettingConstants.MainCode + "|" + expCode;
                try
                {
                    string noComments = Regex.Replace(selected.Text, expCode, "$1");
                    TextRanges dummy = null;

                    /*Replace old selection with uncomment-selection*/
                    selected.ReplacePattern(selected.Text, noComments, 0, ref dummy);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, false);
                    return false;
                }
            }

            if (!selected.IsEmpty)
            {
                if (((int)dOpt & 0x001) == (int)VGDelCommentsOptions.SmartFormat)
                {
                    /*auto-format selection text*/
                    selected.SmartFormat();
                }

                if (((int)dOpt & 0x002) == (int)VGDelCommentsOptions.DeleteAllBreakLine)
                {
                    /*Delete all line break*/
                    selected.DeleteWhitespace(vsWhitespaceOptions.vsWhitespaceOptionsVertical);
                }
            }

            if (undoObj.IsOpen)
                undoObj.Close();

            return true;
        }


        public static string AdvanceFind(DTE dte, string searchText)
        {
            //if (dte.Documents.Count > 0)
            //{

            //}

            //string[] path = {
            //                    "C:\\Users\\thuanpv3\\Documents\\Visual Studio 2008\\Projects\\asdfasdf\\asdfasdf\\asdfasdf.cpp",
            //                    "C:\\Users\\thuanpv3\\Documents\\Visual Studio 2008\\Projects\\asdfasdf\\asdfasdf\\stdafx.cpp"
            //                };

            //for (int i = 0; i < path.Count(); i++)
            //{
            //    var t = dte.ItemOperations.OpenFile(path[i], Constants.vsViewKindCode).Document;
            //    TextRanges dummy = null;

            //    var selected = dte.ActiveDocument.Selection as TextSelection;
            //    while (selected.FindPattern("Review Screen Layout", (int)vsFindOptions.vsFindOptionsMatchCase, ref dummy))
            //    {
            //        var pos = (CodeFunction)selected.ActivePoint.get_CodeElement(vsCMElement.vsCMElementFunction);
            //        if (pos != null)
            //        {
            //            string name = pos.FullName;
            //        }
            //    }
            //}



            Find find = dte.Find;
            find.Action = vsFindAction.vsFindActionFind;
            find.FindWhat = searchText;
            find.MatchCase = true;
            find.Backwards = false;
            find.ResultsLocation = vsFindResultsLocation.vsFindResultsNone;
            find.Target = vsFindTarget.vsFindTargetSolution;
            find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxLiteral;
            find.SearchSubfolders = true;
            find.KeepModifiedDocumentsOpen = false;

            while (find.Execute() != vsFindResult.vsFindResultNotFound)
            {
                var t = find.DTE.ActiveDocument.Selection as TextSelection;
                var l = (CodeFunction)t.ActivePoint.get_CodeElement(vsCMElement.vsCMElementFunction);
                if (l != null)
                {
                    string name = l.FullName;
                }
            }


            var findWindow = dte.Windows.Item(EnvDTE.Constants.vsWindowKindFindResults1);
            string data = String.Empty;

            //if (result == vsFindResult.vsFindResultFound)
            //{
            //   // var t = GetFileFromResultWindow(dte);
            //    var selection = findWindow.Selection as TextSelection;
            //    var endPoint = selection.AnchorPoint.CreateEditPoint();
            //    endPoint.EndOfDocument();
            //    var text = endPoint.GetLines(1, endPoint.Line);
            //    selection.SelectAll();
            //    data = selection.Text;
            //}
            return data;
        }

        public static List<string> GetFileFromResultWindow(DTE dte)
        {
            List<string> files = new List<string>();
            string strIndex = String.Empty;

            if(dte.Find.ResultsLocation == vsFindResultsLocation.vsFindResults1)
                strIndex = EnvDTE.Constants.vsWindowKindFindResults1;
            else if(dte.Find.ResultsLocation == vsFindResultsLocation.vsFindResults2)
                strIndex = EnvDTE.Constants.vsWindowKindFindResults2;

            var findWindow = dte.Windows.Item(strIndex);

            var selected = findWindow.Selection as TextSelection;
            selected.SelectAll();

            var text = selected.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            return text.ToList();
        }
        #endregion
    }
}
