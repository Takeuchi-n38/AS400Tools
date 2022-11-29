using Delta.AS400.DataTypes.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Statements.Items.Variables
{
    public class Variable
    {
        public readonly TypeOfVariable TypeOfVariable;

        public string TypeId => TypeOfVariable.Id;

        public string TypeSpelling => TypeOfVariable.Spelling;

        public int TypeLength => TypeOfVariable.Length;
        public int TypeNumberOfDecimalPlaces => TypeOfVariable.NumberOfDecimalPlaces;

        public string TypeInitialValueSpelling => TypeOfVariable.InitialValueSpelling;

        public bool OfTypeIsString => TypeOfVariable.IsString;

        public bool OfTypeIsLong => TypeOfVariable.IsLong;

        public bool OfTypeIsByte => TypeOfVariable.IsByte;

        public bool OfTypeIsInt => TypeOfVariable.IsInt;

        public bool OfTypeIsShort => TypeOfVariable.IsShort;

        public bool OfTypeIsInteger => TypeOfVariable.IsInteger;

        public bool OfTypeIsNumeric => TypeOfVariable.IsNumeric;

        public bool OfTypeIsDecimal => TypeOfVariable.IsDecimal;

        public bool OfTypeIsArray => TypeOfVariable.IsArray;

        public bool OfTypeIsDateTime => TypeOfVariable.IsDateTime;

        public bool OfTypeIsUnknown => TypeOfVariable.IsUnknown;

        public bool OfTypeIsEqual(Variable target) => TypeOfVariable.Equals(target.TypeOfVariable);

        public readonly string Name;
        public string CamelCaseName => Name.ToCamelCase();

        public bool IsStringConst => Name == "string.Empty" || Name.StartsWith("\"") && Name.EndsWith("\"");
        public bool IsIntegerConst => Name.StartsWith("int.M") || int.TryParse(Name, out int temp);

        public bool IsConst => IsStringConst || IsIntegerConst;

        protected Variable(TypeOfVariable typeOfVariable, string name)
        {
            TypeOfVariable = typeOfVariable;
            Name = name;
        }

        protected Variable(Variable variable) : this(variable.TypeOfVariable, variable.Name)
        {
        }

        public Variable Of(string newName)
        {
            return new Variable(TypeOfVariable, newName);
        }
        //public Variable Of(int length)
        //{
        //    return new Variable(new TypeOfScalarVariable(this.TypeOfVariable,length), this.Name);
        //}

        public static Variable Of(TypeOfVariable typeOfVariable, string name) { return new Variable(typeOfVariable, name); }

        //static Variable Of(string typeSpelling, string name) { return Of(TypeOfVariable.Of(typeSpelling), name); }

        public static Variable OfUnknownNameBy(string unknownVariableName)
        {
            if (unknownVariableName == "string.Empty") return Of(TypeOfVariable.OfString(0), unknownVariableName);
            if (unknownVariableName.StartsWith("int.M")) return Of(TypeOfVariable.OfInt(9), unknownVariableName);
            if (unknownVariableName == "0") return Of(TypeOfVariable.OfInt(1), unknownVariableName);
            if (unknownVariableName.StartsWith("In") && unknownVariableName.Length == 4 && int.TryParse(unknownVariableName.Substring(2), out int temp)) return Of(TypeOfVariable.OfString(1), unknownVariableName);
            if (int.TryParse(unknownVariableName, out int temp2)) return Of(TypeOfVariable.OfInt(unknownVariableName.Length), unknownVariableName);

            if (unknownVariableName.StartsWith("\"") && unknownVariableName.EndsWith("\""))
            {
                return Of(TypeOfVariable.OfString(unknownVariableName.Length - 2), unknownVariableName);
            }
            return Of(TypeOfVariable.OfUnknown, unknownVariableName);
        }


        public static string GetCastSpelling(Variable? source, Variable? target)
        {
            if (source == null || target == null || source.OfTypeIsEqual(target)) return string.Empty;

            if (target.TypeOfVariable.IsShort && (source.TypeOfVariable.IsInt || source.TypeOfVariable.IsLong)) return "(short)";
            if (target.TypeOfVariable.IsInt && source.TypeOfVariable.IsLong) return "(int)";
            return string.Empty;
        }

        public (string spelling, string comment) AddCastSpellingWithRightJustified(Variable source)
        {
            return AddCastSpelling(this, source, true);
        }
        public (string spelling, string comment) AddCastSpelling(Variable source)
        {
            return AddCastSpelling(this, source, false);
        }
        static (string spelling, string comment) AddCastSpelling(Variable target, Variable source, bool withRightJustified)
        {

            if (source.Name == "int.MinValue" || source.Name == "int.MaxValue")
            {
                if (target.OfTypeIsString || target.OfTypeIsUnknown)
                {
                    if (source.Name == "int.MinValue")
                    {
                        return ("string.Empty", string.Empty);
                    }
                    else
                    {
                        return ($"new String('9', {target.TypeLength});", string.Empty);
                    }
                }

                if (target.OfTypeIsNumeric)
                {
                    return (source.Name.Replace("int", target.TypeSpelling), string.Empty);
                }

                throw new NotImplementedException();

            }
            if (source.Name == "0")
            {

                if (target.OfTypeIsDecimal || target.OfTypeIsInt || target.OfTypeIsLong)
                {
                    return ("0", string.Empty);
                }
                else
                if (target.OfTypeIsShort)
                {
                    return ("(short)0", string.Empty);
                }
                else
                if (target.OfTypeIsArray)
                {
                }
                else
                if (target.OfTypeIsString)
                {
                    return ("\"0\"", string.Empty);
                }
                else
                {
                    throw new NotImplementedException();
                }

            }
            if (target.TypeId == source.TypeId && target.TypeLength == source.TypeLength && !target.OfTypeIsUnknown) return (source.Name, string.Empty);

            string typesSpelling = $"{target.TypeId}:{target.TypeLength} <-- {source.TypeId}:{source.TypeLength}";

            string castComment = $"//TODO:Convert{(withRightJustified ? "withRightJustified" : string.Empty)} {typesSpelling}";

            if (source.OfTypeIsUnknown)
            {
                if (source.Name.StartsWith("Xall\""))//*ALL'
                {
                    var c = source.Name.Substring(5).Replace("\"", string.Empty);
                    return ($"new string(\'{c}\',{target.TypeLength})",string.Empty);
                }
                else 
                if (target.OfTypeIsString)
                {
                    return ($"{source.Name}.ToString()", $"//TODO:Convert{(withRightJustified ? "withRightJustified" : string.Empty)} {typesSpelling}");
                }

            }

            if (target.OfTypeIsUnknown || source.OfTypeIsUnknown)
            {
                return (source.Name, $"//TODO:Convert{(withRightJustified ? "withRightJustified" : string.Empty)} {typesSpelling}");
            }

            if (target.TypeId == source.TypeId)
            {
                if (source.Name == "string.Empty") return (source.Name, string.Empty);

                if (source.OfTypeIsInt && source.IsIntegerConst) return (source.Name, string.Empty);
                if (source.OfTypeIsString)
                {
                    if (source.IsStringConst) return (source.Name, $"//REVIEW: {typesSpelling}");

                    if (target.TypeLength < source.TypeLength)
                    {
                        if (withRightJustified)
                        {
                            return ($"{source.Name}.PadRight({source.TypeLength}).Substring({source.Name}.Length - {target.TypeLength}).TrimEnd()", $"//REVIEW: {typesSpelling}");
                        }
                        else
                        {
                            return ($"{source.Name}.Substring(0,Math.Min({target.TypeLength}, {source.Name}.Length))", $"//REVIEW: {typesSpelling}");
                        }
                    }
                    else
                    {
                        //if (withRightJustified)
                        //{
                        //    return ($"({target.Name}.PadRight({target.TypeLength - source.TypeLength}).Substring(0,{target.TypeLength - source.TypeLength}) + {source.Name}.PadRight({source.TypeLength}).Substring({source.TypeLength})).TrimEnd()", $"//REVIEW: {typesSpelling}");
                        //}
                        //else
                        //{
                        //    return ($"({source.Name}.PadRight({source.TypeLength}) + {target.Name}.PadRight({source.TypeLength}).Substring({source.TypeLength})).TrimEnd()", $"//REVIEW: {typesSpelling}");
                        //}
                    }
                }

                return (source.Name, castComment);

            }

            string castSpelling = source.Name;

            if (target.OfTypeIsString)
            {
                if(source.OfTypeIsArray && source.TypeOfVariable.ArrayItemType.IsByte )
                {
                    castSpelling = $"CodePage930.ToStringFrom({source.Name})";
                }
                else
                {
                    castSpelling = $"{source.Name}.ToString()";
                }
            }
            else
            if (target.OfTypeIsByte)
            {
                if (source.OfTypeIsShort)
                {
                    return ($"({target.TypeSpelling}){source.Name}", source.IsIntegerConst ? string.Empty : castComment);
                }
            }
            else
            if (target.OfTypeIsNumeric)
            {
                if (source.OfTypeIsString || source.IsStringConst)
                {
                    castSpelling = $"{target.TypeSpelling}.Parse({source.Name})";
                }
                else
                if (source.OfTypeIsDateTime)
                {
                    castSpelling = $"{target.TypeSpelling}.Parse({source.Name}.ToString(\"yyyyMMdd\"))";
                }
                else
                if (source.OfTypeIsByte)
                {
                }
                else
                if (source.OfTypeIsShort)
                {
                }
                else
                if (source.OfTypeIsInt)
                {
                    if (target.OfTypeIsLong || target.OfTypeIsDecimal)
                    {

                    }
                    else
                    if (target.OfTypeIsShort)
                    {
                        return ($"({target.TypeSpelling}){source.Name}", source.IsIntegerConst ? string.Empty : castComment);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                }
                else
                if (source.OfTypeIsLong)
                {
                    if (target.OfTypeIsInt || target.OfTypeIsShort)
                    {
                        castSpelling = $"({target.TypeSpelling}){source.Name}";
                    }
                    else
                    if (target.OfTypeIsDecimal)
                    {

                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                }
                else
                if (source.OfTypeIsDecimal)
                {
                    if (target.OfTypeIsInteger)
                    {
                        castSpelling = $"({target.TypeSpelling})Math.Floor({source.Name})";
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            if (target.OfTypeIsDateTime)
            {
                if (source.OfTypeIsNumeric)
                {
                    castSpelling = $"DateTime.ParseExact({source.Name}.ToString(), \"yyyyMMdd\", null)";
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            if (target.OfTypeIsArray)
            {
                if (source.IsStringConst)
                {
                    castComment = string.Empty;
                    if(target.TypeOfVariable.ArrayItemType.IsByte && source.Name == "string.Empty")
                    {
                        castSpelling = $"{target.Name} = Enumerable.Repeat(CodePage290.ByteOfSpace, {target.Name}.Length).ToArray()";
                    }
                    else
                    {
                        castSpelling = $"for (int i = 0; i < {target.Name}.Length; i++) {target.Name}[i] = {source.Name}";
                    }
                }
                else
                if (source.IsIntegerConst)
                {
                    castComment = string.Empty;
                    castSpelling = $"for (int i = 0; i < {target.Name}.Length; i++) {target.Name}[i] = {source.Name}";
                }
                else
                if (target.TypeOfVariable.ArrayItemType.IsByte && source.OfTypeIsString)
                {
                    castComment=string.Empty;
                    castSpelling = $"CodePage930.ToBytesFrom({source.Name},{target.TypeOfVariable.ArraySize})";
                }
                else
                if (target.TypeOfVariable.ArrayItemType.IsChar || source.OfTypeIsString)
                {
                    castComment = string.Empty;
                    castSpelling = $"{source.Name}.PadRight({target.TypeOfVariable.ArraySize}).ToCharArray()";
                }
                else
                if (target.TypeOfVariable.ArrayItemType.IsLong || source.OfTypeIsLong)
                {
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            return (castSpelling, castComment);
        }

        public (string spelling, string comment) SubCastSpelling(Variable source)
        {
            var target = this;
            
            if (target.TypeId == source.TypeId && target.TypeLength == source.TypeLength && !target.OfTypeIsUnknown) return ($"-{source.Name}", string.Empty);

            string typesSpelling = $"{target.TypeId}:{target.TypeLength} <-- {source.TypeId}:{source.TypeLength}";

            string castComment = $"//TODO:Convert {typesSpelling}";

            if (target.OfTypeIsUnknown || source.OfTypeIsUnknown)
            {
                return (source.Name, castComment);
            }

            if (target.TypeId == source.TypeId)
            {
                return (source.Name, castComment);
            }

            string castSpelling = source.Name;

            if (target.OfTypeIsNumeric)
            {
                if (source.OfTypeIsString || source.IsStringConst)
                {
                    castSpelling = $"{target.TypeSpelling}.Parse({source.Name})";
                }
                else
                if (source.OfTypeIsDateTime)
                {
                    castSpelling = $"{target.TypeSpelling}.Parse({source.Name}.ToString(\"yyyyMMdd\"))";
                }
                else
                if (source.OfTypeIsByte)
                {
                }
                else
                if (source.OfTypeIsShort)
                {
                }
                else
                if (source.OfTypeIsInt)
                {
                    if (target.OfTypeIsLong || target.OfTypeIsDecimal)
                    {

                    }
                    else
                    if (target.OfTypeIsShort)
                    {
                        return ($"({target.TypeSpelling}){source.Name}", source.IsIntegerConst ? string.Empty : castComment);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                }
                else
                if (source.OfTypeIsLong)
                {
                    if (target.OfTypeIsInt || target.OfTypeIsShort)
                    {
                        castSpelling = $"({target.TypeSpelling}){source.Name}";
                    }
                    else
                    if (target.OfTypeIsDecimal)
                    {

                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                }
                else
                if (source.OfTypeIsDecimal)
                {
                    if (target.OfTypeIsInteger)
                    {
                        castSpelling = $"({target.TypeSpelling})Math.Floor({source.Name})";
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }

                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            else
            {
                throw new NotImplementedException();
            }

            return (castSpelling, castComment);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            //if (TypeId == "unknown") return false;

            if (!TypeOfVariable.Equals(((Variable)obj).TypeOfVariable)) return false;

            if (Name != ((Variable)obj).Name) return false;

            return true;

        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            //throw new NotImplementedException();
            return base.GetHashCode();
        }
    }
}
/*
TODO:	あとで追加、修正するべき機能がある。
FIXME:	既知の不具合があるコード。修正が必要。
HACK:	あまりきれいじゃないコード。リファクタリングが必要。
XXX:	危険！動くけどなぜうごくかわからない。
REVIEW:	意図した通りに動くか、見直す必要がある。
OPTIMIZE:	無駄が多く、ボトルネックになっている。
CHANGED:	コードをどのように変更したか。
NOTE:	なぜ、こうなったという情報を残す。
WARNING:	注意が必要。
 
 */