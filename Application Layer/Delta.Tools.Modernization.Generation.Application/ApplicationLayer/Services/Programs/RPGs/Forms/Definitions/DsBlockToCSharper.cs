using Delta.AS400.DataTypes;
using Delta.Tools.AS400.Generator.Statements.Variables;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dss;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.DSs;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.CSharp.Statements.Items.Variables;
using Delta.Tools.Sources.Items;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs.RPGs.Forms.Definitions
{
    public class DsBlockToCSharper
    {
        public static void ToCSharp(DsBlock3 dsBlock3, List<string> CSharpSourceLines, List<Variable> variables)
        {
            var openerLine = dsBlock3.openerLine;
            var Statements = dsBlock3.Statements;

            string dsValueName = (openerLine.FileName == string.Empty)?$"dsValue{openerLine.StartLineIndex}CCSID930Bytes" : openerLine.FileName.ToCSharpOperand();

            int LengthOfValueContainer = (openerLine.ToLocation == string.Empty) ? Statements.Where(e => e is InputItemLine3).Max(e => ((IRPGInputLine)e).Length):int.Parse(openerLine.ToLocation);

            bool IsExistString = Statements.Any(e => e is InputItemLine3 && ((IRPGInputLine)e).DecimalPositions == string.Empty);

            TypeOfVariable TypeOfVariableOfContainer = IsExistString ? TypeOfVariable.OfString(LengthOfValueContainer) : TypeOfVariable.OfInteger(LengthOfValueContainer);

            CSharpSourceLines.Add(openerLine.ToOriginalComment());

            //            CSharpSourceLines.Add($"readonly byte[] CCSID930Bytes = Enumerable.Repeat(CodePage290.ByteOfSpace, {programDescribedFile.MaxOfToLocation}).ToArray();");

            CSharpSourceLines.Add($"byte[] {dsValueName} = Enumerable.Repeat(CodePage290.ByteOfSpace, {LengthOfValueContainer}).ToArray();");

            variables.Add(Variable.Of(TypeOfVariable.OfByteArray(LengthOfValueContainer), dsValueName));


            Statements.ForEach(element => {
                if (element is InputItemLine3 dsItemLine)
                {
                    var variable = VariableFactory.Of(dsItemLine.TypeDefinition);
                    var fromIndex = int.Parse(((IRPGInputLine3)dsItemLine).FromLocation) - 1;
                    var length =((IRPGInputLine)dsItemLine).Length;

                    CSharpSourceLines.Add($"{dsItemLine.ToOriginalComment()}");
                    OutputProp(CSharpSourceLines, variable, 
                        ((IRPGInputLine3)dsItemLine).FromLocationInIntType, 
                        ((IRPGInputLine3)dsItemLine).ToLocationInIntType, 
                        ((IRPGInputLine3)dsItemLine).DecimalPositions, 
                        dsValueName);
                    variables.Add(variable);
                }
                else
                {
                    CSharpSourceLines.Add($"{((ILine)element).ToOriginalComment()}//TODO");
                }

            });
        }

        public static void ToCSharp(DsBlock4 dsBlock4,List<string> CSharpSourceLines, List<Variable> variables)
        {
            IRPGDefinitionLine openerLine = dsBlock4.openerLine;
            List<IStatement> Statements = dsBlock4.Statements;

            string dsValueName = $"dsValue{openerLine.StartLineIndex}";

            int LengthOfValueContainer = Statements.Where(e => e is DsItemLine4).Max(e => int.Parse(((IRPGDefinitionLine4)e).ToPosition_Length));

            bool IsExistString = Statements.Any(e => e is DsItemLine4 && ((IRPGDefinitionLine4)e).DecimalPositions == string.Empty);

            TypeOfVariable TypeOfVariableOfContainer = IsExistString? TypeOfVariable.OfString(LengthOfValueContainer) : TypeOfVariable.OfInteger(LengthOfValueContainer);

            CSharpSourceLines.Add(openerLine.ToOriginalComment());

            CSharpSourceLines.Add($"byte[] {dsValueName} = Enumerable.Repeat(CodePage290.ByteOfSpace, {LengthOfValueContainer}).ToArray();");

            if (openerLine.Name.ToCSharpOperand() != string.Empty)
            {
                var curLine = openerLine;
                var variable = Variable.Of(TypeOfVariableOfContainer, curLine.Name.ToCSharpOperand().ToPublicModernName());
                OutputProp(CSharpSourceLines, variable,
                    1, 
                    LengthOfValueContainer,
                    "0",
                    dsValueName);
                variables.Add(variable);
            }
            else
            if (Statements.Count(e => e is DifinitionBlockItemLine) > 0)
            {
                var curLine = (IRPGDefinitionLine4)Statements.Where(e => e is DifinitionBlockItemLine).First();
                CSharpSourceLines.Add(curLine.ToOriginalComment());
                var variable = Variable.Of(TypeOfVariableOfContainer, curLine.Name.ToCSharpOperand().ToPublicModernName());
                OutputProp(CSharpSourceLines, variable,
                    1,
                    LengthOfValueContainer,
                    "0",
                    dsValueName);
                variables.Add(variable);
            }
            else
            if (Statements.Count(e => e is DsAliasNameItemLine4) == 1)
            {
                var curLine = (IRPGDefinitionLine4)Statements.Where(e => e is DsAliasNameItemLine4).First();
                var variable = Variable.Of(TypeOfVariableOfContainer, curLine.Name.ToCSharpOperand().ToPublicModernName());
                OutputProp(CSharpSourceLines, variable,
                    1,
                    LengthOfValueContainer,
                    "0",
                    dsValueName);
                variables.Add(variable);
            }

            Statements.ForEach(element => {
                if (element is DifinitionBlockItemLine)
                {
                }
                else
                if (element is DsAliasNameItemLine4 dsAliasNameItemLine)
                {
                    CSharpSourceLines.Add($"{dsAliasNameItemLine.ToOriginalComment()}");
                    if (openerLine.Name.ToCSharpOperand() != string.Empty)
                    {
                        var variable = Variable.Of(TypeOfVariableOfContainer, ((IRPGDefinitionLine4)element).Name.ToCSharpOperand().ToPublicModernName());
                        OutputProp(CSharpSourceLines, variable, openerLine.Name.ToCSharpOperand());
                        variables.Add(variable);
                    }
                    else
                    if (Statements.Count(e => e is DifinitionBlockItemLine) > 0)
                    {
                        var variable = Variable.Of(TypeOfVariableOfContainer, ((IRPGDefinitionLine4)element).Name.ToCSharpOperand().ToPublicModernName());
                        OutputProp(CSharpSourceLines, variable, ((IRPGDefinitionLine4)Statements.Where(e => e is DsAliasNameItemLine4).First()).Name.ToCSharpOperand().ToPublicModernName());
                        variables.Add(variable);
                    }
                }
                else
                if (element is DsItemLine4 dsItemLine)
                {
                    var variable = VariableFactory.Of((IDataTypeDefinition)element);
                    var fromIndex = ((IRPGDefinitionLine4)element).FromIndex;
                    var length = int.Parse(((IRPGDefinitionLine4)element).ToPosition_Length) - fromIndex;

                    CSharpSourceLines.Add($"{dsItemLine.ToOriginalComment()}");
                    OutputProp(CSharpSourceLines, variable,
                        int.Parse(((IRPGDefinitionLine4)element).FromPosition),
                        int.Parse(((IRPGDefinitionLine4)element).ToPosition_Length),
                        ((IRPGDefinitionLine4)element).DecimalPositions,
                        dsValueName);
                    variables.Add(variable);
                }
                else
                {
                    CSharpSourceLines.Add($"{((ILine)element).ToOriginalComment()}//TODO");
                }

            });
        }

        static void OutputProp(List<string> CSharpSourceLines, Variable variable, int fromLocation, int toLocation,string DecimalPositions, string dsValueName)
        {
            string modernTypeName = variable.TypeSpelling;
            string name = variable.Name;

            //TODO:else int
            CSharpSourceLines.Add($"{modernTypeName} {name}");
            CSharpSourceLines.Add($"{{");
            CSharpSourceLines.Add($"{Indent.Single}get");
            CSharpSourceLines.Add($"{Indent.Single}{{");

            //var toStr = $"new String({dsValueName}.Skip({fromIndex}).Take({length}).ToArray())";
            //CSharpSourceLines.Add($"{Indent.Couple}return {(modernTypeName == "string" ? toStr : $"{modernTypeName}.Parse({toStr})")};");
            if (variable.OfTypeIsString)
            {
                CSharpSourceLines.Add($"{Indent.Couple}return CodePage930.ToStringFrom({dsValueName}, {fromLocation}, {toLocation});");
            }
            else
            if (variable.OfTypeIsDecimal)
            {
                CSharpSourceLines.Add($"{Indent.Couple}return ZonedDecimal.ToDecimalFrom({dsValueName}, {fromLocation}, {toLocation}, {int.Parse(DecimalPositions)});");
            }
            else
            if (variable.OfTypeIsInteger)
            {
                CSharpSourceLines.Add($"{Indent.Couple}return ({variable.TypeSpelling})ZonedDecimal.ToDecimalFrom({dsValueName}, {fromLocation}, {toLocation});");
            }
            else
            if (variable.OfTypeIsByte)
            {
                CSharpSourceLines.Add($"{Indent.Couple}return {dsValueName}.Skip({fromLocation}-1).Take({toLocation}-({fromLocation} - 1)).ToArray().First();//TODO:byte");
            }
            else
            {
                throw new NotImplementedException();
            }


            CSharpSourceLines.Add($"{Indent.Single}}}");
            CSharpSourceLines.Add($"{Indent.Single}set");
            CSharpSourceLines.Add($"{Indent.Single}{{");

            //CSharpSourceLines.Add($"{Indent.Couple}value.{((modernTypeName == "string") ? $"PadRight({length},' ')" : $"ToString(\"D{length}\")")}.CopyTo(0, {dsValueName}, {fromIndex}, {length});");
            if (variable.OfTypeIsString)
            {
                CSharpSourceLines.Add($"{Indent.Couple}CodePage930.SetBytes(value, {dsValueName}, {fromLocation}, {toLocation});");
            }
            else
            if (variable.OfTypeIsDecimal)
            {
                CSharpSourceLines.Add($"{Indent.Couple}ZonedDecimal.SetUnsignedBytes(value, {dsValueName}, {fromLocation}, {toLocation}, {int.Parse(DecimalPositions)});");
            }
            else
            if (variable.OfTypeIsInteger)
            {
                CSharpSourceLines.Add($"{Indent.Couple}ZonedDecimal.SetUnsignedBytes(value, {dsValueName}, {fromLocation}, {toLocation});");
            }
            else
            if (variable.OfTypeIsByte)
            {
                // dsValue41[1] = value;//TODO:byte
                CSharpSourceLines.Add($"{Indent.Couple}{dsValueName}[{fromLocation}] = value;//TODO:byte");
            }
            else
            {
                throw new NotImplementedException();
            }

            CSharpSourceLines.Add($"{Indent.Single}}}");
            CSharpSourceLines.Add($"}}");

        }

        //static void OutputProp(List<string> CSharpSourceLines, Variable variable, int fromIndex, int length,string dsValueName)
        //{
        //    string modernTypeName = variable.TypeSpelling;
        //    string name = variable.Name;

        //    //TODO:else int
        //    CSharpSourceLines.Add($"{modernTypeName} {name}");
        //    CSharpSourceLines.Add($"{{");
        //    CSharpSourceLines.Add($"{Indent.Single}get");
        //    CSharpSourceLines.Add($"{Indent.Single}{{");

        //    var toStr = $"new String({dsValueName}.Skip({fromIndex}).Take({length}).ToArray())";
        //    CSharpSourceLines.Add($"{Indent.Couple}return {(modernTypeName == "string" ? toStr : $"{modernTypeName}.Parse({toStr})")};");

        //    CSharpSourceLines.Add($"{Indent.Single}}}");
        //    CSharpSourceLines.Add($"{Indent.Single}set");
        //    CSharpSourceLines.Add($"{Indent.Single}{{");

        //    CSharpSourceLines.Add($"{Indent.Couple}value.{((modernTypeName == "string") ? $"PadRight({length},' ')" : $"ToString(\"D{length}\")")}.CopyTo(0, {dsValueName}, {fromIndex}, {length});");

        //    CSharpSourceLines.Add($"{Indent.Single}}}");
        //    CSharpSourceLines.Add($"}}");

        //}

        static void OutputProp(List<string> CSharpSourceLines, Variable variable, string referName)
        {
            string modernTypeName = variable.TypeSpelling;
            string name = variable.Name;

            CSharpSourceLines.Add($"{modernTypeName} {name}");
            CSharpSourceLines.Add($"{{");
            CSharpSourceLines.Add($"{Indent.Single}get");
            CSharpSourceLines.Add($"{Indent.Single}{{");
            CSharpSourceLines.Add($"{Indent.Couple}return {referName};");
            CSharpSourceLines.Add($"{Indent.Single}}}");
            CSharpSourceLines.Add($"{Indent.Single}set");
            CSharpSourceLines.Add($"{Indent.Single}{{");
            CSharpSourceLines.Add($"{Indent.Couple}{referName} = value;");
            CSharpSourceLines.Add($"{Indent.Single}}}");
            CSharpSourceLines.Add($"}}");

        }

        //static string parseOnGet(string modernTypeName, string toStr)
        //{
        //    return modernTypeName == "string"? toStr : $"{modernTypeName}.Parse({toStr})";
        //}

        //static string toStringOnSet(string modernTypeName, int length)
        //{
        //    return (modernTypeName == "string")? $"PadRight({length},' ')" : $"ToString(\"D{length}\")";
        //}

    }
}
