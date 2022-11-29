using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DataTypes.Characters
{
    public partial class CodePage290
    {
        public static readonly Dictionary<byte,char> Characters = new Dictionary<byte, char>()
        {
            //{0x00,'NUL'},   {0x01,'SOH'},   {0x02,'STX'},   {0x03,'ETX'},   {0x04,'ST'},    {0x05,'HT'},    {0x06,'SSA'},   {0x07,'DEL'},   {0x08,'EPA'},   {0x09,'RI'},    {0x0A,'SS2'},   {0x0B,'VT'},    {0x0C,'FF'},    {0x0D,'CR'},    {0x0E,'SO'},    {0x0F,'SI'},
            //{0x10,'DLE'},   {0x11,'DC1'},   {0x12,'DC2'},   {0x13,'DC3'},   {0x14,'OSC'},   {0x15,'NEL'},   {0x16,'BS'},    {0x17,'ESA'},   {0x18,'CAN'},   {0x19,'EM'},    {0x1A,'PU2'},   {0x1B,'SS3'},   {0x1C,'FS'},    {0x1D,'GS'},    {0x1E,'RS'},    {0x1F,'US'},
            //{0x20,'PAD'},   {0x21,'HOP'},   {0x22,'BPH'},   {0x23,'NBH'},   {0x24,'IND'},   {0x25,'LF'},    {0x26,'ETB'},   {0x27,'ESC'},   {0x28,'HTS'},   {0x29,'HTJ'},   {0x2A,'VTS'},   {0x2B,'PLD'},   {0x2C,'PLU'},   {0x2D,'ENQ'},   {0x2E,'ACK'},   {0x2F,'BEL'},
            //{0x30,'DCS'},   {0x31,'PU1'},   {0x32,'SYN'},   {0x33,'STS'},   {0x34,'CCH'},   {0x35,'MW'},    {0x36,'SPA'},   {0x37,'EOT'},   {0x38,'SOS'},   {0x39,'SGCI'},  {0x3A,'SCI'},   {0x3B,'CSI'},   {0x3C,'DC4'},   {0x3D,'NAK'},   {0x3E,'PM'},    {0x3F,'SUB'},
            {0x40,' '}, {0x41,'｡'}, {0x42,'｢'}, {0x43,'｣'}, {0x44,'､'}, {0x45,'･'}, {0x46,'ｦ'}, {0x47,'ｧ'}, {0x48,'ｨ'}, {0x49,'ｩ'}, {0x4A,'£'}, {0x4B,'.'}, {0x4C,'<'}, {0x4D,'('}, {0x4E,'+'}, {0x4F,'|'},
            {0x50,'&'}, {0x51,'ｪ'}, {0x52,'ｫ'}, {0x53,'ｬ'}, {0x54,'ｭ'}, {0x55,'ｮ'}, {0x56,'ｯ'}, //{0x57,''},  
            {0x58,'ｰ'}, //{0x59,''},  
            {0x5A,'!'}, {0x5B,'¥'}, {0x5C,'*'}, {0x5D,')'}, {0x5E,';'}, {0x5F,'¬'},
            {0x60,'-'}, {0x61,'/'}, {0x62,'a'}, {0x63,'b'}, {0x64,'c'}, {0x65,'d'}, {0x66,'e'}, {0x67,'f'}, {0x68,'g'}, {0x69,'h'}, //{0x6A,''},  
            {0x6B,','}, {0x6C,'%'}, {0x6D,'_'}, {0x6E,'>'}, {0x6F,'?'},
            {0x70,'['}, {0x71,'i'}, {0x72,'j'}, {0x73,'k'}, {0x74,'l'}, {0x75,'m'}, {0x76,'n'}, {0x77,'o'}, {0x78,'p'}, {0x79,'`'}, {0x7A,':'}, {0x7B,'#'}, {0x7C,'@'}, {0x7D,'\''},{0x7E,'='},	{0x7F,'"'},
            {0x80,']'}, {0x81,'ｱ'}, {0x82,'ｲ'}, {0x83,'ｳ'}, {0x84,'ｴ'}, {0x85,'ｵ'}, {0x86,'ｶ'}, {0x87,'ｷ'}, {0x88,'ｸ'}, {0x89,'ｹ'}, {0x8A,'ｺ'}, {0x8B,'q'}, {0x8C,'ｻ'}, {0x8D,'ｼ'}, {0x8E,'ｽ'}, {0x8F,'ｾ'},
            {0x90,'ｿ'}, {0x91,'ﾀ'}, {0x92,'ﾁ'}, {0x93,'ﾂ'}, {0x94,'ﾃ'}, {0x95,'ﾄ'}, {0x96,'ﾅ'}, {0x97,'ﾆ'}, {0x98,'ﾇ'}, {0x99,'ﾈ'}, {0x9A,'ﾉ'}, {0x9B,'r'}, //{0x9C,''},  
            {0x9D,'ﾊ'}, {0x9E,'ﾋ'}, {0x9F,'ﾌ'},
            {0xA0,'~'}, {0xA1,'‾'}, {0xA2,'ﾍ'}, {0xA3,'ﾎ'}, {0xA4,'ﾏ'}, {0xA5,'ﾐ'}, {0xA6,'ﾑ'}, {0xA7,'ﾒ'}, {0xA8,'ﾓ'}, {0xA9,'ﾔ'}, {0xAA,'ﾕ'}, {0xAB,'s'}, {0xAC,'ﾖ'}, {0xAD,'ﾗ'}, {0xAE,'ﾘ'}, {0xAF,'ﾙ'},
            {0xB0,'^'}, {0xB1,'¢'}, {0xB2,'\\'},{0xB3,'t'},	{0xB4,'u'},	{0xB5,'v'},	{0xB6,'w'},	{0xB7,'x'},	{0xB8,'y'},	{0xB9,'z'},	{0xBA,'ﾚ'},	{0xBB,'ﾛ'},	{0xBC,'ﾜ'},	{0xBD,'ﾝ'},	{0xBE,'ﾞ'},	{0xBF,'ﾟ'},
            {0xC0,'{'}, {0xC1,'A'}, {0xC2,'B'}, {0xC3,'C'}, {0xC4,'D'}, {0xC5,'E'}, {0xC6,'F'}, {0xC7,'G'}, {0xC8,'H'}, {0xC9,'I'}, //{0xCA,''},  {0xCB,''},  {0xCC,''},  {0xCD,''},  {0xCE,''},  {0xCF,''},
            {0xD0,'}'}, {0xD1,'J'}, {0xD2,'K'}, {0xD3,'L'}, {0xD4,'M'}, {0xD5,'N'}, {0xD6,'O'}, {0xD7,'P'}, {0xD8,'Q'}, {0xD9,'R'}, //{0xDA,''},  {0xDB,''},  {0xDC,''},  {0xDD,''},  {0xDE,''},  {0xDF,''},
            {0xE0,'$'}, {0xE1,'€'}, {0xE2,'S'}, {0xE3,'T'}, {0xE4,'U'}, {0xE5,'V'}, {0xE6,'W'}, {0xE7,'X'}, {0xE8,'Y'}, {0xE9,'Z'}, //{0xEA,''},  {0xEB,''},  {0xEC,''},  {0xED,''},  {0xEE,''},  {0xEF,''},
            {0xF0,'0'}, {0xF1,'1'}, {0xF2,'2'}, {0xF3,'3'}, {0xF4,'4'}, {0xF5,'5'}, {0xF6,'6'}, {0xF7,'7'}, {0xF8,'8'}, {0xF9,'9'}, //{0xFA,''},  {0xFB,''},  {0xFC,''},  {0xFD,''},  {0xFE,''},  {0xFF,'APC'},
        }
            ;

        static void BuildCodeTable()
        {
            //http://tachyonsoft.com/cp00290.htm
            //CP290Characters[0x00] = 'NUL'; CP290Characters[0x01] = 'SOH'; CP290Characters[0x02] = 'STX'; CP290Characters[0x03] = 'ETX'; CP290Characters[0x04] = 'ST'; CP290Characters[0x05] = 'HT'; CP290Characters[0x06] = 'SSA'; CP290Characters[0x07] = 'DEL'; CP290Characters[0x08] = 'EPA'; CP290Characters[0x09] = 'RI'; CP290Characters[0x0A] = 'SS2'; CP290Characters[0x0B] = 'VT'; CP290Characters[0x0C] = 'FF'; CP290Characters[0x0D] = 'CR'; CP290Characters[0x0E] = 'SO'; CP290Characters[0x0F] = 'SI';
            //CP290Characters[0x10] = 'DLE'; CP290Characters[0x11] = 'DC1'; CP290Characters[0x12] = 'DC2'; CP290Characters[0x13] = 'DC3'; CP290Characters[0x14] = 'OSC'; CP290Characters[0x15] = 'NEL'; CP290Characters[0x16] = 'BS'; CP290Characters[0x17] = 'ESA'; CP290Characters[0x18] = 'CAN'; CP290Characters[0x19] = 'EM'; CP290Characters[0x1A] = 'PU2'; CP290Characters[0x1B] = 'SS3'; CP290Characters[0x1C] = 'FS'; CP290Characters[0x1D] = 'GS'; CP290Characters[0x1E] = 'RS'; CP290Characters[0x1F] = 'US';
            //CP290Characters[0x20] = 'PAD'; CP290Characters[0x21] = 'HOP'; CP290Characters[0x22] = 'BPH'; CP290Characters[0x23] = 'NBH'; CP290Characters[0x24] = 'IND'; CP290Characters[0x25] = 'LF'; CP290Characters[0x26] = 'ETB'; CP290Characters[0x27] = 'ESC'; CP290Characters[0x28] = 'HTS'; CP290Characters[0x29] = 'HTJ'; CP290Characters[0x2A] = 'VTS'; CP290Characters[0x2B] = 'PLD'; CP290Characters[0x2C] = 'PLU'; CP290Characters[0x2D] = 'ENQ'; CP290Characters[0x2E] = 'ACK'; CP290Characters[0x2F] = 'BEL';
            //CP290Characters[0x30] = 'DCS'; CP290Characters[0x31] = 'PU1'; CP290Characters[0x32] = 'SYN'; CP290Characters[0x33] = 'STS'; CP290Characters[0x34] = 'CCH'; CP290Characters[0x35] = 'MW'; CP290Characters[0x36] = 'SPA'; CP290Characters[0x37] = 'EOT'; CP290Characters[0x38] = 'SOS'; CP290Characters[0x39] = 'SGCI'; CP290Characters[0x3A] = 'SCI'; CP290Characters[0x3B] = 'CSI'; CP290Characters[0x3C] = 'DC4'; CP290Characters[0x3D] = 'NAK'; CP290Characters[0x3E] = 'PM'; CP290Characters[0x3F] = 'SUB';
            Characters[0x40] = ' ';//'SP'; 
            Characters[0x41] = '｡'; Characters[0x42] = '｢'; Characters[0x43] = '｣'; 
            Characters[0x44] = '､'; Characters[0x45] = '･'; Characters[0x46] = 'ｦ'; 
            Characters[0x47] = 'ｧ'; Characters[0x48] = 'ｨ'; Characters[0x49] = 'ｩ'; 
            Characters[0x4A] = '£'; Characters[0x4B] = '.'; Characters[0x4C] = '<'; 
            Characters[0x4D] = '('; Characters[0x4E] = '+'; Characters[0x4F] = '|';
            Characters[0x50] = '&'; Characters[0x51] = 'ｪ'; Characters[0x52] = 'ｫ'; Characters[0x53] = 'ｬ'; 
            Characters[0x54] = 'ｭ'; Characters[0x55] = 'ｮ'; Characters[0x56] = 'ｯ'; 
            //CP290Characters[0x57] = ''; 
            Characters[0x58] = 'ｰ'; 
            //CP290Characters[0x59] = ''; 
            Characters[0x5A] = '!'; Characters[0x5B] = '¥'; Characters[0x5C] = '*'; Characters[0x5D] = ')'; 
            Characters[0x5E] = ';'; Characters[0x5F] = '¬';
            Characters[0x60] = '-'; Characters[0x61] = '/'; 
            Characters[0x62] = 'a'; Characters[0x63] = 'b'; Characters[0x64] = 'c'; Characters[0x65] = 'd'; 
            Characters[0x66] = 'e'; Characters[0x67] = 'f'; Characters[0x68] = 'g'; Characters[0x69] = 'h'; 
            //CP290Characters[0x6A] = ''; 
            Characters[0x6B] = ','; Characters[0x6C] = '%'; Characters[0x6D] = '_'; Characters[0x6E] = '>'; 
            Characters[0x6F] = '?';
            Characters[0x70] = '['; Characters[0x71] = 'i'; Characters[0x72] = 'j'; Characters[0x73] = 'k'; 
            Characters[0x74] = 'l'; Characters[0x75] = 'm'; Characters[0x76] = 'n'; Characters[0x77] = 'o'; 
            Characters[0x78] = 'p'; Characters[0x79] = '`'; Characters[0x7A] = ':'; Characters[0x7B] = '#'; 
            Characters[0x7C] = '@'; Characters[0x7D] = '\'';	
            Characters[0x7E] = '='; Characters[0x7F] = '"';
            Characters[0x80] = ']'; Characters[0x81] = 'ｱ'; Characters[0x82] = 'ｲ'; Characters[0x83] = 'ｳ'; 
            Characters[0x84] = 'ｴ'; Characters[0x85] = 'ｵ'; Characters[0x86] = 'ｶ'; Characters[0x87] = 'ｷ'; 
            Characters[0x88] = 'ｸ'; Characters[0x89] = 'ｹ'; Characters[0x8A] = 'ｺ'; Characters[0x8B] = 'q'; 
            Characters[0x8C] = 'ｻ'; Characters[0x8D] = 'ｼ'; Characters[0x8E] = 'ｽ'; Characters[0x8F] = 'ｾ';
            Characters[0x90] = 'ｿ'; Characters[0x91] = 'ﾀ'; Characters[0x92] = 'ﾁ'; Characters[0x93] = 'ﾂ'; 
            Characters[0x94] = 'ﾃ'; Characters[0x95] = 'ﾄ'; Characters[0x96] = 'ﾅ'; Characters[0x97] = 'ﾆ'; 
            Characters[0x98] = 'ﾇ'; Characters[0x99] = 'ﾈ'; Characters[0x9A] = 'ﾉ'; Characters[0x9B] = 'r'; 
            //CP290Characters[0x9C] = ''; 
            Characters[0x9D] = 'ﾊ'; Characters[0x9E] = 'ﾋ'; Characters[0x9F] = 'ﾌ';
            Characters[0xA0] = '~'; Characters[0xA1] = '‾'; Characters[0xA2] = 'ﾍ'; Characters[0xA3] = 'ﾎ'; 
            Characters[0xA4] = 'ﾏ'; Characters[0xA5] = 'ﾐ'; Characters[0xA6] = 'ﾑ'; Characters[0xA7] = 'ﾒ'; 
            Characters[0xA8] = 'ﾓ'; Characters[0xA9] = 'ﾔ'; Characters[0xAA] = 'ﾕ'; Characters[0xAB] = 's'; 
            Characters[0xAC] = 'ﾖ'; Characters[0xAD] = 'ﾗ'; Characters[0xAE] = 'ﾘ'; Characters[0xAF] = 'ﾙ';
            Characters[0xB0] = '^'; Characters[0xB1] = '¢'; Characters[0xB2] = '\\';	
            Characters[0xB3] = 't'; Characters[0xB4] = 'u'; Characters[0xB5] = 'v'; Characters[0xB6] = 'w';	
            Characters[0xB7] = 'x'; Characters[0xB8] = 'y'; Characters[0xB9] = 'z'; Characters[0xBA] = 'ﾚ';
            Characters[0xBB] = 'ﾛ'; Characters[0xBC] = 'ﾜ'; Characters[0xBD] = 'ﾝ'; Characters[0xBE] = 'ﾞ';	
            Characters[0xBF] = 'ﾟ'; Characters[0xC0] = '{'; Characters[0xC1] = 'A'; Characters[0xC2] = 'B'; 
            Characters[0xC3] = 'C'; Characters[0xC4] = 'D'; Characters[0xC5] = 'E'; Characters[0xC6] = 'F'; 
            Characters[0xC7] = 'G'; Characters[0xC8] = 'H'; Characters[0xC9] = 'I'; 
            //CP290Characters[0xCA] = ''; CP290Characters[0xCB] = ''; CP290Characters[0xCC] = ''; CP290Characters[0xCD] = ''; CP290Characters[0xCE] = ''; CP290Characters[0xCF] = '';
            Characters[0xD0] = '}'; Characters[0xD1] = 'J'; Characters[0xD2] = 'K'; Characters[0xD3] = 'L'; 
            Characters[0xD4] = 'M'; Characters[0xD5] = 'N'; Characters[0xD6] = 'O'; Characters[0xD7] = 'P'; 
            Characters[0xD8] = 'Q'; Characters[0xD9] = 'R'; 
            //CP290Characters[0xDA] = ''; CP290Characters[0xDB] = ''; CP290Characters[0xDC] = ''; CP290Characters[0xDD] = ''; CP290Characters[0xDE] = ''; CP290Characters[0xDF] = '';
            Characters[0xE0] = '$'; Characters[0xE1] = '€'; Characters[0xE2] = 'S'; Characters[0xE3] = 'T'; 
            Characters[0xE4] = 'U'; Characters[0xE5] = 'V'; Characters[0xE6] = 'W'; Characters[0xE7] = 'X'; 
            Characters[0xE8] = 'Y'; Characters[0xE9] = 'Z'; 
            //CP290Characters[0xEA] = ''; CP290Characters[0xEB] = ''; CP290Characters[0xEC] = ''; CP290Characters[0xED] = ''; CP290Characters[0xEE] = ''; CP290Characters[0xEF] = '';
            Characters[0xF0] = '0'; Characters[0xF1] = '1'; Characters[0xF2] = '2'; Characters[0xF3] = '3'; 
            Characters[0xF4] = '4'; Characters[0xF5] = '5'; Characters[0xF6] = '6'; Characters[0xF7] = '7';
            Characters[0xF8] = '8'; Characters[0xF9] = '9'; 
            //CP290Characters[0xFA] = ''; CP290Characters[0xFB] = ''; CP290Characters[0xFC] = ''; CP290Characters[0xFD] = ''; CP290Characters[0xFE] = ''; 
            //CP290Characters[0xFF] = 'APC';

        }

        //public static readonly CodePage290 shiftOut = new CodePage290('?', 0x0e);
        //public static readonly CodePage290 shiftIn = new CodePage290('?', 0x0f);
        //public static readonly CodePage290 LineFeed = new CodePage290('\n', 0x25);

        private static readonly CodePage290 whiteSpace = new CodePage290(' ', 0x40);
        private static readonly CodePage290 touten = new CodePage290('｡', 0x41);
        private static readonly CodePage290 leftSquareBrackets = new CodePage290('｢', 0x42);
        private static readonly CodePage290 rightSquareBrackets = new CodePage290('｣', 0x43);
        private static readonly CodePage290 kuten = new CodePage290('､', 0x44);
        private static readonly CodePage290 ｦ = new CodePage290('ｦ', 0x46);
        private static readonly CodePage290 ｧ = new CodePage290('ｧ', 0x47);
        private static readonly CodePage290 ｨ = new CodePage290('ｨ', 0x48);
        private static readonly CodePage290 ｩ = new CodePage290('ｩ', 0x49);
        private static readonly CodePage290 period = new CodePage290('.', 0x4B);
        private static readonly CodePage290 lessThan = new CodePage290('<', 0x4C);
        private static readonly CodePage290 leftParenthesis = new CodePage290('(', 0x4D);
        private static readonly CodePage290 plus = new CodePage290('+', 0x4E);
        private static readonly CodePage290 verticalLine = new CodePage290('|', 0x4F);
        private static readonly CodePage290 ampersand = new CodePage290('&', 0x50);
        private static readonly CodePage290 ｪ = new CodePage290('ｪ', 0x51);
        private static readonly CodePage290 ｫ = new CodePage290('ｫ', 0x52);
        private static readonly CodePage290 ｬ = new CodePage290('ｬ', 0x53);
        private static readonly CodePage290 ｭ = new CodePage290('ｭ', 0x54);
        private static readonly CodePage290 ｮ = new CodePage290('ｮ', 0x55);
        private static readonly CodePage290 ｯ = new CodePage290('ｯ', 0x56);
        private static readonly CodePage290 ｰ = new CodePage290('ｰ', 0x58);
        private static readonly CodePage290 exclamationMark = new CodePage290('!', 0x5A);
        private static readonly CodePage290 yenMark = new CodePage290('\\', 0x5B);
        private static readonly CodePage290 asterisk = new CodePage290('*', 0x5C);
        private static readonly CodePage290 rightParenthesis = new CodePage290(')', 0x5D);
        private static readonly CodePage290 semiColon = new CodePage290(';', 0x5E);
        private static readonly CodePage290 logicalNegationSign = new CodePage290('¬', 0x5F);
        private static readonly CodePage290 hyphenMinus = new CodePage290('-', 0x60);
        private static readonly CodePage290 slash = new CodePage290('/', 0x61);
        private static readonly CodePage290 a = new CodePage290('a', 0x62);
        private static readonly CodePage290 b = new CodePage290('b', 0x63);
        private static readonly CodePage290 c = new CodePage290('c', 0x64);
        private static readonly CodePage290 d = new CodePage290('d', 0x65);
        private static readonly CodePage290 e = new CodePage290('e', 0x66);
        private static readonly CodePage290 f = new CodePage290('f', 0x67);
        private static readonly CodePage290 g = new CodePage290('g', 0x68);
        private static readonly CodePage290 h = new CodePage290('h', 0x69);
        private static readonly CodePage290 comma = new CodePage290(',', 0x6B);
        private static readonly CodePage290 percent = new CodePage290('%', 0x6C);
        private static readonly CodePage290 underLine = new CodePage290('_', 0x6D);
        private static readonly CodePage290 greaterThan = new CodePage290('>', 0x6E);
        private static readonly CodePage290 questionMark = new CodePage290('?', 0x6F);
        private static readonly CodePage290 leftStraightBrace = new CodePage290('[', 0x70);
        private static readonly CodePage290 i = new CodePage290('i', 0x71);
        private static readonly CodePage290 j = new CodePage290('j', 0x72);
        private static readonly CodePage290 k = new CodePage290('k', 0x73);
        private static readonly CodePage290 l = new CodePage290('l', 0x74);
        private static readonly CodePage290 m = new CodePage290('m', 0x75);
        private static readonly CodePage290 n = new CodePage290('n', 0x76);
        private static readonly CodePage290 o = new CodePage290('o', 0x77);
        private static readonly CodePage290 p = new CodePage290('p', 0x78);
        private static readonly CodePage290 suppressingSign = new CodePage290('`', 0x79);
        private static readonly CodePage290 colon = new CodePage290(':', 0x7A);
        private static readonly CodePage290 sharp = new CodePage290('#', 0x7B);
        private static readonly CodePage290 atSign = new CodePage290('@', 0x7C);
        private static readonly CodePage290 apostrophe = new CodePage290('\'', 0x7D);
        private static readonly CodePage290 equal = new CodePage290('=', 0x7E);
        private static readonly CodePage290 doubleQuotes = new CodePage290('"', 0x7F);
        private static readonly CodePage290 rightStraightBrace = new CodePage290(']', 0x80);
        private static readonly CodePage290 ｱ = new CodePage290('ｱ', 0x81);
        private static readonly CodePage290 ｲ = new CodePage290('ｲ', 0x82);
        private static readonly CodePage290 ｳ = new CodePage290('ｳ', 0x83);
        private static readonly CodePage290 ｴ = new CodePage290('ｴ', 0x84);
        private static readonly CodePage290 ｵ = new CodePage290('ｵ', 0x85);
        private static readonly CodePage290 ｶ = new CodePage290('ｶ', 0x86);
        private static readonly CodePage290 ｷ = new CodePage290('ｷ', 0x87);
        private static readonly CodePage290 ｸ = new CodePage290('ｸ', 0x88);
        private static readonly CodePage290 ｹ = new CodePage290('ｹ', 0x89);
        private static readonly CodePage290 ｺ = new CodePage290('ｺ', 0x8A);
        private static readonly CodePage290 q = new CodePage290('q', 0x8B);
        private static readonly CodePage290 ｻ = new CodePage290('ｻ', 0x8C);
        private static readonly CodePage290 ｼ = new CodePage290('ｼ', 0x8D);
        private static readonly CodePage290 ｽ = new CodePage290('ｽ', 0x8E);
        private static readonly CodePage290 ｾ = new CodePage290('ｾ', 0x8F);
        private static readonly CodePage290 ｿ = new CodePage290('ｿ', 0x90);
        private static readonly CodePage290 ﾀ = new CodePage290('ﾀ', 0x91);
        private static readonly CodePage290 ﾁ = new CodePage290('ﾁ', 0x92);
        private static readonly CodePage290 ﾂ = new CodePage290('ﾂ', 0x93);
        private static readonly CodePage290 ﾃ = new CodePage290('ﾃ', 0x94);
        private static readonly CodePage290 ﾄ = new CodePage290('ﾄ', 0x95);
        private static readonly CodePage290 ﾅ = new CodePage290('ﾅ', 0x96);
        private static readonly CodePage290 ﾆ = new CodePage290('ﾆ', 0x97);
        private static readonly CodePage290 ﾇ = new CodePage290('ﾇ', 0x98);
        private static readonly CodePage290 ﾈ = new CodePage290('ﾈ', 0x99);
        private static readonly CodePage290 ﾉ = new CodePage290('ﾉ', 0x9A);
        private static readonly CodePage290 r = new CodePage290('r', 0x9B);
        private static readonly CodePage290 ﾊ = new CodePage290('ﾊ', 0x9D);
        private static readonly CodePage290 ﾋ = new CodePage290('ﾋ', 0x9E);
        private static readonly CodePage290 ﾌ = new CodePage290('ﾌ', 0x9F);
        private static readonly CodePage290 tilde = new CodePage290('~', 0xA0);
        private static readonly CodePage290 ﾍ = new CodePage290('ﾍ', 0xA2);
        private static readonly CodePage290 ﾎ = new CodePage290('ﾎ', 0xA3);
        private static readonly CodePage290 ﾏ = new CodePage290('ﾏ', 0xA4);
        private static readonly CodePage290 ﾐ = new CodePage290('ﾐ', 0xA5);
        private static readonly CodePage290 ﾑ = new CodePage290('ﾑ', 0xA6);
        private static readonly CodePage290 ﾒ = new CodePage290('ﾒ', 0xA7);
        private static readonly CodePage290 ﾓ = new CodePage290('ﾓ', 0xA8);
        private static readonly CodePage290 ﾔ = new CodePage290('ﾔ', 0xA9);
        private static readonly CodePage290 ﾕ = new CodePage290('ﾕ', 0xAA);
        private static readonly CodePage290 s = new CodePage290('s', 0xAB);
        private static readonly CodePage290 ﾖ = new CodePage290('ﾖ', 0xAC);
        private static readonly CodePage290 ﾗ = new CodePage290('ﾗ', 0xAD);
        private static readonly CodePage290 ﾘ = new CodePage290('ﾘ', 0xAE);
        private static readonly CodePage290 ﾙ = new CodePage290('ﾙ', 0xAF);
        private static readonly CodePage290 caret = new CodePage290('^', 0xB0);
        private static readonly CodePage290 t = new CodePage290('t', 0xB3);
        private static readonly CodePage290 u = new CodePage290('u', 0xB4);
        private static readonly CodePage290 v = new CodePage290('v', 0xB5);
        private static readonly CodePage290 w = new CodePage290('w', 0xB6);
        private static readonly CodePage290 x = new CodePage290('x', 0xB7);
        private static readonly CodePage290 y = new CodePage290('y', 0xB8);
        private static readonly CodePage290 z = new CodePage290('z', 0xB9);
        private static readonly CodePage290 ﾚ = new CodePage290('ﾚ', 0xBA);
        private static readonly CodePage290 ﾛ = new CodePage290('ﾛ', 0xBB);
        private static readonly CodePage290 ﾜ = new CodePage290('ﾜ', 0xBC);
        private static readonly CodePage290 ﾝ = new CodePage290('ﾝ', 0xBD);
        private static readonly CodePage290 ﾞ = new CodePage290('ﾞ', 0xBE);
        private static readonly CodePage290 ﾟ = new CodePage290('ﾟ', 0xBF);
        private static readonly CodePage290 leftCurlyBrace = new CodePage290('{', 0xC0);
        private static readonly CodePage290 A = new CodePage290('A', 0xC1);
        private static readonly CodePage290 B = new CodePage290('B', 0xC2);
        private static readonly CodePage290 C = new CodePage290('C', 0xC3);
        private static readonly CodePage290 D = new CodePage290('D', 0xC4);
        private static readonly CodePage290 E = new CodePage290('E', 0xC5);
        private static readonly CodePage290 F = new CodePage290('F', 0xC6);
        private static readonly CodePage290 G = new CodePage290('G', 0xC7);
        private static readonly CodePage290 H = new CodePage290('H', 0xC8);
        private static readonly CodePage290 I = new CodePage290('I', 0xC9);
        private static readonly CodePage290 rightCurlyBrace = new CodePage290('}', 0xD0);
        private static readonly CodePage290 J = new CodePage290('J', 0xD1);
        private static readonly CodePage290 K = new CodePage290('K', 0xD2);
        private static readonly CodePage290 L = new CodePage290('L', 0xD3);
        private static readonly CodePage290 M = new CodePage290('M', 0xD4);
        private static readonly CodePage290 N = new CodePage290('N', 0xD5);
        private static readonly CodePage290 O = new CodePage290('O', 0xD6);
        private static readonly CodePage290 P = new CodePage290('P', 0xD7);
        private static readonly CodePage290 Q = new CodePage290('Q', 0xD8);
        private static readonly CodePage290 R = new CodePage290('R', 0xD9);
        private static readonly CodePage290 dollarSign = new CodePage290('$', 0xE0);
        private static readonly CodePage290 S = new CodePage290('S', 0xE2);
        private static readonly CodePage290 T = new CodePage290('T', 0xE3);
        private static readonly CodePage290 U = new CodePage290('U', 0xE4);
        private static readonly CodePage290 V = new CodePage290('V', 0xE5);
        private static readonly CodePage290 W = new CodePage290('W', 0xE6);
        private static readonly CodePage290 X = new CodePage290('X', 0xE7);
        private static readonly CodePage290 Y = new CodePage290('Y', 0xE8);
        private static readonly CodePage290 Z = new CodePage290('Z', 0xE9);
        private static readonly CodePage290 _0 = new CodePage290('0', 0xF0);
        private static readonly CodePage290 _1 = new CodePage290('1', 0xF1);
        private static readonly CodePage290 _2 = new CodePage290('2', 0xF2);
        private static readonly CodePage290 _3 = new CodePage290('3', 0xF3);
        private static readonly CodePage290 _4 = new CodePage290('4', 0xF4);
        private static readonly CodePage290 _5 = new CodePage290('5', 0xF5);
        private static readonly CodePage290 _6 = new CodePage290('6', 0xF6);
        private static readonly CodePage290 _7 = new CodePage290('7', 0xF7);
        private static readonly CodePage290 _8 = new CodePage290('8', 0xF8);
        private static readonly CodePage290 _9 = new CodePage290('9', 0xF9);
        //private static readonly CodePage290 verticalLine = new CodePage290('|', 0xFA);

        public static readonly List<CodePage290> All = new List<CodePage290>()
                {
        whiteSpace  ,
        touten  ,
        leftSquareBrackets  ,
        rightSquareBrackets ,
        kuten   ,
        ｦ   ,
        ｧ   ,
        ｨ   ,
        ｩ   ,
        period  ,
        lessThan    ,
        leftParenthesis ,
        plus    ,
        ampersand   ,
        ｪ   ,
        ｫ   ,
        ｬ   ,
        ｭ   ,
        ｮ   ,
        ｯ   ,
        ｰ   ,
        exclamationMark ,
        yenMark ,
        asterisk    ,
        rightParenthesis    ,
        semiColon   ,
        logicalNegationSign ,
        hyphenMinus ,
        slash   ,
        a   ,
        b   ,
        c   ,
        d   ,
        e   ,
        f   ,
        g   ,
        h   ,
        comma   ,
        percent ,
        underLine   ,
        greaterThan ,
        questionMark    ,
        leftStraightBrace   ,
        i   ,
        j   ,
        k   ,
        l   ,
        m   ,
        n   ,
        o   ,
        p   ,
        suppressingSign ,
        colon   ,
        sharp   ,
        atSign  ,
        apostrophe  ,
        equal   ,
        doubleQuotes    ,
        rightStraightBrace  ,
        ｱ   ,
        ｲ   ,
        ｳ   ,
        ｴ   ,
        ｵ   ,
        ｶ   ,
        ｷ   ,
        ｸ   ,
        ｹ   ,
        ｺ   ,
        q   ,
        ｻ   ,
        ｼ   ,
        ｽ   ,
        ｾ   ,
        ｿ   ,
        ﾀ   ,
        ﾁ   ,
        ﾂ   ,
        ﾃ   ,
        ﾄ   ,
        ﾅ   ,
        ﾆ   ,
        ﾇ   ,
        ﾈ   ,
        ﾉ   ,
        r   ,
        ﾊ   ,
        ﾋ   ,
        ﾌ   ,
        tilde   ,
        ﾍ   ,
        ﾎ   ,
        ﾏ   ,
        ﾐ   ,
        ﾑ   ,
        ﾒ   ,
        ﾓ   ,
        ﾔ   ,
        ﾕ   ,
        s   ,
        ﾖ   ,
        ﾗ   ,
        ﾘ   ,
        ﾙ   ,
        caret   ,
        t   ,
        u   ,
        v   ,
        w   ,
        x   ,
        y   ,
        z   ,
        ﾚ   ,
        ﾛ   ,
        ﾜ   ,
        ﾝ   ,
        ﾞ   ,
        ﾟ   ,
        leftCurlyBrace  ,
        A   ,
        B   ,
        C   ,
        D   ,
        E   ,
        F   ,
        G   ,
        H   ,
        I   ,
        rightCurlyBrace ,
        J   ,
        K   ,
        L   ,
        M   ,
        N   ,
        O   ,
        P   ,
        Q   ,
        R   ,
        dollarSign  ,
        S   ,
        T   ,
        U   ,
        V   ,
        W   ,
        X   ,
        Y   ,
        Z   ,
        _0  ,
        _1  ,
        _2  ,
        _3  ,
        _4  ,
        _5  ,
        _6  ,
        _7  ,
        _8  ,
        _9  ,
        verticalLine    ,
            };
    } 

}
