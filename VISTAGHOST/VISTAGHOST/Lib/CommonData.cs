using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vistaghost.VISTAGHOST.Lib
{
    public enum ActionType
    {
        Modify = 1,
        Add,
        Delete,
        ChangeInfo,
        None
    }

    public enum LogFileType
    {
        TextFile = 0,
        Xml,
        Excel
    }

    public enum HeaderStyle
    {
        Aloka1 = 0,
        Aloka2,
        Aloka3,
        Aloka4,
        Doxygen,
        Vistaghost
    }

    public enum LanguageType
    {
        Cplusplus = 0,
        CSharp,
        Java,
        VisualBasic,
        Php,
        Xml,
        Unknown
    }

    public enum PrototypeType
    {
        FuncName = 0,
        FullProt
    }

    public enum SearchType
    {
        None = -1,
        AllFunction,
        NoneHeaderFunction,
        Class,
        Enumerable,
        Structure,
        Union,
        TypeDef
    }

    public enum DateFormat
    {
        yyyymmdd = 0,
        yyyyddmm,
        ddmmyyyy,
        mmddyyyy,
        // dd-mm-yyyy
        DateFormat1,
        // dd/mm/yyyy
        DateFormat2,
        FullDate,       // include date and time
    }

    public enum ErrorType
    {
        ErrSystem = 0,
        ErrNetwork,
        ErrIOFile
    }

    public enum VGDelCommentsType
    {
        DeleteDoubleSlash = 0,
        DeleteSlashStar,
        DeleteBoth,
        None
    }

    public enum VGDelCommentsOptions
    {
        SmartFormat = 0x001,
        DeleteAllBreakLine = 0x002,
        None = 0x000
    }

    public class VGCommand
    {
        public string KeyName { get; set; }

        public string HotKeys { get; set; }

        public int GroupID { get; set; }

        public string MissName { get; set; }

        public string DisName { get; set; }

        public VGCommand(int groupId, string keyname, string misname, string disname, string hotkey)
        {
            this.GroupID = groupId;
            this.KeyName = keyname;
            this.MissName = misname;
            this.HotKeys = hotkey;
            this.DisName = disname;
        }
    }

    [Serializable]
    public class RegisterData
    {
        public string FullName { get; set; }
        public string Account { get; set; }
        public string Rank { get; set; }
        public string WorkAt { get; set; }
        public string Date { get; set; }
        public bool Registered { get; set; }
    }

    public class CommentInfo
    {
        // input informations
        public string Content { get; set; }
        public string DevID { get; set; }
        public string Account { get; set; }
        public bool KeepComment { get; set; }


        // add comments config informations
        public string OpenTagBegin { get; set; }
        public string OpenTagEnd { get; set; }
        public string CloseTagBegin { get; set; }
        public string CloseTagEnd { get; set; }
        public int DateFormat { get; set; }
        public int EmptyLineNum { get; set; }
        public bool AutoShowInputDialog { get; set; }
        public bool JustOneLine { get; set; }

        public CommentInfo()
        {
            Account = "<account>";
            Content = "<content>";
            DevID = "<devid>";
            KeepComment = true;

            OpenTagBegin = "//<Not set> ";
            OpenTagEnd = "/";
            CloseTagBegin = "//<Not set> ";
            CloseTagEnd = "/";
            DateFormat = 0;
            EmptyLineNum = 0;
            AutoShowInputDialog = true;
            JustOneLine = false;
        }
    }

    public class HeaderInfo
    {
        public string BeginHeader { get; set; }
        public string EndHeader { get; set; }
        public bool AddBreakLine { get; set; }
        public string History { get; set; }
        public int Style { get; set; }
        public string XAModel { get; set; }
        public List<ComponentInfo> HeaderComponents { get; set; }

        public HeaderInfo()
        {
            BeginHeader = "/*=============================================================Aloka===========";
            EndHeader = "==============================================================Aloka==========*/";
            AddBreakLine = false;
            History = String.Empty;
            XAModel = "XA-161";
            Style = 0; //default style is Aloka1
            //HeaderComponents = new List<ComponentInfo>();
            //HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Module Name:" });
            //HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Calling Sequence:" });
            //HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Function:" });
            //HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Arguments:" });
            //HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Return Value:" });
            //HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "Note:" });
            //HeaderComponents.Add(new ComponentInfo { Checked = false, Name = "History:" });
        }
    }

    public class HistoryInfo
    {
        public bool DisplayHistory { get; set; }
        public bool WriteLogHistory { get; set; }
        public string LogPath { get; set; }
        public string LogExtension { get; set; }

        public HistoryInfo()
        {
            DisplayHistory = false;
            WriteLogHistory = false;
            LogPath = String.Empty;
            LogExtension = ".txt";
        }
    }

    [Serializable]
    public class Settings
    {
        public CommentInfo CommentInfo { get; set; }
        public HeaderInfo HeaderInfo { get; set; }
        public HistoryInfo HistoryInfo { get; set; }
        public DataInfo DataInfo { get; set; }

        public Settings()
        {
            CommentInfo = new CommentInfo();
            HeaderInfo = new HeaderInfo();
            HistoryInfo = new HistoryInfo();
            DataInfo = new DataInfo();
        }
    }

    public class IOType
    {
        public string Name { get; set; }
        public bool Input { get; set; }
        public bool Output { get; set; }

        public IOType()
        {
            Input = true;
            Output = false;
        }
    }

    public class DataInfo
    {
        public string FullPath { get; set; }
        public bool Storge { get; set; }
        public string ProcessorID { get; set; }
        public string BeginDate { get; set; }
        public bool RegisteredOnWeb { get; set; }

        public DataInfo()
        {
            FullPath = String.Empty;
            Storge = true;
            ProcessorID = String.Empty;
            BeginDate = String.Empty;
            RegisteredOnWeb = false;
        }
    }

    public class ComponentInfo
    {
        public string Name { get; set; }
        public bool Checked { get; set; }
    }

    public class ActionInfo
    {
        public string Path { get; set; }
        public int Line { get; set; }

        public void SetInfo(string path, int line)
        {
            Path = path;
            Line = line;
        }
    }

    enum ExImType   // import/export type
    {
        vgEXPORT = 0,
        vgIMPORT,
        vgRESET_ALL
    }

    public class FileNameInfo
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
    }

    public class Range
    {
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class ObjectType
    {
        public string Name { get; set; }
        public string Prototype { get; set; }
        public string Comment { get; set; }
        public int Line { get; set; }
        public string Description { get; set; }
        public List<IOType> Parameters { get; set; }
        public int Count { get; set; }
        public Range FuncRange { get; set; }

        public ObjectType()
        {
            Parameters = new List<IOType>();
        }
    }

    public class LVFuncInfo
    {
        public int Index { get; set; }
        public string FuncString { get; set; }
    }

    public enum FileFilter
    {
        ffSource = 0,
        ffHeader,
        ffAll
    }

    public class FileContainer
    {
        public string FileName { get; set; }
        public List<int> Lines { get; set; }

        public FileContainer()
        {
            Lines = new List<int>();
        }
    }
}
