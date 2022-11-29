using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs
{
    public interface IRPGOutputLine4 : IRPGOutputLine
    {
        //record Positions 7-16 (File Name)
        string IRPGOutputLine.FileName => Value.Substring(6, 10).TrimEnd();
        string IRPGOutputLine.UpdateType => Value.Substring(15, 3);

        //record Position 17 (Type – Program-Described File)
        string IRPGOutputLine.LineNameMark => Value.Substring(16, 1);

        //record Positions 30-39 (EXCEPT Name)
        //field  Positions 30-43 (Field Name)
        string IRPGOutputLine.Name => Value.Substring(29, 10).TrimEnd();


        //Position 44 (Edit Codes)
        string IRPGOutputLine.EditCodes => Value.Substring(43, 1).TrimEnd();

        //field  Positions 47-51 (End Position)
        string IRPGOutputLine.EndPositionInLine => Value.Substring(46, 5).TrimStart();

        //Positions 53-80 (Constant, Edit Word, Data Attributes, Format Name)
        //Constants
        //Edit Words
        //Record Format Name
        string IRPGOutputLine.StaticValue => Value.Substring(52).TrimEnd();

        //record Positions 40-42 (Space Before)
        int IRPGOutputLine.SpaceBefore => ToIntForSpaceAndSkip(Value.Substring(39, 3));
        //record Positions 43-45 (Space After)
        int IRPGOutputLine.SpaceAfter => ToIntForSpaceAndSkip(Value.Substring(42, 3));
        //record Positions 46-48 (Skip Before)
        int IRPGOutputLine.SkipBefore => ToIntForSpaceAndSkip(Value.Substring(45, 3));
        //record Positions 49-51 (Skip After)
        int IRPGOutputLine.SkipAfter => ToIntForSpaceAndSkip(Value.Substring(48, 3));

        static int ToIntForSpaceAndSkip(string original)
        {
            if (original.Trim() == string.Empty) return 0;
            return int.Parse(original.Trim());
        }
    }
}
/*
     OFILEB   D        01 10
     O                         DATA     128
     O                                  108 '    '
     OFILEC   D        01 20
     O                         DATA     128
     O                                  108 '    '
 */
/*
     OQPRINT    E            #HED             03
     O                                           10 'PQEA050'
     O                                           74 'オーダー売上リスト'

     O                       SWPRICE       J    115 
 * */

/*
 record
Positions 16-18 (Program-described Logical Relationship)
Positions 18-20 (Record Addition/Deletion)
Position 18 (Fetch Overflow/Release)
Fetch Overflow
Release
Positions 21-29 (File Record ID Indicators)
Positions 40-51 (Space and Skip)
Positions 40-42 (Space Before)
Positions 43-45 (Space After)
Positions 46-48 (Skip Before)
Positions 49-51 (Skip After)
 

 field
Positions 21-29 (File Field Description Indicators)
Field Names, Blanks, Tables and Arrays
PAGE, PAGE1-PAGE7
*PLACE
User Date Reserved Words
*IN, *INxx, *IN(xx)
Position 44 (Edit Codes)
Position 45 (Blank After)
Position 52 (Data Format)

 */