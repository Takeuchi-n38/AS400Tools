using Delta.Tools.CSharp.Statements.Items.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Statements.Items.Properties
{
    public class PropertyItem
    {
        readonly AccessModifier AccessModifier;
        readonly Variable Variable;

        public PropertyItem(AccessModifier accessModifier, Variable variable)
        {
            AccessModifier = accessModifier;
            Variable = variable;
        }

        public static PropertyItem Of(Variable variable)
        {
            return new PropertyItem(DefaultAccessModifier.instance, variable);
        }

        public static PropertyItem OfInternal(Variable variable)
        {
            return new PropertyItem(InternalAccessModifier.instance, variable);
        }

        public string ToAutoImplementedPropertiesString()
        {
            var prop = $"{Variable.TypeSpelling} {Variable.Name} {{ get; set; }}";
            if (AccessModifier is DefaultAccessModifier) return prop;
            return $"{AccessModifier} {prop}";
        }

        public string ToAutoImplementedPropertiesStringWithIntialValue()
        {
            var prop = $"{Variable.TypeSpelling} {Variable.Name} {{ get; set; }} = {Variable.TypeInitialValueSpelling};";
            if (AccessModifier is DefaultAccessModifier) return prop;
            return $"{AccessModifier} {prop}";
        }
    }
}
