namespace Delta.AS400.DataTypes
{
    public interface IDataTypeDefinition
    {
        string Name { get; }

        string Length { get; }
        int LengthToInt => int.Parse(Length);

        string InternalDataType { get; }

        string DecimalPositions { get; }

        int DecimalPositionsToInt => DecimalPositions == string.Empty ? 0 : int.Parse(DecimalPositions);

        public string Summary => string.Empty;

        bool IsExplicitCharacter => IsSingleByteCharacter || IsDoubleByteCharacter;

        bool IsSingleByteCharacter => InternalDataType == "A";

        bool IsDoubleByteCharacter => InternalDataType == "O";

        bool IsBinary => InternalDataType == "B";

        bool IsUnknownDatetime => InternalDataType == "D";

        bool IsExplicitNumeric => IsPackedDecimal || IsZonedDecimal || IsUnknownNumeric;

        bool IsExplicitInteger => IsExplicitNumeric && DecimalPositionsToInt==0;

        bool IsExplicitDecimal => IsExplicitNumeric && DecimalPositionsToInt > 0;
        /*
                     if (length > 9) return OfLong(length);
            if (length > 4) return OfInt(length);
            return OfShort(length);

         */
        bool IsPackedDecimal => InternalDataType == "P";

        bool IsZonedDecimal => InternalDataType == "S";

        bool IsUnknownNumeric => InternalDataType == "Y";

        //public TypeDefinition Of(string newName)
        //{
        //    return new TypeDefinition(newName, Length, InternalDataType, DecimalPositions);
        //}
        /*
public static TypeOfVariable Of(string InternalDataType, string DecimalPositions, string Length)
{
    if (InternalDataType == "A" || InternalDataType == "O") return TypeOfVariable.OfString(Length);

    if (InternalDataType == "B") return TypeOfVariable.OfByte(Length);

    if (InternalDataType == "D") return TypeOfVariable.OfDateTime(Length);

    if (InternalDataType == "S" || InternalDataType == "P" || InternalDataType == "Y")
    {
        if (DecimalPositions == string.Empty || int.Parse(DecimalPositions) == 0)
        {
            return TypeOfVariable.OfInteger(int.Parse(Length));
        }
        if (int.Parse(DecimalPositions) > 0) return TypeOfVariable.OfDecimal(Length);
    }
    if (InternalDataType == string.Empty)
    {
        if (DecimalPositions == string.Empty)
        {
            if (Length == string.Empty)
            {
                return TypeOfVariable.OfUnknown;
            }
            else
            {
                return TypeOfVariable.OfString(Length);
            }
        }
        else
        {
            if (int.Parse(DecimalPositions) > 0) return TypeOfVariable.OfDecimal(Length);

            return TypeOfVariable.OfInteger(int.Parse(Length));
        }
    }
    return TypeOfVariable.OfUnknown;
}


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
    }

}
