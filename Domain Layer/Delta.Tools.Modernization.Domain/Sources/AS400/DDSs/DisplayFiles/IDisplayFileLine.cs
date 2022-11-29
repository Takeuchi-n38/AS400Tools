using Delta.Utilities.Extensions.SystemString;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DisplayFiles
{
    public interface IDisplayFileLine : IDDSLine
    {
        public bool IsNegationOfIndicator => Value.Substring(7, 1).TrimEnd()=="N";

        public string IndicatorNumber => Value.Substring(8, 2).TrimStart();
        public bool HasIndicator => IndicatorNumber != string.Empty;

        public Indicator Indicator
        {
            get
            {
                if(!HasIndicator) throw new InvalidOperationException();
                return new Indicator(IsNegationOfIndicator, int.Parse(IndicatorNumber));
            }
        }

        public bool IsBoth => Usage == "B";

        public bool IsField => (Line != string.Empty && IsOutput) ;

        public bool IsSubFileRecord => Keywords == "SFL";

        public bool IsSubFileControlRecord => Keywords.StartsWith("SFLCTL");

        public string SubFileRecordName => TextClipper.ClipParameter(Keywords, "SFLCTL");

        public bool IsContainsOverlayKeyword => Keywords.Contains("OVERLAY");

        public bool IsContainsProtectKeyword => Keywords.Contains("PROTECT");

        public bool IsContainsCAKeyword(int commandNumber) => Keywords.Contains($"CA{commandNumber:D2}({commandNumber:D2})");
        public bool IsContainsCFKeyword(int commandNumber) => Keywords.Contains($"CF{commandNumber:D2}({commandNumber:D2})");


        public bool IsContainsRolldownKeyword => Keywords.Contains("ROLLDOWN(07)");
        public bool IsContainsRollupKeyword => Keywords.Contains("ROLLUP(08)");


    }
}
/*
 You specify positional entries in the first 44 positions of the data description specifications (DDS) form for display files.

Positional entries for display files (positions 1 through 7)
You can specify the sequence number, the form type, and comments in positions 1 through 7.

Condition for display files (positions 7 through 16)
Positions 7 through 16 are a multiple-field area in which you can specify option indicators.

Type of name or specification for display files (position 17)
You can specify a value in this position to identify the type of name in positions 19 through 28.

Reserved for display files (position 18)
This position does not apply to any file type. Leave this position blank unless you use it for comment text.

Name for display files (positions 19 through 28)
You use these positions to specify record format names and field names.

Reference for display files (position 29)
You can specify R in this position to use the reference function of the IBM® i operating system. This function copies the attributes of a previously defined, named field (called the referenced field) to the field you are defining.

Length for display files (positions 30 through 34)
You must specify a length for each named field unless you are copying the length from a referenced field.

Data type and keyboard shift for display files (position 35)
The entry you make in position 35 is the data type and keyboard shift attribute for display files.

Decimal positions for display files (positions 36 and 37)
You use these positions to specify the decimal placement within a zoned decimal field and also to specify the data type of the field as it appears in your program.

Usage for display files (position 38)
You use this position to specify that a named field is an output-only, input-only, input/output (both), hidden, program-to-system, or message field.

Location for display files (positions 39 through 44)
You use these positions to specify the exact location on the display where each field begins.

You type the keyword entries that define display files in positions 45 through 80 (functions).
 */