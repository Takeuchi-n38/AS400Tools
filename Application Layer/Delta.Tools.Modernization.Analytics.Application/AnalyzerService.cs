using Delta.AS400.DataAreas;
using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Modernization;
using Delta.Tools.AS400.Configs;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.Jobs;
using Delta.Tools.AS400.Libraries;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs;
using Delta.Tools.AS400.Programs.CLs;
using Delta.Tools.AS400.Programs.CLs.Lines;
using Delta.Tools.AS400.Programs.CLs.Lines.Libs;
using Delta.Tools.AS400.Programs.COBOLs;
using Delta.Tools.AS400.Programs.RPGs;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using Delta.Tools.AS400.Structures.Programs;
using Delta.Tools.Modernization.Sources.AS400.Programs.CLs.Lines;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections;
using Delta.AS400.Partitions;


namespace Delta.Tools.Modernization.Analytics
{
    public class AnalyzerService
    {
        DiskFileStructureBuilder DiskFileStructureBuilder;
        ProgramStructureBuilder ProgramStructureBuilder;

        SourceFactoryBuilder SourceFactoryBuilder;
        ISourceFactory DSPSourceFileReader => SourceFactoryBuilder.DSPSourceFileReader();
        ISourceFactory PRTSourceFileReader => SourceFactoryBuilder.PRTSourceFileReader();

        ObjectIDFactory ObjectIDFactory;
        LibraryFactory LibraryFactory => ObjectIDFactory.LibraryFactory;

        List<Library> ReferLibraries => LibraryFactory.Libraries;

        List<ObjectID> CheckedObjectIDs;
        List<DataArea> DataAreas;
        DataArea CreateDataArea(ObjectID dtaaraObjectID)
        {
            var dtaarea = DataAreas.Where(d => d.ObjectID == dtaaraObjectID).FirstOrDefault();
            if (dtaarea != null) return dtaarea;

            return DataArea.Of(dtaaraObjectID);
            //var splitted = dataAreaName.Split('/');
            //if (splitted.Length == 1)
            //{
            //    return DataArea.Of(Library.OfUnKnown(), splitted[0]);
            //}
            //else if (splitted.Length > 1)
            //{
            //    dtaarea = DataAreas.Where(d => d.Name == splitted[1]).FirstOrDefault();
            //    if (dtaarea != null) return dtaarea;

            //    return DataArea.Of(Library.OfUnKnown(splitted[0]), splitted[1]);
            //}
            //else
            //{
            //    throw new NotImplementedException();
            //}
        }

        IAnalyzerConfig AnalyzerConfig;

        PathResolver PathResolver => AnalyzerConfig.PathResolver;

        DirectoryInfo CometSourcesDirectory => AnalyzerConfig.PathResolver.ModernaizationRootDirectory.CreateIfNotExists("CometSources");

        string AnalzeReportOutputFolderPath => Path.Combine(AnalyzerConfig.PathResolver.SolutionFolderPathOfMainLibrary,
    $"Delta.{AnalyzerConfig.PathResolver.MainLibrary.Partition.Name.ToPascalCase()}.{AnalyzerConfig.PathResolver.MainLibrary.Name.ToPascalCase()}.Tools",
    "Analytics", "Reports");

        AnalyzerService(IAnalyzerConfig analyzerConfig)
        {
            AnalyzerConfig = analyzerConfig;

            SourceFactoryBuilder = SourceFactoryBuilder.Of(CometSourcesDirectory.FullName);

            ObjectIDFactory = new ObjectIDFactory(analyzerConfig.LibraryFactory);

            DiskFileStructureBuilder = DiskFileStructureBuilder.Of(ObjectIDFactory, SourceFactoryBuilder, analyzerConfig.ReCreateFileObjectIDs());

            ProgramStructureBuilder = ProgramStructureBuilder.Of(ObjectIDFactory, SourceFactoryBuilder);

            CheckedObjectIDs = analyzerConfig.CheckedObjectIDs();

            DataAreas = analyzerConfig.DataAreas();

        }

        public static AnalyzerService Of(IAnalyzerConfig analyzerConfig)
        {
            return new AnalyzerService(analyzerConfig);
        }

        void WriteAllText(string fileName, IEnumerable<string> contents)
        {
            FileHelper.WriteAllText(AnalzeReportOutputFolderPath, fileName, contents);
        }
        void WriteAllText(string fileName, string contents)
        {
            FileHelper.WriteAllText(AnalzeReportOutputFolderPath, fileName, contents);
        }

        public static void Main(IAnalyzerConfig analyzerConfig)
        {
            Job.Instance.ChangeLibrary(analyzerConfig.LibraryList, analyzerConfig.MainLibrary);

            var analyzerService = Of(analyzerConfig);
            analyzerService.Analize(analyzerConfig.EntryObjectIDs());

            Console.WriteLine("Analyzed.");
        }

        string FileName(ObjectID objectID) => $"{objectID.Library.Partition.Name.ToUpper()}_{objectID.Library.Name.ToUpper()}_{objectID.Name.ToUpper()}";

