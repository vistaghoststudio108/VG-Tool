using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistaghost.VISTAGHOST.User_Control
{
    public partial class ColorComboBox : Component
    {
        public ColorComboBox()
        {
            InitializeComponent();
        }

        public ColorComboBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
