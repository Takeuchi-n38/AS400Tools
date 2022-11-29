using Delta.AS400.DataTypes;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Delta.Tools.AS400.Generator.DomainLayer
{
    public class EntityTestHelperGenerator
    {

        public static string CreateContents(
            bool isDb2,
            ObjectID objectID,
            bool isUnique,
            IEnumerable<IDataTypeDefinition> ITypeDefinitions,
            ScalarVariableList keyList)
        {
            var fields = VariableFactory.Of(ITypeDefinitions);
            var ClassName = $"{objectID.Name.ToPascalCase()}TestHelper";
            var EntityName = objectID.Name.ToPublicModernName();

            var contents = new StringBuilder();

            contents.AppendLine(NamespaceItemFactory.DeltaAS400DataTypesCharacters.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaAS400DataTypesNumerics.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaAS400Objects.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectID.Library.Partition).ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectID).ToUsingLine);
            if (!isDb2) contents.AppendLine(NamespaceItemFactory.DeltaEntities.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemCollectionsGeneric.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.System.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemLinq.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaToolsModernizationTest.ToUsingLine);

            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectID).ToNamespaceLine);

            contents.AppendLine("{");

            var baseClassName= isDb2? "Db2foriEntityTestHelper" : "EntityTestHelper";//EFEntityTestHelper<TypeOfEFEntity>

            contents.AppendLine($"{Indent.Single}public class {ClassName} : {(isDb2 ? "Db2foriEntityTestHelper" : $"EFEntityTestHelper<{EntityName}>")}");

            contents.AppendLine($"{Indent.Single}{{");

            contents.AppendLine(string.Empty);
            contents.AppendLine($"{Indent.Couple}public static ObjectID ObjectIDOfEntity => new Library(Partition.{objectID.Library.Partition.Name.ToPublicModernName()}, \"{objectID.Library.Name.ToPublicModernName()}\").ObjectIDOf(\"{objectID.Name}\");");

            contents.AppendLine($"{Indent.Couple}{ClassName}():base(ObjectIDOfEntity)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Couple}}}");

            contents.AppendLine(string.Empty);

            contents.AppendLine($"{Indent.Couple}public static {ClassName} Of => new {ClassName}();");

            contents.AppendLine(string.Empty);

            if (isDb2)
            {
                contents.AppendLine($"{Indent.Couple}public override IEnumerable<string> ColumnNames()");
                contents.AppendLine($"{Indent.Couple}{{");

                ITypeDefinitions.ToList().ForEach(ddsTypeDef =>
                {
                    contents.AppendLine($"{Indent.Triple}yield return \"{ddsTypeDef.Name}\";");
                });

                contents.AppendLine($"{Indent.Couple}}}");

                contents.AppendLine(string.Empty);

            }


            contents.AppendLine($"{Indent.Couple}public override IEnumerable<string> ToStringValues(byte[] CCSID930Bytes)");
            contents.AppendLine($"{Indent.Couple}{{");

            var startIndex = 0;
            ITypeDefinitions.ToList().ForEach(ddsTypeDef =>
            {
                var field = VariableFactory.Of(ddsTypeDef);

                var length = field.TypeLength;
                var startPosition = startIndex + 1;
                var endPosition = startIndex + length;
                var toValueFromBytes = string.Empty;
                if (ddsTypeDef.IsExplicitCharacter)
                {
                    toValueFromBytes = $"CodePage930.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition})";
                }
                else
                if (ddsTypeDef.IsPackedDecimal)
                {
                    endPosition = startIndex + (length+1)/2;
                    //if(length % 2 ==0) throw new NotImplementedException();
                    toValueFromBytes = $"PackedDecimal.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
                }
                else
                if (ddsTypeDef.IsZonedDecimal)
                {
                    toValueFromBytes = $"ZonedDecimal.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
                }
                else
                {
                    toValueFromBytes = $"CodePage930.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition});//TODO {ddsTypeDef.InternalDataType}";
                }
                contents.AppendLine($"{Indent.Triple}yield return {toValueFromBytes};");

                startIndex = endPosition;

                if (!isDb2 && keyList.Names.Any(kn => kn == field.Name))
                {
                    var suffix = (field.OfTypeIsInteger)? $".PadLeft({endPosition}-{startPosition}+1,\'0\')":string.Empty;
                    contents.AppendLine($"{Indent.Triple}yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip({startPosition} - 1).Take({endPosition} - ({startPosition} - 1))){suffix};");
                    //startIndex += field.TypeLength;
                }

            });

            contents.AppendLine($"{Indent.Couple}}}");

            contents.AppendLine(string.Empty);

            contents.AppendLine($"{Indent.Couple}public override IEnumerable<string> ToStringValuesWithQuote(string lineNumber,byte[] CCSID930Bytes, string quote)");
            contents.AppendLine($"{Indent.Couple}{{");

            if(!isUnique) contents.AppendLine($"{Indent.Triple}yield return lineNumber;");

            startIndex = 0;
            ITypeDefinitions.ToList().ForEach(ddsTypeDef =>
            {
                var field = VariableFactory.Of(ddsTypeDef);

                var length = field.TypeLength;
                var startPosition = startIndex + 1;
                var endPosition = startIndex + length;
                var toValueFromBytes = string.Empty;
                if (ddsTypeDef.IsExplicitCharacter)
                {
                    toValueFromBytes = $"ToStringValueFromStringWithQuote(CCSID930Bytes, {startPosition}, {endPosition}, quote)";
                }
                else
                if (ddsTypeDef.IsPackedDecimal)
                {
                    endPosition = startIndex + (length + 1) / 2;
                    //if (length % 2 == 0) throw new NotImplementedException();
                    toValueFromBytes = $"PackedDecimal.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
                }
                else
                if (ddsTypeDef.IsZonedDecimal)
                {
                    toValueFromBytes = $"ZonedDecimal.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
                }
                else
                {
                    toValueFromBytes = $"ToStringValueFromStringWithQuote(CCSID930Bytes, {startPosition}, {endPosition}, quote)";
                }

                contents.AppendLine($"{Indent.Triple}yield return {toValueFromBytes};");

                startIndex = endPosition;

                if (!isDb2 && keyList.Names.Any(kn => kn == field.Name))
                {
                    var toHexValueFromBytes = string.Empty;

                    if (ddsTypeDef.IsExplicitCharacter)
                    {
                        toHexValueFromBytes = $"BytesExtensions.ToHexString(CCSID930Bytes.Skip({startPosition} - 1).Take({endPosition} - ({startPosition} - 1)))";
                    }
                    else
                    if (ddsTypeDef.IsPackedDecimal)
                    {
                        toHexValueFromBytes = $"PackedDecimal.To4sStringFrom(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
                    }
                    else
                    if (ddsTypeDef.IsZonedDecimal)
                    {
                        toHexValueFromBytes = $"ZonedDecimal.To4sStringFrom(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
                    }
                    else
                    {
                        toHexValueFromBytes = $"BytesExtensions.ToHexString(CCSID930Bytes.Skip({startPosition} - 1).Take({endPosition} - ({startPosition} - 1)))";
                    }

                    contents.AppendLine($"{Indent.Triple}yield return $\"{{quote}}{{{toHexValueFromBytes}}}{{quote}}\";");

                }

            });

            contents.AppendLine($"{Indent.Couple}}}");

            contents.AppendLine(string.Empty);

            contents.AppendLine($"{Indent.Couple}public static {EntityName} Create(byte[] CCSID930Bytes)");
            contents.AppendLine($"{Indent.Couple}{{");
            contents.AppendLine($"{Indent.Triple}var entity = new {EntityName}();");

            startIndex = 0;
            ITypeDefinitions.ToList().ForEach(ddsTypeDef =>
            {
                var field = VariableFactory.Of(ddsTypeDef);

                var length = field.TypeLength;
                var startPosition = startIndex + 1;
                var endPosition = startIndex + length;
                var toValueFromBytes = string.Empty;
                if (ddsTypeDef.IsExplicitCharacter)
                {
                    toValueFromBytes = $"CodePage930.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition})";
                }
                else
                if (ddsTypeDef.IsPackedDecimal)
                {
                    endPosition = startIndex + (length + 1) / 2;
                    //if (length % 2 == 0) throw new NotImplementedException();
                    toValueFromBytes = $"PackedDecimal.ToDecimalFrom(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
                }
                else
                if (ddsTypeDef.IsZonedDecimal)
                {
                    toValueFromBytes = $"ZonedDecimal.ToDecimalFrom(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
                }
                else
                {
                    toValueFromBytes = $"CodePage930.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition});//TODO {ddsTypeDef.InternalDataType}";
                }

                if (ddsTypeDef.IsPackedDecimal || (ddsTypeDef.IsZonedDecimal))
                {
                    toValueFromBytes = $"({field.TypeSpelling}){toValueFromBytes}";
                }
                contents.AppendLine($"{Indent.Triple}entity.{field.Name} = {toValueFromBytes};");

                startIndex = endPosition;
            });

            if (!isDb2) contents.AppendLine($"{Indent.Triple}((ISortaleBy4s)entity).Update4sValues();");
            contents.AppendLine($"{Indent.Triple}return entity;");

            contents.AppendLine($"{Indent.Couple}}}");

            contents.AppendLine(string.Empty);

            if (!isDb2)
            {

                contents.AppendLine($"{Indent.Couple}public override IEnumerable<string> ColumnNames()");
                contents.AppendLine($"{Indent.Couple}{{");
                ITypeDefinitions.ToList().ForEach(ddsTypeDef =>
                {
                    var field = VariableFactory.Of(ddsTypeDef);

                    contents.AppendLine($"{Indent.Triple}yield return \"{ddsTypeDef.Name}\";");
                    if (keyList.Names.Any(kn => kn == field.Name))
                    {
                        contents.AppendLine($"{Indent.Triple}yield return \"{ddsTypeDef.Name}4s\";");
                    }
                });
                contents.AppendLine($"{Indent.Couple}}}");

                contents.AppendLine(string.Empty);

                contents.AppendLine($"{Indent.Couple}public override IEnumerable<string> ToStringValuesWithQuote({EntityName} entity, string quote)");
                contents.AppendLine($"{Indent.Couple}{{");
                ITypeDefinitions.ToList().ForEach(ddsTypeDef =>
                {
                    var field = VariableFactory.Of(ddsTypeDef);
                    if (ddsTypeDef.IsExplicitCharacter)
                    {
                        //$"{quote}{CCSID930.To4sStringFromZone(CCSID930Bytes, 1, 5, 0)}{quote}"
                        contents.AppendLine($"{Indent.Triple}yield return $\"{{quote}}{{entity.{ddsTypeDef.Name.ToPascalCase()}}}{{quote}}\";");
                    }
                    else
                    {
                        contents.AppendLine($"{Indent.Triple}yield return entity.{ddsTypeDef.Name.ToPascalCase()}.ToString();");
                    }

                    if (keyList.Names.Any(kn => kn == field.Name))
                    {
                        contents.AppendLine($"{Indent.Triple}yield return $\"{{quote}}{{entity.{ddsTypeDef.Name.ToPascalCase()}4s}}{{quote}}\";");
                    }
                });
                contents.AppendLine($"{Indent.Couple}}}");

                contents.AppendLine(string.Empty);

            }

            contents.AppendLine($"{Indent.Single}}}");

            contents.AppendLine("}");

            return contents.ToString();
        }

    }

}