        public void Analize(List<ObjectID> EntryProgramObjectIDs)
        {
            var programList = new List<string>();
            //programList.Add("0,CL,pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmComment+BlankCount,Dclf");
            programList.Add("Depth,Type,Partition,Library,Name,ActualStep,Comment,Workstn,Disk,Printer");
            //programList.Add("0,else,pgmPartition,pgmLibrary,pgmName,pgmStepCount");
            var programObjects = new List<ObjectID>();
            var programStructures = new List<IStructure>();
            var datas = new Dictionary<ObjectID,string>();
            foreach (var EntryProgramObjectID in EntryProgramObjectIDs)
            {
                var text = new List<string>();

                var actual = ProgramStructureBuilder.Create(EntryProgramObjectID);
                Analyze(text, 0, "-,-,-", "-", actual);
                CreatePgmList(actual, programStructures);

                var crud = new StringBuilder();
                if (text.Count > 0)
                {
                    //                //fileDevice/cmdName,fileFormat/cmdPara,

                    crud.AppendLine("caller_pgmDepth,caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_lineno,pgmDepth,pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmType,objNo,objPartition,objLibrary,objName,cmdName/fileDevice,cmdPara/fileFormat,recordLength/fileStepCount,fileType,C,i,s,u,d,T,D,I,E,type,real_objPartition,real_ objLibrary,real_ objName");
                    text.ForEach(line => 
                    {
                        crud.AppendLine(line);
                        var items = line.Split(',');
                        var obj =  Library.Of(Partition.Of(items[29]), items[30]).ObjectIDOf(items[31]);
                        string? type ;
                        var newType=items[28];
                        if (datas.TryGetValue(obj,out type))
                        {
                            if((type == "N"|| type == "-")&& (newType != "N" || newType != "-"))
                            {
                                datas[obj] = newType;
                            }
                        }
                        else
                        {
                            datas.Add(obj, newType);
                        }
                    }
                    );

                }
                WriteAllText($"CRUD_{EntryProgramObjectID.Library.Partition.Name.ToUpper()}_{EntryProgramObjectID.Library.Name.ToUpper()}_{EntryProgramObjectID.Name.ToUpper()}.gen.csv", crud.ToString());

                CallingTable(programObjects, programList,0,actual);

            }

            WriteAllText("Programs.gen.csv", programList);

            var datasCSV=new List<string>();
            datasCSV.Add($"partition,library,name,type");
            datas.Select(item=> $"{item.Key.Library.Partition.Name.ToUpper()},{item.Key.Library.Name.ToUpper()},{item.Key.Name.ToUpper()},{item.Value}").Distinct().OrderBy(item => item)
                .ToList().ForEach(item=> datasCSV.Add(item));
            WriteAllText("Datas.gen.csv", datasCSV);

            var clStructures = programStructures.Where(pgm => pgm is CLStructure).Cast<CLStructure>();
            OutputUnKnownCLCommand(clStructures);

            OutputNotReferLibraries(clStructures.SelectMany(cl => cl.Elements));

            OutputNotFoundProgramSources(programStructures.Where(pgm => pgm.OriginalSource.IsNotFound).Select(pgm => pgm.OriginalSource.ObjectID));

            var crtPfObjectIDs = clStructures.SelectMany(cl => cl.Elements).Where(line => line is CrtpfLine).Cast<CrtpfLine>()
            .Where(line => line.Rcdlen != string.Empty).Select(item => item.FileObjectID);

            OutputCreatePFs(clStructures.SelectMany(cl => cl.Elements).Where(line => line is CrtpfLine).Cast<CrtpfLine>());

            var InternalFileObjectIDs = programStructures.Where(pgm => pgm is RPGStructure).Cast<RPGStructure>().SelectMany(rpg => rpg.InternalFileObjectID);

            var norFoundSourceObjectIDonCLs = clStructures
            .SelectMany(cl => cl.Elements)
            .SelectMany(line => DiskFileStructureBuilder.CreateFilesOperateStatement(line))
            .Where(structure => structure.OriginalSource.IsNotFound)
            .Select(s => s.OriginalSource.ObjectID)
            .Where(o => !InternalFileObjectIDs.Contains(o) && !crtPfObjectIDs.Contains(o));

            OutputNotFoundFilesOnCL(norFoundSourceObjectIDonCLs);

            var external = programStructures.Where(pgm => pgm is RPGStructure).Cast<RPGStructure>()
            .SelectMany(rpg => rpg.ExternalDiskFileDescriptionLines)
            .Select(line => DiskFileStructureBuilder.Create(line.FileObjectID));

            OutputExternalFormatFiles(external);


            OutputInternalFormatFiles(InternalFileObjectIDs);

            WriteAllText("AutostartLines.csv", OutputFindoutLines<AutostartLine>(clStructures).ToString());
            WriteAllText("SbmjobLines.csv", OutputFindoutLines<SbmjobLine>(clStructures).ToString());
            WriteAllText("SbmrmtcmdLines.csv", OutputFindoutLines<SbmrmtcmdLine>(clStructures).ToString());
            WriteAllText("FtpLines.csv", OutputFindoutLines<FtpLine>(clStructures).ToString());
            WriteAllText("SndbrkmsgLines.csv", OutputFindoutLines<SndbrkmsgLine>(clStructures).ToString());
            WriteAllText("SndmsgLines.csv", OutputFindoutLines<SndmsgLine>(clStructures).ToString());
            WriteAllText("FmtdtaLines.csv", OutputFindoutLines<FmtdtaLine>(clStructures).ToString());
        }

        void CreatePgmList(IStructure structure, List<IStructure> structures)
        {
            if (structures.Count(pgm => pgm.OriginalSource.ObjectID.Equals(structure.OriginalSource.ObjectID)) > 0) return;

            structures.Add(structure);

            if (structure is CLStructure cl)
            {
                CLCallerPrograms(cl)
                    //.Where(pgm => pgm is ProgramStructure).Cast<ProgramStructure>()
                    .ToList().ForEach(pgm => CreatePgmList(pgm, structures));
            }
            else
            if (structure is RPGStructure rpg)
            {
                RPGCallerPrograms(rpg)
                    //.Where(pgm => pgm is ProgramStructure).Cast<ProgramStructure>()
                    .ToList().ForEach(pgm => CreatePgmList(pgm, structures));
            }
            else
            if (structure is COBOLStructure cobol)
            {
                return;
            }
            //else
            //{
            //    throw new NotImplementedException();
            //}
        }
        //void OutputNotFoundSourcesList(IEnumerable<NotFoundSourceStructure> pgms)
        //{
        //    var CLs = new StringBuilder();
        //    CLs.AppendLine("pgmPartition,pgmLibrary,pgmName");
        //    pgms
        //    .Select(cl => $"{cl.Description}").Distinct()
        //    .OrderBy(item => item)
        //    .ToList().ForEach(
        //        item => CLs.AppendLine(item)
        //        );
        //    WriteAllText("CLs.csv", CLs.ToString());
        //}

        void OutputNotFoundProgramSources(IEnumerable<ObjectID> pgms)
        {
            var notFounds = new StringBuilder();

            pgms
            .Where(pgm => !CheckedObjectIDs.Contains(pgm))
            .Select(pgm => $"{ClassificationToStringWithComma(pgm)}").Distinct().OrderBy(item => item)
            .Distinct()
            .ToList().ForEach(item =>
            {
                notFounds.AppendLine(item);
            }
            );

            WriteAllText("NotFoundProgramSources.csv", notFounds.ToString());
        }

