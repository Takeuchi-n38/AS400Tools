using Delta.AS400.DataTypes;
using Delta.Tools.CSharp.Statements.Items.Variables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Generator.Statements.Variables
{
    public class VariableFactory
    {

        public static Variable Of(IDataTypeDefinition typeDefinition) => Variable.Of(TypeOfVariableFactory.Of(typeDefinition), typeDefinition.Name.ToPublicModernName());

        public static IEnumerable<Variable> Of(IEnumerable<IDataTypeDefinition> ITypeDefinitions) => ITypeDefinitions.Select(f => Of(f));

    }
}
