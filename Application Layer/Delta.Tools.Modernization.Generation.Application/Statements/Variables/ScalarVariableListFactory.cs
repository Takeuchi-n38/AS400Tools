using Delta.AS400.DataTypes;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.DiskFiles.LFs;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.CSharp.Statements.Items.Variables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Generator.Statements.Variables
{
    public class ScalarVariableListFactory
    {
        public static ScalarVariableList OfKey(
            (List<RecordFormatKeyLine> RecordFormatKeys, List<IDDSLine> RecordFormatFields) KeysAndFields,
             IEnumerable<IDataTypeDefinition> typeDefinitions)
        {
            List<RecordFormatKeyLine> RecordFormatKeyNames = KeysAndFields.RecordFormatKeys;
            List<IDDSLine> RecordFormatFields = KeysAndFields.RecordFormatFields;

            var scalarVariableList = new ScalarVariableList();
            RecordFormatKeyNames.ForEach(k => //Variable.Of(TypeOfVariableFactory.Of(k.TypeDefinition), ((IDDSLine)k).Name.ToCSharpOperand())).ToList().ForEach(v =>
                {
                    var Name = k.KeyName;
                    var findedType = typeDefinitions.ToList().Find(f => f.Name == Name);
                    TypeOfVariable tv;
                    if (findedType == null)
                    {
                        var f = RecordFormatFields.ToList().Find(f => f.Name == Name);
                        if(f is RecordFormatSstKeywordLine line)
                        {
                            findedType = typeDefinitions.ToList().Find(f => f.Name == line.TargetFieldName);
                            tv = TypeOfVariableFactory.Of(findedType).Of(line.Length);
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                    else
                    {
                        tv = TypeOfVariableFactory.Of(findedType);
                    }

                    var v = Variable.Of(tv, Name.ToCSharpOperand());

                    if (v.OfTypeIsUnknown || v.TypeLength == -1)
                    {
                        var finded = RecordFormatFields.Find(t => t.Name.ToLower() == v.Name.ToLower());
                        var length = -1;
                        if (finded != null && int.TryParse(finded.Length, out length))
                        {
                        }
                        scalarVariableList.Add(Variable.Of(v.TypeOfVariable.Of(length), v.Name));
                    }
                    else
                    {
                        scalarVariableList.Add(v);
                    }
                });


            if (scalarVariableList.ItemCount == 0)
            {
                scalarVariableList.Add(Variable.Of(TypeOfVariable.OfInt(9), "Id"));
            }

            return scalarVariableList;
        }

    }
}