        void OutputProgramsList(IEnumerable<ProgramStructure> pgms)
        {

            var ProgramInfos = new StringBuilder();

            foreach(var pgm in pgms)
            {
                var line = string.Empty;
                if(pgm is CLStructure cl)
                {
                    
                    //ProgramInfos.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmComment+BlankCount,Dclf");
                    line = $"{cl.OriginalSource.Description.ToUpper()},{cl.CommentCount},{cl.CountOfDCLF}";
                }
                else
                if (pgm is RPGStructure3 rpg3)
                {
                    //RPG3s.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmCommentCount,Workstn,Disk,Printer");
                    line = $"{rpg3.Description}";
                }
                else
                if (pgm is RPGStructure4 rpg4)
                {
                    //RPG4s.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmCommentCount,Workstn,Disk,Printer");
                    line = $"{rpg4.Description}";
                }
                else
                if (pgm is COBOLStructure cobol)
                {
                    //COBOLs.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount");
                    line = $"{cobol.OriginalSource.Description}";
                }
                ProgramInfos.AppendLine(line);
            }

            WriteAllText("Programs.csv", ProgramInfos.ToString());
        }

        void OutputCLsList(IEnumerable<CLStructure> pgms)
        {
            var text = new List<string>();
            pgms
            .Select(cl => $"{cl.OriginalSource.Description.ToUpper()},{cl.CommentCount},{cl.CountOfDCLF}").Distinct()
            .OrderBy(item => item)
            .ToList().ForEach(
                item => text.Add(item)
                );

            var CLs = new StringBuilder();
            if (text.Count > 0)
            {
                CLs.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmComment+BlankCount,Dclf");
                text.ForEach(line => CLs.AppendLine(line));
            }

            WriteAllText("CLs.csv", CLs.ToString());
        }

        void OutputRPG3sList(IEnumerable<RPGStructure3> pgms)
        {
            var text = new List<string>();

            pgms
            .Select(rpg => $"{rpg.Description}").Distinct()
            .OrderBy(item => item)
            .ToList().ForEach(
                item => text.Add(item)
                );

            var RPG3s = new StringBuilder();
            if (text.Count > 0)
            {
                RPG3s.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmCommentCount,Workstn,Disk,Printer");
                text.ForEach(line => RPG3s.AppendLine(line));
            }

            WriteAllText("RPG3s.csv", RPG3s.ToString());
        }

        void OutputRPG4sList(IEnumerable<RPGStructure4> pgms)
        {
            var text = new List<string>();

            pgms
            .Select(rpg => $"{rpg.Description}").Distinct()
            .OrderBy(item => item)
            .ToList().ForEach(
                item => text.Add(item)
                );

            var RPG4s = new StringBuilder();
            if (text.Count > 0)
            {
                RPG4s.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmCommentCount,Workstn,Disk,Printer");
                text.ForEach(line => RPG4s.AppendLine(line));
            }

            WriteAllText("RPG4s.csv", RPG4s.ToString());
        }

        void OutputCOBOLsList(IEnumerable<COBOLStructure> pgms)
        {
            var text = new List<string>();

            pgms
            .Select(pgm => $"{pgm.OriginalSource.Description}").Distinct()
            .OrderBy(item => item)
            .ToList().ForEach(
                item => text.Add(item)
                );

            var COBOLs = new StringBuilder();
            if (text.Count > 0)
            {
                COBOLs.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount");
                text.ForEach(line => COBOLs.AppendLine(line));
            }

            WriteAllText("COBOLs.csv", COBOLs.ToString());
        }

        void OutputUnKnownCLCommand(IEnumerable<CLStructure> pgms)
        {
            var commands = new StringBuilder();
            pgms
            .SelectMany(cl => cl.Elements).Where(line => line is CLUnKnownLine).Cast<CLUnKnownLine>()
            .Select(cmd => cmd.Command).Distinct()
            .ToList().ForEach(
                item => commands.AppendLine(item)
                );
            WriteAllText("UnKnownCLCommand.csv", commands.ToString());
        }

        void OutputNotFoundFilesOnCL(IEnumerable<ObjectID> notFoundObjectIDs)
        {
            var notFoundFilesOnCL = new StringBuilder();
            notFoundObjectIDs
            .Select(objectID => $"{string.Join(",", objectID.ToClassification())},0").Distinct().OrderBy(item => item)
            .ToList().ForEach(
                item => notFoundFilesOnCL.AppendLine(item)
                );

            WriteAllText("NotFoundFilesOnCL.csv", notFoundFilesOnCL.ToString());
        }

        void OutputNotReferLibraries(IEnumerable<IStatement> pgms)
        {
            var libraries = new List<Library>();
            pgms
            .Where(line => line is ChgliblLine).Cast<ChgliblLine>()
            .SelectMany(line => line.LIBL)
            .ToList().ForEach(item => libraries.Add(item));

            pgms
            .Where(line => line is AddlibleLine).Cast<AddlibleLine>()
            .Select(line => line.LIB)
            .ToList().ForEach(item => libraries.Add(item));

            pgms
            .Where(line => line is ChgcurlibLine).Cast<ChgcurlibLine>()
            .Select(line => line.CURLIB)
            .ToList().ForEach(item => libraries.Add(item));

            var libraryDisctipions = new StringBuilder();

            libraries.Where(l => !ReferLibraries.Contains(l)).Select(lib => string.Join(",", lib.ToClassification())).Distinct().OrderBy(s => s)
            .ToList().ForEach(
                item => libraryDisctipions.AppendLine(item)
                );

            WriteAllText("NotReferLibraries.csv", libraryDisctipions.ToString());
        }
        void OutputCreatePFs(IEnumerable<CrtpfLine> pgms)
        {
            var rcdlens = new StringBuilder();
            pgms
            .Where(line => line.Rcdlen != string.Empty).Select(item => $"{ReplaceLibraryName(item.File)},{item.Rcdlen}").OrderBy(line => line)
            .ToList().ForEach(item => rcdlens.AppendLine(item));
            WriteAllText("CreatePFbyRcdlens.csv", rcdlens.ToString());

            var srcs = new StringBuilder();
            pgms
            .Where(line => line.Rcdlen == string.Empty).Select(item => $"{ReplaceLibraryName(item.File)},{ReplaceLibraryName(item.Srcfile)},{item.Srcmbr}").OrderBy(line => line)
            .ToList().ForEach(item => srcs.AppendLine(item));
            WriteAllText("CreatePFbySrcs.csv", srcs.ToString());
        }

        string ReplaceLibraryName(string original)
        {
            var sepIndex = original.IndexOf('/');
            if (sepIndex == -1) return original;

            return $"{LibraryFactory.Create(original.Substring(0, sepIndex)).Name.ToUpper()}{original.Substring(sepIndex)}";

        }

        ObjectID? ObjectIDby(string original)
        {
            var sepIndex = original.IndexOf('/');
            if (sepIndex == -1) return null;

            return LibraryFactory.Create(original.Substring(0, sepIndex)).ObjectIDOf(original.Substring(sepIndex));

        }

