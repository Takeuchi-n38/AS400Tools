using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Statements.Items.Properties
{
    public class PropertyForBindableBaseContentsFactory
    {

        public static IEnumerable<string> Create(Variable Variable)
        {
            string[] contentLines = new string[]
            {
                $"{Variable.TypeSpelling} {Variable.CamelCaseName};",
                $"public {Variable.TypeSpelling} {Variable.Name}",
                "{",
                $"{Indent.Single}get {{ return {Variable.CamelCaseName}; }}",
                $"{Indent.Single}set {{ SetProperty(ref {Variable.CamelCaseName}, value); }}",
                "}",
            };
            return contentLines;
        }

    }
}
