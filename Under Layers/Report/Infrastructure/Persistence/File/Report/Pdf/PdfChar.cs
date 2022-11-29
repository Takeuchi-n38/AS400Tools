using System;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfChar
    {

        // Format a byte value as hexadecimal string
        //private static readonly String HEX_FORMAT = "%02X";
        private static readonly String HEX_FORMAT = "{0:X2}";

        // Character value as byte form
        private readonly byte data;

        // HALF_SIZE or FULL_SIZE
        private readonly Common.TypeChar type;

        //*
        //* Create a object by character value and type of this character.
        //* @param data - character value as byte form
        //* @param type - type of this character


        public PdfChar(byte data, Common.TypeChar type)
        {
            this.data = data;
            this.type = type;
        }

        //*
        //* Create a object by character value.Default type of this character is HALF SIZE. 
        //* @param data - character value as byte form

        public PdfChar(byte data)
        {
            this.data = data;
            this.type = Common.TypeChar.HALF_SIZE;
        }

        /**
         * Get character value as byte form
         * @return
         */
        byte GetData()
        {
            return data;
        }

        /**
         * Get character type.
         * @return
         */
        public new Common.TypeChar GetType()
        {
            return type;
        }

        /**
         * Create a object that its data is space.
         * @return
         */
        public static PdfChar Space()
        {
            return new PdfChar((byte)0x20);
        }

        /**
         * Check whether object's data is space character or '\0' character.
         * @return
         */
        public bool IsInvisible()
        {
            return data == 0x20 || data == 0;
        }

        /**
         * Format object's data as hexadecimal string.
         * @return
         */
        public String ToHexFormat()
        {
            return String.Format(HEX_FORMAT, data);
        }
    }
}
