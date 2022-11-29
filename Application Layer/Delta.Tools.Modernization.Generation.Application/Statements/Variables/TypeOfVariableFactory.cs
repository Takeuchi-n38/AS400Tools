using Delta.AS400.DataTypes;
using Delta.Tools.CSharp.Statements.Items.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Generator.Statements.Variables
{
    public class TypeOfVariableFactory
    {
        public static TypeOfVariable Of(IDataTypeDefinition? typeDefinition)
        {

            if (typeDefinition is null) return TypeOfVariable.OfUnknown;

            if (typeDefinition.IsExplicitCharacter) return TypeOfVariable.OfString(typeDefinition.LengthToInt);

            if (typeDefinition.IsBinary) return TypeOfVariable.OfByte(typeDefinition.Length);

            if (typeDefinition.IsUnknownDatetime) return TypeOfVariable.OfDateTime(typeDefinition.Length);

            if (typeDefinition.IsExplicitNumeric)
            {
                var length = typeDefinition.LengthToInt;// typeDefinition.IsPackedDecimal ? typeDefinition.LengthToInt * 2 - 1 : typeDefinition.LengthToInt;
                return (typeDefinition.DecimalPositionsToInt == 0) ?
                        TypeOfVariable.OfInteger(length) :
                    TypeOfVariable.OfDecimal(length, typeDefinition.DecimalPositionsToInt);
            }

            if (typeDefinition.InternalDataType == string.Empty)
            {
                string Length = typeDefinition.Length;

                if (typeDefinition.DecimalPositions == string.Empty)
                {
                    if (typeDefinition.Length == string.Empty)
                    {
                        return TypeOfVariable.OfUnknown;
                    }
                    else
                    {
                        return TypeOfVariable.OfString(typeDefinition.LengthToInt);
                    }
                }
                else
                {
                    var length = typeDefinition.LengthToInt;//typeDefinition.IsPackedDecimal ? typeDefinition.LengthToInt * 2 - 1 : typeDefinition.LengthToInt;

                    return (typeDefinition.DecimalPositionsToInt == 0) ?
                        TypeOfVariable.OfInteger(length) :
                        TypeOfVariable.OfDecimal(length, typeDefinition.DecimalPositionsToInt);
                }
            }
            return TypeOfVariable.OfUnknown;
        }
        /*
            P
            Packed decimal
            S
            Zoned decimal
            B
            Binary
            F
            Floating-point
            A
            Character
            H
            Hexadecimal
            L
            Date
            T
            Time
            Z
            Timestamp
            5
            Binary character
         */
        /*
            Entry keyboard shifts	Meaning	Data type permitted
            Blank	Default	 
            X	Alphabetic only	Character
            A	Alphanumeric shift	Character
            N	Numeric shift	Character or numeric
            S	Signed numeric	Numeric
            Y	Numeric only	Numeric
            W	Katakana (for Japan only)	Character
            I	Inhibit keyboard entry	Character or numeric
            D	Digits only	Character or numeric
            M	Numeric only character	Character
            Data type (see note)	 	 
            F	Floating point	Numeric
            L	Date	 
            T	Time	 
            Z	Timestamp
         */
    }
}
