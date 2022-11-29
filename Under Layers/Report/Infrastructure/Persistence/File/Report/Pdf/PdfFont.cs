using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfFont
    {
        /**
         * The value of FONT_WIDTH, FONT_XSTART, FONT_ROW is integer type.<br>
         * Integer value is more accurate than float/double value when calculating value.
         */

        private static readonly string FORMAT_FONT_HEIGHT = "{0}";

        /* Font 8.100 */
        private static readonly float FONT_8_100_HEIGHT = 8.1f;

        /* Font 8.900 */
        private static readonly float FONT_8_900_HEIGHT = 8.9f;

        /* Font 10.800 */
        private static readonly float FONT_10_800_HEIGHT = 10.8f;


        private static readonly PdfFont PDF_FONT_8_100;


        private static readonly PdfFont PDF_FONT_8_900;

        /* End Font 8.900 */

        /* Create Font 10.800 */
        private static readonly PdfFont PDF_FONT_10_800;
        /* End Font 10.800 */
        static PdfFont()
        {
            /* Create Font 8.100 */
            PDF_FONT_8_100 = new PdfFont
            {
                fontHeight = String.Format(FORMAT_FONT_HEIGHT, FONT_8_100_HEIGHT),
                fontWidth = 6075,
                xStart = 17500,
                rowPos =
                new int[] {571439, 562878, 554318, 545757, 537196, 528636, 520075, 511515, 502954, 494393,
            485833, 477272, 468712, 460151, 451590, 443030, 434469, 425909, 417348, 408787, 400227,
            391666, 383106, 374545, 365984, 357424, 348863, 340303, 331742, 323181, 314621, 306060,
            297499, 288939, 280378, 271818, 263257, 254696, 246136, 237575, 229015, 220454, 211893,
            203333, 194772, 186212, 177651, 169090, 160530, 151969, 143409, 134848, 126287, 117727,
            109166, 100606, 92045, 83484, 74924, 66363, 57803, 49242, 40682, 32121, 23560, 15000},
                configs = new List<PdfObject>()
            };

            PDF_FONT_8_100.configs.Add(new PdfObject(1, "<<\n/DescendantFonts [2 0 R]\n/BaseFont /MS-Mincho\n/Subtype /Type0\n/Encoding /90ms-RKSJ-H\n/Type /Font\n>>"));
            PDF_FONT_8_100.configs.Add(new PdfObject(2, "<<\n/BaseFont /MS-Mincho\n/CIDSystemInfo \n<<\n/Supplement 2\n/Ordering (Japan1)\n/Registry (Adobe)\n>>\n/Subtype /CIDFontType2\n/FontDescriptor 3 0 R\n/W [1 632 750 633 9600 1500]\n/Type /Font\n>>"));
            PDF_FONT_8_100.configs.Add(new PdfObject(3, "<<\n/FontName /MS-Mincho\n/StemV 69\n/Ascent 859\n/Flags 7\n/FontFamily (MS Mincho)\n/Descent -140\n/ItalicAngle 0\n/FontBBox [0 -137 1000 859]\n/Type /FontDescriptor\n/CapHeight 709\n>>"));
            PDF_FONT_8_100.configs.Add(new PdfObject(4, "<<\n/DescendantFonts [5 0 R]\n/BaseFont /MS-Gothic\n/Subtype /Type0\n/Encoding /90ms-RKSJ-H\n/Type /Font\n>>"));
            PDF_FONT_8_100.configs.Add(new PdfObject(5, "<<\n/BaseFont /MS-Gothic\n/CIDSystemInfo \n<<\n/Supplement 2\n/Ordering (Japan1)\n/Registry (Adobe)\n>>\n/Subtype /CIDFontType2\n/FontDescriptor 6 0 R\n/W [1 632 750 633 9600 1500]\n/Type /Font\n>>"));
            PDF_FONT_8_100.configs.Add(new PdfObject(6, "<<\n/FontName /MS-Gothic\n/StemV 69\n/Ascent 859\n/Flags 5\n/FontFamily (MS Gothic)\n/Descent -140\n/ItalicAngle 0\n/FontBBox [0 -137 1000 859]\n/Type /FontDescriptor\n/CapHeight 737\n>>"));
            PDF_FONT_8_100.configs.Add(new PdfObject(7, "<<\n/LastChar 165\n/BaseFont /OCRB\n/Subtype /TrueType\n/FontDescriptor 8 0 R\n/Widths [602 602 0 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 0 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 602 0 602 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 602]\n/Encoding /WinAnsiEncoding\n/Type /Font\n/FirstChar 32\n>>"));
            PDF_FONT_8_100.configs.Add(new PdfObject(8, "<<\n/FontName /OCRB\n/StemV 88\n/Ascent 706\n/Flags 32\n/FontWeight 400\n/FontFamily (OCRB)\n/XHeight 468\n/FontStretch /ExtraExpanded\n/Descent -164\n/ItalicAngle 0\n/FontBBox [-19 -165 622 711]\n/Type /FontDescriptor\n/CapHeight 640\n>>"));
            /* End Font 8.100 */

            /* Start Font 8.900 */
            PDF_FONT_8_900 = new PdfFont
            {
                fontHeight = String.Format(FORMAT_FONT_HEIGHT, FONT_8_900_HEIGHT),
                fontWidth = 6052,
                xStart = 16800,
                rowPos = new int[] {570583, 0, 551749, 0, 532916, 0, 514083, 0, 495249, 0,
        476416, 0, 457583, 0, 438749, 0, 419916, 0, 401083, 0, 382249, 0, 363416, 0, 344583, 0,
        325749, 0, 306916, 0, 288083, 0, 269249, 0, 250416, 0, 231583, 0, 212749, 0, 193916, 0,
        175083, 0, 156249, 0, 137416, 0, 118583, 0, 99749, 0, 80916},
                configs = new List<PdfObject>()
            };

            PDF_FONT_8_900.configs.Add(new PdfObject(1, "<<\n/DescendantFonts [2 0 R]\n/BaseFont /MS-Mincho\n/Subtype /Type0\n/Encoding /90ms-RKSJ-H\n/Type /Font\n>>"));
            PDF_FONT_8_900.configs.Add(new PdfObject(2, "<<\n/BaseFont /MS-Mincho\n/CIDSystemInfo \n<<\n/Supplement 2\n/Ordering (Japan1)\n/Registry (Adobe)\n>>\n/Subtype /CIDFontType2\n/FontDescriptor 3 0 R\n/W [1 632 680 633 9600 1360]\n/Type /Font\n>>"));
            PDF_FONT_8_900.configs.Add(new PdfObject(3, "<<\n/FontName /MS-Mincho\n/StemV 69\n/Ascent 859\n/Flags 7\n/FontFamily (MS Mincho)\n/Descent -140\n/ItalicAngle 0\n/FontBBox [0 -137 1000 859]\n/Type /FontDescriptor\n/CapHeight 709\n>>"));
            PDF_FONT_8_900.configs.Add(new PdfObject(4, "<<\n/DescendantFonts [5 0 R]\n/BaseFont /MS-Gothic\n/Subtype /Type0\n/Encoding /90ms-RKSJ-H\n/Type /Font\n>>"));
            PDF_FONT_8_900.configs.Add(new PdfObject(5, "<<\n/BaseFont /MS-Gothic\n/CIDSystemInfo \n<<\n/Supplement 2\n/Ordering (Japan1)\n/Registry (Adobe)\n>>\n/Subtype /CIDFontType2\n/FontDescriptor 6 0 R\n/W [1 632 680 633 9600 1360]\n/Type /Font\n>>"));
            PDF_FONT_8_900.configs.Add(new PdfObject(6, "<<\n/FontName /MS-Gothic\n/StemV 69\n/Ascent 859\n/Flags 5\n/FontFamily (MS Gothic)\n/Descent -140\n/ItalicAngle 0\n/FontBBox [0 -137 1000 859]\n/Type /FontDescriptor\n/CapHeight 737\n>>"));
            PDF_FONT_8_900.configs.Add(new PdfObject(7, "<<\n/LastChar 165\n/BaseFont /OCRB\n/Subtype /TrueType\n/FontDescriptor 8 0 R\n/Widths [602 602 0 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 0 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 602 0 602 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 602]\n/Encoding /WinAnsiEncoding\n/Type /Font\n/FirstChar 32\n>>"));
            PDF_FONT_8_900.configs.Add(new PdfObject(8, "<<\n/FontName /OCRB\n/StemV 88\n/Ascent 706\n/Flags 32\n/FontWeight 400\n/FontFamily (OCRB)\n/XHeight 468\n/FontStretch /ExtraExpanded\n/Descent -164\n/ItalicAngle 0\n/FontBBox [-19 -165 622 711]\n/Type /FontDescriptor\n/CapHeight 640\n>>"));
            /* End Font 8.900 */

            /* Start Font 8.900 */
            PDF_FONT_10_800 = new PdfFont
            {
                fontHeight = String.Format(FORMAT_FONT_HEIGHT, FONT_10_800_HEIGHT),
                fontWidth = 6048,
                xStart = 15600,
                rowPos = new int[] {568229, 556458, 544687, 532916, 521145, 509374, 497604,
        485833, 474062, 462291, 450520, 438749, 426979, 415208, 403437, 391666, 379895, 368124,
        356354, 344583, 332812, 321041, 309270, 297499, 285729, 273958, 262187, 250416, 238645,
        226874, 215104, 203333, 191562, 179791, 168020, 156249, 144479, 132708, 120937, 109166,
        97395, 85624, 73854, 62083, 50312, 38541, 26770, 15000},
                configs = new List<PdfObject>()
            };

            PDF_FONT_10_800.configs.Add(new PdfObject(1, "<<\n/DescendantFonts [2 0 R]\n/BaseFont /MS-Mincho\n/Subtype /Type0\n/Encoding /90ms-RKSJ-H\n/Type /Font\n>>"));
            PDF_FONT_10_800.configs.Add(new PdfObject(2, "<<\n/BaseFont /MS-Mincho\n/CIDSystemInfo \n<<\n/Supplement 2\n/Ordering (Japan1)\n/Registry (Adobe)\n>>\n/Subtype /CIDFontType2\n/FontDescriptor 3 0 R\n/W [1 632 560 633 9600 1120]\n/Type /Font\n>>"));
            PDF_FONT_10_800.configs.Add(new PdfObject(3, "<<\n/FontName /MS-Mincho\n/StemV 69\n/Ascent 859\n/Flags 7\n/FontFamily (MS Mincho)\n/Descent -140\n/ItalicAngle 0\n/FontBBox [0 -137 1000 859]\n/Type /FontDescriptor\n/CapHeight 709\n>>"));
            PDF_FONT_10_800.configs.Add(new PdfObject(4, "<<\n/DescendantFonts [5 0 R]\n/BaseFont /MS-Gothic\n/Subtype /Type0\n/Encoding /90ms-RKSJ-H\n/Type /Font\n>>"));
            PDF_FONT_10_800.configs.Add(new PdfObject(5, "<<\n/BaseFont /MS-Gothic\n/CIDSystemInfo \n<<\n/Supplement 2\n/Ordering (Japan1)\n/Registry (Adobe)\n>>\n/Subtype /CIDFontType2\n/FontDescriptor 6 0 R\n/W [1 632 560 633 9600 1120]\n/Type /Font\n>>"));
            PDF_FONT_10_800.configs.Add(new PdfObject(6, "<<\n/FontName /MS-Gothic\n/StemV 69\n/Ascent 859\n/Flags 5\n/FontFamily (MS Gothic)\n/Descent -140\n/ItalicAngle 0\n/FontBBox [0 -137 1000 859]\n/Type /FontDescriptor\n/CapHeight 737\n>>"));
            PDF_FONT_10_800.configs.Add(new PdfObject(7, "<<\n/LastChar 165\n/BaseFont /OCRB\n/Subtype /TrueType\n/FontDescriptor 8 0 R\n/Widths [602 602 0 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 0 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 602 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 602 0 602 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 602]\n/Encoding /WinAnsiEncoding\n/Type /Font\n/FirstChar 32\n>>"));
            PDF_FONT_10_800.configs.Add(new PdfObject(8, "<<\n/FontName /OCRB\n/StemV 88\n/Ascent 706\n/Flags 32\n/FontWeight 400\n/FontFamily (OCRB)\n/XHeight 468\n/FontStretch /ExtraExpanded\n/Descent -164\n/ItalicAngle 0\n/FontBBox [-19 -165 622 711]\n/Type /FontDescriptor\n/CapHeight 640\n>>"));
            /* End Font 8.900 */
        }


        private string fontHeight;

        private int fontWidth;

        // X coordinate of first column in a line
        private int xStart;

        // Y coordinate of all row
        private int[] rowPos;

        // Font configurations in PDF file
        public List<PdfObject> configs;

        //*
        //* Get font by font height.
        //* @param fontHeight
        //* @return

        public static PdfFont GetFont(float fontHeightVal)
        {
            if (FONT_8_100_HEIGHT == fontHeightVal)
            {
                return PDF_FONT_8_100;
            }
            else if (FONT_8_900_HEIGHT == fontHeightVal)
            {
                return PDF_FONT_8_900;
            }
            else if (FONT_10_800_HEIGHT == fontHeightVal)
            {
                return PDF_FONT_10_800;
            }
            else
            {
                throw new Exception("Invalid height of font.");
            }
        }

        /**
         * Get font height as string form.
         * @return
         */
        public string GetFontHeight()
        {
            return fontHeight;
        }

        /**
         * Get font configurations.
         * @return
         */
        public List<PdfObject> GetFontConfig()
        {
            return configs;
        }

        /**
         * Get Y position of nth row.
         * @param row
         * @return Y position of nth row (1 <= nth <= max row).
         */

        public int GetY(int row)
        {
            return rowPos[row - 1];
        }

        /**
         * Get X position of column by font
         * @param col
         * @return X position of column. Start column is 1.
         */
        public int GetX(int col)
        {
            return xStart + (col - 1) * fontWidth;
        }

        /**
         * Check whether row is valid.
         * @param row
         * @return
         */
        public bool ValidateRow(int row)
        {
            return row > 0 && row <= rowPos.Length;
        }

        /**
         * Get number of row.
         * @return
         */
        public int CountRow()
        {
            return rowPos.Length;
        }
    }
}
