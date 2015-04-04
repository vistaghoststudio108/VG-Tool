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
        mmddyyyy
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
        public bool AutoShowInputDialog { get; set; }
        public bool DisplayHistory { get; set; }

        public CommentInfo()
        {
            Account = "<account>";
            Content = "<content>";
            DevID = "<devid>";
            DateFormat = 0;
            OpenTagBegin = "//<Not set> ";
            OpenTagEnd = "/";
            CloseTagBegin = "//<Not set> ";
            CloseTagEnd = "/";
            KeepComment = true;
            AutoShowInputDialog = true;
            DisplayHistory = false;
        }
    }

    public class HeaderInfo
    {
        public string BeginHeader { get; set; }
        public string EndHeader { get; set; }
        public bool AddBreakLine { get; set; }
        public string History { get; set; }
        public int Style { get; set; }
        public List<ComponentInfo> HeaderComponents { get; set; }

        public HeaderInfo()
        {
            BeginHeader = "/*<Not set>";
            EndHeader = "<Not set>*/";
            AddBreakLine = false;
            History = String.Empty;
            Style = 0; //default style is Aloka1
        }
    }

    [Serializable]
    public class Settings
    {
        public CommentInfo CommentInfo { get; set; }
        public HeaderInfo HeaderInfo { get; set; }
        public DataInfo DataInfo { get; set; }

        public Settings()
        {
            CommentInfo = new CommentInfo();
            HeaderInfo = new HeaderInfo();
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

        public DataInfo()
        {
            FullPath = String.Empty;
            Storge = true;
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

    public class ObjectType
    {
        public string Name { get; set; }
        public string Prototype { get; set; }
        public string Comment { get; set; }
        public int Line { get; set; }
        public string Description { get; set; }
        public List<IOType> Parameters { get; set; }
        public int Count { get; set; }

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
}
