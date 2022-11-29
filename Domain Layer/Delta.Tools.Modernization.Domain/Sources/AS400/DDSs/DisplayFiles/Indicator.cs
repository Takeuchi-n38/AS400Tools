using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DisplayFiles
{
    public class Indicator : IEquatable<Indicator?>
    {
        public readonly bool IsNegation;

        public readonly int Number;

        public Indicator(bool isNegation, int number)
        {
            IsNegation = isNegation;
            Number = number;
        }

        public string NumberD2 => Number.ToString("D2");

        public string ValueString => $"{(IsNegation ? "N" : string.Empty)}{NumberD2}";

        #region "equals"
        public override bool Equals(object? obj)
        {
            return Equals(obj as Indicator);
        }

        public bool Equals(Indicator? other)
        {
            return other != null &&
                   IsNegation == other.IsNegation &&
                   Number == other.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsNegation, Number);
        }

        public static bool operator ==(Indicator? left, Indicator? right)
        {
            return EqualityComparer<Indicator>.Default.Equals(left, right);
        }

        public static bool operator !=(Indicator? left, Indicator? right)
        {
            return !(left == right);
        }
        #endregion

    }
}