        void OutputExternalFormatFiles(IEnumerable<IStructure> pgms)
        {
            var notFoundEFiles = new StringBuilder();

            pgms
            .Where(file => file.OriginalSource.IsNotFound)
            .Select(file => $"{file.OriginalSource.Description}").Distinct().OrderBy(item => item)
            .ToList().ForEach(item =>
            {
                notFoundEFiles.AppendLine(item);
            }
            );

            WriteAllText("NotFoundExternalFormatFiles.csv", notFoundEFiles.ToString());

            var FoundEFiles = new StringBuilder();
            pgms
            .Where(file => !file.OriginalSource.IsNotFound)
            .Select(file => $"{file.OriginalSource.Description}").Distinct().OrderBy(item => item)
            .ToList().ForEach(item =>
            {
                FoundEFiles.AppendLine(item);
            }
            );

            WriteAllText("FoundedExternalFormatFiles.csv", FoundEFiles.ToString());
        }

        void OutputInternalFormatFiles(IEnumerable<ObjectID> InternalFileObjectIDs)
        {
            var contents = new StringBuilder();

            InternalFileObjectIDs
            .Select(line => string.Join(",", line.ToClassification())).Distinct().OrderBy(item => item)
            .ToList().ForEach(item =>
            {
                contents.AppendLine(item);
            }
            );

            WriteAllText("InternalFormatFiles.csv", contents.ToString());
        }


        //void OutputOperationExtenders3(List<ProgramStructure> pgms)
        //{
        //    var OperationExtenders = new StringBuilder();
        //    pgms.Where(pgm => pgm is RPGStructure3).Cast<RPGStructure3>()
        //    .SelectMany(rpg => rpg.CalculationBlock.AllElements).Where(line => line is CalculationLine && !(line is CalculationCommentLine)).Cast<CalculationLine>()
        //    .Select(line => line.OperationExtender).Distinct().OrderBy(item => item)
        //    .ToList().ForEach(
        //        item => OperationExtenders.AppendLine(item)
        //        );
        //    System.IO.File.WriteAllText($"{OutputFolderPath}OperationExtenders3.csv", OperationExtenders.ToString());
        //}

        //void OutputOperationExtenders4(List<ProgramStructure> pgms)
        //{ 
        //    var OperationExtenders = new StringBuilder();
        //    pgms.Where(pgm => pgm is RPGStructure4).Cast<RPGStructure4>()
        //    .SelectMany(rpg => rpg.CalculationBlock.AllElements).Where(line => line is CalculationLine && !(line is CalculationCommentLine)).Cast<CalculationLine>()
        //    .Select(line => line.OperationExtender).Distinct().OrderBy(item => item)
        //    .ToList().ForEach(
        //        item => OperationExtenders.AppendLine(item)
        //        );
        //    System.IO.File.WriteAllText($"{OutputFolderPath}OperationExtenders4.csv", OperationExtenders.ToString());
        //}
        void Analyze(List<string> text, int callerLevel, string callerObjectIDDescription, string callerLineIndex, IStructure Structure)
        {
            var callerDescription = $"{callerLevel},{callerObjectIDDescription},{callerLineIndex}";
            //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_linen
            //testTargetType,useFor,real_filePartition,real_ fileLibrary,real_ fileName");
            var level = callerLevel + 1;

            if (Structure is NotFoundSourceStructure)
            {
                text.Add($"{callerDescription},{level},{Structure.OriginalSource.Description},NotFoundSource,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-");
                return;
            }
            else
            if (Structure is COBOLStructure)
            {
                text.Add($"{callerDescription},{level},{Structure.OriginalSource.Description},COBOL,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-");
                return;
            }
            else
            if (Structure is CLStructure)
            {
                Analyze(text, callerDescription, level, (CLStructure)Structure);
                return;
            }
            else
            if (Structure is RPGStructure)
            {
                AnalyzeRPG(text, callerLevel, callerObjectIDDescription, callerLineIndex, (RPGStructure)Structure);
                return;
            }
            else
            {
                throw new InvalidOperationException(Structure.Description);
            }
        }


        void CallingTable(List<ObjectID> listed, List<string> text, int callerLevel, IStructure Structure)
        {
            if(listed.Contains(Structure.OriginalSource.ObjectID)) return;

            listed.Add(Structure.OriginalSource.ObjectID);

            //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_linen
            //testTargetType,useFor,real_filePartition,real_ fileLibrary,real_ fileName");
            var level = callerLevel + 1;

            if (Structure is NotFoundSourceStructure)
            {
                text.Add($"{level},NotFoundSource,{Structure.OriginalSource.Description}");
                return;
            }
            else
            if (Structure is COBOLStructure)
            {
                //        //COBOLs.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount");
                //        line = $"{cobol.OriginalSource.Description}";

                text.Add($"{level},CB,{Structure.OriginalSource.Description}");
                return;
            }
            else
            if (Structure is CLStructure cl)
            {
                //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_lineno,
                //pgmDepth,
                //pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmType,
                //ProgramInfos.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmComment+BlankCount,Dclf");
                //        line = $"{cl.OriginalSource.Description.ToUpper()},{cl.CommentCount},{cl.CountOfDCLF}";

                text.Add($"{level},CL,{Structure.OriginalSource.Description},{cl.CommentCount},{cl.CountOfDCLF}");

                if (Structure.OriginalSource.ObjectID.Name.Equals("CMENOK")) return;

                cl.ThisCallerProgramLines.ToList()
                .ForEach(pl =>
                {
                    var pgm = CallerProgram(pl);
                    CallingTable(listed,text, level, pgm);
                });

                return;
            }
            else
            if (Structure is RPGStructure rpg)
            {

                text.Add($"{level},R{(rpg is RPGStructure3 ? "3" : "4")},{rpg.Description}");
                //        //RPG3s.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmCommentCount,Workstn,Disk,Printer");
                //        line = $"{rpg3.Description}";
                //        //RPG4s.AppendLine("pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmCommentCount,Workstn,Disk,Printer");
                //        line = $"{rpg4.Description}";

                rpg.RPGCallLines.ForEach(l =>
                {
                    var objectID = ObjectIDFactory.Create(rpg.OriginalSource.ObjectID.Library, l.ProgramName);
                    var pgm = ProgramStructureBuilder.Create(objectID);
                    //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_linen
                    CallingTable(listed,text, level, pgm);
                });

                return;
            }
            else
            {
                throw new InvalidOperationException(Structure.Description);
            }

            //var ProgramInfos = new StringBuilder();

            //foreach (var pgm in pgms)
            //{
            //    var line = string.Empty;
            //    if (pgm is CLStructure cl)
            //    {

            //    }
            //    else
            //    if (pgm is RPGStructure3 rpg3)
            //    {
            //    }
            //    else
            //    if (pgm is RPGStructure4 rpg4)
            //    {
            //    }
            //    else
            //    if (pgm is COBOLStructure cobol)
            //    {
            //    }
            //    ProgramInfos.AppendLine(line);
            //}

        }



