using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Pdfs
{
    public class Font
    {

        public readonly float Height;
        private Font(float height)
        {
            this.Height = height;
        }

        public static Font DefaultFont = new Font(8.1f);

        //public enum EFont
        //{
        //    FONT_8_100_HEIGHT,
        //    FONT_8_900_HEIGHT,
        //    FONT_10_800_HEIGHT
        //}

        //public float GetEFont(EFont eFont)
        //{
        //    switch (eFont)
        //    {
        //        case EFont.FONT_8_100_HEIGHT:
        //            return 8.1f;
        //        case EFont.FONT_8_900_HEIGHT:
        //            return 8.9f;
        //        case EFont.FONT_10_800_HEIGHT:
        //            return 10.8f;
        //    }

        //    return 0;
        //}



    }

}
