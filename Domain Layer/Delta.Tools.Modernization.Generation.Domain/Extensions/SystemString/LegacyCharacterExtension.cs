using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class LegacyCharacterExtension
    {
        static Dictionary<char, char> Escapes = new Dictionary<char, char>(){
            {'\'', '"'},
            {'\\', 'o'},
            {'#', 'i'},
            {'*', 'x'},
            {'@', 'a'},
        };

        static string EscapeLegacyCharacter(this string legacyCharacters)
        {
            if(legacyCharacters.Length==0) return legacyCharacters;
            Escapes.ToList().ForEach(kv => legacyCharacters = legacyCharacters.Replace(kv.Key, kv.Value));
            if(legacyCharacters[0]=='&') legacyCharacters= legacyCharacters[1..];//CL変数プリフィックス除去
            return legacyCharacters;
        }

        public static string ToPublicModernName(this string legacyName)
        {
            return legacyName.Trim().EscapeLegacyCharacter().ToPascalCase();
        }

        public static string ToCSharpOperand(this string legacyOperand)
        {
            var trimmed = legacyOperand.Trim();

            var formatted = string.Empty;

            if (TryParseConst(trimmed, out formatted))
            {
                return formatted;
            }

            trimmed = trimmed.EscapeLegacyCharacter();

            if (TryReplaceLegacyArrayBlanket(trimmed, out formatted))
            {
                return formatted;
            }

            return trimmed.ToPascalCase();

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
                if (legacyCharacters.StartsWith("*INLR"))
                {
                    result = "LR";
                    return true;
                }
                else
                {
                    var index = legacyCharacters.Substring(3, 2).ToUpper();
                    result = $"In{index}";
                    return true;
                }
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

            //CCSID930.ToStringFrom(0x0F) //0048      C           WK1,I     IFEQ X'0F'             
            if (legacyCharacters.StartsWith("X\'") && legacyCharacters.EndsWith('\''))
            {
                result = $"CCSID930.ToStringFrom(0x{legacyCharacters[2..^1]})";
                return true;
            }

            return false;
        }

        static bool TryReplaceLegacyArrayBlanket(string legacyCharacters, out string result)
        {
            result = string.Empty;

            if (!legacyCharacters.StartsWith("(") && legacyCharacters.EndsWith(")") && legacyCharacters.Contains("("))
            {
                var indexLength = legacyCharacters.IndexOf(")") - legacyCharacters.IndexOf("(") - 1;
                var index = legacyCharacters.Substring(legacyCharacters.IndexOf("(") + 1, indexLength).ToPascalCase();
                result = legacyCharacters.Substring(0, legacyCharacters.IndexOf("(")).ToPascalCase() + "[" + index + " - 1]";
                return true;
            }

            if (legacyCharacters.Contains(','))
            {
                var splitted = legacyCharacters.Split(',');
                //CRSU,I
                var arrayName = splitted[0].ToPascalCase();
                var index = splitted[1].ToPascalCase();
                result = arrayName + "[" + index + "]";
                return true;

            }

            return false;
        }
    }
}
