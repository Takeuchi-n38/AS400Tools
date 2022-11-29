using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.Sources.Items;
using Delta.Tools.Sources.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using Delta.Tools.AS400.Programs.RPGs.Forms.Outputs;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.CSharp.Statements.Items.Properties;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.AS400.DataTypes;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Inputs
{
    public class ProgramDescribedFileToCSharper
    {

        public static void GenerateOutputRowsByProgramDescribed(List<string> CSharpSourceLines,string RecordLength, string ContainerName, bool isRightJustified, OutputRowBlock outputRowBlock, List<Variable> variables)
        {

            var fieldLines = outputRowBlock.Statements.Where(l => l is IRPGOutputLine).Cast<IRPGOutputLine>();

            CSharpSourceLines.Add($"class {ContainerName}RecordFormatSpecification");
            CSharpSourceLines.Add($"{{");

            //var firstLine = fieldLines.First();
            //GeneratorFromPrtf.SkipAndSpace(CSharpSourceLines, firstLine.SkipBefore, firstLine.SkipAfter, firstLine.SpaceBefore, firstLine.SpaceAfter);

            fieldLines
                .Where(l => l.IsLineItemLine && !(l.Name.ToUpper().StartsWith("PAGE") || l.Name.ToUpper() == "DATE")).Select(l => GeneratorFromPrtf.StringVariable(l))
                .ToList().ForEach(variable => 
                CSharpSourceLines.Add($"{Indent.Single}internal {variable.TypeSpelling} {variable.Name} {{ get; set; }}"));
            ;
            CSharpSourceLines.Add($"{Indent.Single}internal RecordFormatSpecification RecordFormat()");
            CSharpSourceLines.Add($"{Indent.Single}{{");
            CSharpSourceLines.Add($"{Indent.Couple}var record = new RecordFormatSpecification({RecordLength});");
            fieldLines.ToList().ForEach(filed => {
                if (filed.IsLineItemLine)
                {
                    string value = filed.Name.ToPublicModernName();
                    var todoFormat = string.Empty;
                    if (value.StartsWith("Page"))
                    {
                        value = "line.Page.Number.ToString().PadLeft(4, ' ')";
                    }
                    else
                    if (value == "Udate" || value == "Date")
                    {
                        value = "Retriever.Instance.Job.Udate.ToString(\"yy/MM/dd\")";
                    }

                    var TypeLength=string.Empty;
                    var v =variables.Where(v=>v.Name == value).FirstOrDefault();
                    if (v != null&&v.OfTypeIsString)
                    {
                        TypeLength= $".PadRight({v.TypeLength}, ' ')";
                    }

                    CSharpSourceLines.Add($"{Indent.Couple}record.AddOn{(isRightJustified ? "Right" : "Left")}Justified({value}{TypeLength}, {filed.EndPositionInLine});{todoFormat}");
                }
                else
                if (filed.IsStaticValueLine)
                {
                    CSharpSourceLines.Add($"{Indent.Couple}record.AddOn{(isRightJustified ? "Right" : "Left")}Justified({filed.StaticValue.Replace('\'', '\"').TrimEnd()}, {filed.EndPositionInLine});");
                }
                else
                {
                    //throw new InvalidOperationException();
                }
            });

            CSharpSourceLines.Add($"{Indent.Couple}return record;");
            CSharpSourceLines.Add($"{Indent.Single}}}");
            CSharpSourceLines.Add($"}}");

            CSharpSourceLines.Add($"{ContainerName}RecordFormatSpecification {ContainerName}");
            CSharpSourceLines.Add($"{{");
            CSharpSourceLines.Add($"{Indent.Single}get");
            CSharpSourceLines.Add($"{Indent.Single}{{");
            CSharpSourceLines.Add($"{Indent.Couple}var record = new {ContainerName}RecordFormatSpecification();");

            fieldLines.Where(filed => filed.IsLineItemLine && !(filed.Name.ToUpper().StartsWith("PAGE")) && !(filed.Name.ToUpper() == "UDATE" || filed.Name.ToUpper() == "DATE")).ToList()
                .ForEach(lineItemLine =>
                {

                    var targetVariable = GeneratorFromPrtf.StringVariable(lineItemLine);

                    var sourceVariable = variables.Where(v => v.Name == targetVariable.Name).FirstOrDefault() ?? Variable.Of(TypeOfVariable.OfUnknown, targetVariable.Name);

                    var castSpellings = targetVariable.AddCastSpelling(sourceVariable);

                    CSharpSourceLines.Add($"{Indent.Couple}record.{targetVariable.Name} = {castSpellings.spelling};{castSpellings.comment}");
                });

            CSharpSourceLines.Add($"{Indent.Couple}return record;");
            CSharpSourceLines.Add($"{Indent.Single}}}");
            CSharpSourceLines.Add($"}}");

        }


        public static void GenerateInputCSharpSourceLines(ProgramDescribedInputFile programDescribedFile, List<string> CSharpSourceLines, List<Variable> variables)
        {
            var openerLine = programDescribedFile.openerLine;
            var Statements = programDescribedFile.Statements;

            var fileName = openerLine.FileName.ToCSharpOperand();

            CSharpSourceLines.Add($"{ openerLine.ToOriginalComment()}");
            Statements.Cast<ILine>().ToList().ForEach(line=>CSharpSourceLines.Add($"{ line.ToOriginalComment()}"));

            if(!programDescribedFile.Fields.Any(f => f.IsRefer))
            {
                CSharpSourceLines.Add($"{fileName} {fileName} {{ get; set;}}");
            }

            var fieldVariables = new List<Variable>();

            foreach(var f in programDescribedFile.Fields.Where(field=>field.FieldName!=string.Empty).ToList())
            {
                var referName = (f.IsRefer?f.ReferName: f.FieldName).ToCSharpOperand();
                var variable = f.IsRefer ? VariableFactory.Of(DataTypeDefinition.Of(f.FieldName, "1", string.Empty, string.Empty)): VariableFactory.Of(f.ToTypeDefinition);
                if(fieldVariables .Contains(variable)) continue;
                fieldVariables .Add(variable);
                CSharpSourceLines.Add($"{variable.TypeSpelling} {variable.Name}");
                CSharpSourceLines.Add($"{{");
                CSharpSourceLines.Add($"{Indent.Single}get {{ return {fileName}.{referName}; }}");
                CSharpSourceLines.Add($"{Indent.Single}set {{ {fileName}.{referName} = value; }}");
                CSharpSourceLines.Add($"}}");
            };

            //var fieldVariables = programDescribedFile.TypeDefinitions.Select(line => VariableFactory.Of(line)).Distinct().ToList();

            variables.AddRange(fieldVariables);
        }

        public static List<string> GenerateInputCSharpClass(string className,ProgramDescribedInputFile programDescribedFile, int MaxOfToLocation)
        {

            List<string> CSharpSourceLines = new List<string>();

            var variables = programDescribedFile.TypeDefinitions.Select(line => VariableFactory.Of(line)).ToList();

            CSharpSourceLines.Add($"{ programDescribedFile.openerLine.ToOriginalComment()}");

            programDescribedFile.IRPGInputLines.ToList()
                .ForEach(line => CSharpSourceLines.Add($"{line.ToOriginalComment()}"));

            CSharpSourceLines.Add($"readonly byte[] CCSID930Bytes = Enumerable.Repeat(CodePage290.ByteOfSpace, {MaxOfToLocation}).ToArray();");

            var vList = new List<Variable>();

            for (int i = 0; i < programDescribedFile.Fields.Count(); i++)
            {
                var inputLine = programDescribedFile.Fields.ElementAt(i);
                var typeDefinition = inputLine.ToTypeDefinition;
                if(typeDefinition.Name==string.Empty) continue;

                var v = VariableFactory.Of(typeDefinition);
                if(vList.Contains(v)) continue;
                vList.Add(v);
                var createSpelling = string.Empty;
                if (v.OfTypeIsString)
                {
                    CSharpSourceLines.Add($"public {v.TypeSpelling} {v.Name} {{ get=> CodePage930.ToStringFrom(CCSID930Bytes, {inputLine.FromLocationInIntType}, {inputLine.ToLocationInIntType}); set=> CodePage930.SetBytes(value, CCSID930Bytes, {inputLine.FromLocationInIntType}, {inputLine.ToLocationInIntType}); }}");
                }
                else
                if (v.OfTypeIsDecimal)
                {
                    CSharpSourceLines.Add($"public {v.TypeSpelling} {v.Name} {{ get => ZonedDecimal.ToDecimalFrom(CCSID930Bytes, {inputLine.FromLocationInIntType}, {inputLine.ToLocationInIntType}, {inputLine.DecimalPositionsInIntType}); set=> ZonedDecimal.SetUnsignedBytes(value, CCSID930Bytes, {inputLine.FromLocationInIntType}, {inputLine.ToLocationInIntType}, {inputLine.DecimalPositionsInIntType}); }}");
                }
                else
                if (v.OfTypeIsNumeric)
                {
                    if (typeDefinition.IsPackedDecimal)
                    {
                        CSharpSourceLines.Add($"public {v.TypeSpelling} {v.Name} {{ get => ({v.TypeSpelling})PackedDecimal.ToDecimalFrom(CCSID930Bytes, {inputLine.FromLocationInIntType}, {inputLine.ToLocationInIntType}, {inputLine.DecimalPositionsInIntType}); set => PackedDecimal.SetUnsignedBytes(value, CCSID930Bytes, {inputLine.FromLocationInIntType}, {inputLine.ToLocationInIntType}, {inputLine.DecimalPositionsInIntType}); }}");
                    }
                    else
                    {
                        CSharpSourceLines.Add($"public {v.TypeSpelling} {v.Name} {{ get => ({v.TypeSpelling})ZonedDecimal.ToDecimalFrom(CCSID930Bytes, {inputLine.FromLocationInIntType}, {inputLine.ToLocationInIntType}, {inputLine.DecimalPositionsInIntType}); set => ZonedDecimal.SetUnsignedBytes(value, CCSID930Bytes, {inputLine.FromLocationInIntType}, {inputLine.ToLocationInIntType}, {inputLine.DecimalPositionsInIntType}); }}");
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }

            }

            CSharpSourceLines.Add($"public {className}(byte[] aCCSID930Bytes)");
            CSharpSourceLines.Add("{");
            CSharpSourceLines.Add($"{Indent.Single}this.CCSID930Bytes = aCCSID930Bytes;");
            CSharpSourceLines.Add("}");

            return CSharpSourceLines;
        }

    }
}
