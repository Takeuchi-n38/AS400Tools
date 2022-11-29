using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Modernization.Statements.Items.Namespaces;
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
    public class CLServiceStructureFactory
    {

        internal ClassStructure Create(Library mainLibrary,Source source, IEnumerable<NamespaceItem> namespaceItems,
            ObjectID? workstationFileObjectID,
            IEnumerable<DiskFileDescriptionLine> DiskFileDescriptionLines,
            List<Variable> parameters, 
            List<Variable> variables,
            bool IsCalling, bool IsSndpgmmsg, bool IsSndmsg,
            IEnumerable<string> contentLines)
        {

            var SourceObjectIDPublicModernName= source.ObjectID.Name.ToPublicModernName();

            var serviceClass= new ClassStructure(
            NamespaceItemFactory.DeltaOf(source.ObjectID),true,
            $"{SourceObjectIDPublicModernName}Service",
            "",
            $"{SourceObjectIDPublicModernName}Service",
            "gen.cs"
            );

            var isReactive= workstationFileObjectID != null;

            ServiceStructureFactory.UsingNamespaces(namespaceItems,isReactive,false).ToList().ForEach(namespaceItem => serviceClass.AddUsingNamespace(namespaceItem));

            if (isReactive)
            {
                //serviceClass.AddUsingNamespace(NamespaceItemFactory.ReactiveBindings);
                serviceClass.AddUsingNamespace(NamespaceItemFactory.DeltaOf(workstationFileObjectID));
            }

            serviceClass.AddContentLines(IndicatorContents(isReactive));

            serviceClass.AddContentLines(ServiceStructureFactory.FunctionContents());


            if (IsSndpgmmsg)
            {
                serviceClass.AddContentLine("IPgmMsgSender pgmMsgSender;");
            }
            if (IsSndmsg)
            {
                serviceClass.AddContentLine("IMsgSender msgSender;");
            }
            serviceClass.AddContentLine("IComparer<string> CodePage290Comparator = CodePage290.Comparator;");
            serviceClass.AddContentLine($"I{mainLibrary.Partition.Name.ToPascalCase()}{mainLibrary.Name.ToPascalCase()}DependencyInjector DependencyInjector;");
            if (IsCalling)
            {
                serviceClass.AddContentLine("IPgmCaller pgmCaller => DependencyInjector.PgmCaller;");
            }
            serviceClass.AddContentLines(
                ConstructorContents(mainLibrary, source.ObjectID,workstationFileObjectID, DiskFileDescriptionLines, 
                 IsSndpgmmsg,IsSndmsg));

            serviceClass.AddContentLines(ServiceStructureFactory.VariablesContents(parameters));
            
            serviceClass.AddContentLines(ServiceStructureFactory.SetParametersContents(parameters));

            serviceClass.AddContentLines(MainMethodContents(isReactive));

            serviceClass.AddContentLines(contentLines);

            serviceClass.AddAppendLinesOfEndOfFile(CommentFactory.OriginalLineCommentLines(source.OriginalLines));

            return serviceClass;
        }

        internal static CLServiceStructureFactory Instance = new CLServiceStructureFactory();

        IEnumerable<string> IndicatorContents(bool isReactive)
        {
            var indicatorContents = new List<string>();

            indicatorContents.Add("#region indicators");


            indicatorContents.Add($"IndicatorDictionary IndicatorsForCommandButtons {{ get; }} = new IndicatorDictionary(Enumerable.Range(1, 24).ToArray());");
            Enumerable.Range(1, 24).ToList().ForEach(i=> indicatorContents.Add($"string In{i:D2} {{ get => IndicatorsForCommandButtons.GetStr({i}); set => IndicatorsForCommandButtons.SetStr({i}, value); }}"));

            Enumerable.Range(25, 75).ToList().ForEach(i => {
                if (isReactive)
                {
                    indicatorContents.Add($"public ReactivePropertySlim<bool> ReactiveIn{i} {{ get; }} = new ReactivePropertySlim<bool>();");
                    indicatorContents.Add($"string In{i} {{ get => ReactiveIn{i}.Value ? \"1\":\"0\"; set => ReactiveIn{i}.Value = value==\"1\"; }}");
                }
                else
                {
                    indicatorContents.Add($"string In{i} {{ get; set; }} = \"0\";");
                }
            });

            indicatorContents.Add("IndicatorDictionary Flags = new IndicatorDictionary(Enumerable.Range(1, 9).ToArray());");
            Enumerable.Range(1, 9).ToList().ForEach(i => {
                indicatorContents.Add($"static readonly int H{i} = {i};");
                indicatorContents.Add($"string InH{i} {{ get => Flags.GetStr({i}); set => Flags.SetStr({i}, value); }}");
            });

            if (isReactive)
            {
                indicatorContents.Add("public BooleanNotifier LR { get; } = new BooleanNotifier();");
            }
            else
            {
                indicatorContents.Add($"string LR {{ get; set; }} = \"0\";");
            }

            if (isReactive)
            {
                indicatorContents.AddRange(ServiceStructureFactory.KIndicatorProperties());
            }

            //indicatorContents.Add("static readonly int OF = 125;");//OA から OG、OV
            indicatorContents.Add($"string InOF {{ get; set; }}");
            indicatorContents.Add($"string InOG {{ get; set; }}");

            Enumerable.Range(1, 9).ToList().ForEach(i => indicatorContents.Add($"string InL{i} {{ get; set; }}")); 
            
            Enumerable.Range(1, 8).ToList().ForEach(i => indicatorContents.Add($"string InU{i} {{ get => Retriever.Instance.Job.InU{i}; set => Retriever.Instance.Job.InU{i} = value; }}"));

            indicatorContents.Add("#endregion indicators");

            return indicatorContents;
        }

        IEnumerable<string> ConstructorContents(Library mainLibrary, ObjectID sourceObjectID,
             ObjectID? workstationFileObjectID,
            IEnumerable<DiskFileDescriptionLine> DiskFileDescriptionLines,
            bool IsSndpgmmsg,bool IsSndmsg)
        {

            var workstationFileObjectIDPublicModernName = workstationFileObjectID==null?string.Empty:workstationFileObjectID.Name.ToPublicModernName();

            var constructorContents = new List<string>();
            constructorContents.Add("#region constructor");

            constructorContents.Add($"public {sourceObjectID.Name.ToPublicModernName() + "Service"}("); 
            var parameters = new List<string>();
            DiskFileDescriptionLines.ToList().ForEach(l=>
            {
                parameters.Add(DiskFileDescriptionLineToCSharper.RepositoryTypeAndName(l));
            });

            parameters.Add($"I{mainLibrary.Partition.Name.ToPascalCase()}{mainLibrary.Name.ToPascalCase()}DependencyInjector aDependencyInjector");

            if (workstationFileObjectID != null)
            {
                parameters.Add($"I{workstationFileObjectIDPublicModernName}Presenter {workstationFileObjectIDPublicModernName.ToLower()}Presenter");
            }

            if(IsSndpgmmsg)
            {
                parameters.Add("IPgmMsgSender pgmMsgSender");
            }
            if (IsSndmsg)
            {
                parameters.Add("IMsgSender msgSender");
            }

            for (int i = 0; i < parameters.Count(); i++)
            {
                var joiner = (i == parameters.Count() -1) ? string.Empty : ",";
                constructorContents.Add($"{Indent.Single}{parameters[i]}{joiner}");
            }
            constructorContents.Add(")");
            constructorContents.Add("{");

            DiskFileDescriptionLines.Select(l=>l.FileObjectID.Name.ToPublicModernName().ToLower()).ToList().ForEach(diskFileName => constructorContents.Add($"{Indent.Single}this.{diskFileName}Repository = {diskFileName}Repository;"));

            constructorContents.Add($"{Indent.Single}this.DependencyInjector = aDependencyInjector;");

            if (workstationFileObjectID != null) constructorContents.Add($"{Indent.Single}this.{workstationFileObjectIDPublicModernName.ToLower()}Presenter = {workstationFileObjectIDPublicModernName.ToLower()}Presenter;");

            if (IsSndpgmmsg)
            {
                constructorContents.Add($"{Indent.Single}this.pgmMsgSender = pgmMsgSender;");
            }
            if (IsSndmsg)
            {
                constructorContents.Add($"{Indent.Single}this.msgSender = msgSender;");
            }
            constructorContents.Add("}");
            constructorContents.Add("#endregion constructor");

            return constructorContents;
        }

        IEnumerable<string> MainMethodContents(bool IsReactive)
        {
            var mainMethodContents = new List<string>();
            mainMethodContents.Add("#region Main");

            mainMethodContents.Add("public void Main()");
            mainMethodContents.Add("{");
            mainMethodContents.Add($"{Indent.Single}IndicatorsForCommandButtons.SetOffAll();");
            if (IsReactive)
            {
                mainMethodContents.Add($"{Indent.Single}LR.Value = false;");
            }
            else
            {
                mainMethodContents.Add($"{Indent.Single}LR = \"0\";");
            }


            //string[] KAtoY = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" ,
            //    "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" ,"U", "V", "W", "X", "Y" };
            //KAtoY.ToList().ForEach(k => mainMethodContents.Add($"{Indent.Single}InK{k} = \"0\";"));

            mainMethodContents.Add($"{Indent.Single}InOF = \"0\";");
            mainMethodContents.Add($"{Indent.Single}InOG = \"0\";");

            Enumerable.Range(1, 9).ToList().ForEach(i => mainMethodContents.Add($"{Indent.Single}InH{i} = \"0\";"));
            Enumerable.Range(1, 9).ToList().ForEach(i => mainMethodContents.Add($"{Indent.Single}InL{i} = \"0\";"));

            mainMethodContents.Add($"{Indent.Single}{ServiceStructureFactory.CalculateName}();");
            mainMethodContents.Add("}");

            mainMethodContents.Add("#endregion Main");

            return mainMethodContents;
        }
    }
}
