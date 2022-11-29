using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DiskFiles
{
    public interface IDiskFileLine : IDDSLine
    {
        public bool IsKeyLine => HaveName && TypeOfNameOrSpecification == "K";

    }
}
/*
 Sequence number for physical and logical files (positions 1 through 5)
You can use the first five positions to indicate the sequence number for each line on the form. The sequence number is optional and is used for documentation purposes only.

Form type for physical and logical files (position 6)
You type an A in this position to designate this as a DDS form. The form type is optional and is for documentation purposes only.

Comment for physical and logical files (position 7)
You type an asterisk (*) in this position to identify this line as a comment, and then use positions 8 through 80 for comment text.

Condition for physical and logical files (positions 8 through 16)
These positions do not apply to physical or logical files. Leave these positions blank unless you use them for comment text.

Type of name or specification for physical and logical files (position 17)
For physical files, type a value in this position to identify the type of name. For logical files, type a value to identify the type of specification. If you specify a name type, the name is specified in positions 19 through 28.

Reserved for physical and logical files (position 18)
This position does not apply to any file type. Leave this position blank unless you use it for comment text.

Name for physical and logical files (positions 19 through 28)
You use these positions to specify record or field names.

Reference for physical and logical files (position 29)
You use this position to specify reference for physical files only.

Length for physical and logical files (positions 30 through 34)
You use these positions to specify the length of a physical or logical file field.

Data type for physical and logical files (position 35)
For a physical file, you use this position to specify the data type of the field within the database. You specify data type in a logical file only to override or change the data type of the corresponding field in the physical file on which this logical file is based.

Decimal positions for physical and logical files (positions 36 and 37)
You use these positions to specify the decimal placement within a packed decimal, zoned decimal, binary, or floating-point field.

Usage for physical and logical files (position 38)
You use this field to specify that a named field is to be an input-only, both (both input and output are allowed), or neither (neither input nor output is allowed) field.

Location for physical and logical files (positions 39 through 44)
These positions do not apply to physical or logical files. Leave these positions blank unless you use them for comment text.

Keyword entries are typed in positions 45 through 80 (functions).
 */