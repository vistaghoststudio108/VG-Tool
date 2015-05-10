using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistaghost.VISTAGHOST.DataModel
{
    public class VGCodeElement : IEquatable<VGCodeElement>, IComparable<VGCodeElement>
    {
        public string Name { get; protected set; }
        public string Comment { get; set; }
        public string File { get; protected set; }
        public int BeginLine { get; protected set; }
        public int EndLine { get; protected set; }

        public string Preview { get; set; }

        public VGCodeElement(string file, string name, int beginline, int endline)
        {
            this.File = file;
            this.Name = name;
            this.BeginLine = beginline;
            this.EndLine = endline;
        }

        public bool Equals(VGCodeElement other)
        {
            return other.Name.Equals(this.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public VGCodeElement Copy()
        {
            VGCodeElement mem = new VGCodeElement(this.File, this.Name, this.BeginLine, this.EndLine);
            return mem;
        }

        public int CompareTo(VGCodeElement other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
