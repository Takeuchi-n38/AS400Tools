using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Calculations
{
    public class Formula
    {
        static bool IsArithmeticOperationsContainedIn(string source) => source.IndexOfAny(new char[] { '+', '-', '*', '/', }) > 0;
        /*
 足し算	addition	+	plus	sum
引き算	subtraction	-	minus	difference
掛け算	multiplication	×	times, multiplied by	product
割り算	division	÷	divided by	quotient
 */
        public static string Of(string source)
        {
            if (!IsArithmeticOperationsContainedIn(source) || source.Trim() == "'*'")
            {//単なる代入
                return source.ToCSharpOperand();
            }

            var formatted = string.Empty;
            var trimmedSource = source.Trim();

            if ((trimmedSource.StartsWith("*") || trimmedSource.StartsWith("\'") && trimmedSource.EndsWith("\'")) && TryParseConst(trimmedSource, out formatted))
            {//400定数の代入 or 定数の代入
                return formatted;
            }

            var formula = new StringBuilder();
            var searchedIndex = -1;
            while (true)
            {
                var newSearchdIndex = source.IndexOfAny(new char[] { '+', '-', '*', '/', }, searchedIndex + 1);
                if (newSearchdIndex == -1)
                {
                    var lastOperand = source.Substring(searchedIndex + 1).ToCSharpOperand();
                    formula.Append(lastOperand);
                    break;
                }

                var operand = source[(searchedIndex + 1)..newSearchdIndex].ToCSharpOperand();
                if (operand.StartsWith("("))
                {
                    operand = "(" + operand.Substring(1).ToPascalCase();
                }
                formula.Append($"{operand} {source[newSearchdIndex]} ");
                searchedIndex = newSearchdIndex;
            }

            return formula.ToString();
        }

        static Dictionary<string, string> Consts = new Dictionary<string, string>(){
            {"*OFF", "\"0\""},
            {"*ON", "\"1\""},
            {"*LOVAL", "int.MinValue"},
            {"*HIVAL", "int.MaxValue"},
            {"*BLANK", "string.Empty"},
            {"*ZERO", "0"},
        };

        static bool TryParseConst(string legacyCharacters, out string result)
        {
            result = string.Empty;

            if (legacyCharacters == string.Empty)
            {
                return false;
            }

            if (Consts.ContainsKey(legacyCharacters))
            {
                result = Consts.First(kv => kv.Key == legacyCharacters).Value;
                return true;
            }

            if (legacyCharacters.StartsWith("*IN") || legacyCharacters.StartsWith("&IN"))
            {
                var index = legacyCharacters.Substring(3, 2).ToUpper();
                result = $"In{index}";
                return true;
            }

            //if (legacyCharacters.StartsWith("'"))
            //{
            //    result = legacyCharacters.Replace('\'', '"');
            //}
            if (legacyCharacters.StartsWith('\'') && legacyCharacters.EndsWith('\'')
                //&& legacyCharacters.Length>2
                )
            {
                result = $"\"{legacyCharacters[1..^1].Trim()}\"";
                return true;
            }

            return false;
        }

    }
}
