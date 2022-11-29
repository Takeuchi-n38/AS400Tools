using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.Sources.Items
{
    public class Indent
    {
        public static int indentSize = 4;
        public static string indentUnit = new string(' ', indentSize);

        readonly string value;

        Indent(string value)
        {
            this.value = value;
        }

        public Indent() : this(indentUnit)
        {

        }

        public static Indent Zero = new Indent(string.Empty);
        public static Indent Single = new Indent();
        public static Indent Couple = Single.Increment();
        public static Indent Triple = Couple.Increment();
        public static Indent Quadruple = Triple.Increment();
        public static Indent Quintuple = Quadruple.Increment();
        public static Indent Sextuple = Quintuple.Increment();
        public static Indent Septuple = Sextuple.Increment();
        //7- septuple 8- octuple 9- nonuple

        public Indent Increment()
        {
            return new Indent(value + indentUnit);
        }

        public Indent Decrement()
        {
            return new Indent(value.Substring(indentSize));
        }

        public override string ToString()
        {
            return value;
        }
    }
}
