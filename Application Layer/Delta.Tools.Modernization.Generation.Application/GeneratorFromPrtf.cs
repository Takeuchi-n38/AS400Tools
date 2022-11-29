using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.PrinterFiles;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.AS400.Programs.RPGs.Forms.Outputs;
using Delta.Tools.CSharp.Statements.Items.Properties;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Generator
{
    public class GeneratorFromPrtf
    {
        DiskFileStructureBuilder DiskFileStructureFactory;

        public GeneratorFromPrtf(DiskFileStructureBuilder DiskFileStructureFactory)
        {
            this.DiskFileStructureFactory= DiskFileStructureFactory;
        }

        public void GenerateOutputRowsByProgramDescribed(List<string> CSharpSourceLines, string ContainerName, int LineNumber,bool isRightJustified, OutputRowBlock outputRowBlock, List<Variable> variables)
        {
            var fieldLines = outputRowBlock.Statements.Where(l => l is IRPGOutputLine).Cast<IRPGOutputLine>();

            CSharpSourceLines.Add($"class {ContainerName}_{LineNumber}OutputRow : OutputRow");
            CSharpSourceLines.Add($"{{");

            var firstLine= fieldLines.First();
            SkipAndSpace(CSharpSourceLines, firstLine.SkipBefore, firstLine.SkipAfter, firstLine.SpaceBefore, firstLine.SpaceAfter);

            fieldLines
                .Where(l => l.IsLineItemLine && !(l.Name.ToUpper().StartsWith("PAGE") || l.Name.ToUpper() == "UDATE" || l.Name.ToUpper() == "DATE")).Select(l => StringVariable(l))
                .ToList().ForEach(variable => CSharpSourceLines.Add($"{Indent.Single}internal {variable.TypeSpelling} {ToPropNameFrom(variable.Name)} {{ get; set; }}"));

            CSharpSourceLines.Add($"{Indent.Single}public override void AddReportItem(ReportLine record)");
            CSharpSourceLines.Add($"{Indent.Single}{{");

            fieldLines.ToList().ForEach(filed => {
                if (filed.IsLineItemLine)
                {
                    string value = filed.Name.ToPublicModernName();
                    var todoFormat = string.Empty;
                    if (value.StartsWith("Page"))
                    {
                        value = "record.Page.Number.ToString().PadLeft(4, ' ')";
                        CSharpSourceLines.Add($"{Indent.Couple}record.AddOn{(isRightJustified ? "Right" : "Left")}Justified({value}, {filed.EndPositionInLine});{todoFormat}");

                    }
                    else
                    if (value == "Udate" || value == "Date")
                    {
                        value = "Retriever.Instance.Job.Udate.ToString(\"yy/MM/dd\")";
                        CSharpSourceLines.Add($"{Indent.Couple}record.AddOn{(isRightJustified ? "Right" : "Left")}Justified({value}, {filed.EndPositionInLine});{todoFormat}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"{Indent.Couple}record.AddOn{(isRightJustified ? "Right" : "Left")}Justified({value.Replace(',', '_')}, {filed.EndPositionInLine});{todoFormat}");
                    }
                }
                else
                if (filed.IsStaticValueLine)
                {
                    CSharpSourceLines.Add($"{Indent.Couple}record.AddOn{(isRightJustified ? "Right" : "Left")}Justified({filed.StaticValue.Replace('\'','\"').TrimEnd()}, {filed.EndPositionInLine});");
                }
                else
                {
                    //throw new InvalidOperationException();
                }
            });

            CSharpSourceLines.Add($"{Indent.Single}}}");

            CSharpSourceLines.Add($"}}");


            CSharpSourceLines.Add($"{ContainerName}_{LineNumber}OutputRow {ContainerName}_{LineNumber}");
            CSharpSourceLines.Add($"{{");
            CSharpSourceLines.Add($"{Indent.Single}get");
            CSharpSourceLines.Add($"{Indent.Single}{{");
            CSharpSourceLines.Add($"{Indent.Couple}var row = new {ContainerName}_{LineNumber}OutputRow();");

            fieldLines.Where(filed =>filed.IsLineItemLine && !(filed.Name.ToUpper().StartsWith("PAGE")) && !(filed.Name.ToUpper() == "UDATE" || filed.Name.ToUpper() == "DATE")).ToList()
                .ForEach(lineItemLine => {

                    var targetVariable = StringVariable(lineItemLine);

                    var sourceVariable = variables.Where(v=>v.Name== targetVariable.Name).FirstOrDefault() ?? Variable.Of(TypeOfVariable.OfUnknown, targetVariable.Name);

                    var castSpellings = targetVariable.AddCastSpelling(sourceVariable);

                    CSharpSourceLines.Add($"{Indent.Couple}row.{ToPropNameFrom(targetVariable.Name)} = {castSpellings.spelling};{castSpellings.comment}");
            });

            CSharpSourceLines.Add($"{Indent.Couple}return row;");
            CSharpSourceLines.Add($"{Indent.Single}}}");
            CSharpSourceLines.Add($"}}");

        }

        static string ToPropNameFrom(string source)
        {
            if(!source.Contains("[")) return source;
            return source.Replace("[","_").Replace("]",string.Empty);
        }

        public void GenerateOutputRowsByExternallyDescribed(List<string> CSharpSourceLines, string ContainerName, int LineNumber, bool isRightJustified, PrintLine OutputLineItemLines, List<Variable> variables)
        {

            CSharpSourceLines.Add($"class {ContainerName}_{LineNumber}OutputRow : OutputRow");
            CSharpSourceLines.Add($"{{");

            SkipAndSpace(CSharpSourceLines,OutputLineItemLines.SkipBefore,OutputLineItemLines.SkipAfter,OutputLineItemLines.SpaceBefore,OutputLineItemLines.SpaceAfter);

            var TypeDefinitions = DiskFileStructureFactory.TypeDefinitionList(OutputLineItemLines.IsVariables.ToList(), null);
            var OutputLineItemLinesVariables = VariableFactory.Of(TypeDefinitions);

            OutputLineItemLinesVariables.Where(v => !(v.Name.StartsWith("Page") || v.Name == "Date")).ToList().ForEach(variable => CSharpSourceLines.Add($"{Indent.Single}{PropertyItem.OfInternal(variable).ToAutoImplementedPropertiesString()}"));

            CSharpSourceLines.Add($"{Indent.Single}public override void AddReportItem(ReportLine record)");
            CSharpSourceLines.Add($"{Indent.Single}{{");

            OutputLineItemLines.Outputs.ToList().ForEach(record => {
                if(record is RecordFormatRefFieldLine || record is RecordFormatFieldLine)
                {
                    
                    var variable = VariableFactory.Of(DiskFileStructureFactory.CreateTypeDefinition(record, null));
                    //var variable = VariableFactory.Of((ITypeDefinition)record);
                    string value = variable.Name;
                    var format = string.Empty;
                    var todoFormat = string.Empty;
                    if (variable.OfTypeIsString)
                    {

                    }
                    else
                    if (variable.OfTypeIsUnknown)
                    {
                        todoFormat = $"//TODO:Format Unknown";
                    }
                    else
                    {
                        format = ".ToString()";
                        value = $"{variable.Name}{format}";
                        todoFormat = $"//TODO:Format {variable.TypeSpelling}";
                    }
                    CSharpSourceLines.Add($"{Indent.Couple}record.AddOn{(isRightJustified ? "Right" : "Left")}Justified({value}, {record.Position});{todoFormat}");
                }
                else
                if(record.HaveKeywords)
                {
                    string value=string.Empty;
                    if (record.Keywords.StartsWith("PAGNBR"))
                    {
                        value = "record.Page.Number.ToString().PadLeft(4, ' ')";
                    }
                    else
                    if (record.Keywords == "DATE")
                    {
                        value = "Retriever.Instance.Job.Udate.ToString(\"yy/MM/dd\")";
                    }
                    else
                    {
                        value = record.Keywords.Replace('\'','\"').TrimEnd();
                        if(!value.StartsWith("\"")) value = $"\"{value}";
                        if (!value.EndsWith("\"")) value = $"{value}\"";
                    }
                    CSharpSourceLines.Add($"{Indent.Couple}record.AddOn{(isRightJustified ? "Right" : "Left")}Justified({value}, {record.Position});");
                }
                else
                {
                    throw new NotImplementedException();
                }
            });

            CSharpSourceLines.Add($"{Indent.Single}}}");

            CSharpSourceLines.Add($"}}");


            CSharpSourceLines.Add($"{ContainerName}_{LineNumber}OutputRow {ContainerName}_{LineNumber}");
            CSharpSourceLines.Add($"{{");
            CSharpSourceLines.Add($"{Indent.Single}get");
            CSharpSourceLines.Add($"{Indent.Single}{{");
            CSharpSourceLines.Add($"{Indent.Couple}var row = new {ContainerName}_{LineNumber}OutputRow();");

            OutputLineItemLinesVariables.Where(v => !(v.Name.StartsWith("Page")) && !(v.Name == "Udate" || v.Name == "Date")).ToList()
                .ForEach(target => {

                    var source = variables.Where(v => v.Name == target.Name).FirstOrDefault() ?? Variable.Of(TypeOfVariable.OfUnknown, target.Name);

                    var castSpellings = target.AddCastSpelling(source);

                    CSharpSourceLines.Add($"{Indent.Couple}row.{target.Name} = {castSpellings.spelling};{castSpellings.comment}");
                });

            CSharpSourceLines.Add($"{Indent.Couple}return row;");
            CSharpSourceLines.Add($"{Indent.Single}}}");
            CSharpSourceLines.Add($"}}");

        }

        public void Output(List<string> text, string containerName, int itemsCount)
        {

            var parameters = string.Join(',', Enumerable.Range(1, itemsCount).Select(i => $"{containerName}_{i}").ToArray());

            text.Add($"{containerName}OutputRowsContainer {containerName} => new {containerName}OutputRowsContainer({parameters});");

            text.Add($"class {containerName}OutputRowsContainer : OutputRowsContainer");
            text.Add("{");

            var rows = Enumerable.Range(1, itemsCount).Select(i => $"{containerName}_{i}OutputRow row{i}").ToList();
            rows.ForEach(row => text.Add($"{Indent.Single}{row};"));
            var rowsParamaeter = string.Join(',', rows);
            text.Add($"{Indent.Single}public {containerName}OutputRowsContainer({rowsParamaeter})");
            text.Add($"{Indent.Single}{{");
            Enumerable.Range(1, itemsCount).Select(i => $"this.row{i} = row{i};").ToList().ForEach(row =>
                text.Add($"{Indent.Couple}{row}")
            );
            text.Add($"{Indent.Single}}}");

            text.Add($"{Indent.Single}public override void AddOutputRows(List<OutputRow> outputRows)");
            text.Add($"{Indent.Single}{{");
            Enumerable.Range(1, itemsCount).Select(i => $"outputRows.Add(row{i});").ToList().ForEach(row =>
                text.Add($"{Indent.Couple}{row}")
            );
            text.Add($"{Indent.Single}}}");

            text.Add("}");

        }

        internal static void SkipAndSpace(List<string> CSharpSourceLines, int SkipBefore, int SkipAfter, int SpaceBefore, int SpaceAfter)
        {
            if (SkipBefore > 0)
            {
                CSharpSourceLines.Add($"{Indent.Single}public override int PositionOfLineToBePrintedBeforePrinting()");
                CSharpSourceLines.Add($"{Indent.Single}{{");
                CSharpSourceLines.Add($"{Indent.Couple}return {SkipBefore};");
                CSharpSourceLines.Add($"{Indent.Single}}}");
            }
            if (SkipAfter > 0)
            {
                CSharpSourceLines.Add($"{Indent.Single}public override int PositionOfLineToBePrintedAfterPrinting()");
                CSharpSourceLines.Add($"{Indent.Single}{{");
                CSharpSourceLines.Add($"{Indent.Couple}return {SkipAfter};");
                CSharpSourceLines.Add($"{Indent.Single}}}");
            }

            if (SpaceBefore > 0)
            {
                CSharpSourceLines.Add($"{Indent.Single}public override int NumberOfLineBreaksBeforePrinting()");
                CSharpSourceLines.Add($"{Indent.Single}{{");
                CSharpSourceLines.Add($"{Indent.Couple}return {SpaceBefore};");
                CSharpSourceLines.Add($"{Indent.Single}}}");
            }
            if (SpaceAfter > 0)
            {
                CSharpSourceLines.Add($"{Indent.Single}public override int NumberOfLineBreaksAfterPrinting()");
                CSharpSourceLines.Add($"{Indent.Single}{{");
                CSharpSourceLines.Add($"{Indent.Couple}return {SpaceAfter};");
                CSharpSourceLines.Add($"{Indent.Single}}}");
            }
        }

        internal static Variable StringVariable(IRPGOutputLine outputLine) {

            if (outputLine.Name.Contains(','))
            {
                var splitted = outputLine.Name.Split(',');
                var arrayName = splitted[0].ToCSharpOperand();
                var arrayIndex = splitted[1].ToCSharpOperand();
                var name = $"{arrayName}[{arrayIndex}]";
                var length = (outputLine.EndPositionInLine==string.Empty)?1:int.Parse(outputLine.EndPositionInLine);
                return Variable.Of(TypeOfVariable.OfString(length), name);//長さは不明 EndPosition以下ではない
            }
            else
            {
                return Variable.Of(TypeOfVariable.OfString(outputLine.EndPositionInLine), outputLine.Name.ToPublicModernName().Replace(',', '_'));//長さは不明 EndPosition以下ではない
            }
        }

    }
}
