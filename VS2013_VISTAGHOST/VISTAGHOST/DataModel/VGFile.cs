using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vistaghost.VISTAGHOST.DataModel
{
    public class VGFile : IEquatable<VGFile>
    {
        public string Path { get; private set; }
        public List<VGCodeElement> Members { get; private set; }
        public VGFile(string path) 
        {
            this.Path = path;
            Members = new List<VGCodeElement>();
        }

        public bool Equals(VGFile other)
        {
			return this.Path.Equals(other.Path);
        }

        public override string ToString()
        {
            return System.IO.Path.GetFileName(Path);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}
