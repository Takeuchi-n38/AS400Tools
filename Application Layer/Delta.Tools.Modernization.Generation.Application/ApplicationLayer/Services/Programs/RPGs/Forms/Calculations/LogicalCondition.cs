using Delta.Tools.CSharp.Statements.Items.Variables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Calculations
{
    public class LogicalCondition
    {
        public readonly string Expression;

        LogicalCondition(string expression)
        {
            Expression = expression;
        }

        public static LogicalCondition Of(string line, List<Variable> variables)
        {
            line = line.Trim();

            var comparisonConditions = new List<ComparisonCondition>();
            var logicalOperators = new List<string>();

            var operands =
                line
                .Replace("AND(", "AND (").Replace(")AND", ") AND")
                .Replace("OR(", "OR (").Replace(")OR", ") OR")
                .Replace(" AND ", " OR ").Split(" OR ").ToList()
                .Select(o => new { original = o.Trim(), comparisonCondition = ComparisonCondition.Of(o.Trim()) }).ToList();

            operands.ForEach(ope => line = line.Replace(ope.original, ope.comparisonCondition.Expression(variables)));
            line = line.Replace(" AND ", " && ").Replace(")AND ", ") && ").Replace(" AND(", " && (").Replace(")AND(", ") && (")
                       .Replace(" OR ", " || ").Replace(")OR ", ") || ").Replace(" OR(", " || (").Replace(")OR(", ") || (");
            return new LogicalCondition(line);

        }
    }
}
