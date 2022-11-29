using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DisplayFiles.Commands
{
    public abstract class Command : IEquatable<Command?>
    {
         public readonly int Number;

       readonly Indicator? indicator;
        public Indicator Indicator
        {
            get
            {
                if(indicator == null) throw new InvalidOperationException();
                return indicator;
            }
        }


        protected Command(int number,Indicator? indicator)
        {
            Number = number;
            this.indicator = indicator;
        }
        protected Command(int number):this(number,null)
        {
        }

        public bool HasIndicator => indicator != null;

        #region "equals"

        public override bool Equals(object? obj)
        {
            return Equals(obj as Command);
        }

        public bool Equals(Command? other)
        {
            return other != null &&
                   EqualityComparer<Indicator?>.Default.Equals(indicator, other.indicator) &&
                   Number == other.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(indicator, Number);
        }

        public static bool operator ==(Command? left, Command? right)
        {
            return EqualityComparer<Command>.Default.Equals(left, right);
        }

        public static bool operator !=(Command? left, Command? right)
        {
            return !(left == right);
        }

        #endregion

    }
}
