using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Vistaghost.VISTAGHOST.ToolWindows
{
    class SearchListItem
    {
        public string Id { get; set; }
        public string PathBase { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
        public string Highlight { get; set; }

        public Brush Background
        {
            get;
            set;
        }

        public SearchListItem() { }

        public SearchListItem(string id, string pathbase, int a)
        {
            this.Id = id + ":" + a.ToString();

            PathBase = pathbase;

            //no folder setting contains
            Left = string.Format("{0} - ({1},{2}) : {3}", "C://abc/sdfsd//dsf", 1, 2, "end");
            Highlight = "highlight";
            Right = "right";
        }

        public SearchListItem(string text, string highlight)
        {
            if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(highlight)) return;
            int index = text.IndexOf(highlight, StringComparison.OrdinalIgnoreCase);
            if (index == -1) return;
            Left = text.Substring(0, index);
            Highlight = highlight;
            Right = text.Substring(index + Highlight.Length);
        }

        public override string ToString()
        {
            return string.Concat(Left, Highlight, Right);
        }
    }
}
