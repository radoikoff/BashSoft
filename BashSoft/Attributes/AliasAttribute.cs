using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AliasAttribute : Attribute
    {
        private string name;

        public AliasAttribute(string name)
        {
            this.name = name;
        }

        public string Name => this.name;

        public override bool Equals(object obj)
        {
            return this.name.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

    }
}