        readonly string recordLengthNull = "-";
        readonly string isudTodo = "todo,todo,todo,todo";
        readonly string isudOnlyi = "i,-,-,-";
        readonly string isudOnlys = "-,s,-,-";
        readonly string isudOnlyu = "-,-,u,-";
        readonly string isudOnlyd = "-,-,-,d";
        readonly string isudNull = "-,-,-,-";

        readonly string CisudTDIETodo = "todo,todo,todo,todo,todo,todo,todo,todo,todo";
        readonly string CisudTDIEisudTodo = "-,todo,todo,todo,todo,-,-,-,-";
        readonly string CisudTDIEOnlyC = "C,-,-,-,-,-,-,-,-";
        readonly string CisudTDIEOnlyi = "-,i,-,-,-,-,-,-,-";
        readonly string CisudTDIEOnlys = "-,-,s,-,-,-,-,-,-";
        readonly string CisudTDIEOnlyu = "-,-,-,u,-,-,-,-,-";
        readonly string CisudTDIEonlyT = "-,-,-,-,-,T,-,-,-";
        readonly string CisudTDIEonlyD = "-,-,-,-,-,-,D,-,-";
        readonly string CisudTDIEonlyDC = "C,-,-,-,-,-,D,-,-";
        readonly string CisudTDIEonlyI = "-,-,-,-,-,-,-,I,-";
        readonly string CisudTDIEonlyE = "-,-,-,-,-,-,-,-,E";
        readonly string CisudTDIENull = "-,-,-,-,-,-,-,-,-";
        readonly string CisudTDIEIETodo = "-,-,-,-,-,-,-,todo,todo";

        //CisudTDIE
        string testTargetTypeTodo = "todo";
        string testTargetTypeNone = "N";
        string testTargetTypePF = "PF";
        string testTargetTypeSAVF = "SAVF";
        string testTargetTypeDDM = "DDM";
        string testTargetTypeDDS = "DDS";
        string testTargetTypeDSPF = "DSPF";
        string testTargetTypeFMT = "FMT";
        string testTargetTypeFTP = "FTP";
        string testTargetTypeSRC = "SRC";
        string testTargetTypeNull = "-";
        string testTargetTypeLOG = "LOG";
        string testTargetTypeDataArea = "DTAARA";
        string testTargetTypeReport = "Report todo (HOLD)";

        readonly string realNull = "-,-,-";

        string ClassificationToStringWithComma(ObjectID ObjectID) => string.Join(",", ObjectID.ToClassification());
        string ClassificationToStringWithComma(Library library) => string.Join(",", library.ToClassification());

