using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.AS400.Sources;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.CSharp.Structures;
using Delta.Tools.Modernization;
using Delta.Tools.Modernization.Generation.ApplicationLayer.Services;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services
{
    public class ServiceGenerator 
    {
        Library mainLibrary=>PathResolver.MainLibrary;
        PathResolverForTool PathResolver;
        RPGServiceStructureFactory RPGServiceStructureFactory;

        public ServiceGenerator(PathResolverForTool PathResolver)
        {
            this.PathResolver = PathResolver;
            RPGServiceStructureFactory = new RPGServiceStructureFactory(PathResolver.MainLibrary);
        }

        public void CLGenerate(Source source, IEnumerable<NamespaceItem> namespaceItems,
            ObjectID? workstationFileObjectID, IEnumerable<DiskFileDescriptionLine> diskFileNames, 
            List<Variable> parameters, List<Variable> variables,  
            bool IsCalling, bool IsSndpgmmsg, bool IsSndmsg, IEnumerable<string> contentLines)
        {
            var service = CLServiceStructureFactory.Instance.Create(mainLibrary, source, namespaceItems, workstationFileObjectID, diskFileNames, 
                 parameters, variables, IsCalling, IsSndpgmmsg ,IsSndmsg, contentLines);

            PathResolver.WriteCLServiceSource(source.ObjectID, service.FileName, ((ISourceFile)service).ToSourceContents());
        }

        public void RPGGenerate(Source source, IEnumerable<NamespaceItem> namespaceItems,
    ObjectID? workstationFileObjectID, IEnumerable<DiskFileDescriptionLine> diskFileNames, IEnumerable<PrinterFileDescriptionLine> PrinterFileDescriptionLines,
    List<Variable> parameters, List<Variable> variables, bool IsExistInzsr, DiskFileDescriptionLine? CycleFileLine,
    bool IsCalling, bool IsSndpgmmsg, bool IsSndmsg, IEnumerable<string> contentLines)
        {

            var service = RPGServiceStructureFactory.Create(source, namespaceItems, workstationFileObjectID, diskFileNames,
                PrinterFileDescriptionLines, parameters, variables, IsExistInzsr, CycleFileLine, IsCalling, IsSndpgmmsg, IsSndmsg, contentLines);

            PathResolver.WriteRPGServiceSource(source.ObjectID, service.FileName, ((ISourceFile)service).ToSourceContents());
        }

        public void COBOLGenerate(Source source)
        {
            var service = COBOLServiceStructureFactory.Instance.Create(mainLibrary, source);

            PathResolver.WriteCOBOLServiceSource(source.ObjectID, service.FileName, ((ISourceFile)service).ToSourceContents());
        }

        public void GenerateProgramDescribedFileClass(RPGStructure rpg, ProgramDescribedInputFile programDescribedFile)
        {
            var source = rpg.OriginalSource;

            string className = $"{programDescribedFile.openerLine.FileName.ToCSharpOperand()}";
            var classStructure = new ClassStructure(
                NamespaceItemFactory.DeltaOf(source.ObjectID),
                true,
                className,
                "",
                className,
                "gen.cs"
                );

            classStructure.AddUsingNamespace(NamespaceItemFactory.System);
            classStructure.AddUsingNamespace(NamespaceItemFactory.SystemLinq);
            classStructure.AddUsingNamespace(NamespaceItemFactory.DeltaAS400DataTypesCharacters);
            classStructure.AddUsingNamespace(NamespaceItemFactory.DeltaAS400DataTypesNumerics);

            int MaxOfToLocation =0;
            if (programDescribedFile.Fields.Count() == 0)
            {
                MaxOfToLocation = int.Parse(rpg.FileDescriptions.Where(f => f.FileName == programDescribedFile.FileName).First().RecordLength);
            }
            else
            {
                MaxOfToLocation = programDescribedFile.MaxOfToLocation;
            }

            var CSharpSourceLines = ProgramDescribedFileToCSharper.GenerateInputCSharpClass(className,programDescribedFile, MaxOfToLocation);

            CSharpSourceLines.ForEach(l =>
            {
                classStructure.AddContentLine(l);
            });

            PathResolver.WriteProgramDescribedFileSource(source.ObjectID, className, ((ISourceFile)classStructure).ToSourceContents());

        }

        public void GenerateTest(Source source, Library mainLibrary, IEnumerable<ObjectID> objectIDsForUsing,
    ObjectID? workstationFileObjectID, IEnumerable<DiskFileDescriptionLine> diskFileNames, IEnumerable<PrinterFileDescriptionLine> PrinterFileDescriptionLines,
    List<Variable> parameters, List<Variable> variables, bool IsExistInzsr, DiskFileDescriptionLine? CycleFileLine, bool IsCalling, IEnumerable<string> contentLines,List<Library> LibrariesOfDB2foriRepository)
        {
            var serviceTest = ServiceTestStructureFactory.Instance.Create(source, mainLibrary, objectIDsForUsing, workstationFileObjectID, diskFileNames.ToList(), PrinterFileDescriptionLines.ToList(), parameters, variables, IsExistInzsr, CycleFileLine, IsCalling, contentLines, LibrariesOfDB2foriRepository);

            PathResolver.WriteServiceTestSource(source.ObjectID, serviceTest.FileName, ((ISourceFile)serviceTest).ToSourceContents());

        }


        static string Version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;

    }
}
