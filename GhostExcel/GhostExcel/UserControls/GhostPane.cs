using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using GhostExcel.DataModel;
using System.Diagnostics;
using System.Collections;

namespace GhostExcel.UserControls
{
    public partial class GhostPane : UserControl
    {
        Point OriginPos = new Point(5, 27);
        Size FixedSize = new Size(230, 230);
        List<Function> listFunc = new List<Function>();
        bool CancelMenuContext = false;

        public GhostPane()
        {
            InitializeComponent();

            this.contextMenuStrip1.Opening += contextMenuStrip1_Opening;
        }

        void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = this.CancelMenuContext;
        }

        public void AddObj(object item)
        {
            this.objListViewFast.AddObject(item);
        }

        public void ClearList()
        {
            this.objListViewFast.ClearObjects();
        }

        private void AddIListViewItems(List<Function> list)
        {
            this.olvColumn1.AspectGetter = delegate(object x) { return ((Function)x).Name; };

            this.objListViewFast.SetObjects(list);
        }

        private void GhostPane_Load(object sender, EventArgs e)
        {
            this.textBoxFilterFast.Location = this.OriginPos;
            this.cbFilterType.Location = new Point(this.cbFilterType.Location.X, this.OriginPos.Y);
            this.label1.Location = new Point(this.OriginPos.X, this.OriginPos.Y - this.label1.Height);

            cbFilterType.SelectedIndex = 0;
        }

        private void GhostPane_SizeChanged(object sender, EventArgs e)
        {
            textBoxFilterFast.Width = this.Width - 15 - this.cbFilterType.Width;
            this.cbFilterType.Location = new Point
            {
                X = this.OriginPos.X + this.textBoxFilterFast.Width + 5,
                Y = this.OriginPos.Y - 1
            };

            this.objListViewFast.Height = this.Height - 64 - this.toolStripStatus.Height;
        }

        private void textBoxFilterFast_TextChanged(object sender, EventArgs e)
        {
            this.TimedFilter(this.objListViewFast, textBoxFilterFast.Text, cbFilterType.SelectedIndex);
        }

        private void TimedFilter(ObjectListView olv, string txt, int matchKind)
        {
            TextMatchFilter filter = null;
            if (!String.IsNullOrEmpty(txt))
            {
                switch (matchKind)
                {
                    case 0:
                    default:
                        filter = TextMatchFilter.Contains(olv, txt);
                        break;
                    case 1:
                        filter = TextMatchFilter.Prefix(olv, txt);
                        break;
                    case 2:
                        filter = TextMatchFilter.Regex(olv, txt);
                        break;
                }
            }
            // Setup a default renderer to draw the filter matches
            if (filter == null)
                olv.DefaultRenderer = null;
            else
            {
                olv.DefaultRenderer = new HighlightTextRenderer(filter);

                // Uncomment this line to see how the GDI+ rendering looks
                //olv.DefaultRenderer = new HighlightTextRenderer { Filter = filter, UseGdiTextRendering = false };
            }

            // Some lists have renderers already installed
            HighlightTextRenderer highlightingRenderer = olv.GetColumn(0).Renderer as HighlightTextRenderer;
            if (highlightingRenderer != null)
                highlightingRenderer.Filter = filter;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            olv.AdditionalFilter = filter;
            //olv.Invalidate();
            stopWatch.Stop();

            IList objects = olv.Objects as IList;
            if (objects == null)
                this.toolStripStatus.Text = String.Format("Filtered in {0}ms", stopWatch.ElapsedMilliseconds);
            else
                this.toolStripStatus.Text = String.Format("Filtered {0} items down to {1} items in {2}ms",
                                                          objects.Count,
                                                          olv.Items.Count,
                                                          stopWatch.ElapsedMilliseconds);
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void objListViewFast_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) 
                return;

            if (this.objListViewFast.FocusedItem != null &&
                this.objListViewFast.FocusedItem.Bounds.Contains(e.Location) == true)
            {
                this.CancelMenuContext = false;
            }
            else
                this.CancelMenuContext = true;
        }
    }
}