        void Analyze(List<string> text, string callerDescription, int level, CLStructure CLStructure)
        {
            //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_lineno,
            //pgmDepth,
            //pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmType,
            var prefix = $"{callerDescription},{level},{CLStructure.OriginalSource.Description},CL";

            //int fNumber = 1;

            var tmptext = text.Count;

            CLStructure.Elements.ForEach(line =>
            {
                //fileNo,
                var linePrefix = $"{prefix},{((ILine)line).StartLineIndex}";
                //filePartition,fileLibrary,fileName,
                //fileDevice/cmdName,fileFormat/cmdPara,
                //recordLength/fileStepCount,
                //fileType,
                //testTargetType,N/R/D
                //real_filePartition,real_ fileLibrary,real_ fileName
                //C,R,U,D,

                if (line is ChgdtaaraLine chgdtaaraLine)
                {
                    var dataarea = CreateDataArea(chgdtaaraLine.DtaaraObjectID);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, dataarea.Description, "CHGDTAARA", "DTAARA", recordLengthNull, CisudTDIEOnlyu, testTargetTypeDataArea));
                }
                else
                if (line is ChkobjLine chkobjLine)
                {
                    var fileStructure = DiskFileStructureBuilder.Create(chkobjLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "CHKOBJ", "OBJ", recordLengthNull, CisudTDIEOnlys, testTargetTypePF));
                }
                else
                if (line is ClrpfLine clrpfLine)
                {
                    var fileStructure = DiskFileStructureBuilder.Create(clrpfLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "CLRPFM", "FILE", recordLengthNull, CisudTDIEonlyT, testTargetTypePF));
                }
                else
                if (line is ClrsavfLine clrsavfLine)
                {
                    /*
                     * CLRSAVF    FILE(WKLIB/CSBU11)
                     */
                    var fileStructure = DiskFileStructureBuilder.Create(clrsavfLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "CLRSAVF", "FILE", recordLengthNull, CisudTDIEOnlyu, testTargetTypeSAVF));
                }
                else
                if (line is CpyfLine cpyfLine)
                {
                    var fileStructures = DiskFileStructureBuilder.Create(cpyfLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructures.FromFileStructure.ObjectID), "CPYF", "FROMFILE", recordLengthNull, CisudTDIEOnlys, testTargetTypePF));
                    if (cpyfLine.IsReplace)
                    {
                        text.Add(composeClAnalyzeLinea(linePrefix, 2, ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID), "CPYF", "TOFILE/*REPLACE", recordLengthNull, CisudTDIEonlyDC, testTargetTypePF));
                    }
                    else
                    {
                        text.Add(composeClAnalyzeLinea(linePrefix, 2, ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID), "CPYF", "TOFILE", recordLengthNull, CisudTDIEOnlyC, testTargetTypePF));
                    }
                }
                else
                if (line is CpytoimpfLine cpytoimpfLine)
                {
                    var fileStructures = DiskFileStructureBuilder.Create(cpytoimpfLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructures.FromFileStructure.ObjectID), "CPYTOIMPF", "FILE", recordLengthNull, CisudTDIEOnlys, testTargetTypePF));
                    var targetFileProp= cpytoimpfLine.HaveStmFile?"TOSTMF":"TOFILE";
                    if (cpytoimpfLine.IsReplace)
                    {
                        text.Add(composeClAnalyzeLinea(linePrefix, 2, ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID), "CPYTOIMPF", $"{targetFileProp}/*REPLACE", recordLengthNull, CisudTDIEonlyDC, testTargetTypePF));
                    }
                    else
                    {
                        text.Add(composeClAnalyzeLinea(linePrefix, 2, ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID), "CPYTOIMPF", targetFileProp, recordLengthNull, CisudTDIEOnlyi, testTargetTypePF));
                    }

                }
                else
                if (line is CrtddmfLine crtddmfLine)
                {
                    var fileStructures = DiskFileStructureBuilder.Create(crtddmfLine);
                    text.Add(composeClAnalyzeLine(linePrefix, 1, ClassificationToStringWithComma(fileStructures.FromFileStructure.ObjectID), "CRTDDMF", "FILE", recordLengthNull, CisudTDIEOnlyC, testTargetTypeDDM,ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID)));
                    text.Add(composeClAnalyzeLinea(linePrefix, 2, ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID), "CRTDDMF", "RMTFILE", recordLengthNull, CisudTDIEOnlys, testTargetTypeDDM));
                }
                else
                if (line is CrtlfLine crtlfLine)
                {
                    var fileStructures = DiskFileStructureBuilder.Create(crtlfLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructures.FromFileStructure.ObjectID), "CRTLF", "SRCFILE", recordLengthNull, CisudTDIEOnlys, testTargetTypeDDS));
                    text.Add(composeClAnalyzeLinea(linePrefix, 2, ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID), "CRTLF", "FILE", recordLengthNull, CisudTDIEOnlyC, testTargetTypePF));
                }
                else
                if (line is CrtpfLine crtpfLine1)
                {
                    var fileStructure = DiskFileStructureBuilder.Create(crtpfLine1);
                    var rcdlen = crtpfLine1.Rcdlen == string.Empty ? "-" : crtpfLine1.Rcdlen;

                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "CRTPF", "FILE", rcdlen, CisudTDIEOnlyC, testTargetTypePF));
                }
                else
                if (line is CrtsavfLine crtsavfLine)
                {
                    var fileStructure = DiskFileStructureBuilder.Create(crtsavfLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "CRTSAVF", "FILE", recordLengthNull, CisudTDIEOnlyC, testTargetTypeSAVF));
                }
                else
                if (line is DclfLine dclfLine)
                {
                    var fileStructure = DiskFileStructureBuilder.Create(dclfLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "DCLF", "FILE", recordLengthNull, CisudTDIEOnlys, testTargetTypeDSPF));
                }
                else
                if (line is DltfLine dltfLine)
                {
                    var fileStructure = DiskFileStructureBuilder.Create(dltfLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "DLTF", "FILE", recordLengthNull, CisudTDIEonlyD, testTargetTypePF));
                }
                else
                if (line is FmtdtaLine fmtdtaLine)
                {
                    var inOutFileSrucutres = DiskFileStructureBuilder.Create(fmtdtaLine);
                    var i = 1;
                    inOutFileSrucutres.InFileStructures.ForEach(inFile =>
                    {
                        text.Add(composeClAnalyzeLinea(linePrefix, i++, ClassificationToStringWithComma(inFile.ObjectID), "FMTDTA", "INFILE", recordLengthNull, CisudTDIEOnlys, testTargetTypePF));
                    });
                    text.Add(composeClAnalyzeLinea(linePrefix, i++, ClassificationToStringWithComma(inOutFileSrucutres.OutFileStructure.ObjectID), "FMTDTA", "OUTFILE", recordLengthNull, CisudTDIEOnlyC, testTargetTypePF));
                    text.Add(composeClAnalyzeLinea(linePrefix, i, ClassificationToStringWithComma(inOutFileSrucutres.SrcStructure.ObjectID), "FMTDTA", "SRCFILE/MBR", recordLengthNull, CisudTDIEOnlys, testTargetTypeFMT));
                }
                else
                if (line is FtpLine ftpLine)
                {
                    var libDesc = ClassificationToStringWithComma(CLStructure.ObjectID.Library);

                    text.Add(composeClAnalyzeLine(linePrefix, 1, $"{libDesc},INPUT", "FTP", ftpLine.Rmtsys, recordLengthNull, CisudTDIENull, testTargetTypeFTP, "todo,todo,todo"));
                    text.Add(composeClAnalyzeLine(linePrefix, 2, $"{libDesc},OUT", "FTP", ftpLine.Rmtsys, recordLengthNull, CisudTDIEOnlyi, testTargetTypeLOG, "todo,todo,todo"));
                    text.Add(composeClAnalyzeLine(linePrefix, 3, $"{libDesc},FILE", "FTP", ftpLine.Rmtsys, recordLengthNull, CisudTDIEIETodo, testTargetTypePF, "todo,todo,todo"));
                }
                else
                if (line is MonmsgLine monmsgLine)
                {
                    if (monmsgLine.Command is CrtpfLine crtpfLine)
                    {
                        var fileStructure = DiskFileStructureBuilder.Create(crtpfLine);
                        text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "CRTPF", "FILE", recordLengthNull, CisudTDIEOnlyC, testTargetTypePF));
                    }
                }
                else
                if (line is MvsrcLine mvsrcLine)
                {
                    var fileStructures = DiskFileStructureBuilder.Create(mvsrcLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructures.FromFileStructure.ObjectID), "MVSRC", "FRFILE", recordLengthNull, CisudTDIEOnlys, testTargetTypeTodo));
                    text.Add(composeClAnalyzeLinea(linePrefix, 2, ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID), "MVSRC", "TOFILE", recordLengthNull, CisudTDIEonlyDC, testTargetTypeTodo));
                }
                else
                if (line is OvrdbfLine ovrdbfLine)
                {
                    var fileStructures = DiskFileStructureBuilder.Create(ovrdbfLine);
                    text.Add(composeClAnalyzeLine(linePrefix, 1, ClassificationToStringWithComma(fileStructures.FromFileStructure.ObjectID), "OVRDBF", "FROMFILE", recordLengthNull, CisudTDIENull, testTargetTypeNone, realNull));
                    text.Add(composeClAnalyzeLinea(linePrefix, 2, ClassificationToStringWithComma(fileStructures.ToFileStructure.ObjectID), "OVRDBF", "TOFILE", recordLengthNull, CisudTDIENull, testTargetTypeNone));
                }
                else
                if (line is OvrprtfLine ovrprtfLine)
                {
                    text.Add(composeClAnalyzeLine(linePrefix, 1, ClassificationToStringWithComma(ovrprtfLine.FileObjectID), "OVRPRTF", "FILE", recordLengthNull, CisudTDIENull, testTargetTypeNone, realNull));
                    text.Add(composeClAnalyzeLinea(linePrefix, 2, $"{ClassificationToStringWithComma(ovrprtfLine.FileObjectID.Library)},{ovrprtfLine.OUTQ}.{ovrprtfLine.SPLFNAME}", "OVRPRTF", "OUTQ.SPLFNAME", recordLengthNull, CisudTDIENull, testTargetTypeNone));
                }
                else
                if (line is RgzpfmLine rgzpfmLine)
                {
                    var fileStructure = DiskFileStructureBuilder.Create(rgzpfmLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "RGZPFM", "FILE", recordLengthNull, CisudTDIENull, testTargetTypeNull));
                }
                else
                if (line is RstobjLine rstobjLine)
                {
                    /*
                     RSTOBJ     OBJ(NHSNDFTG NHSNDFTM) SAVLIB(TACHILIB) +
                          DEV(TAP02) SEQNBR(6468) MBROPT(*ALL) +
                          ALWOBJDIF(*ALL) RSTLIB(YAGIWK)
                     */
                    var fileStructure = DiskFileStructureBuilder.Create(rstobjLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "RSTOBJ", "SAVF", recordLengthNull, CisudTDIEOnlys, testTargetTypeSAVF));
                    for(var i = 1; i <= rstobjLine.Obj.Count; i++)
                    {
                        text.Add(composeClAnalyzeLinea(linePrefix, i + 1, ClassificationToStringWithComma(fileStructure.ObjectID), "RSTOBJ", "OBJ", recordLengthNull, CisudTDIEonlyI, testTargetTypePF));
                    }
                }
                else
                if (line is RtvdtaaraLine rtvdtaaraLine)
                {
                    var dataarea = CreateDataArea(rtvdtaaraLine.DtaaraObjectID);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, dataarea.Description, "RTVDTAARA", "DTAARA", recordLengthNull, CisudTDIEOnlys, testTargetTypeDataArea));
                }
                else
                if (line is RtvmbrdLine rtvmbrdLine)
                {
                    var fileStructure = DiskFileStructureBuilder.Create(rtvmbrdLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "RTVMBRD", "FILE", recordLengthNull, CisudTDIEOnlys, testTargetTypePF));
                }
                else
                if (line is RunsqlstmLine runsqlstmLine)
                {
                    var libraryName = runsqlstmLine.Srcfile.Replace("/QSQLSRC", "");
                    var library = ObjectIDFactory.LibraryFactory.Create(libraryName);
                    var objectName = runsqlstmLine.Srcmbr;
                    var objectID = library.ObjectIDOf(objectName);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(objectID), "RUNSQLSTM", "SRCFILE", recordLengthNull, CisudTDIEisudTodo, testTargetTypePF));
                }
                else
                if (line is SavobjLine savobjLine)
                {
                    /*
              SAVOBJ     OBJ(WARIMR BROAD CALEND* PRCALN* PSMSTR* +
      IMDATA* IMMSTR*) LIB(&PRD) DEV(*SAVF) +
      SAVF(WSVFLIB/CIID010BA) SAVACT(*SYSDFN) +
      ACCPTH(*YES) DTACPR(*YES)
 */
                    var fileStructure = DiskFileStructureBuilder.Create(savobjLine);
                    text.Add(composeClAnalyzeLinea(linePrefix, 1, ClassificationToStringWithComma(fileStructure.ObjectID), "SAVOBJ", "SAVF", recordLengthNull, CisudTDIEOnlyC, testTargetTypeSAVF));
                    for (var i = 1; i <= savobjLine.Obj.Count; i++)
                    {
                        text.Add(composeClAnalyzeLinea(linePrefix, i + 1, ClassificationToStringWithComma(fileStructure.ObjectID), "SAVOBJ", "OBJ", recordLengthNull, CisudTDIEonlyE, testTargetTypePF));
                    }

                }

            });

            if (text.Count == tmptext)
            {
                text.Add($"{prefix},0,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-,-");
            }

            if (CLStructure.OriginalSource.ObjectID.Name.Equals("CMENOK")) return;

            CLStructure.ThisCallerProgramLines.ToList()
            .ForEach(pl =>
            {
                var pgm = CallerProgram(pl);
                //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_linen
                Analyze(text, level, ClassificationToStringWithComma(CLStructure.OriginalSource.ObjectID), ((ILine)pl).StartLineIndex.ToString(), pgm);
            });

        }


        string composeClAnalyzeLinea(string linePrefix, int indexNo, string descripiton, string cmdName, string prmName, string recordLength, 
            string CisudTDIE, string testTargetType)
        {
            return composeClAnalyzeLine(linePrefix, indexNo, descripiton, cmdName, prmName, recordLength, CisudTDIE, testTargetType, descripiton);
        }
    //    string composeClAnalyzeLine(string linePrefix, int indexNo, string descripiton, string cmdName, string prmName, string recordLength,
    //string create, string isud, string truncate, string drop, string import, string export, string testTargetType, string realDescription)
    //    {
    //        var CisudTDIE = $"{create},{isud},{truncate},{drop},{import},{export}";
    //        composeClAnalyzeLinea(linePrefix, indexNo, descripiton, cmdName, prmName, recordLength, CisudTDIE,testTargetType, realDescription);
    //    }
            string composeClAnalyzeLine(string linePrefix, int indexNo, string descripiton, string cmdName, string prmName, string recordLength, 
           string CisudTDIE, string testTargetType, string realDescription)
        {
            //caller_pgmDepth,
            //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_lineno,
            //pgmDepth,
            //pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmType,
            //fileNo,

            //filePartition,fileLibrary,fileName,
            //cmdName/fileDevice,cmdPara/fileFormat,
            //recordLength/fileStepCount,
            //fileType,
            //C,
            //i,s,u,d,
            //T,
            //D,
            //I,
            //E,
            //testTargetType,
            //real_filePartition,real_ fileLibrary,real_ fileName
            return $"{linePrefix}{indexNo},{descripiton},{cmdName},{prmName},{recordLength},-,{CisudTDIE},{testTargetType},{realDescription}";
        }


        void AnalyzeRPG(List<string> text, int callerLevel, string callerObjectIDDescription, string callerLineIndex, RPGStructure RPGStructure)
        {
            var callerDescription = $"{callerLevel},{callerObjectIDDescription},{callerLineIndex}";

            var level = callerLevel + 1;
            int fNumber = 1;
            RPGStructure.FileDescriptions.ToList().ForEach(line =>
            {
                Source originalSource;
                string isudMemo;
                string Device;
                var recordLength = "-";
                var testTargetType = string.Empty;
                //string useFor="-";
                var realFileNameDefault = "todo,todo,todo";

                var FileType = line.FileType;

                if (line is DiskFileDescriptionLine line1)
                {
                    if (line1.IsExternalFileFormat)
                    {
                        var file = DiskFileStructureBuilder.Create(line1.FileObjectID);
                        originalSource = file.OriginalSource;
                        realFileNameDefault =ClassificationToStringWithComma(originalSource.ObjectID);
                    }
                    else
                    {
                        originalSource = Source.NullValue(line1.FileObjectID);
                        recordLength = line1.RecordLength;
                    }

                    testTargetType = "PF";

                    switch (FileType)
                    {
                        case "I":
                            if (line1.IsAddableProgramDescriptionFile)
                            {
                                isudMemo = "todo,todo,-,todo";
                                FileType = "IA";
                            }
                            else
                            {
                                isudMemo = "-,s,-,-";
                            }
                            //useFor = "S";
                            break;
                        case "U":
                            isudMemo = isudTodo;
                            //useFor = "B";
                            break;
                        case "O":
                            isudMemo = isudOnlyi;
                            //useFor = "B";
                            break;
                        case "C":
                            throw new ArgumentException();
                        default:
                            throw new ArgumentException();
                    }

                    Device = "D";
                }
                else
                if (line is PrinterFileDescriptionLine line2)
                {
                    if (line2.FileFormat == "E")
                    {
                        originalSource = PRTSourceFileReader.Read(line2.FileObjectID);
                        realFileNameDefault = ClassificationToStringWithComma(originalSource.ObjectID);
                    }
                    else
                    {
                        originalSource = Source.NullValue(line2.FileObjectID); ;
                    }

                    recordLength = line2.RecordLength;
                    testTargetType = "Report todo (HOLD)";
                    //useFor="E";
                    switch (FileType)
                    {
                        case "I":
                            throw new ArgumentException();
                        case "U":
                            throw new ArgumentException();
                        case "O":
                            isudMemo = isudOnlyi;
                            break;
                        case "C":
                            throw new ArgumentException();
                        default:
                            throw new ArgumentException();
                    }

                    Device = "P";
                }
                else
                if (line is WorkstnFileDescriptionLine line3)

                {
                    originalSource = DSPSourceFileReader.Read(line3.FileObjectID);
                    realFileNameDefault = ClassificationToStringWithComma(originalSource.ObjectID);

                    switch (FileType)
                    {
                        case "I":
                            throw new ArgumentException();
                        case "U":
                            throw new ArgumentException();
                        case "O":
                            throw new ArgumentException();
                        case "C":
                            isudMemo = isudNull;
                            break;
                        default:
                            throw new ArgumentException();
                    }

                    Device = "W";
                    testTargetType = "DSPF";
                }
                else
                {
                    //switch (FileType)
                    //{
                    //    case "I":
                    //        return "n,R,n,n";
                    //    case "U":
                    //        return "todo,todo,todo,todo";
                    //    case "O":
                    //        return "C,n,n,n";
                    //    case "C":
                    //        throw new ArgumentException();
                    //    default:
                    //        return "unknown,unknown,unknown,unknown";
                    //}
                    throw new InvalidOperationException();
                }


                //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_lineno,
                //pgmDepth,
                //pgmPartition,pgmLibrary,pgmName,pgmStepCount,pgmType,
                //fileNo,
                //filePartition,fileLibrary,fileName,
                //fileDevice,fileFormat,
                //recordLength/fileStepCount,
                //fileType,
                //testTargetType,
                //real_filePartition,real_ fileLibrary,real_ fileName
                //C,R,U,D,

                text.Add($"{callerDescription},{level},{RPGStructure.OriginalSource.Description},RPG{(RPGStructure is RPGStructure3 ? "3" : "4")},{callerLineIndex}{fNumber},{ClassificationToStringWithComma(originalSource.ObjectID)},{Device},{line.FileFormat},{recordLength},{FileType},-,{isudMemo},-,-,-,-,{testTargetType},{realFileNameDefault}");
                fNumber++;
            });

            //testTargetType,useFor,real_filePartition,real_ fileLibrary,real_ fileName");

            RPGStructure.RPGCallLines.ForEach(l =>
            {
                var objectID = ObjectIDFactory.Create(RPGStructure.OriginalSource.ObjectID.Library, l.ProgramName);
                var pgm = ProgramStructureBuilder.Create(objectID);
                //caller_pgmPartition,caller_pgmLibrary,caller_pgmName,caller_linen
                Analyze(text, level, ClassificationToStringWithComma(RPGStructure.OriginalSource.ObjectID), l.StartLineIndex.ToString(), pgm);
            });

        }

        List<IStructure> RPGCallerPrograms(RPGStructure rPGStructure)
        {
            var rpgCallerPrograms = new List<IStructure>();
            rPGStructure.RPGCallLines.ForEach(l =>
            {
                var objectID = ObjectIDFactory.Create(rPGStructure.OriginalSource.ObjectID.Library, l.ProgramName);
                var pgm = ProgramStructureBuilder.Create(objectID);
                rpgCallerPrograms.Add(pgm);
            });
            return rpgCallerPrograms;
        }

        List<IStructure> CLCallerPrograms(CLStructure clStructure)
        {
            return clStructure.ThisCallerProgramLines.Select(pl => CallerProgram(pl)).ToList();
        }

        IStructure CallerProgram(IStatement line)
        {
            Library Library;
            string ProgramName;
            if (line is AutostartLine)
            {
                Library = ((AutostartLine)line).Library;
                ProgramName = ((AutostartLine)line).ProgramName;
            }
            else
            if (line is CallLine)
            {
                Library = ((CallLine)line).Library;
                ProgramName = ((CallLine)line).ProgramName;
            }
            else
            {
                throw new NotImplementedException();
            }

            var objectID = ObjectIDFactory.Create(Library, ProgramName);
            return ProgramStructureBuilder.Create(objectID);

        }

        StringBuilder OutputFindoutLines<T>(IEnumerable<CLStructure> pgms) where T : CLLine
        {
            var contents = new StringBuilder();

            pgms.SelectMany(p => p.FindLines<T>().Select(line => $"{ClassificationToStringWithComma(p.ObjectID)},{line.StartLineIndex},{line.Value}"))
            .Distinct().OrderBy(item => item)
            .ToList().ForEach(item =>
            {
                contents.AppendLine(item);
            }
            );

            return contents;
        }
    }
}
