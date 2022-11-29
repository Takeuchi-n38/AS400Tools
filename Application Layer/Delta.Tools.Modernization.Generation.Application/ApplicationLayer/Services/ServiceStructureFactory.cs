using Delta.CSharp.Statements.Items.Namespaces;
using Delta.Modernization.Statements.Items.Namespaces;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services
{
    class ServiceStructureFactory
    {
        internal static string CalculateName => "Calculate";
        internal static IEnumerable<NamespaceItem> UsingNamespaces(
    IEnumerable<NamespaceItem> namespaceItems,
    bool isReactive,bool isPrinting)
        {
            var usingNamespaces = new List<NamespaceItem>();


            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400Adapters);
            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400DDSs);
            if (isPrinting)
            {
                usingNamespaces.Add(NamespaceItemFactory.DeltaAS400DDSsPrinters);
            }
            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400DataTypes);
            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400DataTypesNumerics);

            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400DataTypesCharacters);
            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400Environments);
            usingNamespaces.Add(NamespaceItemFactory.DeltaAS400Indicators);

            if (isReactive)
            {
                usingNamespaces.Add(NamespaceItemFactory.ReactiveBindings);
                usingNamespaces.Add(NamespaceItemFactory.ReactiveBindingsNotifiers);
            }

            usingNamespaces.Add(NamespaceItemFactory.System);
            usingNamespaces.Add(NamespaceItemFactory.SystemLinq);
            usingNamespaces.Add(NamespaceItemFactory.SystemCollectionsGeneric);

            namespaceItems.ToList().ForEach(namespaceItem => usingNamespaces.Add(namespaceItem));
            return usingNamespaces;

        }
        internal static IEnumerable<NamespaceItem> UsingNamespaces()
        { 
            return UsingNamespaces(new List<NamespaceItem>(),false,false);
        }

        internal static IEnumerable<string> FunctionContents()
        {
            var functionContents = new List<string>();

            functionContents.Add("#region functions");
            functionContents.Add("int Xdate => Retriever.Instance.Job.Xdate;");
            functionContents.Add("long Time => Retriever.Instance.System.Time;");
            functionContents.Add("string Xlda=>Retriever.Instance.DataAreaSingleValues.Lda;");
            functionContents.Add("#endregion functions");

            return functionContents;
        }

        internal static IEnumerable<string> VariablesContents(List<Variable> Variables)
        {
            var mainMethodContents = new List<string>();
            Variables
                .Select(v => $"{v.TypeSpelling} {v.Name} {{ get; set; }} = {v.TypeInitialValueSpelling};")
                .Distinct().ToList()
                .ForEach(v => mainMethodContents.Add(v));
            return mainMethodContents;
        }

        internal static IEnumerable<string> SetParametersContents(List<Variable> parameters)
        {
            var setParametersContents = new List<string>();
            if (parameters.Count == 0) return setParametersContents;

            setParametersContents.Add("#region parameter");

            setParametersContents.Add("public void SetParameters(object[] parameters)");
            setParametersContents.Add("{");
            for (int i = 0; i < parameters.Count(); i++)
            {
                setParametersContents.Add($"{Indent.Single}{parameters[i].Name} = ({parameters[i].TypeSpelling})parameters[{i}];");
            }
            setParametersContents.Add("}");

            setParametersContents.Add("public object[] GetParameters()");
            setParametersContents.Add("{");
            setParametersContents.Add($"{Indent.Single}var parameters = new object[{parameters.Count()}];");
            for (int i = 0; i < parameters.Count(); i++)
            {
                setParametersContents.Add($"{Indent.Single}parameters[{i}] = {parameters[i].Name};");
            }
            setParametersContents.Add($"{Indent.Single}return parameters;");
            setParametersContents.Add("}");
            setParametersContents.Add("#endregion parameter");

            return setParametersContents;
        }

        internal static List<string> KIndicatorProperties()
        {
            var contents = new List<string>();
            string[] KAtoY = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" ,
                "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" ,"U", "V", "W", "X", "Y" };
            for (var i = 1; i <= 24; i++)
            {
                contents.Add($"string InK{KAtoY[i - 1]} {{ get => IndicatorsForCommandButtons.GetStr({i}); set => IndicatorsForCommandButtons.SetStr(4, value); }}");
            }
            return contents;
        }
    }
}
