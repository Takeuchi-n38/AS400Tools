using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Generator.DomainLayer
{
    public class FormatterTestGenerator
    {

        public static ClassStructure Create(Library mainLibrary,ObjectID objectID)
        {
            var SourceObjectIDPublicModernName = objectID.Name.ToPublicModernName();

            var serviceClass = new ClassStructure(
            NamespaceItemFactory.DeltaOf(objectID), true,
            $"{SourceObjectIDPublicModernName}FormatterTest",
            "",
            $"{SourceObjectIDPublicModernName}FormatterTest",
            "gen.cs"
            );

            serviceClass.AddInterface("FormatterTest");
            
            UsingNamespaces().ToList().ForEach(un=>serviceClass.AddUsingNamespace(un));

            serviceClass.AddContentLine($"static readonly TestTarget TestTarget = TestTarget.Of({objectID.Library.Partition.Name.ToPascalCase()}LibraryList.{objectID.Library.Name.ToPascalCase()}.ObjectIDOf(\"{objectID.Name}\"));");
            serviceClass.AddContentLine($"static readonly TestHelper TestHelperInstance = {mainLibrary.Partition.Name.ToPascalCase()}{mainLibrary.Name.ToPascalCase()}TestHelper.ForFormatterOf(TestTarget);");
            serviceClass.AddContentLine("protected override TestHelper TargetTestHelper => TestHelperInstance;");

            serviceClass.AddContentLine("protected override EntityTestHelper SetupEntityTestHelperOf(string caseName) => throw new NotImplementedException();//Honsha01IidlibOrddtaTestHelper.Of;");
            serviceClass.AddContentLine(string.Empty);

            serviceClass.AddContentLine("protected override EntityTestHelper ExpectedEntityTestHelperOf(string caseName) => throw new NotImplementedException();//Honsha01IidlibOrddtaTestHelper.Of;");
            serviceClass.AddContentLine(string.Empty);

            serviceClass.AddBlankLine();

            serviceClass.AddContentLine($"[Fact]");
            serviceClass.AddContentLine("public void DownloadTestDataBinaryOfCase1()");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}var caseName = \"1\";");
            serviceClass.AddContentLine($"{Indent.Single}DownloadTestDataBinaryOf(caseName);");
            serviceClass.AddContentLine("}");

            serviceClass.AddBlankLine();

            serviceClass.AddContentLine($"[Fact]");
            serviceClass.AddContentLine("public void ConvertTestDataBinaryToCsvOfCase1()");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}var caseName = \"1\";");
            serviceClass.AddContentLine($"{Indent.Single}ConvertTestDataBinaryToCsvOf(caseName);");
            serviceClass.AddContentLine("}");

            serviceClass.AddBlankLine();

            serviceClass.AddContentLine($"[Fact]");
            serviceClass.AddContentLine("public void Case1()");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}var caseName = \"1\";");
            serviceClass.AddContentLine($"{Indent.Single}/*");
            serviceClass.AddContentLine($"{Indent.Single}IEnumerable<byte[]> inputBytes = TestHelperInstance.ReadBytesFromExpectedFolder(caseName, Honsha01LibraryList.Wrkcat1.ObjectIDOf(\"GPDAIORD1\"));");
            serviceClass.AddContentLine($"{Indent.Single}var actual1 = DnordFormatter.Format(inputBytes);");
            serviceClass.AddContentLine($"{Indent.Single}var expectedFile1 = Honsha01LibraryList.Wrkcat1.ObjectIDOf(\"GPDAIORD2\");");
            serviceClass.AddContentLine($"{Indent.Single}TestHelperInstance.WriteAllBytesToActualFolder(caseName, actual1, expectedFile1);");
            serviceClass.AddContentLine($"{Indent.Single}IEnumerable<byte[]> expected1 = TestHelperInstance.ReadBytesFromExpectedFolder(caseName, expectedFile1);");
            serviceClass.AddContentLine($"{Indent.Single}Assert.Equal(expected1, actual1);");
            serviceClass.AddContentLine($"{Indent.Single}*/");

            serviceClass.AddContentLine("}");

            serviceClass.AddBlankLine();

            return serviceClass;
        }

        static IEnumerable<NamespaceItem> UsingNamespaces()
        {
            var usingNamespaces = new List<NamespaceItem>();

            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400Objects);
            usingNamespaces.Add(NamespaceItemFactory.DeltaToolsModernizationTest);
            usingNamespaces.Add(NamespaceItemFactory.System);
            usingNamespaces.Add(NamespaceItemFactory.SystemCollectionsGeneric);
            usingNamespaces.Add(NamespaceItemFactory.SystemIO);
            usingNamespaces.Add(NamespaceItemFactory.SystemLinq);
            usingNamespaces.Add(NamespaceItemFactory.Xunit);

            return usingNamespaces;

        }
    }
}
