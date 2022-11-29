using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class FileDescriptionLineFactory4 : FileDescriptionLineFactory
    {
        //Position 17 (File Type)
        protected override int FileTypeStartIndex => 16;

        //Position 22 (File Format)
        protected override int FileFormatStartIndex => 21;

        //Positions 23-27 (Record Length)
        protected override string RecordLength(string line)
        {
            if (line.Length < 27) return string.Empty;
            return line.Substring(22, 5).TrimStart();
        }

        //Positions 36-42 (Device)
        protected override int DeviceStartIndex => 35;
    }
}
/*
Position 6 (Form Type)
Positions 7-16 (File Name)

Position 18 (File Designation)
Position 19 (End of File)
Position 20 (File Addition)
Position 21 (Sequence)

Position 28 (Limits Processing)
Positions 29-33 (Length of Key or Record Address)
Position 34 (Record Address Type)
Position 35 (File Organization)

Position 43 (Reserved)
Positions 44-80 (Keywords)
 */
