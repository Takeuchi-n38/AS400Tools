using System.Linq;

namespace Delta.AS400.Workstations.Fields.Edts
{
    public class Edtcde
    {

        protected readonly int length;
        protected readonly int decimalPositions;
        protected readonly bool signed;
        protected readonly bool existThousandSeparator;

        protected Edtcde(int length, int decimalPositions, bool signed, bool existThousandSeparator)
        {
            this.length = length;
            this.decimalPositions = decimalPositions;
            this.signed = signed;
            this.existThousandSeparator = existThousandSeparator;
        }

        public static Edtcde KforY(int length, int decimalPositions)
        {
            return new Edtcde(length, decimalPositions, false, true);
        }

        public static Edtcde MforY(int length, int decimalPositions)
        {
            return new Edtcde(length, decimalPositions, false, false);
        }

        public static Edtcde Of(int length, int decimalPositions, bool signed, string edtcde)
        {
            return new Edtcde(length, decimalPositions, signed, edtcde == "K");
        }

        protected string integerFormat => existThousandSeparator ? "#,#" : "#";
        protected string decimalFormat => decimalPositions == 0 ? string.Empty : "." + new string('0', decimalPositions);
        protected string format => $"{integerFormat}{decimalFormat} ;{integerFormat}{decimalFormat}-";

        protected bool isInteger => decimalPositions == 0;

        protected bool isDecimal => decimalPositions > 0;

        public string ConvertToInputUI(decimal value)
        {
            if (value == decimal.Zero) return "";
            return value.ToString(format);
        }

        public decimal ConvertBackFromInputUI(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;

            var removedSeparator = value.Replace(",", "");

            var isNegative = value.Contains("-");

            var removedSign = value.Replace("-", "");

            var parsableValue = isNegative ? "-" : string.Empty + removedSign;

            if (isInteger || !value.Contains(".")) return decimal.Parse(value);

            var splipped = value.Split('.');
            var intValue = splipped[0];
            var decValue = splipped[1].Length <= decimalPositions ? splipped[1] : splipped[1].Substring(0, decimalPositions);
            return decimal.Parse(intValue + "." + decValue);
        }

        public string ValidateNotifyError(string value)
        {
            if (string.IsNullOrEmpty(value)) return null;

            if (isInteger && value.Contains(".")) return "error";

            if (value.Count(c => c == '.') > 1) return "error";

            if (!existThousandSeparator && value.Contains(",")) return "error";

            if (!signed && value.Contains("-")) return "error";

            if (value.Count(c => c == '-') > 1) return "error";

            var removedSeparator = value.Replace(",", "");

            if (isInteger)
            {
                return int.TryParse(removedSeparator, out int result) ? null : "error";
            }

            if (isDecimal)
            {
                return decimal.TryParse(removedSeparator, out decimal result) ? null : "error";
            }

            return "error";

        }
    }
}
