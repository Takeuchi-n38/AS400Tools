using Delta.AS400.DataTypes.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.CSharp.Statements.Items.Variables
{
    public class ScalarVariableList
    {
        public List<Variable> ScalarVariables = new List<Variable>();
        public int ItemCount => ScalarVariables.Count;

        public void Add(TypeOfVariable typeOfVariable, string name, int length)
        {
            Add(Variable.Of(typeOfVariable.Of(length), name));
        }
        public void Add(Variable scalarVariable)
        {
            ScalarVariables.Add(scalarVariable);
        }

        public List<string> Names => ScalarVariables.Select(v => v.Name).ToList();

        public List<string> TuppleSpellings()
        {
            return TuppleSpellings(ItemCount);
        }

        List<string> TuppleSpellings(int takeCount)
        {
            if (takeCount == 0)
            {
                //throw new ArgumentException();
                return new List<string>() { "string" };
            }

            List<string> tuppleSpels = new List<string>() { ScalarVariables[0].TypeSpelling };

            for (int i = 2; i <= takeCount; i++)
            {
                var keyTupple = $"({JoinedTypes(i)})";
                tuppleSpels.Add(keyTupple);
            }
            return tuppleSpels;

        }

        public string JoinedTypes() => JoinedTypes(ScalarVariables.Count);

        string JoinedTypes(int takeCount)
        {
            var list = new List<string>();
            ScalarVariables.Take(takeCount).ToList().ForEach(k =>
            {
                list.Add($"{k.TypeSpelling} {k.CamelCaseName}");
            }
            );
            return string.Join(",", list.ToArray());
        }

        public List<string> LowLimitSpels()
        {
            if (ItemCount == 0)
            {
                //throw new ArgumentException();
                return new List<string>() { "0" };//Id列作成の時、たぶん。
            }

            List<string> tuppleSpels = new List<string>();

            for (int paramCount = 0; paramCount < ItemCount; paramCount++)
            {
                var list = new List<string>();
                for (int j = 0; j < ScalarVariables.Count; j++)
                {
                    var v = ScalarVariables[j];
                    if (j < paramCount)
                    {
                        if (paramCount == 1)
                        {
                            list.Add($"key");
                        }
                        else
                        {
                            list.Add($"key.{v.CamelCaseName}");
                        }
                    }
                    else
                    {
                        if (v.OfTypeIsString)
                        {
                            list.Add($"string.Empty");
                        }
                        else
                        {
                            //list.Add($"{v.TypeSpelling}.MinValue");
                            list.Add($"0");
                        }
                    }
                }
                var keyTupple = $"({string.Join(",", list.ToArray())})";
                tuppleSpels.Add(keyTupple);
            }

            tuppleSpels.Add("key");

            return tuppleSpels;

        }
        public List<string> HiLimitSpels()
        {
            if (ItemCount == 0)
            {
                //throw new ArgumentException();
                return new List<string>() { "int.MaxValue" };//Id列作成の時、たぶん。
            }

            List<string> tuppleSpels = new List<string>();

            for (int paramCount = 0; paramCount < ItemCount; paramCount++)
            {
                var list = new List<string>();
                for (int j = 0; j < ScalarVariables.Count; j++)
                {
                    var v = ScalarVariables[j];
                    if (j < paramCount)
                    {
                        if (paramCount == 1)
                        {
                            list.Add($"key");
                        }
                        else
                        {
                            list.Add($"key.{v.CamelCaseName}");
                        }
                    }
                    else
                    {
                        if (v.OfTypeIsString)
                        {
                            list.Add($"new string('9', {v.TypeLength})");
                        }
                        else
                        {
                            //list.Add($"{v.TypeSpelling}.MaxValue");
                            
                            list.Add(CodePage290.MaxString(v.TypeLength));
                        }
                    }
                }
                var keyTupple = $"({string.Join(",", list.ToArray())})";
                tuppleSpels.Add(keyTupple);
            }

            tuppleSpels.Add("key");

            return tuppleSpels;

        }

    }
}
