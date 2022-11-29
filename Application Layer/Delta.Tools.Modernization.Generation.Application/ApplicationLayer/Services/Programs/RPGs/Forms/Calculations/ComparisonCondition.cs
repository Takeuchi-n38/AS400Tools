using System;
using System.Collections.Generic;
using System.Linq;
using Delta.Tools.CSharp.Statements.Items.Variables;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Calculations
{
    public class ComparisonCondition
    {
        static Dictionary<string, string> ComparisonOperators;

        static ComparisonCondition()
        {
            ComparisonOperators = new Dictionary<string, string>();
            //rpg
            ComparisonOperators.Add("<>", "!=");
            ComparisonOperators.Add("<=", "<=");
            ComparisonOperators.Add(">=", ">=");
            ComparisonOperators.Add("=", "==");
            ComparisonOperators.Add("<", "<");
            ComparisonOperators.Add(">", ">");

            //cl
            ComparisonOperators.Add("*NE", "!=");
            ComparisonOperators.Add("*LE", "<=");
            ComparisonOperators.Add("*GE", ">=");
            ComparisonOperators.Add("*EQ", "==");
            ComparisonOperators.Add("*LT", "<");
            ComparisonOperators.Add("*GT", ">");
        }

        internal readonly string LeftOperand;
        internal readonly string ComparisonOperator;
        internal readonly string RightOperand;

        ComparisonCondition(string leftOperand, string comparisonOperator, string rightOperand)
        {
            LeftOperand = leftOperand;
            ComparisonOperator = comparisonOperator;
            RightOperand = rightOperand;
        }

        public static ComparisonCondition Of(string trimmedFunciton)
        {

            var LeftOperand = string.Empty;
            var ComparisonOperator = string.Empty;
            var RightOperand = string.Empty;

            foreach (var co in ComparisonOperators)
            {
                if (trimmedFunciton.Contains(co.Key))
                {
                    ComparisonOperator = co.Value;
                    var operands = trimmedFunciton.Split(co.Key);
                    LeftOperand = RemoveBlacketAndFormat(operands[0].Trim());
                    RightOperand = RemoveBlacketAndFormat(operands[1].Trim());
                    return new ComparisonCondition(LeftOperand, ComparisonOperator, RightOperand);
                }
            }

            throw new ArgumentException(trimmedFunciton);
        }

        static string RemoveBlacketAndFormat(string trimmed)
        {
            if (!trimmed.Contains("(") && !trimmed.Contains(")")) return ToFormula(trimmed);

            if (trimmed.StartsWith("(") && trimmed.Count(c => c == '(') == 1 && !trimmed.Contains(")"))
            {
                return "(" + ToFormula(trimmed.Substring(1));
            }

            if (trimmed.EndsWith(")") && trimmed.Count(c => c == ')') == 1 && !trimmed.Contains("("))
            {
                return ToFormula(trimmed[0..^1]) + ")";
            }

            return ToFormula(trimmed);
        }

        static string ToFormula(string original)
        {
            return Formula.Of(original);
        }

        public string Expression(List<Variable> variables)
        {
            if (ComparisonOperator == "==" || ComparisonOperator == "!=") return $"{LeftOperand} {ComparisonOperator} {RightOperand}";

            var leftOperandRemovedBlacket = LeftOperand.Replace("(", string.Empty);
            var leftVariable = variables.Where(v => v.Name == leftOperandRemovedBlacket).FirstOrDefault() ?? Variable.OfUnknownNameBy(leftOperandRemovedBlacket);
            if (!leftVariable.OfTypeIsString) return $"{LeftOperand} {ComparisonOperator} {RightOperand}";

            var rightOperandRemovedBlacket = RightOperand.Replace(")", string.Empty);
            var rightVariable = variables.Where(v => v.Name == rightOperandRemovedBlacket).FirstOrDefault() ?? Variable.OfUnknownNameBy(rightOperandRemovedBlacket);
            if (!rightVariable.OfTypeIsString) return $"{LeftOperand} {ComparisonOperator} {RightOperand}";

            return $"CodePage290Comparator.Compare({leftOperandRemovedBlacket}, {rightOperandRemovedBlacket}) {ComparisonOperator} 0";

        }

    }
}
