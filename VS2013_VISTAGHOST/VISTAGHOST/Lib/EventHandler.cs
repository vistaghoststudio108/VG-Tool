using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistaghost.VISTAGHOST.Lib
{
    public delegate void DeleteEventHandler(bool delDS,          /*double-slash*/
                                            bool delSS,          /*slash-star*/
                                            bool delBL,          /*break line*/
                                            bool smartFM        /*format*/);

    public delegate void ExportEventHandler(string message, bool result);

    public delegate void ConfigEventHandler(Settings data);

    public delegate void AddCommentEventHandler(string content, string accout, string devid,
                                    string find, string replace, bool moreopt,
                                    ActionType mode, bool keep_comments, bool contentchanged);

    public delegate void AddHeaderEventHandler(ObjectType func);

    public delegate void HeaderInputEventHandler(List<LVFuncInfo> funcinfo);

    public delegate void HeaderNotifyEventHandler(int index);

    public delegate void HistoryViewerEventHandler(string file);

    public delegate void ToolWindowPaneEventHandler(int swCode);
}
