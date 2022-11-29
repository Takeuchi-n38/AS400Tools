using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Calculations;
using Delta.Tools.AS400.Generator.Statements.Singles;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs;
using Delta.Tools.AS400.Programs.CLs;
using Delta.Tools.AS400.Programs.CLs.Lines;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using Delta.Tools.Sources.Statements.Blocks.Ifs;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Utilities.Extensions.SystemString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.CLs
{
    public class CLStructureToCSharper
    {
        ObjectIDFactory ObjectIDFactory;
        ProgramStructureBuilder ProgramStructureFactory;

        public CLStructureToCSharper(ObjectIDFactory ObjectIDFactory,ProgramStructureBuilder programStructureFactory)
        {
            this.ObjectIDFactory= ObjectIDFactory;
            ProgramStructureFactory = programStructureFactory;
        }

        public static List<Variable> Parameters(CLStructure cLStructure)
        {
            var parameters = new List<Variable>();

            cLStructure.Elements.Where(line => line is PgmStatement).Cast<PgmStatement>().FirstOrDefault().ParameterNames.ForEach(para => {
                var parameterName = para.ToCSharpOperand();
                var finded = Variables(cLStructure).Find(v => v.Name == parameterName);
                if (finded != null)
                {
                    parameters.Add(finded);
                }
                else
                {
                    parameters.Add(Variable.Of(TypeOfVariable.OfUnknown, parameterName));
                }
            });

            return parameters;
        }

        public static List<Variable> Variables(CLStructure cLStructure) => cLStructure.Elements.Where(line => line is DclStatement).Cast<DclStatement>()
                                                .Select(line => ToVariable(line)).ToList();
        static Variable ToVariable(DclStatement line)
        {
            return Variable.Of(ToTypeOfVariable(line), line.Var.Substring(1).ToPublicModernName());
        }

        static TypeOfVariable ToTypeOfVariable(DclStatement dclStatement)
        {
            if (dclStatement.Type == "*CHAR") return TypeOfVariable.OfString(dclStatement.Length);
            //if (InternalDataType == "B") return "byte";
            //if (InternalDataType == "D") return "DateTime";

            if (dclStatement.Type == "*DEC")
            {
                if (dclStatement.DecimalPositions == 0)
                {
                    if (dclStatement.Length > 9) return TypeOfVariable.OfLong(dclStatement.Length);
                    if (dclStatement.Length > 4) return TypeOfVariable.OfInt(dclStatement.Length);
                    return TypeOfVariable.OfShort(dclStatement.Length);
                }
                return TypeOfVariable.OfDecimal(dclStatement.Length, dclStatement.DecimalPositions);
            }
            //if (InternalDataType == string.Empty)
            //{
            //    if (DecimalPositions == string.Empty) return "string";
            //    if (int.Parse(DecimalPositions) > 0) return "decimal";

            //    if (int.Parse(Length) > 9) return "long";
            //    if (int.Parse(Length) > 4) return "int";
            //    return "short";
            //}
            return TypeOfVariable.OfUnknown;

        }

        public void CalculateMethodBlockStatement(List<string> cSharpSourceLines, List<Variable> parameters, List<Variable> variables,CLStructure cLStructure)
        {
            var calculateMethodBlockStatement = MethodBlockStatement.CalculateOf();

            var ExfmtLineNumbers = cLStructure.Elements.Where(line => line is SndrcvfStatement).Cast<SndrcvfStatement>()
                                                .Select(line => line.StartLineIndex).ToList();

            cLStructure.Elements.ForEach(element => calculateMethodBlockStatement.Add(element));

            NestedBlockStatement<IStatement>.GatherNestedBlock(calculateMethodBlockStatement, NestedBlockFactory.Create);

            ExfmtLineNumbers.ForEach(exfmtLineNumber =>
            {
                cSharpSourceLines.Add($"bool GoToAfterExfmt{exfmtLineNumber:D4} {{ get;set;}} = false;");
            });

            cSharpSourceLines.Add($"void {calculateMethodBlockStatement.Name.ToPublicModernName()}() ");
            cSharpSourceLines.Add("{");
            ExfmtLineNumbers.ForEach(exfmtLineNumber =>GoToAfterExfmtToCSharp.GenerateCSharpSourceLines(cSharpSourceLines, Indent.Single, exfmtLineNumber, false));
            GenerateCSharpSourceLines(cSharpSourceLines, Indent.Single,parameters, variables, calculateMethodBlockStatement.Statements);
            cSharpSourceLines.Add("}");

        }

        void GenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, List<Variable> parameters, List<Variable> variables, IEnumerable<IStatement> statements)
        {
            statements.ToList().ForEach(statement => {
                var originalComment =(statement is ILine)?((ILine)statement).ToOriginalComment():string.Empty;

                //CallLine
                if (statement is CLCommentLine)
                {
                    CSharpSourceLines.Add($"{indent}{originalComment}");
                }
                else
                if (statement is AutostartLine autostartLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO:AUTOSTART {originalComment}");
                }
                else
                if (statement is CallLine callLine)
                {
                    var objectID = ObjectIDFactory.Create(callLine.Library, callLine.ProgramName);
                    try
                    {
                        var ThisCallerProgram = ProgramStructureFactory.Create(objectID);
                        CallProgramStatementCSharpGenerater.Generate(callLine.ThisCallProgramStatement(ThisCallerProgram), indent, CSharpSourceLines, variables);
                    }
                    catch
                    {
                       
                        var log = $"{indent}//TODO:error ProgramStructureFactory.Create { string.Join(",", objectID.ToClassification())} {originalComment}";
                        CSharpSourceLines.Add(log);
                        Console.WriteLine(log);
                    }
                }
                else
                if (statement is ChgjobLine chgjobLine)
                {
                    CSharpSourceLines.Add($"{indent}Retriever.Instance.Job.Sws=\"{chgjobLine.Sws}\"; {originalComment}");
                }
                else
                if (statement is ChgvarStatement chgvarStatement)
                {
                    ChgvarGenerateCSharpSourceLines(chgvarStatement, indent, CSharpSourceLines);
                }
                else if (statement is CLGotoStatement cLGotoStatement)
                {
                    CSharpSourceLines.Add($"{indent}goto {cLGotoStatement.Value.ToPublicModernName().ToLower()}; {originalComment}");
                }
                else
                if (statement is CLLabelStatement cLLabelStatement)
                {
                    CSharpSourceLines.Add($"{indent}{cLLabelStatement.Value.ToPublicModernName().ToLower()}: ;{originalComment}");
                }
                else
                if (statement is CLTodoLine)
                {
                    CSharpSourceLines.Add($"{indent}//todo:design migrate specific {originalComment}");
                }
                else
                if (statement is DclfLine || statement is EndpgmStatement || statement is PgmStatement)
                {
                    CSharpSourceLines.Add($"{indent}//Done {originalComment}");
                }
                else
                if (statement is DclStatement dclStatement)
                {
                    var v = ToVariable(dclStatement);
                    if (parameters.Any(p => p.Name == v.Name))
                    {
                        CSharpSourceLines.Add($"{indent}{v.Name} = {v.TypeInitialValueSpelling};{originalComment}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{v.TypeSpelling} {v.Name} = {v.TypeInitialValueSpelling};{originalComment}");
                    }

                    //if (dclStatement.HasInitialValue == string.Empty)
                    //{
                    //    CSharpSourceLines.Add($"{indent}//Done {originalComment}");
                    //}
                    //else
                    //{
                    //    CSharpSourceLines.Add($"{indent}{dclStatement.Var.Substring(1).ToPublicModernName()} = {dclStatement.InitialValue};{originalComment}");
                    //}
                }
                else
                if (statement is ElseCmdDoLIne elseCmdDoLIne)
                {
                    CSharpSourceLines.Add($"{indent}else{{{originalComment}");
                }
                else
                if (statement is EnddoStatement enddoStatement)
                {
                    CSharpSourceLines.Add($"{indent}}}{originalComment}");
                }
                else
                if (statement is FmtdtaLine fmtdtaLine)
                {
                    //0320 FMTDTA     INFILE((&WC2/ALLDATA)) OUTFILE(&WC2/JJB100IN) SRCFILE(&JJ_/QFMTSRC) SRCMBR(JJB100IN) OPTION(*NOCHK *NOPRT)
                    CSharpSourceLines.Add($"{indent}//var {fmtdtaLine.OutFileObjectID.Name.ToPublicModernName().ToLower()} = {fmtdtaLine.SrcObjectID.Name.ToPublicModernName()}Formatter.Format({fmtdtaLine.InFileObjectIDs.First().Name.ToPublicModernName().ToLower()});{originalComment}");
                }
                else
                if (statement is FtpLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO:FTP {originalComment}");
                }
                else
                if (statement is IfThenStatement ifThenStatement)
                {
                    var Then = ifThenStatement.Then;
                    var expression = Then;
                    if (Then == "RETURN") {
                        expression = "LR.TurnOn();return;";
                    }
                    else
                    if (Then == "SIGNOFF") 
                    {
                        expression = "SignOff(); return";
                    }
                    else
                    if (Then.StartsWith("GOTO "))
                    {
                        expression = "//TODO:GOTO ";
                        var splitted = Then.CompressSpacesToSingleSpace().Split(" ");
                        var labelName = splitted[1];
                        if (labelName.Contains("CMDLBL"))
                        {
                            labelName = TextClipper.ClipParameter(Then, "CMDLBL");
                        }
                        expression = $"goto {labelName.ToCSharpOperand().ToLower()}";
                    }
                    else
                    {
                        expression = $"//TODO {Then}";
                    }

                    var condition = string.Empty;
                    if (ifThenStatement.Cond.StartsWith("%SST"))
                    {
                        //%SST(&SWS 2 1) = '1'
                        var openBlaketIndex = ifThenStatement.Cond.LastIndexOf('(');
                        var closeBlaketIndex = ifThenStatement.Cond.LastIndexOf(')');
                        var splitted = ifThenStatement.Cond.Substring(openBlaketIndex+1, closeBlaketIndex- openBlaketIndex-1).Split(' ');
                        var Stanaka= $"{splitted[0].ToCSharpOperand()}.Substring({(int.Parse(splitted[1]) - 1)},{int.Parse(splitted[2])})";
                        var tmp = LogicalCondition.Of($"Stanaka{ifThenStatement.Cond.Substring(closeBlaketIndex+1)}", variables).Expression;
                        condition = tmp.Replace("Stanaka",Stanaka);
                    }
                    else
                    {
                        condition = LogicalCondition.Of(ifThenStatement.Cond, variables).Expression;
                    }

                    CSharpSourceLines.Add($"{indent}if ({condition}){originalComment}");
                    CSharpSourceLines.Add($"{indent}{{");
                    CSharpSourceLines.Add($"{indent.Increment()}{expression};");
                    CSharpSourceLines.Add($"{indent}}}");
                }
                else
                if (statement is MonmsgLine monmsgLine)
                {
                    if (monmsgLine.Value.Contains("EXEC(DO)"))
                    {
                        CSharpSourceLines.Add($"{indent}if(false){{//TODO:MONMSG {originalComment}");
                    }
                    //else
                    //{
                    //    CSharpSourceLines.Add($"{indent}//TODO:MONMSG {originalComment}");
                    //}
                }
                else
                if (statement is RclrscLine)
                {
                    CSharpSourceLines.Add($"{indent}{originalComment}");
                }
                else
                if (statement is ReturnStatement)
                {
                    CSharpSourceLines.Add($"{indent}LR=\"1\";return;{originalComment}");
                }
                else
                if (statement is RtvdtaaraLine rtvdtaaraLine)
                {
                    if (rtvdtaaraLine.DtaaraName=="*LDA")
                    {
                        CSharpSourceLines.Add($"{indent}{rtvdtaaraLine.Rtnvar[1..].ToCSharpOperand()} = Retriever.Instance.DataAreaSingleValues.Lda.Substring({rtvdtaaraLine.StartingPosition - 1},{rtvdtaaraLine.SubstringLength});{originalComment}");
                    }
                    else
                    {
                        //Retriever.Instance.DataAreaSingleValues.Lda.Substring(100,1);
                        CSharpSourceLines.Add($"{indent}{rtvdtaaraLine.Rtnvar[1..].ToCSharpOperand()} = {rtvdtaaraLine.DtaaraName.ToCSharpOperand()}.Value.Substring({rtvdtaaraLine.StartingPosition - 1},{rtvdtaaraLine.SubstringLength});{originalComment}");
                    }
                }
                else
                if (statement is RtvmbrdLine rtvmbrdLine)
                {
                    var count = rtvmbrdLine.Nbrcurrcd[1..].ToCSharpOperand();
                    var fileName = rtvmbrdLine.FileObjectID.Name.ToCSharpOperand();
                    //Kensu = DependencyInjector.Pddat1Repository.Count(); //0012 RTVMBRD    FILE(&LIB/PDDAT1) NBRCURRCD(&KENSU)
                    CSharpSourceLines.Add($"{indent}//TODO: {count} = DependencyInjector.{fileName}Repository.Count();{originalComment}");
                }
                else
                if (statement is RtvnetaStatement rtvnetaStatement)
                {
                    if (rtvnetaStatement.Sysname != string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}{rtvnetaStatement.Sysname.ToCSharpOperand()} = Retriever.Instance.Network.Sysname;{originalComment}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{indent}{originalComment}");
                    }
                }
                else
                if (statement is RtvjobaStatement rtvjobaStatement)
                {
                    CSharpSourceLines.Add($"{indent}{originalComment}");
                    var Job = rtvjobaStatement.Job;
                    var User = rtvjobaStatement.User;
                    var Nbr = rtvjobaStatement.Nbr;
                    if (Job != string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}{Job.ToCSharpOperand()} = Retriever.Instance.Job.Job;");
                    }
                    if (User != string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}{User.ToCSharpOperand()} = Retriever.Instance.Job.User;");
                    }
                    if (Nbr != string.Empty)
                    {
                        CSharpSourceLines.Add($"{indent}{Nbr.ToCSharpOperand()} = Retriever.Instance.Job.Nbr;");
                    }
                }
                else
                if (statement is RtvsysvalStatement rtvsysvalStatement)
                {
                    CSharpSourceLines.Add($"{indent}{rtvsysvalStatement.Rtnvar.ToCSharpOperand()} = {rtvsysvalStatement.SystemValue()} {originalComment}");
                }
                else
                if (statement is SbmjobLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO:SBMJOB {originalComment}");
                }
                else
                if (statement is SignOffStatement)
                {
                    CSharpSourceLines.Add($"{indent}SignOff(); return;{originalComment}");
                }
                else
                if (statement is SndbrkmsgLine sndbrkmsgLine)
                {
                    CSharpSourceLines.Add($"{indent}//TODO:SNDBRKMSG {originalComment}");
                }
                else
                if (statement is SndfStatement sndfStatement)
                {
                    CSharpSourceLines.Add($"{indent}Write({sndfStatement.Rcdfmt.ToPublicModernName()}); {originalComment}");
                }
                else
                if (statement is SndmsgLine sndmsgLine)
                {
                    CSharpSourceLines.Add($"{indent}msgSender.Send(\"{sndmsgLine.Msg}\",\"{sndmsgLine.Tomsgq}\");{originalComment}");
                }
                else
                if (statement is SndpgmmsgLine sndpgmmsgLine)
                {
                    //pgmMsgSender.Send("PLFB200 実行中"); //0191 SNDPGMMSG  MSGID(CPF9898) MSGF(QCPFMSG) MSGDTA('PLFB200 実行中') TOPGMQ(*EXT) MSGTYPE(*STATUS)
                    CSharpSourceLines.Add($"{indent}pgmMsgSender.Send({sndpgmmsgLine.Msgdta.Replace('\'', '"')});{originalComment}");
                }
                else
                if (statement is SndrcvfStatement sndrcvfStatement)
                {
                    var Rcdfmt = sndrcvfStatement.Rcdfmt.ToPublicModernName();
                    var LineNumber = sndrcvfStatement.StartLineIndex.ToString("D4");

                    CSharpSourceLines.Add($"{indent}{originalComment}");
                    CSharpSourceLines.Add($"{indent}Exfmt({Rcdfmt});");
                    CSharpSourceLines.Add($"{indent}GoToAfterExfmt{LineNumber} = true;");
                    CSharpSourceLines.Add($"{indent}if(GoToAfterExfmt{LineNumber}) return;");
                    CSharpSourceLines.Add($"{indent}AfterExfmt{LineNumber}:;");
                }
                else
                if (statement is SndusrmsgStatement sndusrmsgStatement)
                {
                    //SNDUSRMSG  MSG('Ｂ／Ｕ処理中の為、使用できません。実行ＫＥＹを押して下さい。 ')
                    CSharpSourceLines.Add($"{indent}pgmCaller.SndUsrInfMsg(\"{sndusrmsgStatement.Msg}\");{originalComment}");
                }
                else
                if (statement is IfBlockStatement ifBlockStatement)
                {
                    IfBlockStatementGenerateCSharpSourceLines(CSharpSourceLines, indent, parameters, variables, ifBlockStatement);
                }
                else
                if (statement is EndLine)
                {
                }
                else
                if (statement is CLLine)
                {
                    CSharpSourceLines.Add($"{indent}{((CLLine)statement).ToOriginalComment()}");
                }
                else
                {
                    throw new NotImplementedException();
                }
            });

        }

        void IfBlockStatementGenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, List<Variable> parameters, List<Variable> variables, IfBlockStatement IfBlockStatement)
        {
            var openerLine = (IfThenDoStatement)IfBlockStatement.openerLine;
            var condition = LogicalCondition.Of(openerLine.Cond.Replace("*AND", "AND").Replace("*OR", "OR").Replace("*GT", ">"), variables).Expression;
            var closerLine = IfBlockStatement.closerLine;
            CSharpSourceLines.Add($"{indent}if({condition}) {openerLine.ToOriginalComment()}");
            CSharpSourceLines.Add(indent + "{");
            GenerateCSharpSourceLines(CSharpSourceLines, indent.Increment(), parameters, variables, IfBlockStatement.Statements);
            CSharpSourceLines.Add(indent + "} " + ((Line)closerLine).ToOriginalComment());
        }
        static void ChgvarGenerateCSharpSourceLines(ChgvarStatement statement,Indent indent, List<string> CSharpSourceLines)
        {
            var VarName=statement.VarName;
            var settingValue = SettingValue(statement.ValueOfVar);
            
            var func = string.Empty;
            if (VarName.StartsWith("%SST"))
            {
                var values = VarName[5..^1].Split(" ");
                var varName = values[0].Substring(1).ToPublicModernName();
                var startIndex = int.Parse(values[1]) - 1;
                var length = int.Parse(values[2]);
                func = $"{varName} = {varName}.ReplacePart({startIndex}, {length}, {settingValue});";
            }
            else
            {
                var PublicModernName = string.Empty;
                if (VarName.StartsWith("&IN"))
                {
                    var index = VarName.Substring(3, 2).ToUpper();
                    PublicModernName = $"In{index}";
                }
                else
                {
                    PublicModernName = VarName.Substring(1).ToPublicModernName();
                }
                func = $"{PublicModernName} = {settingValue};";
            }

            CSharpSourceLines.Add($"{indent}{func}{statement.ToOriginalComment()}");
        }

        static string SettingValue(string ValueOfVar)
        {
            if (ValueOfVar.Count(c => c == '\'') == 2 && ValueOfVar.StartsWith("'") && ValueOfVar.EndsWith("'")) return $"\"{ValueOfVar[1..^1].Trim()}\"";

            var settingValue = new StringBuilder();

            var values = ValueOfVar.Split(" ");

            if (values.Length == 1 && ValueOfVar.StartsWith("&")) return ValueOfVar.Substring(1).ToPublicModernName();

            values.ToList().ForEach(value =>
            {
                if (value.StartsWith("&"))
                {
                    settingValue.Append(value.Substring(1).ToPublicModernName());
                }
                else if (value == "*TCAT")
                {
                    settingValue.Append(" + ");
                }
                else if (value.StartsWith("'") && value.EndsWith("'"))
                {
                    settingValue.Append($"\"{value[1..^1].Trim()}\"");
                }
                else if (value.StartsWith("X'") && value.EndsWith("'"))//TODO Ebcdic
                {
                    var hex = value.Substring(2, 2);
                    string stringValue = char.ConvertFromUtf32(Convert.ToInt32(hex, 16));
                    settingValue.Append($"\"{stringValue}\"");
                }
                else
                {
                    settingValue.Append(value);
                }

            });
            return settingValue.ToString();

        }

    }
}
