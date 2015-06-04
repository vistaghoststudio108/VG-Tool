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
using Vistaghost.VISTAGHOST.ToolWindows;

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

        public static bool GetFuncPrototype(DTE2 dte2, PrototypeType proType, out string _prototype)
        {
            TextSelection selected = (TextSelection)dte2.ActiveDocument.Selection;
            CodeFunction codeFunc;
            _prototype = String.Empty;

            try
            {
                codeFunc = (CodeFunction)selected.ActivePoint.get_CodeElement(vsCMElement.vsCMElementFunction);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, false);
                return false;
            }

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

                return true;
            }
            else
                return false;
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

            if (bJustOne)
                return ("//" + tlist[(int)mode - 1] + " ");

            return ("//" + tlist[(int)mode - 1] + t[1]);
        }

        static string get_FinalString(string str1, string str2)
        {
            string lineBreakStr = "\n";
            for (int i = 0; i < VGSetting.SettingData.CommentInfo.EmptyLineNum; i++)
            {
                lineBreakStr += "\n";
            }

            return lineBreakStr + str1 + (String.IsNullOrEmpty(str2) ? lineBreakStr : "\n" + str2 + lineBreakStr);
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
                        selected.NewLine(VGSetting.SettingData.CommentInfo.EmptyLineNum * 2 + 2);
                        selected.Insert(closetag, 1);
                        selected.LineUp(false, VGSetting.SettingData.CommentInfo.EmptyLineNum + 1);

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
                        result = selected.ReplacePattern(selected.Text, blank + opentag + get_FinalString(selected.Text, strSelected) + blank + closetag, (int)vsFindOptions.vsFindOptionsNone, ref dummy);
                        if (result)
                        {
                            selected.EndOfLine(false);
                            selected.LineUp(false, VGSetting.SettingData.CommentInfo.EmptyLineNum + 1);
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
                                selected.NewLine(VGSetting.SettingData.CommentInfo.EmptyLineNum + 1);
                                selected.LineDown(false, numLines - 1);
                                selected.EndOfLine(false);
                                selected.NewLine(VGSetting.SettingData.CommentInfo.EmptyLineNum + 1);
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
                                result = selected.ReplacePattern(selected.Text, blank + opentag + get_FinalString(selected.Text, String.Empty) + blank + closetag, (int)vsFindOptions.vsFindOptionsNone, ref dummy);
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

        static string GetFileName(string input)
        {
            var parts = input.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            var pos = parts[1].LastIndexOf("(");
            return parts[0] + ":" + parts[1].Remove(pos);
        }

        static int GetLine(string input)
        {
            var parts = input.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            var begin = parts[1].LastIndexOf("(");
            var end = parts[1].LastIndexOf(")");

            int numLine;
            if (int.TryParse(parts[1].Substring(begin + 1, end - begin - 1), out numLine))
                return numLine;

            return -1;
        }

        static void GetFindWhatString(string text)
        {
            int pos1 = text.IndexOf("\"");
            int pos2 = -1;
            if (pos1 != -1)
            {
                pos2 = text.IndexOf("\"", pos1 + 1);
            }

            if (pos2 != -1)
            {
                VGSetting.Instance.FindWhat = text.Substring(pos1 + 1, pos2 - pos1 - 1);
            }
        }

        public static List<FileContainer> GetFileFromResultWindow(DTE dte, string wndGuid, FileFilter filter)
        {
            List<FileContainer> fContainer = new List<FileContainer>();
            string curFileName = String.Empty;
            string newFileName = String.Empty;
            int nLine = 0;

            var findWindow = dte.Windows.Item(wndGuid);
            if (findWindow == null)
            {
                return fContainer;
            }

            TextSelection selected = findWindow.Selection as TextSelection;

            selected.SelectAll();

            var files = selected.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (files.Count == 0 || files.Count == 1 || files.Count == 2)
                return fContainer;

            if (files[0].Contains("List filenames only"))
            {
                VGSetting.Instance.FileNameOnly = true;
            }

            GetFindWhatString(files[0]);

            files.RemoveAt(0);
            files.RemoveAt(files.Count - 1);

            foreach (var f in files)
            {
                switch (filter)
                {
                    case FileFilter.ffSource:
                        {
                            if (!f.Contains(".cpp"))
                                continue;
                        }
                        break;
                    case FileFilter.ffHeader:
                        {
                            if (!f.Contains(".h"))
                                continue;
                        }
                        break;
                    case FileFilter.ffAll:
                        break;
                }

                if (VGSetting.Instance.FileNameOnly)
                {
                    var fc = new FileContainer();
                    fc.FileName = f;
                    fContainer.Add(fc);
                }
                else
                {
                    newFileName = GetFileName(f);
                    nLine = GetLine(f);

                    if (nLine == -1)
                        continue;

                    if (newFileName == curFileName)
                    {
                        fContainer[fContainer.Count - 1].Lines.Add(nLine);
                    }
                    else
                    {
                        curFileName = newFileName;
                        var fc = new FileContainer();
                        fc.FileName = newFileName;
                        fc.Lines.Add(nLine);

                        fContainer.Add(fc);
                    }
                }
            }

            selected.Cancel();

            return fContainer;
        }

        static bool InSideFunc(object codeElement, List<int> lines, SearchType sType)
        {
            bool bValied = false;

            foreach (var line in lines)
            {
                switch (sType)
                {
                    case SearchType.Function:
                        {
                            if (line <= ((CodeFunction)codeElement).EndPoint.Line && line >= ((CodeFunction)codeElement).StartPoint.Line)
                                bValied = true;
                        }
                        break;

                    case SearchType.Class:
                        {
                            if (line <= ((CodeClass)codeElement).EndPoint.Line && line >= ((CodeClass)codeElement).StartPoint.Line)
                                bValied = true;
                        }
                        break;

                    case SearchType.Enumerable:
                        {
                            if (line <= ((CodeEnum)codeElement).EndPoint.Line && line >= ((CodeEnum)codeElement).StartPoint.Line)
                                bValied = true;
                        }
                        break;

                    case SearchType.Structure:
                        {
                            if (line <= ((CodeStruct)codeElement).EndPoint.Line && line >= ((CodeStruct)codeElement).StartPoint.Line)
                                bValied = true;
                        }
                        break;
                    default:
                        break;
                }
            }

            return bValied;
        }

        public static List<ObjectType> GetFunctionProtFromHistory(DTE dte, List<FileContainer> fileList,
                                                                  SearchType sType,
                                                                  ref UCVistaghostWindow owPane,
                                                                  out int totalFileSearched,
                                                                  ref bool Canceled)
        {
            List<ObjectType> elementList = new List<ObjectType>();
            Document doc;
            bool bOpen = false;
            string fileName = String.Empty;
            List<int> lines;
            FileCodeModel fcm;
            totalFileSearched = 0;

            try
            {
                for (int i = 0; i < fileList.Count; i++)
                {
                    fileName = Path.GetFullPath(fileList[i].FileName);
                    lines = fileList[i].Lines;
                    string curElement = String.Empty;
                    totalFileSearched++;

                    dte.StatusBar.Text = "Searching " + fileList[i].FileName;

                    if (dte.ItemOperations.IsFileOpen(fileName, Constants.vsViewKindCode))
                    {
                        doc = dte.Documents.Item(fileName);
                        //doc.Activate();
                    }
                    else
                    {
                        doc = dte.ItemOperations.OpenFile(fileName, Constants.vsViewKindCode).Document;
                        bOpen = true;
                    }

                    if (doc == null || doc.ProjectItem == null || doc.ProjectItem.FileCodeModel == null)
                        continue;

                    var selected = doc.Selection as TextSelection;
                    selected.StartOfDocument(false);

                    if (VGSetting.Instance.FileNameOnly)
                    {
                        while (selected.FindText(VGSetting.Instance.FindWhat, (int)(vsFindOptions.vsFindOptionsMatchCase | vsFindOptions.vsFindOptionsMatchWholeWord)))
                        {
                            try
                            {
                                switch (sType)
                                {
                                    case SearchType.Function:
                                        {
                                            var codeFunc = (CodeFunction)selected.ActivePoint.get_CodeElement(vsCMElement.vsCMElementFunction);
                                            if (codeFunc != null)
                                            {
                                                var func = new ObjectType();
                                                func.Name = codeFunc.FullName;
                                                func.Prototype = codeFunc.get_Prototype((int)((vsCMPrototype.vsCMPrototypeParamNames | vsCMPrototype.vsCMPrototypeParamTypes | vsCMPrototype.vsCMPrototypeType | vsCMPrototype.vsCMPrototypeFullname)));

                                                if (func.Prototype != curElement)
                                                {
                                                    curElement = func.Prototype;
                                                }
                                                else
                                                    continue;

                                                func.Line = codeFunc.StartPoint.Line;
                                                func.Description = codeFunc.Comment;

                                                elementList.Add(func);
                                                owPane.AddString(func.Prototype);
                                            }
                                        }
                                        break;
                                    case SearchType.Class:
                                        {
                                            var codeClass = (CodeClass)selected.ActivePoint.get_CodeElement(vsCMElement.vsCMElementClass);
                                            if (codeClass != null)
                                            {
                                                var _class = new ObjectType();
                                                _class.Name = codeClass.FullName;
                                                if (_class.Name != curElement)
                                                {
                                                    curElement = _class.Name;
                                                }
                                                else
                                                    continue;

                                                _class.Line = codeClass.StartPoint.Line;
                                                elementList.Add(_class);

                                                owPane.AddString(_class.Name);
                                            }
                                        }
                                        break;
                                    case SearchType.Enumerable:
                                        {
                                            var codeEnum = (CodeEnum)selected.ActivePoint.get_CodeElement(vsCMElement.vsCMElementEnum);
                                            if (codeEnum != null)
                                            {
                                                var _enum = new ObjectType();
                                                _enum.Name = codeEnum.FullName;
                                                if (_enum.Name != curElement)
                                                {
                                                    curElement = _enum.Name;
                                                }
                                                else
                                                    continue;

                                                _enum.Line = codeEnum.StartPoint.Line;
                                                elementList.Add(_enum);

                                                owPane.AddString(_enum.Name);
                                            }
                                        }
                                        break;
                                    case SearchType.Structure:
                                        {
                                            var codeStruct = (CodeStruct)selected.ActivePoint.get_CodeElement(vsCMElement.vsCMElementStruct);
                                            if (codeStruct != null)
                                            {
                                                var _struct = new ObjectType();
                                                _struct.Name = codeStruct.FullName;
                                                if (_struct.Name != curElement)
                                                {
                                                    curElement = _struct.Name;
                                                }
                                                else
                                                    continue;

                                                _struct.Line = codeStruct.StartPoint.Line;
                                                elementList.Add(_struct);

                                                owPane.AddString(_struct.Name);
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            catch
                            {
                            }

                            if (Canceled)
                                break;
                        }
                    }
                    else
                    {
                        fcm = doc.ProjectItem.FileCodeModel;
                        lines = fileList[i].Lines;

                        switch (sType)
                        {
                            case SearchType.Function:
                                {
                                    var codeFuncs = fcm.CodeElements.OfType<CodeFunction>();
                                    if (codeFuncs.Count() == 0)
                                        continue;

                                    foreach (var codeFunc in codeFuncs)
                                    {
                                        if (InSideFunc(codeFunc, lines, sType))
                                        {
                                            var func = new ObjectType();
                                            func.Name = codeFunc.Name;
                                            func.Prototype = codeFunc.get_Prototype((int)((vsCMPrototype.vsCMPrototypeParamNames | vsCMPrototype.vsCMPrototypeParamTypes | vsCMPrototype.vsCMPrototypeType | vsCMPrototype.vsCMPrototypeFullname)));

                                            if (func.Prototype != curElement)
                                            {
                                                curElement = func.Prototype;
                                            }
                                            else
                                                continue;

                                            func.Line = codeFunc.StartPoint.Line;
                                            elementList.Add(func);

                                            owPane.AddString(func.Prototype);
                                        }

                                        if (Canceled)
                                            break;
                                    }
                                }
                                break;
                            case SearchType.Class:
                                {
                                    var codeClasses = fcm.CodeElements.OfType<CodeClass>();
                                    if (codeClasses.Count() == 0)
                                        continue;

                                    foreach (var codeClass in codeClasses)
                                    {
                                        if (InSideFunc(codeClass, lines, sType))
                                        {
                                            var _class = new ObjectType();
                                            _class.Name = codeClass.FullName;

                                            if (_class.Name != curElement)
                                            {
                                                curElement = _class.Name;
                                            }
                                            else
                                                continue;

                                            _class.Line = codeClass.StartPoint.Line;
                                            elementList.Add(_class);

                                            owPane.AddString(_class.Name);
                                        }
                                        if (Canceled)
                                            break;
                                    }
                                }
                                break;
                            case SearchType.Enumerable:
                                {
                                    var codeEnums = fcm.CodeElements.OfType<CodeEnum>();
                                    if (codeEnums.Count() == 0)
                                        continue;

                                    foreach (var codeEnum in codeEnums)
                                    {
                                        if (InSideFunc(codeEnum, lines, sType))
                                        {
                                            var _enum = new ObjectType();
                                            _enum.Name = codeEnum.FullName;

                                            if (_enum.Name != curElement)
                                            {
                                                curElement = _enum.Name;
                                            }
                                            else
                                                continue;

                                            _enum.Line = codeEnum.StartPoint.Line;
                                            elementList.Add(_enum);

                                            owPane.AddString(_enum.Name);
                                        }

                                        if (Canceled)
                                            break;
                                    }
                                }
                                break;
                            case SearchType.Structure:
                                {
                                    var codeStructs = fcm.CodeElements.OfType<CodeStruct>();
                                    if (codeStructs.Count() == 0)
                                        continue;

                                    foreach (var codeStruct in codeStructs)
                                    {
                                        if (InSideFunc(codeStruct, lines, sType))
                                        {
                                            var _struct = new ObjectType();
                                            _struct.Name = codeStruct.FullName;

                                            if (_struct.Name != curElement)
                                            {
                                                curElement = _struct.Name;
                                            }
                                            else
                                                continue;

                                            _struct.Line = codeStruct.StartPoint.Line;
                                            elementList.Add(_struct);

                                            owPane.AddString(_struct.Name);
                                        }

                                        if (Canceled)
                                            break;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (bOpen)
                    {
                        bOpen = false;
                        //dte.Documents.Item(fileName).Close(vsSaveChanges.vsSaveChangesYes);
                    }

                    if (Canceled)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, false);
            }

            return elementList;
        }
        #endregion
    }
}
