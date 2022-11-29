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
    public class EntityGenerator
    {
        public static string CreateContents(bool isForDB2, ObjectID objectID,
    IEnumerable<IDataTypeDefinition> ITypeDefinitions,
    ScalarVariableList keyList)
        {
            var fields = VariableFactory.Of(ITypeDefinitions);
            var EntityName = objectID.Name.ToPublicModernName();

            var contents = new StringBuilder();
            contents.AppendLine(NamespaceItemFactory.System.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemCollectionsGeneric.ToUsingLine);
            //if (!isForDB2) contents.AppendLine(NamespaceItemFactory.DeltaAS400DataTypesCharacters.ToUsingLine);
            //contents.AppendLine(NamespaceItemFactory.DeltaRelationalDatabases.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectID).ToNamespaceLine);
            contents.AppendLine("{");

            contents.AppendLine($"{Indent.Single}public partial class {EntityName}");
            contents.AppendLine($"{Indent.Single}{{");

            if(keyList.ScalarVariables.Count==1&& keyList.ScalarVariables.First().Name == "Id")
            {
                contents.AppendLine($"{Indent.Couple}public int Id {{get;set;}}");
            }

            fields.ToList().ForEach(f =>
            {
                contents.AppendLine($"{Indent.Couple}public {f.TypeSpelling} {f.Name} {{get;set;}}");
            });

            if (!isForDB2)
            {
                keyList.ScalarVariables.ForEach(key =>
                {
                    contents.AppendLine($"{Indent.Couple}public string {key.Name}4s {{get;set;}}");
                });
            }

            contents.AppendLine($"{Indent.Single}}}");

            contents.AppendLine("}");

            return contents.ToString();
        }

        public static string CreateExtensionContents(ObjectID objectID,
            IEnumerable<IDataTypeDefinition> ITypeDefinitions,
            ScalarVariableList keyList)
        {
            var fields = VariableFactory.Of(ITypeDefinitions);
            var EntityName = objectID.Name.ToPublicModernName();

            var contents = new StringBuilder();
            contents.AppendLine(NamespaceItemFactory.System.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.SystemCollectionsGeneric.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaAS400DataTypesCharacters.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaEntities.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectID).ToNamespaceLine);
            contents.AppendLine("{");

            contents.AppendLine($"{Indent.Single}public partial class {EntityName} : ISortaleBy4s");
            contents.AppendLine($"{Indent.Single}{{");

            keyList.ScalarVariables.ForEach(key =>
            {
                create4s(contents, key);
            });

            fields.Where(f => keyList.Names.Count(kn => kn == f.Name) == 0).ToList().ForEach(key =>
            {
                create4s(contents, key);
            });

            contents.AppendLine($"{Indent.Couple}void ISortaleBy4s.Update4sValues()");
            contents.AppendLine($"{Indent.Couple}{{");
            keyList.ScalarVariables.ForEach(key =>
            {
                if (key.Name != "Id") contents.AppendLine($"{Indent.Triple}{key.Name}4s = {key.Name}To4s({key.Name});");
            });
            contents.AppendLine($"{Indent.Couple}}}");


            contents.AppendLine($"{Indent.Single}}}");

            contents.AppendLine("}");

            return contents.ToString();
        }

        public static string CreateContentsForLF(ObjectID objectIDOfPF, List<Variable> keyListInLFonly)
        {
            var EntityName = objectIDOfPF.Name.ToPublicModernName();

            var contents = new StringBuilder();
            contents.AppendLine(NamespaceItemFactory.System.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaAS400DataTypesCharacters.ToUsingLine);
            contents.AppendLine(NamespaceItemFactory.DeltaOf(objectIDOfPF).ToNamespaceLine);
            contents.AppendLine("{");
            contents.AppendLine($"{Indent.Single}public partial class {EntityName}");
            contents.AppendLine($"{Indent.Single}{{");

            keyListInLFonly.ForEach(key =>
            {
                //create4s(contents, key);
                //public string Bnhri4s => Bnhri4s(bnhri);
                contents.AppendLine($"{Indent.Couple}internal string {key.Name}4s => {key.Name}To4s({key.Name});");

            });

            contents.AppendLine($"{Indent.Single}}}");

            contents.AppendLine("}");

            return contents.ToString();
        }

        static void create4s(StringBuilder contents, Variable key)
        {
            contents.AppendLine($"{Indent.Couple}public static string {key.Name}To4s({key.TypeSpelling} {key.Name.ToLower()})");
            contents.AppendLine($"{Indent.Couple}{{");
            if (key.OfTypeIsString)
            {
                contents.AppendLine($"{Indent.Triple}return CodePage290.ToHexString(({key.Name.ToLower()} ?? string.Empty).PadRight({key.TypeLength}));");
            }
            else
            if (key.OfTypeIsInteger)
            {
                contents.AppendLine($"{Indent.Triple}return {key.Name.ToLower()}.ToString(\"D{key.TypeLength}\");");
            }
            else
            if (key.OfTypeIsDecimal)
            {
                contents.AppendLine($"{Indent.Triple}return ((long)decimal.Multiply({key.Name.ToLower()},(decimal)Math.Pow(10, {key.TypeNumberOfDecimalPlaces}))).ToString(\"D{key.TypeLength}\");");
            }
            else
            {
                contents.AppendLine($"{Indent.Triple}return {key.Name.ToLower()}.ToString(\"D{key.TypeLength}\");//TODO");
            }

            contents.AppendLine($"{Indent.Couple}}}");

        }

        //static string ToValueFromBytes(IDataTypeDefinition ddsTypeDef, int startPosition, int endPosition)
        //{
        //    var toValueFromBytes = string.Empty;
        //    if (ddsTypeDef.IsExplicitCharacter)
        //    {
        //        toValueFromBytes = $"CCSID930.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition})";
        //    }
        //    else
        //    if (ddsTypeDef.IsPackedDecimal)
        //    {
        //        toValueFromBytes = $"CCSID930.ToDecimalFromPack(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
        //    }
        //    else
        //    if (ddsTypeDef.IsZonedDecimal)
        //    {
        //        toValueFromBytes = $"CCSID930.ToDecimalFromZone(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
        //    }
        //    else
        //    {
        //        toValueFromBytes = $"CCSID930.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition});//TODO {ddsTypeDef.InternalDataType}";
        //    }

        //    return toValueFromBytes;
        //}

        //static string ToStringFromBytes(IDataTypeDefinition ddsTypeDef, int startPosition,int endPosition)
        //{
        //    var toValueFromBytes = string.Empty;
        //    if (ddsTypeDef.IsExplicitCharacter)
        //    {
        //        toValueFromBytes = $"CCSID930.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition})";
        //    }
        //    else
        //    if (ddsTypeDef.IsPackedDecimal)
        //    {
        //        toValueFromBytes = $"CCSID930.ToStringFromPack(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
        //    }
        //    else
        //    if (ddsTypeDef.IsZonedDecimal)
        //    {
        //        toValueFromBytes = $"CCSID930.ToStringFromZone(CCSID930Bytes, {startPosition}, {endPosition}, {ddsTypeDef.DecimalPositionsToInt})";
        //    }
        //    else
        //    {
        //        toValueFromBytes = $"CCSID930.ToStringFrom(CCSID930Bytes, {startPosition}, {endPosition});//TODO {ddsTypeDef.InternalDataType}";
        //    }

        //    return toValueFromBytes;
        //}

    }
}
