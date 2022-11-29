using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Generator.Statements.Singles
{
    public class CallProgramStatementCSharpGenerater
    {
        public static void Generate(CallProgramStatement statement,Indent indent, List<string> CSharpSourceLines, List<Variable> variables)
        {
            var CallerProgramName=statement.CallerProgramName.ToPublicModernName();

            var Comment = statement.Comment.ToOriginalComment(); ;

            var CallerProgramParameterNames=statement.CallerProgramParameterNames;

            //0062 CALL       PGM(*CURLIB/PQEA510) PARM(&OPT)
            var dialogName = CallerProgramName;
            if (Comment.Contains("CMENOK"))
            {
                dialogName = "Dmenok";
            }

            List<string>? splittedParm = null;
            if (CallerProgramParameterNames.Count() > 0)
            {
                var parameterName = $"{CallerProgramName}Parameters";
                splittedParm = CallerProgramParameterNames.Select(n=> n.ToCSharpOperand()).ToList();
                CSharpSourceLines.Add($"{indent}var {parameterName} = new Object[{splittedParm.Count()}];");
                for (int i = 0; i < splittedParm.Count(); i++)
                {
                    CSharpSourceLines.Add($"{indent}{parameterName}[{i}] = {splittedParm[i]};");
                }

                if(splittedParm.Any(p=>!(p.StartsWith("\"") && p.EndsWith("\""))))
                {
                    CSharpSourceLines.Add($"{indent}var {dialogName}ReturnParameters = pgmCaller.Call(\"{dialogName}\", {parameterName}); {Comment}");
                    for (int i = 0; i < splittedParm.Count(); i++)
                    {
                        var castType = "string";
                        var targetVariable = variables.Find(v => v.Name == splittedParm[i]);
                        if (targetVariable != null)
                        {
                            castType = targetVariable.TypeSpelling;
                        }
                        CSharpSourceLines.Add($"{indent}{splittedParm[i]} = ({castType}){dialogName}ReturnParameters[{i}];{(targetVariable != null ? string.Empty : "//TODO:type")}");
                    }
                }


            }
            else
            {
                CSharpSourceLines.Add($"{indent}pgmCaller.Call(\"{dialogName}\"); {Comment}");
            }

        }

    }
}
