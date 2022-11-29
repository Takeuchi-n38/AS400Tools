using Delta.AS400.DataAreas;
using Delta.AS400.DataTypes;
using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Modernization;
using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.AS400.Configs;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.DiskFiles.LFs;
using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.DDSs.FMTs;
using Delta.Tools.AS400.DDSs.PrinterFiles;
using Delta.Tools.AS400.Generator;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.CLs;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Generator.DomainLayer;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.AS400.Jobs;
using Delta.Tools.AS400.Libraries;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs;
using Delta.Tools.AS400.Programs.CLs;
using Delta.Tools.AS400.Programs.CLs.Lines;
using Delta.Tools.AS400.Programs.COBOLs;
using Delta.Tools.AS400.Programs.RPGs;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using Delta.Tools.CSharp.Statements.Comments;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Delta.Tools.Modernization.Generation
{
    public class GeneratorService
    {
        DiskFileStructureBuilder DiskFileStructureBuilder;
        GeneratorFromDspf GeneratorFromDspf;
        StructureBuilder PRTStructureBuilder;
        RPGStructureToCSharper RPGStructureToCSharper;
        CLStructureToCSharper CLStructureToCSharper;
        ServiceGenerator ServiceGenerator;
        FormatterGenerator FormatterGenerator;

        ProgramStructureBuilder ProgramStructureBuilder;

        GeneratorFromDDSofDisk GeneratorFromDDSofDisk;

        DIGenerator DIGenerator;

        Library mainLibrary=>PathResolver.MainLibrary;
        PathResolverForTool PathResolver;
        List<DataArea> DataAreas;
        DataArea CreateDataArea(string dataAreaName)
        {
            var dtaarea = DataAreas.Where(d => d.Name == dataAreaName).FirstOrDefault();
            if (dtaarea != null) return dtaarea;

            var splitted = dataAreaName.Split('/');
            if (splitted.Length == 1)
            {
                return DataArea.Of(Library.OfUnKnown(), splitted[0]);
            }
            else if (splitted.Length > 1)
            {
                dtaarea = DataAreas.Where(d => d.Name == splitted[1]).FirstOrDefault();
                if (dtaarea != null) return dtaarea;
                return DataArea.Of(Library.OfUnKnown(splitted[0]), splitted[1]);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        List<Library> LibrariesOfDB2foriRepository;

        Func<ObjectID,ObjectID,ObjectID> MapRealFile;

        GeneratorService(SourceFactoryBuilder SourceFactoryBuilder, PathResolverForTool PathResolver, LibraryFactory LibraryFactory, List<ObjectID> ReCreateFileObjectIDs,
            List<DataArea> aDataAreas, List<Library> aLibrariesOfDB2foriRepository, Func<ObjectID, ObjectID, ObjectID> aMapRealFile)
        {

            var sourceFactoryBuilder = SourceFactoryBuilder;
            this.PathResolver = PathResolver;

            var ObjectIDFactory = new ObjectIDFactory(LibraryFactory);
            var DSPSourceFileReader = sourceFactoryBuilder.DSPSourceFileReader();

            DiskFileStructureBuilder = DiskFileStructureBuilder.Of(ObjectIDFactory, sourceFactoryBuilder, ReCreateFileObjectIDs);

            GeneratorFromDspf = new GeneratorFromDspf(mainLibrary, PathResolver, DiskFileStructureBuilder, DSPSourceFileReader);

            PRTStructureBuilder = new StructureBuilder(LibraryFactory, sourceFactoryBuilder.PRTSourceFileReader(), PRTStructureFactory.Of());

            ProgramStructureBuilder = ProgramStructureBuilder.Of(ObjectIDFactory, sourceFactoryBuilder);

            RPGStructureToCSharper = new RPGStructureToCSharper(ProgramStructureBuilder, DiskFileStructureBuilder, new GeneratorFromPrtf(DiskFileStructureBuilder), GeneratorFromDspf, PRTStructureBuilder);

            CLStructureToCSharper = new CLStructureToCSharper(ObjectIDFactory, ProgramStructureBuilder);
            //PathResolver.ApplicationFileFolderPath(source.ObjectID)",
            ServiceGenerator = new ServiceGenerator(PathResolver);
            FormatterGenerator = new FormatterGenerator(PathResolver);
            DataAreas = aDataAreas;
            LibrariesOfDB2foriRepository = aLibrariesOfDB2foriRepository;

            GeneratorFromDDSofDisk = new GeneratorFromDDSofDisk(PathResolver, DiskFileStructureBuilder);
            this.MapRealFile = aMapRealFile;

            DIGenerator= DIGenerator.Of(PathResolver);
        }

        public static GeneratorService Of(IGeneratorConfig GeneratorConfig)
        {

            var sourceFactoryBuilder = SourceFactoryBuilder.Of(GeneratorConfig.PathResolver.CometSourcesDirectory.FullName);

            return new GeneratorService(sourceFactoryBuilder, GeneratorConfig.PathResolver, 
                GeneratorConfig.LibraryFactory, GeneratorConfig.ReCreateFileObjectIDs(),
                GeneratorConfig.DataAreas(), GeneratorConfig.LibrariesOfDB2foriRepository(),
                GeneratorConfig.MapRealFile);

        }

        public static void Main(IGeneratorConfig generatorConfig)
        {

            Job.Instance.ChangeLibrary(generatorConfig.LibraryList, generatorConfig.MainLibrary);

            var generatorService = Of(generatorConfig);

            generatorService.Generate(generatorConfig.GenerateCLObjectIDs(),
                generatorConfig.GenerateRPG4ObjectIDs(),
                generatorConfig.GenerateRPG3ObjectIDs(),
                generatorConfig.GenerateCOBOLObjectIDs(),
                generatorConfig.NoGenerateServiceObjectIDs());

            Console.WriteLine("Generated.");
        }

        public void Generate(List<ObjectID> GenerateCLObjectIDs,
            List<ObjectID> GenerateRPG4ObjectIDs,
            List<ObjectID> GenerateRPG3ObjectIDs,
            List<ObjectID> GeneraCOBOLObjectIDs, List<ObjectID> NoGenerateObjectIDs)
        {

            GenerateCls(GenerateCLObjectIDs, NoGenerateObjectIDs);

            var objectIDForDbContexts = new List<(bool isLF, ObjectID objectID)>();
            var objectIDForDB2foris = new List<(bool isLF, ObjectID objectID)>();

            GenerateRpg4s(objectIDForDbContexts, objectIDForDB2foris, GenerateRPG4ObjectIDs, NoGenerateObjectIDs);
            GenerateRpg3s(objectIDForDbContexts, objectIDForDB2foris, GenerateRPG3ObjectIDs, NoGenerateObjectIDs);

            foreach (var cobolObjectID in GeneraCOBOLObjectIDs)
            {
                var cobolSource = (COBOLStructure)ProgramStructureBuilder.COBOLStructureBuilder.Create(cobolObjectID);
                GenerateCOBOL(cobolSource);
            }

            //                var isForDB2= LibrariesOfDB2foriRepository.Contains(pf.ObjectID.Library);

            DIGenerator.CreateDependencyInjector(objectIDForDbContexts, objectIDForDB2foris);

            GeneratePartOfApp(workStationFileIDs, pfDiskFileIDs.Distinct().ToList(), lfDiskFileIDs.Distinct().ToList());
        }

        List<ObjectID> workStationFileIDs = new List<ObjectID>();
        List<ObjectID> pfDiskFileIDs = new List<ObjectID>();
        List<ObjectID> lfDiskFileIDs = new List<ObjectID>();
        void GenerateCls(List<ObjectID> GenerateCLObjectIDs, List<ObjectID> NoGenerateCLObjectIDs)
        {
            foreach (var clObjectID in GenerateCLObjectIDs)
            {
                var clSource = (CLStructure)ProgramStructureBuilder.CLStructureBuilder.Create(clObjectID);
                GenerateCl(clSource, NoGenerateCLObjectIDs);
            }
        }

        void GenerateCl(CLStructure clStructure, List<ObjectID> NoGenerateObjectIDs)
        {

            var contentLines = new List<string>();

            var workstationFileObjectID = clStructure.WorkstationFileObjectID;
            if (workstationFileObjectID != null)
            {
                var outputtedVariables = new List<Variable>();
                var workstationLines = GeneratorFromDspf.CreateContentsForService(workstationFileObjectID, outputtedVariables);
                contentLines.AddRange(workstationLines);
            }

            Source clOriginalSource = clStructure.OriginalSource;
            var NamespaceItems = new List<NamespaceItem>();
            clStructure.DtaaraNames.Where(name => !name.StartsWith("*")).Distinct().ToList().ForEach(name =>
              {
                  var dataArea = CreateDataArea(name);
                  if (!dataArea.Library.Partition.IsUnKnown)
                  {
                      NamespaceItems.Add(NamespaceItemFactory.DeltaOf(dataArea.Library));
                  }
                  else
                  {
                      Console.WriteLine($"not found dtaara:{name} Add Dtareas of ToolConfig");
                  }
              });
            //objectIDsForUsing.AddRange(actual.PFStructures.Select(pf => pf.ObjectID));
            //var DiskFileObjectIDs = new List<ObjectID>();// { ObjectID.Of("SALELIB", "URIHFL"), ObjectID.Of("SALELIB", "CTRLFL"), ObjectID.Of("CDS", "CDMSY") };//TODO:
            //var diskFileNames = DiskFileObjectIDs.Select(diskFileObjectID => diskFileObjectID.Name.ToPublicModernName()).ToList();
            var diskFileNames = new List<DiskFileDescriptionLine>();

            var variables = CLStructureToCSharper.Variables(clStructure);
            List<Variable> parameters = CLStructureToCSharper.Parameters(clStructure);

            CLStructureToCSharper.CalculateMethodBlockStatement(contentLines, parameters, variables, clStructure);

            if (NoGenerateObjectIDs.Contains(clOriginalSource.ObjectID))
            {
                Console.WriteLine($"Skip Generate {clOriginalSource.ObjectID.Name.ToPublicModernName()}Service");
            }
            else
            {
                ServiceGenerator.CLGenerate(clOriginalSource,
                    NamespaceItems.Distinct(),
                    workstationFileObjectID,
                    diskFileNames,
                    parameters, variables, clStructure.IsCalling, clStructure.IsSndpgmmsg, clStructure.IsSndmsg, contentLines);
            }

            //actual.PFStructures.ForEach(pf =>
            //{
            //    IRepositoryGenerator.CreateIRepository(OperandFormatter.Format(pf.ObjectID.Name), pf.ObjectID, pf.KeyTuppleSpels);
            //    RepositoryGenerator.CreateRepository(OperandFormatter.Format(pf.ObjectID.Name), pf.ObjectID, pf.RecordFormatHeaderLine.RecordFormatKeyList.VariableList);
            //});
            //actual.LFStructures.ForEach(lf =>
            //{
            //    IRepositoryGenerator.CreateIRepository(OperandFormatter.Format(lf.FileDifintion.ObjectID.Name), lf.ObjectID, lf.KeyTuppleSpels);
            //    RepositoryGenerator.CreateRepository(OperandFormatter.Format(lf.FileDifintion.ObjectID.Name), lf.ObjectID, lf.RecordFormatHeaderLine.RecordFormatKeyList.VariableList);
            //});


            if (workstationFileObjectID == null)
            {
                ServiceGenerator.GenerateTest(clOriginalSource,
                    mainLibrary,
                    new List<ObjectID>(),
                    workstationFileObjectID,
                    diskFileNames,
                    new List<PrinterFileDescriptionLine>(),
                    parameters, variables, false, null, clStructure.IsCalling, contentLines, LibrariesOfDB2foriRepository);
            }
            else
            {
                workStationFileIDs.Add(workstationFileObjectID);
                GeneratorFromDspf.Generate(true, workstationFileObjectID, clStructure.ObjectID, parameters, clStructure.IsCalling,
                    new List<ObjectID>(), new List<string>(), new List<PrinterFileDescriptionLine>());
            }

            clStructure.FindLines<FmtdtaLine>().Select(l => l.SrcObjectID).ToList().ForEach(SrcObjectID =>
            {
                var structure = DiskFileStructureBuilder.Create(SrcObjectID);
                if (structure is FMTStructure fMTStructure)
                {
                    try
                    {
                        var serviceTest = FormatterTestGenerator.Create(mainLibrary, fMTStructure.OriginalSource.ObjectID);
                        PathResolver.WriteFormatterTestSource(fMTStructure.OriginalSource.ObjectID, serviceTest.FileName, ((ISourceFile)serviceTest).ToSourceContents());

                        FormatterGenerator.Generate(fMTStructure);
                    }
                    catch
                    {
                        Debug.WriteLine($"Error Occured FmtSrc:{structure.Description}");
                    }
                }
                else
                {
                    Debug.WriteLine($"NofFound FmtSrc:{structure.Description}");
                }
            });
        }

        void GenerateRpg4s(List<(bool isLF, ObjectID objectID)> objectIDForDbContexts, List<(bool isLF, ObjectID objectID)> objectIDForDB2foris, List<ObjectID> GenerateRPG4ObjectIDs, List<ObjectID> NoGenerateCLObjectIDs)
        {

            var rpg4Structures = new List<RPGStructure>();
            GenerateRPG4ObjectIDs.ForEach(rpg4 => rpg4Structures.Add((RPGStructure)ProgramStructureBuilder.RPG4StructureBuilder.Create(rpg4)));

            rpg4Structures.ForEach(rpg4 => GenerateRpg(objectIDForDbContexts, objectIDForDB2foris, rpg4, rpg4.IsCalling, NoGenerateCLObjectIDs));

        }

        void GenerateRpg3s(List<(bool isLF, ObjectID objectID)> objectIDForDbContexts, List<(bool isLF, ObjectID objectID)> objectIDForDB2foris, List<ObjectID> GenerateRPG3ObjectIDs, List<ObjectID> NoGenerateCLObjectIDs)
        {
            var rpg3s = new List<RPGStructure>();

            GenerateRPG3ObjectIDs.ForEach(objectID => rpg3s.Add((RPGStructure)ProgramStructureBuilder.RPG3StructureBuilder.Create(objectID)));

            rpg3s.ForEach(rpg3 => GenerateRpg(objectIDForDbContexts, objectIDForDB2foris, rpg3, rpg3.IsCalling, NoGenerateCLObjectIDs));

        }

        void GenerateRpg(List<(bool isLF,ObjectID objectID)> objectIDForDbContexts, List<(bool isLF, ObjectID objectID)> objectIDForDB2foris, RPGStructure rpg, bool IsCalling, List<ObjectID> NoGenerateCLObjectIDs)
        {

            List<PFStructure> PFStructures = new List<PFStructure>();

            List<LFStructure> LFStructures = new List<LFStructure>();

            Source rpgSource = rpg.OriginalSource;
            var objectIDsForUsing = new List<ObjectID>();
            
            //var diskFileNames=new List<string>();
            rpg.ExternalDiskFileDescriptionLines.ToList().ForEach(line =>
            {
                if (line.FileFormat == "E")
                {

                    line.FileObjectID = MapRealFile(rpg.ObjectID,line.FileObjectID);

                    var file = DiskFileStructureBuilder.Create(line.FileObjectID);
                    if (file is LFStructure lFStructure)
                    {
                        LFStructures.Add(lFStructure);
                        if (!objectIDsForUsing.Contains(lFStructure.FileDifintion.ObjectID)) objectIDsForUsing.Add(lFStructure.FileDifintion.ObjectID);

                        if (!PFStructures.Select(p => p.ObjectID).Contains(lFStructure.FileDifintion.ObjectID))
                        {
                            PFStructures.Add(lFStructure.FileDifintion);
                        }
                    }
                    else
                    if (file is PFStructure pFStructure)
                    {
                        if (!PFStructures.Select(p => p.ObjectID).Contains(pFStructure.ObjectID))
                        {
                            PFStructures.Add(pFStructure);
                            if (!objectIDsForUsing.Contains(pFStructure.ObjectID)) objectIDsForUsing.Add(pFStructure.ObjectID);
                        }
                    }
                    line.File = file;
                }
            });

            var diskFileNames = rpg.DiskFileNames.Select(n => n.ToPublicModernName());

            var WorkstationFileObjectID = rpg.WorkstationFileObjectID;
            var DiskFileDescriptionLines = rpg.DiskFileDescriptionLines;
            var PrinterFileDescriptionLines = rpg.PrinterFileDescriptionLines;


            var outputtedVariables = new List<Variable>();

            outputtedVariables.Add(Variable.Of(TypeOfVariable.OfInt(8), "Xdate"));
            outputtedVariables.Add(Variable.Of(TypeOfVariable.OfLong(14), "Time"));

            var outputVariables = new List<Variable>();

            var contentLines = RPGStructureToCSharper.CreateContentLines(rpg, outputVariables, outputtedVariables);

            List<Variable> parameters = new List<Variable>();

            rpg.TypeDefinitions.Select(t => VariableFactory.Of(t)).ToList().ForEach(variable =>
            {
                var addVariable = variable;
                if (outputtedVariables.Count(v => v.Name == variable.Name) > 0)
                {
                    addVariable = outputtedVariables.Find(v => v.Name == variable.Name);
                }
                parameters.Add(addVariable);
            });


            if (NoGenerateCLObjectIDs.Contains(rpgSource.ObjectID))
            {
                Console.WriteLine($"Skip Generate {rpgSource.ObjectID.Name.ToPublicModernName()}Service");
            }
            else
            {
                ServiceGenerator.RPGGenerate(rpgSource, objectIDsForUsing.Select(objectID => NamespaceItemFactory.DeltaOf(objectID)), WorkstationFileObjectID, DiskFileDescriptionLines, PrinterFileDescriptionLines, parameters,
                    outputVariables, rpg.IsExistInzsr, rpg.CycleFileLine,
                    IsCalling, false, false,
                    contentLines);

                rpg.ProgramDescribedInputFiles.ToList().ForEach(programDescribedFile =>
                {
                    ServiceGenerator.GenerateProgramDescribedFileClass(rpg, programDescribedFile);
                });
            }


            PFStructures.ForEach(pf =>
            {
                pfDiskFileIDs.Add(pf.ObjectID);

                var isForDB2 = LibrariesOfDB2foriRepository.Contains(pf.ObjectID.Library);

                GeneratorFromDDSofDisk.GenerateForPF(pf, isForDB2);

                if (isForDB2 && !objectIDForDB2foris.Any(item=>item.objectID.Equals(pf.ObjectID))) objectIDForDB2foris.Add((false,pf.ObjectID));
                if (!isForDB2 && !objectIDForDbContexts.Any(item => item.objectID.Equals(pf.ObjectID))) objectIDForDbContexts.Add((false,pf.ObjectID));

            });

            LFStructures.ForEach(lf =>
            {
                lfDiskFileIDs.Add(lf.ObjectID);

                pfDiskFileIDs.Add(lf.FileDifintion.ObjectID);

                var isForDB2 = LibrariesOfDB2foriRepository.Contains(lf.FileDifintion.ObjectID.Library);

                GeneratorFromDDSofDisk.GenerateForLF(lf, isForDB2);

                if (isForDB2 && !objectIDForDB2foris.Any(item => item.objectID.Equals(lf.ObjectID))) objectIDForDB2foris.Add((true,lf.ObjectID));
                if (!isForDB2 && !objectIDForDbContexts.Any(item => item.objectID.Equals(lf.ObjectID))) objectIDForDbContexts.Add((true,lf.ObjectID));

            });

            if (rpg.WorkstationFileObjectID == null)
            {
                ServiceGenerator.GenerateTest(rpgSource, mainLibrary, objectIDsForUsing, WorkstationFileObjectID, DiskFileDescriptionLines, PrinterFileDescriptionLines, parameters, outputVariables, rpg.IsExistInzsr, rpg.CycleFileLine, IsCalling, contentLines, LibrariesOfDB2foriRepository);
            }
            else
            {
                workStationFileIDs.Add(rpg.WorkstationFileObjectID);
                GeneratorFromDspf.Generate(false, rpg.WorkstationFileObjectID, rpg.ObjectID, parameters, IsCalling, objectIDsForUsing, diskFileNames, PrinterFileDescriptionLines);
            }

        }

        void GenerateCOBOL(COBOLStructure cobolStructure)
        {

            ServiceGenerator.COBOLGenerate(cobolStructure.OriginalSource);

        }

        //void CreateDependencyInjector(List<(bool isLF, ObjectID objectID)> objectIDForDbContexts, List<(bool isLF, ObjectID objectID)> objectIDForDB2foris)
        //{
        //    var contents = DIGenerator.CreateIDependencyInjectorContents(PathResolver.MainLibraryPascalName, objectIDForDbContexts, objectIDForDB2foris);
        //    PathResolver.WriteIDependencyInjectorSource( contents);

        //    contents = DIGenerator.CreatePostgresDependencyInjectorContents(PathResolver.MainLibraryPascalName, objectIDForDbContexts);
        //    PathResolver.WritePostgresDependencyInjectorSource(contents);

        //    contents = DIGenerator.CreateDependencyInjectorContents(PathResolver.MainLibraryPascalName, objectIDForDB2foris);
        //    PathResolver.WriteDependencyInjectorSource(contents);

        //}

        public void GeneratePartOfApp(List<ObjectID> workStationFileIDs, List<ObjectID> pfFileIDs, List<ObjectID> lfFileIDs)
        {

            workStationFileIDs.Select(fileID => NamespaceItemFactory.DeltaOf(fileID).ToUsingLine)
            .Concat(pfFileIDs.Select(fileID => NamespaceItemFactory.DeltaOf(fileID).ToUsingLine))
            .Distinct().OrderBy(s => s).ToList().ForEach(s => Debug.WriteLine(s));

            workStationFileIDs.Select(fileID => fileID.Name.ToPublicModernName()).Distinct().OrderBy(s => s).ToList().ForEach(name => Debug.WriteLine($"containerRegistry.RegisterDialog<{name}View, {name}ViewModel>();"));

            pfFileIDs.Select(fileID => fileID.Name.ToPublicModernName())
            .Concat(lfFileIDs.Select(fileID => fileID.Name.ToPublicModernName()))
            .Distinct().OrderBy(s => s).ToList().ForEach(name => Debug.WriteLine($"containerRegistry.RegisterInstance<I{name}Repository>(new {name}Repository());"));

            //for RegisterTypes in App.xaml.cs
        }

        public void GenerateFromDDSofDisk(ObjectID objectID, bool isForDB2)
        {
            GeneratorFromDDSofDisk.Generate(objectID,isForDB2);
        }

    }
}
