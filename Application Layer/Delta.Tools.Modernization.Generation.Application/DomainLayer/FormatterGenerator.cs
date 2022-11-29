using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Tools.AS400.DDSs.FMTs;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using Delta.Tools.Sources.Items;
using Delta.Tools.Sources.Lines;
using System;
using System.Linq;

namespace Delta.Tools.AS400.Generator.DomainLayer
{
    public class FormatterGenerator
    {
        PathResolverForTool PathResolver;

        public FormatterGenerator(PathResolverForTool pathResolver)
        {
            PathResolver = pathResolver;
        }

        /*
         HSORTRS   28A         X
         *                                  ｿｰﾄｷｰ
         FNC  20  23
         FNC   1  16
         FNC  42  45
         FNC  69  69
         FNC  17  19
         *                                  ｻﾏﾘｰﾌｨｰﾙﾄﾞ
         FDC   1  23
         FSU  24  32
         FDC  33  50
         FSU  51  59
         FDC  60  80
 
     */
        public void Generate(FMTStructure aFmtdta)
        {
            if (!aFmtdta.Header.IsSort&& !aFmtdta.Header.IsSortAndSummary) throw new NotImplementedException();
            if (!aFmtdta.Header.IsAsc) throw new NotImplementedException();
            if (!aFmtdta.Header.IsSortKeyOutput) throw new NotImplementedException();

            var objectID = aFmtdta.OriginalSource.ObjectID;
            string className = $"{objectID.Name.ToCSharpOperand()}Formatter";
            var classStructure = new ClassStructure(
                NamespaceItemFactory.DeltaOf(objectID),//.Library,"Formatters"),
                true,
                className,
                "",
                className,
                "gen.cs"
                );

            classStructure.AddUsingNamespace(NamespaceItemFactory.DeltaAS400DataTypes);
            classStructure.AddUsingNamespace(NamespaceItemFactory.DeltaAS400DataTypesNumerics);
            classStructure.AddUsingNamespace(NamespaceItemFactory.System);
            classStructure.AddUsingNamespace(NamespaceItemFactory.SystemCollectionsGeneric);
            classStructure.AddUsingNamespace(NamespaceItemFactory.SystemLinq);

            aFmtdta.Lines.ToList().ForEach(line =>
            {
                classStructure.AddContentLine(line.ToOriginalComment());
            });

            classStructure.AddContentLine("public static IEnumerable<byte[]> Format(IEnumerable<byte[]> sources)");
            classStructure.AddContentLine("{");
            classStructure.AddContentLine($"{Indent.Single}return sources");
            classStructure.AddContentLine($"{Indent.Single}.Select(s => (sortKey:");

            var firstKey = aFmtdta.Fields.First();
            if(!firstKey.IsKeyAsc) throw new NotImplementedException();
            classStructure.AddContentLine($"{Indent.Couple}s.Skip({firstKey.StartPosition - 1}).Take({firstKey.EndPosition - firstKey.StartPosition + 1})");
            var notKey=false;
            aFmtdta.Fields.Skip(1).ToList().ForEach(field =>
            {
                if (field.IsKeyAsc && notKey) throw new NotImplementedException();
                if (field.IsKeyAsc){
                    classStructure.AddContentLine($"{Indent.Couple}.Concat(s.Skip({field.StartPosition - 1}).Take({field.EndPosition - field.StartPosition + 1}))");
                }
                else
                {
                    notKey = true;
                }
            });
            classStructure.AddContentLine($"{Indent.Couple}, bytes: s))");

            classStructure.AddContentLine($"{Indent.Single}.OrderBy(key_bytes => key_bytes.sortKey, BytesComparator.Instance)");
            classStructure.AddContentLine($"{Indent.Single}.GroupBy(key_bytes => key_bytes.sortKey)");
            classStructure.AddContentLine($"{Indent.Single}.Select(g_key_bytes =>");

            var isFirst=true;
            aFmtdta.Lines.ToList().ForEach(line =>
            {
                if (line.IsHeaderLine || line.IsCommentLine)
                {

                }
                else
                if (line.IsRecordLine)
                {
                    classStructure.AddContentLine($"{Indent.Couple}//{line.Value}");
                }
                else
                if (line.IsFieldLine)
                {
                    var fline= (IFormatDataFieldLine)line;
                    if (fline.IsKeyAsc)
                    { 

                    }
                    else
                    if (fline.IsField || fline.IsSummary)
                    {
                        var contentLine= AddContentLine(fline);
                        if (isFirst)
                        {
                            isFirst=false;
                            classStructure.AddContentLine($"{Indent.Couple}{contentLine}");
                        }
                        else
                        {
                            classStructure.AddContentLine($"{Indent.Couple}.Concat({contentLine})");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    throw new NotImplementedException();
                }
            });

            classStructure.AddContentLine($"{Indent.Couple}.ToArray());");
            classStructure.AddContentLine("}");

            PathResolver.WriteFormatterSource(objectID, className,((ISourceFile)classStructure).ToSourceContents());
        }

        string AddContentLine(IFormatDataFieldLine firstKey)
        {
            if (firstKey.IsField)
            {
                if (firstKey.IsCharacter)
                {
                    return $"g_key_bytes.First().bytes.Skip({firstKey.StartPosition - 1}).Take({firstKey.EndPosition - firstKey.StartPosition + 1})";
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else if (firstKey.IsSummary)
            {
                if (firstKey.IsZone)
                {
                    return $"ZonedDecimal.ToSignedBytesFrom(g_key_bytes.Sum(key_bytes => ZonedDecimal.IntegerToInt(key_bytes.bytes.Skip({firstKey.StartPosition - 1}).Take({firstKey.EndPosition - firstKey.StartPosition + 1}))), {firstKey.EndPosition - firstKey.StartPosition + 1})";
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }

        }
    }
}
