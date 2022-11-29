using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Lines;
using System;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class CalculationLine : Line, IRPGLine
    {
        FormType IRPGLine.FormType => FormType.Calculation;

        readonly bool IsRPG4notRPG3;
        public bool IsRPG3Line =>! IsRPG4notRPG3;
        public CalculationLine(bool IsRPG4notRPG3, string value, int originalLineStartIndex, int originalLineEndIndex)
    : base(value.Length < (IsRPG4notRPG3 ? 80 : 70) ? value.PadRight(IsRPG4notRPG3 ? 80 : 70) : value, originalLineStartIndex, originalLineEndIndex)
        {
            this.IsRPG4notRPG3 = IsRPG4notRPG3;
        }

        protected CalculationLine(CalculationLine rPGCLine) : this(rPGCLine.IsRPG4notRPG3, rPGCLine.Value, rPGCLine.StartLineIndex, rPGCLine.EndLineIndex)
        {

        }

        //Position 6 (Form Type)
        //Positions 7-8 (Control Level)
        public string ControlLevel => Value.Substring(6, 2);
        public bool IsOR => ControlLevel=="OR";

        //Positions 9-11 (Indicators)
        public bool HasIndicator => Indicator1.Trim() != string.Empty;
        public string Indicator1 => Value.Substring(8, 3);
        public bool IsNOfIndicator1 => Value.Substring(8,1)=="N";
        public string NameOfIndicator1 => Value.Substring(9, 2);
        public bool HasIndicator2 => Indicator2.Trim() != string.Empty;
        public string Indicator2 => Value.Substring(11, 3);
        public bool IsNOfIndicator2 => Value.Substring(11, 1) == "N";
        public string NameOfIndicator2 => Value.Substring(12, 2);
        public bool HasIndicator3 => Indicator3.Trim() != string.Empty;
        public string Indicator3 => Value.Substring(14, 3);
        public bool IsNOfIndicator3 => Value.Substring(14, 1) == "N";
        public string NameOfIndicator3 => Value.Substring(15, 2);


        //Positions 12-25 (Factor 1)
        public string Factor1 => Value.Substring(IsRPG4notRPG3 ? 11 : 17, IsRPG4notRPG3 ? 14 : 10).TrimEnd();

        //Positions 26-35 (Operation and Extender)
        public string OperationExtender => Value.Substring(OperationExtenderStartIndex, IsRPG4notRPG3 ? 10 : 5).TrimEnd();
        int OperationExtenderStartIndex => IsRPG4notRPG3 ? 25 : 27; //Factor1StartIndex + OperandLength;

        //Positions 36-49 (Factor 2)
        public string Factor2 => Value.Substring(Factor2StartIndex, IsRPG4notRPG3 ? 14 : 10).TrimEnd();

        int Factor2StartIndex => IsRPG4notRPG3 ? 35 : 32; //OperationExtenderStartIndex + OperationExtenderLength;

        public string Factor2andMore => ((ILine)this).IsJoined ? Value.Substring(Factor2StartIndex) : Value.Substring(Factor2StartIndex, Math.Min(80, Value.Length) - Factor2StartIndex);

        //Positions 50-63 (Result Field) (RPG3)43-48
        public string ResultField => IsRPG4notRPG3 ? Value.Substring(49, 14).TrimEnd(): Value.Substring(42, 6).TrimEnd();
        //int ResultFieldStartIndex => IsRPG4notRPG3 ? 49 : 42; //Factor2StartIndex + OperandLength;

        //Positions 64-68 (Field Length) (RPG3)49-51
        public string FieldLength => IsRPG4notRPG3 ? Value.Substring(63, 5).Trim() : Value.Substring(48, 3).Trim(); //Value.Substring(ResultFieldStartIndex + OperandLength, IsRPG4notRPG3 ? 5 : 3).Trim();

        //Positions 69-70 (Decimal Positions) (RPG3)52
        public string DecimalPositions => IsRPG4notRPG3 ? Value.Substring(68, 2).Trim() : Value.Substring(51, 1).Trim();
        public string OperationExtender2  => IsRPG4notRPG3 ? throw new NotImplementedException() : Value.Substring(52, 1).TrimEnd();

        //Positions 71-76 (Resulting Indicators)
        public string Hi => IsRPG4notRPG3 ? Value.Substring(70, 2).TrimEnd() : Value.Substring(53, 2).TrimEnd();
        public string Lo => IsRPG4notRPG3 ? Value.Substring(72, 2).TrimEnd() : Value.Substring(55, 2).TrimEnd();
        public string Eq => IsRPG4notRPG3 ? Value.Substring(74, 2).TrimEnd() : Value.Substring(57, 2).TrimEnd();

        public string[] HiLoEq => new string[3] { Hi.Trim(), Lo.Trim(), Eq.Trim() };

        public bool IsUniqueIndicator=>HiLoEq.Where(i=>i!=string.Empty).Distinct().Count()==1;
    }
}
