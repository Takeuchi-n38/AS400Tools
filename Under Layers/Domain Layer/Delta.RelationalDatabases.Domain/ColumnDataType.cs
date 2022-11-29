using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.RelationalDatabases
{
    public abstract class ColumnDataType
    {
        public readonly string Name;
        public virtual string DefaultValue => string.Empty;
        public readonly int Length;
        public readonly int Scale;

        protected ColumnDataType(string name, int length, int scale)
        {
            Name = name;
            Length = length;
            Scale = scale;
        }
        protected ColumnDataType(string name, int length) : this(name, length, -1)
        {

        }
        protected ColumnDataType(string name) : this(name, -1)
        {

        }
    }
}
