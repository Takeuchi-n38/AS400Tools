using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.AS400.DDSs.DiskFiles;
using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Sources;
using Delta.Tools.CSharp.Statements.Comments;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services
{
    public class ServiceTestStructureFactory
    {

        internal ClassStructure Create(Source source,
            Library mainLibrary, IEnumerable<ObjectID> objectIDsForUsing,
            ObjectID? workstationFileObjectID,
            List<DiskFileDescriptionLine> DiskFileDescriptionLines,
            List<PrinterFileDescriptionLine> PrinterFileDescriptionLines,
            List<Variable> parameters,
            List<Variable> variables,
            bool IsExistInzsr,
            DiskFileDescriptionLine? CycleFileLine,
            bool IsCalling,
            IEnumerable<string> contentLines,
            List<Library> LibrariesOfDB2foriRepository)
        {
            var SourceObjectIDPublicModernName = source.ObjectID.Name.ToPublicModernName();
            var namspace= NamespaceItemFactory.DeltaOf(source.ObjectID);
            var serviceClass = new ClassStructure(
            namspace, true,
            $"{SourceObjectIDPublicModernName}ServiceTest",
            "",
            $"{SourceObjectIDPublicModernName}ServiceTest",
            "gen.cs"
            );

            serviceClass.AddInterface("ServiceTest");

            UsingNamespaces(objectIDsForUsing).ToList().ForEach(namespaceItem => serviceClass.AddUsingNamespace(namespaceItem));

            serviceClass.AddContentLine($"static readonly TestTarget TestTarget = TestTarget.Of({source.ObjectID.Library.Partition.Name.ToPascalCase()}LibraryList.{source.ObjectID.Library.Name.ToPascalCase()}.ObjectIDOf(\"{source.ObjectID.Name}\"));");
            serviceClass.AddContentLine($"public static readonly TestHelper TestHelperInstance = {mainLibrary.Partition.Name.ToPascalCase()}{mainLibrary.Name.ToPascalCase()}TestHelper.Of(TestTarget);");
            serviceClass.AddContentLine("protected override TestHelper TargetTestHelper => TestHelperInstance;");
            serviceClass.AddContentLine($"static I{mainLibrary.Partition.Name.ToPascalCase()}{mainLibrary.Name.ToPascalCase()}DependencyInjector DependencyInjector = {mainLibrary.Partition.Name.ToPascalCase()}{mainLibrary.Name.ToPascalCase()}TestHelper.DependencyInjector;");

            serviceClass.AddContentLine("private readonly ITestOutputHelper _testOutputHelper;");
            serviceClass.AddContentLine($"public {SourceObjectIDPublicModernName}ServiceTest(ITestOutputHelper testOutputHelper)");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}_testOutputHelper = testOutputHelper;");
            serviceClass.AddContentLine("}");
            serviceClass.AddContentLine("protected override void WriteOnTestExploler(string value)");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}_testOutputHelper.WriteLine(value);");
            serviceClass.AddContentLine("}");

            serviceClass.AddContentLine("protected override void SetupEntityTestHelpersOf(string caseName, List<EntityTestHelper> entityTestHelpers)");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}if (caseName == \"1\")");
            serviceClass.AddContentLine($"{Indent.Single}{{");

            for (var i = 0; i < DiskFileDescriptionLines.Count(); i++)
            {
                var f = DiskFileDescriptionLines.ElementAt(i);

                var helper=string.Empty;
                var isForDB2 = false;
                if (f.IsExternalFileFormat)
                {
                    if (f.File.OriginalSource.IsNotFound)
                    {
                        helper = $"{f.File.ObjectID.Name.ToPascalCase()}TestHelper.Of";
                    }
                    else
                    {
                        isForDB2 = LibrariesOfDB2foriRepository.Contains(f.File.ObjectID.Library);
                        helper = $"{((ExternalFormatFileStructure)f.File).FileDifintion.ObjectID.Name.ToPascalCase()}TestHelper.Of";
                    }
                }
                else
                {
                    helper = $"NonDDSPhycicalFileHelper.Of({f.RecordLength}, {f.FileObjectID.Library.Partition.Name.ToPascalCase()}LibraryList.{f.FileObjectID.Library.Name.ToPascalCase()}.ObjectIDOf(\"{f.FileObjectID.Name}\"))";
                }
                serviceClass.AddContentLine($"{Indent.Couple}{(isForDB2 ? "//" : string.Empty)}entityTestHelpers.Add({helper});");
            }

            serviceClass.AddContentLine($"{Indent.Single}}}");
            serviceClass.AddContentLine($"{Indent.Single}else");
            serviceClass.AddContentLine($"{Indent.Single}{{");
            serviceClass.AddContentLine($"{Indent.Couple}throw new NotImplementedException();");
            serviceClass.AddContentLine($"{Indent.Single}}}");
            serviceClass.AddContentLine("}");
            serviceClass.AddContentLine(string.Empty);

            serviceClass.AddContentLine("protected override void ExpectedEntityTestHelpersOf(string caseName, List<EntityTestHelper> entityTestHelpers)");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}if (caseName == \"1\")");
            serviceClass.AddContentLine($"{Indent.Single}{{");
            for (var i = 0; i < DiskFileDescriptionLines.Count(); i++)
            {
                var f = DiskFileDescriptionLines.ElementAt(i);

                if (f.FileType == "I") continue;

                var helper = string.Empty;
                var isForDB2 = false;
                if (f.IsExternalFileFormat)
                {
                    if (f.File.OriginalSource.IsNotFound)
                    {
                        helper = $"{f.File.ObjectID.Name.ToPascalCase()}TestHelper.Of";
                    }
                    else
                    {
                        isForDB2 = LibrariesOfDB2foriRepository.Contains(f.File.ObjectID.Library);
                        helper = $"{((ExternalFormatFileStructure)f.File).FileDifintion.ObjectID.Name.ToPascalCase()}TestHelper.Of";
                    }
                }
                else
                {
                    helper = $"NonDDSPhycicalFileHelper.Of({f.RecordLength}, {f.FileObjectID.Library.Partition.Name.ToPascalCase()}LibraryList.{f.FileObjectID.Library.Name.ToPascalCase()}.ObjectIDOf(\"{f.FileObjectID.Name}\"))";
                }
                serviceClass.AddContentLine($"{Indent.Couple}{(isForDB2?"//":string.Empty)}entityTestHelpers.Add({helper});");
            }
            serviceClass.AddContentLine($"{Indent.Single}}}");
            serviceClass.AddContentLine($"{Indent.Single}else");
            serviceClass.AddContentLine($"{Indent.Single}{{");
            serviceClass.AddContentLine($"{Indent.Couple}throw new NotImplementedException();");
            serviceClass.AddContentLine($"{Indent.Single}}}");
            serviceClass.AddContentLine("}");

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
            serviceClass.AddContentLine("public void InsertTestDataCsvToSetupAndExpectedSchemaOfCase1()");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}var caseName = \"1\";");
            serviceClass.AddContentLine($"{Indent.Single}InsertTestDataCsvToSetupAndExpectedSchemaOf(caseName);");
            serviceClass.AddContentLine("}");

            serviceClass.AddBlankLine();

            serviceClass.AddContentLine($"[Fact]");
            serviceClass.AddContentLine("public void InsertTestDataBinarySetupAndExpectedSchemaOfCase1()");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}var caseName = \"1\";");
            serviceClass.AddContentLine($"{Indent.Single}InsertTestDataBinarySetupAndExpectedSchemaOf(caseName);");
            serviceClass.AddContentLine("}");

            serviceClass.AddBlankLine();

            serviceClass.AddContentLine($"[Fact]");
            serviceClass.AddContentLine("public void Case1() //createDateOfSAVF : 2022 MMdd");
            serviceClass.AddContentLine("{");
            serviceClass.AddContentLine($"{Indent.Single}var caseName = \"1\";");
            serviceClass.AddContentLine($"{Indent.Single}InsertSetupTablesToActualTablesOf(caseName);");

            for (var i = 0; i < DiskFileDescriptionLines.Count(); i++)
            {
                var f = DiskFileDescriptionLines.ElementAt(i);

                if (!f.IsExternalFileFormat)
                {
                    // var asp16zdtl = Honsha01LibraryList.Iidlib.ObjectIDOf("ASP16ZDTL");
                    serviceClass.AddContentLine($"{Indent.Single}var {f.FileObjectID.Name.ToLower()} = {f.FileObjectID.Library.Partition.Name.ToPascalCase()}LibraryList.{f.FileObjectID.Library.Name.ToPascalCase()}.ObjectIDOf(\"{f.FileObjectID.Name}\");");

                    if (f.FileType == "O")
                    {
                        serviceClass.AddContentLine($"{Indent.Single}var {f.FileObjectID.Name.ToLower()}RecordFormatMemoryRepository = new RecordFormatMemoryRepository();");
                    }
                    else
                    {
                        //            var asp16zdtlBytes = TestHelperInstance.ReadBytesFromSetupFolder(caseName, asp16zdtl).ToList();
                        serviceClass.AddContentLine($"{Indent.Single}var {f.FileObjectID.Name.ToLower()}Bytes = TestHelperInstance.ReadBytesFromSetupFolder(caseName, {f.FileObjectID.Name.ToLower()}).ToList();");
                    }
                }
            }

            serviceClass.AddContentLine($"{Indent.Single}RestartStopwatch();");

            serviceClass.AddContentLine($"{Indent.Single}new {SourceObjectIDPublicModernName}Service(");

            for (var i = 0; i < DiskFileDescriptionLines.Count(); i++)
            {
                var f = DiskFileDescriptionLines.ElementAt(i);

                var repo = $"null{((i == DiskFileDescriptionLines.Count() - 1) ? string.Empty : ",")}";
                if (f.IsExternalFileFormat)
                {
                    if (f.File.OriginalSource.IsNotFound)
                    {
                        repo = $"null{((i == DiskFileDescriptionLines.Count() - 1) ? string.Empty : ",")}//{f.File.ObjectID.Name}";
                    }
                    else
                    {
                        //                BroadDB2Repository.Of(TestHelper.TestDB2foriOperatedBySQL, TestTarget.SetupSchemaName(caseName)),
                        var isForDB2 = LibrariesOfDB2foriRepository.Contains(f.File.ObjectID.Library);
                        if (isForDB2)
                        {
                            repo = $"{f.File.ObjectID.Name.ToPascalCase()}DB2Repository.Of(TestHelper.IPofTestDB2fori, TestTarget.SetupSchemaName(caseName)){((i == DiskFileDescriptionLines.Count() - 1) ? string.Empty : ",")}";
                        }
                        else
                        {
                            repo = $"DependencyInjector.{f.File.ObjectID.Name.ToPascalCase()}Repository{((i == DiskFileDescriptionLines.Count() - 1) ? string.Empty : ",")}";
                        }
                    }
                }
                else
                {
                    if (f.FileType == "O")
                    {
                        repo = $"{f.FileObjectID.Name.ToLower()}RecordFormatMemoryRepository{((i == DiskFileDescriptionLines.Count() - 1) ? string.Empty : ",")}";
                    }
                    else
                    {
                        repo = $"{f.FileObjectID.Name.ToLower()}Bytes.Select(bytes=>new {f.FileObjectID.Name.ToPascalCase()}(bytes)){((i == DiskFileDescriptionLines.Count() - 1) ? string.Empty : ",")}";
                    }
                }
                serviceClass.AddContentLine($"{Indent.Single}{repo}");
            }
            for (var i = 0; i < PrinterFileDescriptionLines.Count(); i++)
            {
                var f = PrinterFileDescriptionLines.ElementAt(i);

                var repo = $",null";
                serviceClass.AddContentLine($"{Indent.Single}{repo}");
            }
            if (IsCalling)
            {
                var repo = $"{((0== DiskFileDescriptionLines.Count()) ? string.Empty : ",")}null";
                serviceClass.AddContentLine($"{Indent.Single}{repo}");
            }
            else
            if(DiskFileDescriptionLines.Count()==0)
            {
                serviceClass.AddContentLine($"{Indent.Single}DependencyInjector");
            }

            serviceClass.AddContentLine($"{Indent.Single}).Main();");
            serviceClass.AddContentLine($"{Indent.Single}StopStopwatch();");
            serviceClass.AddContentLine($"{Indent.Single}WriteElapsedTime();");

            for (var i = 0; i < DiskFileDescriptionLines.Count(); i++)
            {
                var f = DiskFileDescriptionLines.ElementAt(i);

                if (!f.IsExternalFileFormat)
                {
                    // var asp16zdtl = Honsha01LibraryList.Iidlib.ObjectIDOf("ASP16ZDTL");
                    //serviceClass.AddContentLine($"{Indent.Single}var {f.File.ObjectID.Name.ToLower()} = {f.FileObjectID.Library.Partition.Name.ToPascalCase()}LibraryList.{f.FileObjectID.Library.Name.ToPascalCase()}.ObjectIDOf(\"{f.FileObjectID.Name}\");");

                    if (f.FileType != "I")
                    {
                        if (f.FileType == "O")
                        {
                        //            var outflBytes = outflRecordFormatMemoryRepository.AllBytes().ToList();
                            serviceClass.AddContentLine($"{Indent.Single}var {f.FileObjectID.Name.ToLower()}Bytes = {f.FileObjectID.Name.ToLower()}RecordFormatMemoryRepository.AllBytes().ToList();");
                        }
                        //TestHelperInstance.WriteAllBytesToActualFolder(caseName, asp16zdtlBytes, asp16zdtl);
                        serviceClass.AddContentLine($"{Indent.Single}TestHelperInstance.WriteAllBytesToActualFolder(caseName, {f.FileObjectID.Name.ToLower()}Bytes, {f.FileObjectID.Name.ToLower()});");
                        //Assert.Equal(TestHelperInstance.ReadBytesFromExpectedFolder(caseName, asp16zdtl).ToList(), asp16zdtlBytes);    
                        serviceClass.AddContentLine($"{Indent.Single}Assert.Equal(TestHelperInstance.ReadBytesFromExpectedFolder(caseName, {f.FileObjectID.Name.ToLower()}).ToList(), {f.FileObjectID.Name.ToLower()}Bytes);");
                    }
                }
            }

            serviceClass.AddContentLine($"{Indent.Single}Assert.Equal(0, DifferenceCountOfExceptView(caseName));");
            serviceClass.AddContentLine("}");
            serviceClass.AddContentLine($"//dotnet test --collect:\"XPlat Code Coverage\" --filter DisplayName={namspace.Name}.{SourceObjectIDPublicModernName}ServiceTest.Case1");
            serviceClass.AddContentLine("//reportgenerator -reports:\"coverage.cobertura.xml\" -targetdir:\"coveragereport\" -reporttypes:Html");
            
            serviceClass.AddBlankLine();


            //serviceClass.AddContentLine("/*");
            //serviceClass.AddContentLine($"[Fact]");
            //serviceClass.AddContentLine("public void Case1()");
            //serviceClass.AddContentLine("{");
            //serviceClass.AddContentLine($"{Indent.Single}var caseName = \"1\";");
            //serviceClass.AddContentLine($"{Indent.Single}var currentTestHelper = TargetTestHelper;");

            //serviceClass.AddContentLine($"{Indent.Single}IEnumerable<byte[]> inputs = currentTestHelper.ReadBytesFromSetupFolder(Honsha01LibraryList.Master9.ObjectIDOf(\"PIF305DK\"));");
            //serviceClass.AddContentLine($"{Indent.Single}IEnumerable<byte[]> actual1 = new List<byte[]>();// inputs.Select(bytes => Ajjbe03Service.FileaToOutfl(bytes));");
            //serviceClass.AddContentLine($"{Indent.Single}var expectedFileName1 =  Honsha01LibraryList.Master2.ObjectIDOf(\"JJB305DK\");");
            //serviceClass.AddContentLine($"{Indent.Single}currentTestHelper.WriteAllBytesToActualFolder(actual1, expectedFileName1);");
            //serviceClass.AddContentLine($"{Indent.Single}IEnumerable<byte[]> expected1 = currentTestHelper.ReadBytesFromExpectedFolder(expectedFileName1);");
            //serviceClass.AddContentLine($"{Indent.Single}TestHelper.AssertEqual(expected1,actual1);");

            //serviceClass.AddContentLine("}");
            //serviceClass.AddContentLine("*/");

            //serviceClass.AddContentLine("/*");

            //serviceClass.AddContentLine($"[Fact]");
            //serviceClass.AddContentLine("public void Case1()");
            //serviceClass.AddContentLine("{");
            //serviceClass.AddContentLine($"{Indent.Single}var currentTestHelper = TargetTestHelper;");
            //serviceClass.AddContentLine($"{Indent.Single}var actualLibralyName = currentTestHelper.ActualLibraryName;");
            //serviceClass.AddContentLine($"{Indent.Single}SfcdtDB2Repository sfcdtRepository = SfcdtDB2Repository.Of(actualLibralyName, TestHelper.TestDB2Repository);");
            //serviceClass.AddContentLine($"{Indent.Single}var parentTestHelper = Jqkc10d0ServiceTest.CreateTestHelper(caseName);");
            //serviceClass.AddContentLine($"{Indent.Single}sfcdtRepository.Truncate();");
            //serviceClass.AddContentLine($"{Indent.Single}sfcdtRepository.InsertIntoSelectFrom(parentTestHelper.SetupLibraryName);");
            //serviceClass.AddContentLine($"{Indent.Single}IEnumerable<byte[]> wrkcat1Gpdaisfcd2 = parentTestHelper.ReadBytesFromExpectedFolder(Honsha01LibraryList.Wrkcat1.ObjectIDOf(\"GPDAISFCD2\"));");
            //serviceClass.AddContentLine($"{Indent.Single}var azzzdspService = AzzzdspMemoryService.Of();");
            //serviceClass.AddContentLine($"{Indent.Single}new Aija9292Service(azzzdspService,sfcdtRepository).Main(\"DK02\", wrkcat1Gpdaisfcd2);");
            //serviceClass.AddContentLine($"{Indent.Single}Assert.Equal(0, sfcdtRepository.CountOfIntra(currentTestHelper.ExpectedLibraryName));");
            //serviceClass.AddContentLine($"{Indent.Single}Assert.Equal(0, sfcdtRepository.CountOfExtra(currentTestHelper.ExpectedLibraryName));");

            //serviceClass.AddContentLine("}");
            //serviceClass.AddContentLine("*/");


            return serviceClass;
        }



        internal static ServiceTestStructureFactory Instance = new ServiceTestStructureFactory();

        IEnumerable<NamespaceItem> UsingNamespaces(IEnumerable<ObjectID> objectIDs)
        {
            var usingNamespaces = new List<NamespaceItem>();

            usingNamespaces.Add(NamespaceItemFactory.DeltaRelationalDatabasesDb2fori);
            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400DDSs);
            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400Objects);
            usingNamespaces.Add(NamespaceItemFactory.DeltaToolsModernizationTest);
            //if (existPrinter) usingNamespaces.Add(NamespaceItemFactory.DeltaAS400DataDescriptionSpecificationsPrinters);
            usingNamespaces.Add(NamespaceItemFactory.MicrosoftEntityFrameworkCore);
            usingNamespaces.Add(NamespaceItemFactory.System);
            usingNamespaces.Add(NamespaceItemFactory.SystemCollectionsGeneric);
            usingNamespaces.Add(NamespaceItemFactory.SystemIO);
            usingNamespaces.Add(NamespaceItemFactory.SystemLinq);
            usingNamespaces.Add(NamespaceItemFactory.Xunit);
            usingNamespaces.Add(NamespaceItemFactory.XunitAbstractions);
            //
            //using Delta.AS400.Tests;

            objectIDs.ToList().ForEach(objectID => usingNamespaces.Add(NamespaceItemFactory.DeltaOf(objectID)));

            return usingNamespaces;

        }

        //public static string CalculateName => "Calculate";

        //IEnumerable<string> ConstructorContents(ObjectID sourceObjectID,
        //     ObjectID? workstationFileObjectID,
        //    IEnumerable<DiskFileDescriptionLine> DiskFileDescriptionLines,
        //    IEnumerable<PrinterFileDescriptionLine> PrinterFileDescriptionLines,
        //    bool IsCalling)
        //{

        //    var workstationFileObjectIDPublicModernName = workstationFileObjectID == null ? string.Empty : workstationFileObjectID.Name.ToPublicModernName();

        //    var constructorContents = new List<string>();
        //    constructorContents.Add("#region constructor");

        //    constructorContents.Add($"public {sourceObjectID.Name.ToPublicModernName() + "Service"}(");
        //    var parameters = new List<string>();
        //    DiskFileDescriptionLines.ToList().ForEach(l =>
        //    {
        //        parameters.Add(DiskFileDescriptionLineToCSharper.RepositoryTypeAndName(l));
        //    });
        //    PrinterFileDescriptionLines.ToList().ForEach(l =>
        //    {
        //        parameters.Add($"IPrinter {l.FileObjectID.Name.ToLower()}");
        //    });

        //    //parameters.AddRange(diskFileNames.Select(l => $"I{(!l.IsExternalFileFormat && l.FileType=="O" ? "RecordFormat" :l.FileObjectID.Name.ToPublicModernName() )}Repository {l.FileObjectID.Name.ToLower()}Repository").ToList());
        //    if (workstationFileObjectID != null)
        //    {
        //        parameters.Add($"I{workstationFileObjectIDPublicModernName}Presenter {workstationFileObjectIDPublicModernName.ToLower()}Presenter");
        //    }
        //    if (IsCalling)
        //    {
        //        parameters.Add("IPgmCaller pgmCaller");
        //    }
        //    for (int i = 0; i < parameters.Count(); i++)
        //    {
        //        var joiner = (i == parameters.Count() - 1) ? string.Empty : ",";
        //        constructorContents.Add($"{Indent.Single}{parameters[i]}{joiner}");
        //    }
        //    constructorContents.Add(")");
        //    constructorContents.Add("{");

        //    DiskFileDescriptionLines.Select(l => l.FileObjectID.Name.ToPublicModernName().ToLower()).ToList().ForEach(diskFileName => constructorContents.Add($"{Indent.Single}this.{diskFileName}Repository = {diskFileName}Repository;"));
        //    PrinterFileDescriptionLines.Select(l => l.FileObjectID.Name.ToPublicModernName().ToLower()).ToList().ForEach(printerFileName => constructorContents.Add($"{Indent.Single}this.{(printerFileName.StartsWith("qprint") ? printerFileName : "qprint")} = {printerFileName};"));

        //    if (workstationFileObjectID != null) constructorContents.Add($"{Indent.Single}this.{workstationFileObjectIDPublicModernName.ToLower()}Presenter = {workstationFileObjectIDPublicModernName.ToLower()}Presenter;");
        //    if (IsCalling)
        //    {
        //        constructorContents.Add($"{Indent.Single}this.pgmCaller = pgmCaller;");
        //    }
        //    constructorContents.Add("}");
        //    constructorContents.Add("#endregion constructor");

        //    return constructorContents;
        //}


        //IEnumerable<string> SetParametersContents(List<Variable> parameters)
        //{
        //    var setParametersContents = new List<string>();
        //    if (parameters.Count == 0) return setParametersContents;

        //    setParametersContents.Add("#region parameter");

        //    setParametersContents.Add("public void SetParameters(object[] parameters)");
        //    setParametersContents.Add("{");
        //    for (int i = 0; i < parameters.Count(); i++)
        //    {
        //        setParametersContents.Add($"{Indent.Single}{parameters[i].Name} = ({parameters[i].TypeSpelling})parameters[{i}];");
        //    }
        //    setParametersContents.Add("}");

        //    setParametersContents.Add("public object[] GetParameters()");
        //    setParametersContents.Add("{");
        //    setParametersContents.Add($"{Indent.Single}var parameters = new object[{parameters.Count()}];");
        //    for (int i = 0; i < parameters.Count(); i++)
        //    {
        //        setParametersContents.Add($"{Indent.Single}parameters[{i}] = {parameters[i].Name};");
        //    }
        //    setParametersContents.Add($"{Indent.Single}return parameters;");
        //    setParametersContents.Add("}");
        //    setParametersContents.Add("#endregion parameter");

        //    return setParametersContents;
        //}

    }
}
