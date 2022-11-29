using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class VarList
    {
        static public VarList Instance = new VarList();
        VarList()
        {

        }

        Dictionary<string, string> vars = new Dictionary<string, string>();

        public void Chgvar(ChgvarStatement chgvarLine)
        {
            var varName = chgvarLine.VarName;
            var newValue = chgvarLine.ValueOfVar;

            if (vars.ContainsKey(varName))
            {
                vars[varName] = newValue;
            }
            else
            {
                vars.Add(varName, newValue);
            }

        }

        public string Find(string varName)
        {
            string currentValue = null;
            if (vars.TryGetValue(varName, out currentValue))
            {
                return currentValue;
            }
            return null;
        }


        //internal void DeleteAll()
        //{
        //    toFiles.Clear();
        //}

    }
}
