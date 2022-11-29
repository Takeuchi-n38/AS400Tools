using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.DiskFiles.LFs;
using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.CSharp.Statements.Items.Properties;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using Delta.Tools.Sources.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.FileDescriptions
{
    class DiskFileDescriptionLineToCSharper
    {
        DiskFileStructureBuilder DiskFileStructureFactory;

        public DiskFileDescriptionLineToCSharper(DiskFileStructureBuilder diskFileStructureFactory)
        {
            DiskFileStructureFactory = diskFileStructureFactory;
        }

        public void GenerateCSharpSourceLines(DiskFileDescriptionLine DiskFileDescriptionLine, StringBuilder curText, List<Variable> outputtedVariables)
        {
            var FileName = DiskFileDescriptionLine.FileName.ToCSharpOperand();
            var FileType = DiskFileDescriptionLine.FileType;
            var isA = DiskFileDescriptionLine.IsA;

            curText.AppendLine($"#region DiskFileDescriptionLine {FileName}");

            curText.AppendLine(DiskFileDescriptionLine.ToOriginalComment());
            curText.AppendLine($"{RepositoryTypeAndName(DiskFileDescriptionLine)};");

            if (DiskFileDescriptionLine.IsExternalFileFormat)
            {
                var DiskFileDescriptionLineFile = DiskFileDescriptionLine.File;// DiskFileStructureFactory.Instance.Create(DiskFileDescriptionLine.FileObjectID);
                if (DiskFileDescriptionLineFile is PFStructure pf) pfOutput(pf, curText, DiskFileDescriptionLine.Prefix, DiskFileDescriptionLine.RecordFormatReName, FileType, isA, outputtedVariables);
                if (DiskFileDescriptionLineFile is LFStructure lf) lfOutput(lf, curText, DiskFileDescriptionLine.Prefix, DiskFileDescriptionLine.RecordFormatReName, FileType, isA, outputtedVariables);
            }

            curText.AppendLine($"#endregion DiskFileDescriptionLine {FileName}");
        }

        public static string RepositoryTypeAndName(DiskFileDescriptionLine diskFileDescriptionLine)
        {
            string parameterType = string.Empty;
            if (!diskFileDescriptionLine.IsExternalFileFormat && diskFileDescriptionLine.FileType == "O")
            {
                parameterType = $"IRecordFormatRepository";
            }
            else
            if (!diskFileDescriptionLine.IsExternalFileFormat && diskFileDescriptionLine.FileType == "I")
            {
                parameterType = $"IEnumerable<{diskFileDescriptionLine.FileObjectID.Name.ToPublicModernName()}>";
            }
            else
            {
                parameterType = $"I{diskFileDescriptionLine.FileObjectID.Name.ToPublicModernName()}Repository";
            }
            var parameterName = $"{diskFileDescriptionLine.FileObjectID.Name.ToLower()}Repository";
            return $"{parameterType} {parameterName}";
        }

        void pfOutput(PFStructure pf, StringBuilder text, string Prefix, string RecordFormatReName, string fileType,bool isA, List<Variable> outputtedVariables)
        {
            string fileName = pf.OriginalSource.ObjectID.Name.ToPublicModernName();
            string entityName = fileName;
            string repositoryName = $"{entityName.ToLower()}Repository";
            var recordFormatName = (RecordFormatReName == string.Empty ? ((IDDSLine)pf.RecordFormatHeaderLine).Name : RecordFormatReName).ToCSharpOperand();

            if (fileType == "U" || fileType == "O" || (isA))
            {
                WriteLineGenerateMethod(text, entityName, repositoryName);
            }
            if (fileType == "U")
            {
                UpdateLineGenerateMethod(text, entityName, repositoryName);
                DeleteLineGenerateMethod(text, entityName, repositoryName);
            }

            var TypeDefinitions = DiskFileStructureFactory.TypeDefinitionList(pf);

            if (fileType == "I" || fileType == "U")
            {

                var KeyTuppleSpels = ScalarVariableListFactory.OfKey(pf.KeysAndFields, TypeDefinitions).TuppleSpellings();

                text.AppendLine($"{KeyTuppleSpels.Last()} {entityName}Key {{ get; set; }}");
                text.AppendLine($"bool {entityName}isSetLLnotGT {{ get; set; }} = true;");

                ReadLineGenerateMethod(text, "Read", entityName, entityName, repositoryName, "Read");
                ReadLineGenerateMethod(text, "Readn", entityName, entityName, repositoryName, "Read");
                ReadLineGenerateMethod(text, "Readp", entityName, entityName, repositoryName, "ReadPrior");
                ReadLineGenerateMethod(text, "Readpn", entityName, entityName, repositoryName, "ReadPrior");

                ReadeLineGenerateMethod(text, "Reade", entityName, entityName, repositoryName, "ReadEqual", KeyTuppleSpels);
                ReadeLineGenerateMethod(text, "Readen", entityName, entityName, repositoryName, "ReadEqual", KeyTuppleSpels);
                ReadeLineGenerateMethod(text, "Readpe", entityName, entityName, repositoryName, "ReadPriorEqual", KeyTuppleSpels);
                ReadeLineGenerateMethod(text, "Readpen", entityName, entityName, repositoryName, "ReadPriorEqual", KeyTuppleSpels);

                ChainLineGenerateMethod(text, entityName, KeyTuppleSpels);

                SetllLineGenerate(text, entityName, repositoryName, KeyTuppleSpels);
                SetgtLineGenerate(text, entityName, repositoryName, KeyTuppleSpels);

            }

            text.AppendLine($"{entityName} {entityName}");
            text.AppendLine($"{{");
            text.AppendLine($"{Indent.Single}get");
            text.AppendLine($"{Indent.Single}{{");
            text.AppendLine($"{Indent.Couple}return {recordFormatName};");
            text.AppendLine($"{Indent.Single}}}");
            //if (fileType != "O")
            //{
                text.AppendLine($"{Indent.Single}set");
                text.AppendLine($"{Indent.Single}{{");
                text.AppendLine($"{Indent.Couple}{recordFormatName} = value;");
                text.AppendLine($"{Indent.Single}}}");
            //}
            text.AppendLine($"}}");

            var entityPointerName = recordFormatName.ToLower();
            text.AppendLine($"{entityName} {entityPointerName} = null; ");
            text.AppendLine($"{entityName} {recordFormatName}");
            text.AppendLine($"{{");
            text.AppendLine($"{Indent.Single}get");
            text.AppendLine($"{Indent.Single}{{");

            var Variables = VariableFactory.Of(TypeDefinitions).ToList();

            Variables.ForEach(targetVariable =>
            {
                var fieldName = targetVariable.Name;
                var PFieldName = $"{Prefix}{fieldName}".ToPublicModernName();
                var sourceVariable = outputtedVariables.Where(v => v.Name == PFieldName).FirstOrDefault() ?? Variable.Of(targetVariable.TypeOfVariable, PFieldName);

                var castSpellings = targetVariable.AddCastSpelling(sourceVariable);

                text.AppendLine($"{Indent.Couple}{entityPointerName}.{targetVariable.Name} = {castSpellings.spelling}; {castSpellings.comment}");
                //text.AppendLine($"{Indent.Couple}entity.{targetVariable.Name} = {castSpellings.spelling};");

            }
            );

            text.AppendLine($"{Indent.Couple}return {entityPointerName};");
            text.AppendLine($"{Indent.Single}}}");

            //if (fileType != "O")
            //{
            text.AppendLine($"{Indent.Single}set");
            text.AppendLine($"{Indent.Single}{{");
            text.AppendLine($"{Indent.Couple}{entityPointerName} = value;");
            Variables.ForEach(sourceVariable =>
                {
                    var fieldName = sourceVariable.Name;
                    var PFieldName = $"{Prefix}{fieldName}".ToPublicModernName();
                    var targetVariable = outputtedVariables.Where(v => v.Name == PFieldName).FirstOrDefault() ?? Variable.Of(sourceVariable.TypeOfVariable, PFieldName);
                    var castSpellings = targetVariable.AddCastSpelling(sourceVariable.Of("value." + sourceVariable.Name));
                    text.AppendLine($"{Indent.Couple}{PFieldName} = {castSpellings.spelling}; {castSpellings.comment}");
                    //text.AppendLine($"{Indent.Couple}{PFieldName} = value.{fieldName};");
                }
                );
                text.AppendLine($"{Indent.Single}}}");
            //}

            text.AppendLine($"}}");

            Variables.ForEach(f =>
            {
                var v = f.Of($"{Prefix}{f.Name}".ToPublicModernName());
                if (!outputtedVariables.Any(o => o.Name == v.Name))
                {
                    text.AppendLine(PropertyItem.Of(v).ToAutoImplementedPropertiesStringWithIntialValue());
                    outputtedVariables.Add(v);
                }
            }
            );

        }

        void lfOutput(LFStructure lf, StringBuilder text, string Prefix, string RecordFormatReName, string fileType, bool isA, List<Variable> outputtedVariables)
        {
            string fileName = lf.OriginalSource.ObjectID.Name.ToCSharpOperand();
            string entityName = lf.FileDifintion.OriginalSource.ObjectID.Name.ToCSharpOperand();
            string repositoryName = $"{fileName.ToLower()}Repository";
            var recordFormatName = (RecordFormatReName == string.Empty ? fileName : RecordFormatReName).ToCSharpOperand();

            if (fileType == "U")
            {
                if (isA)
                {
                    WriteLineGenerateMethod(text, entityName, repositoryName);
                }
                UpdateLineGenerateMethod(text, entityName, repositoryName);
                DeleteLineGenerateMethod(text, entityName, repositoryName);
            }

            var TypeDefinitions = DiskFileStructureFactory.TypeDefinitionList(lf);

            var KeyTuppleSpels = ScalarVariableListFactory.OfKey(lf.KeysAndFields, TypeDefinitions).TuppleSpellings();

            text.AppendLine($"{KeyTuppleSpels.Last()} {recordFormatName}Key {{ get; set; }}");

            text.AppendLine($"bool {recordFormatName}isSetLLnotGT {{ get; set; }} = true;");

            ReadLineGenerateMethod(text, "Read", entityName, recordFormatName, repositoryName, "Read");
            ReadLineGenerateMethod(text, "Readn", entityName, recordFormatName, repositoryName, "Read");
            ReadLineGenerateMethod(text, "Readp", entityName, recordFormatName, repositoryName, "ReadPrior");
            ReadLineGenerateMethod(text, "Readpn", entityName, recordFormatName, repositoryName, "ReadPrior");

            ReadeLineGenerateMethod(text, "Reade", entityName, recordFormatName, repositoryName, "ReadEqual", KeyTuppleSpels);
            ReadeLineGenerateMethod(text, "Readen", entityName, recordFormatName, repositoryName, "ReadEqual", KeyTuppleSpels);
            ReadeLineGenerateMethod(text, "Readpe", entityName, recordFormatName, repositoryName, "ReadPriorEqual", KeyTuppleSpels);
            ReadeLineGenerateMethod(text, "Readpen", entityName, recordFormatName, repositoryName, "ReadPriorEqual", KeyTuppleSpels);

            ChainLineGenerateMethod(text, recordFormatName, KeyTuppleSpels);

            SetllLineGenerate(text, recordFormatName, repositoryName, KeyTuppleSpels);
            SetgtLineGenerate(text, recordFormatName, repositoryName, KeyTuppleSpels);

            var entityPointerName= recordFormatName.ToLower();
            text.AppendLine($"{entityName} {entityPointerName} = null; ");
            text.AppendLine($"{entityName} {recordFormatName}");
            text.AppendLine($"{{");
            text.AppendLine($"{Indent.Single}get");
            text.AppendLine($"{Indent.Single}{{");
            //text.AppendLine($"{Indent.Couple}var entity = new {entityName}();");

            var Variables = VariableFactory.Of(TypeDefinitions).ToList();

            Variables.ForEach(targetVariable =>
            {
                var fieldName = targetVariable.Name;

                var PFieldName = $"{Prefix}{fieldName}".ToPublicModernName();

                var sourceVariable = outputtedVariables.Where(v => v.Name == PFieldName).FirstOrDefault() ?? Variable.Of(targetVariable.TypeOfVariable, PFieldName);

                var castSpellings = targetVariable.AddCastSpelling(sourceVariable);

                text.AppendLine($"{Indent.Couple}{entityPointerName}.{targetVariable.Name} = {castSpellings.spelling}; {castSpellings.comment}");
                //text.AppendLine($"{Indent.Couple}entity.{targetVariable.Name} = {castSpellings.spelling};");

            }
            );

            text.AppendLine($"{Indent.Couple}return {entityPointerName};");
            text.AppendLine($"{Indent.Single}}}");
            text.AppendLine($"{Indent.Single}set");
            text.AppendLine($"{Indent.Single}{{");
            text.AppendLine($"{Indent.Couple}{entityPointerName} = value;");

            Variables.ForEach(sourceVariable =>
            {
                var fieldName = sourceVariable.Name;
                var PFieldName = $"{Prefix}{fieldName}".ToPublicModernName();
                var targetVariable = outputtedVariables.Where(v => v.Name == PFieldName).FirstOrDefault() ?? Variable.Of(sourceVariable.TypeOfVariable, PFieldName);
                var castSpellings = targetVariable.AddCastSpelling(sourceVariable.Of("value." + sourceVariable.Name));
                text.AppendLine($"{Indent.Couple}{PFieldName} = {castSpellings.spelling}; {castSpellings.comment}");
                //text.AppendLine($"{Indent.Couple}{PFieldName} = value.{fieldName};");
            }
            );

            text.AppendLine($"{Indent.Single}}}");
            text.AppendLine($"}}");

            Variables.ForEach(f =>
            {
                var v = f.Of($"{Prefix}{f.Name}".ToPublicModernName());
                if (!outputtedVariables.Any(o => o.Name == v.Name))
                {
                    text.AppendLine(PropertyItem.Of(v).ToAutoImplementedPropertiesString());
                    outputtedVariables.Add(v);
                }
            }
            );

        }

        static void UpdateLineGenerateMethod(StringBuilder text, string EntityName, string RepositoryName)
        {
            text.AppendLine($"void Update({EntityName} row)");
            text.AppendLine("{");
            text.AppendLine($"{Indent.Single}{RepositoryName}.Update(row);");
            text.AppendLine("}");
        }
        static void DeleteLineGenerateMethod(StringBuilder text, string EntityName, string RepositoryName)
        {
            text.AppendLine($"void Delete({EntityName} row)");
            text.AppendLine("{");
            text.AppendLine($"{Indent.Single}{RepositoryName}.Delete(row);");
            text.AppendLine("}");
        }
        static void WriteLineGenerateMethod(StringBuilder text, string EntityName, string RepositoryName)
        {
            text.AppendLine($"void Write({EntityName} row)");
            text.AppendLine("{");
            text.AppendLine($"{Indent.Single}{RepositoryName}.Insert(row);");
            text.AppendLine("}");
        }
        static void ChainLineGenerateMethod(StringBuilder text, string RecordFormatName, List<string> KeyTupples)
        {
            //                In[90] = Chain(Ix, Pnl010); //0322      C     IX            CHAIN     PNL010                             90    

            KeyTupples.ForEach(KeyTupple => {
                text.AppendLine($"bool Chain{RecordFormatName}({KeyTupple} key)");
                text.AppendLine($"{{");
                text.AppendLine($"{Indent.Single}var foundRecord = Reade{RecordFormatName}(key).FirstOrDefault();");
                text.AppendLine($"{Indent.Single}if (foundRecord == null)");
                text.AppendLine($"{Indent.Single}{{");
                text.AppendLine($"{Indent.Couple}return false;");
                text.AppendLine($"{Indent.Single}}}");
                text.AppendLine($"{Indent.Single}{RecordFormatName} = foundRecord;");
                text.AppendLine($"{Indent.Single}return true;");
                text.AppendLine($"}}");
            });

        }

        static void SetllLineGenerate(StringBuilder text, string RecordFormatName, string RepositoryName, List<string> KeyTupples)
        {
            text.AppendLine($"void Setll{RecordFormatName}()");
            text.AppendLine("{");
            text.AppendLine($"{Indent.Single}{RecordFormatName}isSetLLnotGT = true;");
            text.AppendLine($"{Indent.Single}{RecordFormatName}Key = {RepositoryName}.LowLimit();");
            text.AppendLine("}");

            KeyTupples.ForEach(KeyTupple => {
                text.AppendLine($"void Setll{RecordFormatName}({KeyTupple} limitKey)");
                text.AppendLine("{");
                text.AppendLine($"{Indent.Single}{RecordFormatName}isSetLLnotGT = true;");
                text.AppendLine($"{Indent.Single}{RecordFormatName}Key = {RepositoryName}.LowLimit(limitKey);");
                text.AppendLine("}");
            });

        }

        static void SetgtLineGenerate(StringBuilder text, string RecordFormatName, string RepositoryName, List<string> KeyTupples)
        {
            text.AppendLine($"void Setgt{RecordFormatName}()");
            text.AppendLine("{");
            text.AppendLine($"{Indent.Single}{RecordFormatName}isSetLLnotGT = false;");
            text.AppendLine($"{Indent.Single}{RecordFormatName}Key = {RepositoryName}.HighLimit();");
            text.AppendLine("}");

            KeyTupples.ForEach(KeyTupple => {
                text.AppendLine($"void Setgt{RecordFormatName}({KeyTupple} limitKey)");
                text.AppendLine("{");
                text.AppendLine($"{Indent.Single}{RecordFormatName}isSetLLnotGT = false;");
                text.AppendLine($"{Indent.Single}{RecordFormatName}Key = {RepositoryName}.HighLimit(limitKey);");
                text.AppendLine("}");
            });

        }

        static void ReadLineGenerateMethod(StringBuilder text, string functionName, string EntityName, string RecordFormatName, string RepositoryName, string RepositoryMethodName)
        {
            text.AppendLine($"List<{EntityName}> {functionName}{RecordFormatName}()");
            text.AppendLine("{");
            text.AppendLine($"{Indent.Single}return {RepositoryName}.{RepositoryMethodName}({RecordFormatName}isSetLLnotGT, {RecordFormatName}Key);");
            text.AppendLine("}");
        }
        static void ReadeLineGenerateMethod(StringBuilder text, string functionName, string EntityName, string RecordFormatName, string RepositoryName, string RepositoryMethodName, List<string> KeyTupples)
        {
            KeyTupples.ForEach(KeyTupple =>
            {
                text.AppendLine($"List<{EntityName}> {functionName}{RecordFormatName}({KeyTupple} key)");
                text.AppendLine("{");
                text.AppendLine($"{Indent.Single}return {RepositoryName}.{RepositoryMethodName}({RecordFormatName}isSetLLnotGT, {RecordFormatName}Key, key);");
                text.AppendLine("}");
            });
        }



    }
}
