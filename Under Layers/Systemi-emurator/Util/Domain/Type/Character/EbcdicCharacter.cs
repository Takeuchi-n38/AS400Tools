using System;
using System.Collections.Generic;

namespace Domain.Type.Character
{
    public class EbcdicCharacter
    {
        private static readonly EbcdicCharacter whiteSpace = new EbcdicCharacter(' ', (byte)0x40);
        private static readonly EbcdicCharacter touten = new EbcdicCharacter('｡', (byte)0x41);
        private static readonly EbcdicCharacter leftSquareBrackets = new EbcdicCharacter('｢', (byte)0x42);
        private static readonly EbcdicCharacter rightSquareBrackets = new EbcdicCharacter('｣', (byte)0x43);
        private static readonly EbcdicCharacter kuten = new EbcdicCharacter('､', (byte)0x44);
        private static readonly EbcdicCharacter ｦ = new EbcdicCharacter('ｦ', (byte)0x46);
        private static readonly EbcdicCharacter ｧ = new EbcdicCharacter('ｧ', (byte)0x47);
        private static readonly EbcdicCharacter ｨ = new EbcdicCharacter('ｨ', (byte)0x48);
        private static readonly EbcdicCharacter ｩ = new EbcdicCharacter('ｩ', (byte)0x49);
        private static readonly EbcdicCharacter period = new EbcdicCharacter('.', (byte)0x4B);
        private static readonly EbcdicCharacter lessThan = new EbcdicCharacter('<', (byte)0x4C);
        private static readonly EbcdicCharacter leftParenthesis = new EbcdicCharacter('(', (byte)0x4D);
        private static readonly EbcdicCharacter plus = new EbcdicCharacter('+', (byte)0x4E);
        private static readonly EbcdicCharacter ampersand = new EbcdicCharacter('&', (byte)0x50);
        private static readonly EbcdicCharacter ｪ = new EbcdicCharacter('ｪ', (byte)0x51);
        private static readonly EbcdicCharacter ｫ = new EbcdicCharacter('ｫ', (byte)0x52);
        private static readonly EbcdicCharacter ｬ = new EbcdicCharacter('ｬ', (byte)0x53);
        private static readonly EbcdicCharacter ｭ = new EbcdicCharacter('ｭ', (byte)0x54);
        private static readonly EbcdicCharacter ｮ = new EbcdicCharacter('ｮ', (byte)0x55);
        private static readonly EbcdicCharacter ｯ = new EbcdicCharacter('ｯ', (byte)0x56);
        private static readonly EbcdicCharacter ｰ = new EbcdicCharacter('ｰ', (byte)0x58);
        private static readonly EbcdicCharacter exclamationMark = new EbcdicCharacter('!', (byte)0x5A);
        private static readonly EbcdicCharacter yenMark = new EbcdicCharacter('\\', (byte)0x5B);
        private static readonly EbcdicCharacter asterisk = new EbcdicCharacter('*', (byte)0x5C);
        private static readonly EbcdicCharacter rightParenthesis = new EbcdicCharacter(')', (byte)0x5D);
        private static readonly EbcdicCharacter semiColon = new EbcdicCharacter(';', (byte)0x5E);
        private static readonly EbcdicCharacter logicalNegationSign = new EbcdicCharacter('¬', (byte)0x5F);
        private static readonly EbcdicCharacter hyphenMinus = new EbcdicCharacter('-', (byte)0x60);
        private static readonly EbcdicCharacter slash = new EbcdicCharacter('/', (byte)0x61);
        private static readonly EbcdicCharacter a = new EbcdicCharacter('a', (byte)0x62);
        private static readonly EbcdicCharacter b = new EbcdicCharacter('b', (byte)0x63);
        private static readonly EbcdicCharacter c = new EbcdicCharacter('c', (byte)0x64);
        private static readonly EbcdicCharacter d = new EbcdicCharacter('d', (byte)0x65);
        private static readonly EbcdicCharacter e = new EbcdicCharacter('e', (byte)0x66);
        private static readonly EbcdicCharacter f = new EbcdicCharacter('f', (byte)0x67);
        private static readonly EbcdicCharacter g = new EbcdicCharacter('g', (byte)0x68);
        private static readonly EbcdicCharacter h = new EbcdicCharacter('h', (byte)0x69);
        private static readonly EbcdicCharacter comma = new EbcdicCharacter(',', (byte)0x6B);
        private static readonly EbcdicCharacter percent = new EbcdicCharacter('%', (byte)0x6C);
        private static readonly EbcdicCharacter underLine = new EbcdicCharacter('_', (byte)0x6D);
        private static readonly EbcdicCharacter greaterThan = new EbcdicCharacter('>', (byte)0x6E);
        private static readonly EbcdicCharacter questionMark = new EbcdicCharacter('?', (byte)0x6F);
        private static readonly EbcdicCharacter leftStraightBrace = new EbcdicCharacter('[', (byte)0x70);
        private static readonly EbcdicCharacter i = new EbcdicCharacter('i', (byte)0x71);
        private static readonly EbcdicCharacter j = new EbcdicCharacter('j', (byte)0x72);
        private static readonly EbcdicCharacter k = new EbcdicCharacter('k', (byte)0x73);
        private static readonly EbcdicCharacter l = new EbcdicCharacter('l', (byte)0x74);
        private static readonly EbcdicCharacter m = new EbcdicCharacter('m', (byte)0x75);
        private static readonly EbcdicCharacter n = new EbcdicCharacter('n', (byte)0x76);
        private static readonly EbcdicCharacter o = new EbcdicCharacter('o', (byte)0x77);
        private static readonly EbcdicCharacter p = new EbcdicCharacter('p', (byte)0x78);
        private static readonly EbcdicCharacter suppressingSign = new EbcdicCharacter('`', (byte)0x79);
        private static readonly EbcdicCharacter colon = new EbcdicCharacter(':', (byte)0x7A);
        private static readonly EbcdicCharacter sharp = new EbcdicCharacter('#', (byte)0x7B);
        private static readonly EbcdicCharacter atSign = new EbcdicCharacter('@', (byte)0x7C);
        private static readonly EbcdicCharacter apostrophe = new EbcdicCharacter('\'', (byte)0x7D);
        private static readonly EbcdicCharacter equal = new EbcdicCharacter('=', (byte)0x7E);
        private static readonly EbcdicCharacter doubleQuotes = new EbcdicCharacter('"', (byte)0x7F);
        private static readonly EbcdicCharacter rightStraightBrace = new EbcdicCharacter(']', (byte)0x80);
        private static readonly EbcdicCharacter ｱ = new EbcdicCharacter('ｱ', (byte)0x81);
        private static readonly EbcdicCharacter ｲ = new EbcdicCharacter('ｲ', (byte)0x82);
        private static readonly EbcdicCharacter ｳ = new EbcdicCharacter('ｳ', (byte)0x83);
        private static readonly EbcdicCharacter ｴ = new EbcdicCharacter('ｴ', (byte)0x84);
        private static readonly EbcdicCharacter ｵ = new EbcdicCharacter('ｵ', (byte)0x85);
        private static readonly EbcdicCharacter ｶ = new EbcdicCharacter('ｶ', (byte)0x86);
        private static readonly EbcdicCharacter ｷ = new EbcdicCharacter('ｷ', (byte)0x87);
        private static readonly EbcdicCharacter ｸ = new EbcdicCharacter('ｸ', (byte)0x88);
        private static readonly EbcdicCharacter ｹ = new EbcdicCharacter('ｹ', (byte)0x89);
        private static readonly EbcdicCharacter ｺ = new EbcdicCharacter('ｺ', (byte)0x8A);
        private static readonly EbcdicCharacter q = new EbcdicCharacter('q', (byte)0x8B);
        private static readonly EbcdicCharacter ｻ = new EbcdicCharacter('ｻ', (byte)0x8C);
        private static readonly EbcdicCharacter ｼ = new EbcdicCharacter('ｼ', (byte)0x8D);
        private static readonly EbcdicCharacter ｽ = new EbcdicCharacter('ｽ', (byte)0x8E);
        private static readonly EbcdicCharacter ｾ = new EbcdicCharacter('ｾ', (byte)0x8F);
        private static readonly EbcdicCharacter ｿ = new EbcdicCharacter('ｿ', (byte)0x90);
        private static readonly EbcdicCharacter ﾀ = new EbcdicCharacter('ﾀ', (byte)0x91);
        private static readonly EbcdicCharacter ﾁ = new EbcdicCharacter('ﾁ', (byte)0x92);
        private static readonly EbcdicCharacter ﾂ = new EbcdicCharacter('ﾂ', (byte)0x93);
        private static readonly EbcdicCharacter ﾃ = new EbcdicCharacter('ﾃ', (byte)0x94);
        private static readonly EbcdicCharacter ﾄ = new EbcdicCharacter('ﾄ', (byte)0x95);
        private static readonly EbcdicCharacter ﾅ = new EbcdicCharacter('ﾅ', (byte)0x96);
        private static readonly EbcdicCharacter ﾆ = new EbcdicCharacter('ﾆ', (byte)0x97);
        private static readonly EbcdicCharacter ﾇ = new EbcdicCharacter('ﾇ', (byte)0x98);
        private static readonly EbcdicCharacter ﾈ = new EbcdicCharacter('ﾈ', (byte)0x99);
        private static readonly EbcdicCharacter ﾉ = new EbcdicCharacter('ﾉ', (byte)0x9A);
        private static readonly EbcdicCharacter r = new EbcdicCharacter('r', (byte)0x9B);
        private static readonly EbcdicCharacter ﾊ = new EbcdicCharacter('ﾊ', (byte)0x9D);
        private static readonly EbcdicCharacter ﾋ = new EbcdicCharacter('ﾋ', (byte)0x9E);
        private static readonly EbcdicCharacter ﾌ = new EbcdicCharacter('ﾌ', (byte)0x9F);
        private static readonly EbcdicCharacter tilde = new EbcdicCharacter('~', (byte)0xA0);
        private static readonly EbcdicCharacter ﾍ = new EbcdicCharacter('ﾍ', (byte)0xA2);
        private static readonly EbcdicCharacter ﾎ = new EbcdicCharacter('ﾎ', (byte)0xA3);
        private static readonly EbcdicCharacter ﾏ = new EbcdicCharacter('ﾏ', (byte)0xA4);
        private static readonly EbcdicCharacter ﾐ = new EbcdicCharacter('ﾐ', (byte)0xA5);
        private static readonly EbcdicCharacter ﾑ = new EbcdicCharacter('ﾑ', (byte)0xA6);
        private static readonly EbcdicCharacter ﾒ = new EbcdicCharacter('ﾒ', (byte)0xA7);
        private static readonly EbcdicCharacter ﾓ = new EbcdicCharacter('ﾓ', (byte)0xA8);
        private static readonly EbcdicCharacter ﾔ = new EbcdicCharacter('ﾔ', (byte)0xA9);
        private static readonly EbcdicCharacter ﾕ = new EbcdicCharacter('ﾕ', (byte)0xAA);
        private static readonly EbcdicCharacter s = new EbcdicCharacter('s', (byte)0xAB);
        private static readonly EbcdicCharacter ﾖ = new EbcdicCharacter('ﾖ', (byte)0xAC);
        private static readonly EbcdicCharacter ﾗ = new EbcdicCharacter('ﾗ', (byte)0xAD);
        private static readonly EbcdicCharacter ﾘ = new EbcdicCharacter('ﾘ', (byte)0xAE);
        private static readonly EbcdicCharacter ﾙ = new EbcdicCharacter('ﾙ', (byte)0xAF);
        private static readonly EbcdicCharacter caret = new EbcdicCharacter('^', (byte)0xB0);
        private static readonly EbcdicCharacter t = new EbcdicCharacter('t', (byte)0xB3);
        private static readonly EbcdicCharacter u = new EbcdicCharacter('u', (byte)0xB4);
        private static readonly EbcdicCharacter v = new EbcdicCharacter('v', (byte)0xB5);
        private static readonly EbcdicCharacter w = new EbcdicCharacter('w', (byte)0xB6);
        private static readonly EbcdicCharacter x = new EbcdicCharacter('x', (byte)0xB7);
        private static readonly EbcdicCharacter y = new EbcdicCharacter('y', (byte)0xB8);
        private static readonly EbcdicCharacter z = new EbcdicCharacter('z', (byte)0xB9);
        private static readonly EbcdicCharacter ﾚ = new EbcdicCharacter('ﾚ', (byte)0xBA);
        private static readonly EbcdicCharacter ﾛ = new EbcdicCharacter('ﾛ', (byte)0xBB);
        private static readonly EbcdicCharacter ﾜ = new EbcdicCharacter('ﾜ', (byte)0xBC);
        private static readonly EbcdicCharacter ﾝ = new EbcdicCharacter('ﾝ', (byte)0xBD);
        private static readonly EbcdicCharacter ﾞ = new EbcdicCharacter('ﾞ', (byte)0xBE);
        private static readonly EbcdicCharacter ﾟ = new EbcdicCharacter('ﾟ', (byte)0xBF);
        private static readonly EbcdicCharacter leftCurlyBrace = new EbcdicCharacter('{', (byte)0xC0);
        private static readonly EbcdicCharacter A = new EbcdicCharacter('A', (byte)0xC1);
        private static readonly EbcdicCharacter B = new EbcdicCharacter('B', (byte)0xC2);
        private static readonly EbcdicCharacter C = new EbcdicCharacter('C', (byte)0xC3);
        private static readonly EbcdicCharacter D = new EbcdicCharacter('D', (byte)0xC4);
        private static readonly EbcdicCharacter E = new EbcdicCharacter('E', (byte)0xC5);
        private static readonly EbcdicCharacter F = new EbcdicCharacter('F', (byte)0xC6);
        private static readonly EbcdicCharacter G = new EbcdicCharacter('G', (byte)0xC7);
        private static readonly EbcdicCharacter H = new EbcdicCharacter('H', (byte)0xC8);
        private static readonly EbcdicCharacter I = new EbcdicCharacter('I', (byte)0xC9);
        private static readonly EbcdicCharacter rightCurlyBrace = new EbcdicCharacter('}', (byte)0xD0);
        private static readonly EbcdicCharacter J = new EbcdicCharacter('J', (byte)0xD1);
        private static readonly EbcdicCharacter K = new EbcdicCharacter('K', (byte)0xD2);
        private static readonly EbcdicCharacter L = new EbcdicCharacter('L', (byte)0xD3);
        private static readonly EbcdicCharacter M = new EbcdicCharacter('M', (byte)0xD4);
        private static readonly EbcdicCharacter N = new EbcdicCharacter('N', (byte)0xD5);
        private static readonly EbcdicCharacter O = new EbcdicCharacter('O', (byte)0xD6);
        private static readonly EbcdicCharacter P = new EbcdicCharacter('P', (byte)0xD7);
        private static readonly EbcdicCharacter Q = new EbcdicCharacter('Q', (byte)0xD8);
        private static readonly EbcdicCharacter R = new EbcdicCharacter('R', (byte)0xD9);
        private static readonly EbcdicCharacter dollarSign = new EbcdicCharacter('$', (byte)0xE0);
        private static readonly EbcdicCharacter S = new EbcdicCharacter('S', (byte)0xE2);
        private static readonly EbcdicCharacter T = new EbcdicCharacter('T', (byte)0xE3);
        private static readonly EbcdicCharacter U = new EbcdicCharacter('U', (byte)0xE4);
        private static readonly EbcdicCharacter V = new EbcdicCharacter('V', (byte)0xE5);
        private static readonly EbcdicCharacter W = new EbcdicCharacter('W', (byte)0xE6);
        private static readonly EbcdicCharacter X = new EbcdicCharacter('X', (byte)0xE7);
        private static readonly EbcdicCharacter Y = new EbcdicCharacter('Y', (byte)0xE8);
        private static readonly EbcdicCharacter Z = new EbcdicCharacter('Z', (byte)0xE9);
        private static readonly EbcdicCharacter _0 = new EbcdicCharacter('0', (byte)0xF0);
        private static readonly EbcdicCharacter _1 = new EbcdicCharacter('1', (byte)0xF1);
        private static readonly EbcdicCharacter _2 = new EbcdicCharacter('2', (byte)0xF2);
        private static readonly EbcdicCharacter _3 = new EbcdicCharacter('3', (byte)0xF3);
        private static readonly EbcdicCharacter _4 = new EbcdicCharacter('4', (byte)0xF4);
        private static readonly EbcdicCharacter _5 = new EbcdicCharacter('5', (byte)0xF5);
        private static readonly EbcdicCharacter _6 = new EbcdicCharacter('6', (byte)0xF6);
        private static readonly EbcdicCharacter _7 = new EbcdicCharacter('7', (byte)0xF7);
        private static readonly EbcdicCharacter _8 = new EbcdicCharacter('8', (byte)0xF8);
        private static readonly EbcdicCharacter _9 = new EbcdicCharacter('9', (byte)0xF9);
        private static readonly EbcdicCharacter verticalLine = new EbcdicCharacter('|', (byte)0xFA);

        private static readonly byte markOf2byteCharacter = (byte)0xFF;

        private byte ebcdicCode;
        private char unicodeChar;
        private EbcdicCharacter(char unicodeChar, byte code)
        {
            this.unicodeChar = unicodeChar;
            this.ebcdicCode = code;
        }

        private Boolean Is1byteCharacter()
        {
            return this.ebcdicCode != markOf2byteCharacter;
        }

        private int ByteLength()
        {
            return Is1byteCharacter() ? 1 : 2;
        }

        public int CompareTo(EbcdicCharacter o)
        {
            if ((this.unicodeChar == o.unicodeChar))
            {
                return 0;
            }

            if (Is1byteCharacter())
            {
                if (o.Is1byteCharacter())
                {
                    return this.ebcdicCode - o.ebcdicCode;
                }
                else
                {
                    return -1;
                }

            }
            else if (o.Is1byteCharacter())
            {
                return 1;
            }
            else
            {
                return this.unicodeChar.Equals(o.unicodeChar) ? 1 : -1;
            }
        }

        public static EbcdicCharacter OfO(char unicodeChar)
        {
            return EbcdicCharacter.Of(unicodeChar, false);
        }

        public static EbcdicCharacter Of(char unicodeChar, bool is1byteOnly)
        {
            switch (unicodeChar.ToString())
            {
                case " ":
                    return whiteSpace;
                case "｡":
                    return touten;
                case "｢":
                    return leftSquareBrackets;
                case "｣":
                    return rightSquareBrackets;
                case "､":
                    return kuten;
                case "ｦ":
                    return ｦ;
                case "ｧ":
                    return ｧ;
                case "ｨ":
                    return h;
                case "ｩ":
                    return ｩ;
                case "(":
                    return leftParenthesis;
                case ".":
                    return period;
                case "<":
                    return lessThan;
                case "+":
                    return plus;
                case "&":
                    return ampersand;
                case "ｪ":
                    return ｪ;
                case "ｫ":
                    return ｫ;
                case "ｬ":
                    return ｬ;
                case "ｭ":
                    return ｭ;
                case "ｮ":
                    return ｮ;
                case "ｯ":
                    return ｯ;
                case "ｰ":
                    return ｰ;
                case "!":
                    return exclamationMark;
                case "$":
                    return dollarSign;
                case "*":
                    return asterisk;
                case ")":
                    return rightParenthesis;
                case ";":
                    return semiColon;
                case "¬":
                    return logicalNegationSign;
                case "-":
                    return hyphenMinus;
                case "/":
                    return slash;
                case ",":
                    return comma;
                case "%":
                    return percent;
                case "_":
                    return underLine;
                case ">":
                    return greaterThan;
                case "?":
                    return questionMark;
                case "`":
                    return suppressingSign;
                case ":":
                    return colon;
                case "#":
                    return sharp;
                case "@":
                    return atSign;
                case "'":
                    return apostrophe;
                case "=":
                    return equal;
                case "\"\"":
                    return doubleQuotes;
                case "a":
                    return a;
                case "b":
                    return b;
                case "c":
                    return c;
                case "d":
                    return d;
                case "e":
                    return e;
                case "f":
                    return f;
                case "g":
                    return g;
                case "h":
                    return h;
                case "i":
                    return i;
                case "j":
                    return j;
                case "k":
                    return k;
                case "l":
                    return l;
                case "m":
                    return m;
                case "n":
                    return n;
                case "o":
                    return o;
                case "p":
                    return p;
                case "q":
                    return q;
                case "r":
                    return r;
                case "~":
                    return tilde;
                case "s":
                    return s;
                case "^":
                    return caret;
                case "t":
                    return t;
                case "u":
                    return u;
                case "v":
                    return v;
                case "w":
                    return w;
                case "x":
                    return x;
                case "y":
                    return y;
                case "z":
                    return z;
                case "{":
                    return leftCurlyBrace;
                case "A":
                    return A;
                case "B":
                    return B;
                case "C":
                    return C;
                case "D":
                    return D;
                case "E":
                    return E;
                case "F":
                    return F;
                case "G":
                    return G;
                case "H":
                    return H;
                case "I":
                    return I;
                case "}":
                    return rightCurlyBrace;
                case "J":
                    return J;
                case "K":
                    return K;
                case "L":
                    return L;
                case "M":
                    return M;
                case "N":
                    return N;
                case "O":
                    return O;
                case "P":
                    return P;
                case "Q":
                    return Q;
                case "R":
                    return R;
                case "\\\\":
                    return yenMark;
                case "S":
                    return S;
                case "T":
                    return T;
                case "U":
                    return U;
                case "V":
                    return V;
                case "W":
                    return W;
                case "X":
                    return X;
                case "Y":
                    return Y;
                case "Z":
                    return Z;
                case "0":
                    return _0;
                case "1":
                    return _1;
                case "2":
                    return _2;
                case "3":
                    return _3;
                case "4":
                    return _4;
                case "5":
                    return _5;
                case "6":
                    return _6;
                case "7":
                    return _7;
                case "8":
                    return _8;
                case "9":
                    return _9;
                case "|":
                    return verticalLine;
                case "[":
                    return leftStraightBrace;
                case "]":
                    return rightStraightBrace;
                case "ｱ":
                    return ｱ;
                case "ｲ":
                    return ｲ;
                case "ｳ":
                    return ｳ;
                case "ｴ":
                    return ｴ;
                case "ｵ":
                    return ｵ;
                case "ｶ":
                    return ｶ;
                case "ｷ":
                    return ｷ;
                case "ｸ":
                    return ｸ;
                case "ｹ":
                    return ｹ;
                case "ｺ":
                    return ｺ;
                case "ｻ":
                    return ｻ;
                case "ｼ":
                    return ｼ;
                case "ｽ":
                    return ｽ;
                case "ｾ":
                    return ｾ;
                case "ｿ":
                    return ｿ;
                case "ﾀ":
                    return ﾀ;
                case "ﾁ":
                    return ﾁ;
                case "ﾂ":
                    return ﾂ;
                case "ﾃ":
                    return ﾃ;
                case "ﾄ":
                    return ﾄ;
                case "ﾅ":
                    return ﾅ;
                case "ﾆ":
                    return ﾆ;
                case "ﾇ":
                    return ﾇ;
                case "ﾈ":
                    return ﾈ;
                case "ﾉ":
                    return ﾉ;
                case "ﾊ":
                    return ﾊ;
                case "ﾋ":
                    return ﾋ;
                case "ﾌ":
                    return ﾌ;
                case "ﾍ":
                    return ﾍ;
                case "ﾎ":
                    return ﾎ;
                case "ﾏ":
                    return ﾏ;
                case "ﾐ":
                    return ﾐ;
                case "ﾑ":
                    return ﾑ;
                case "ﾒ":
                    return ﾒ;
                case "ﾓ":
                    return ﾓ;
                case "ﾔ":
                    return ﾔ;
                case "ﾕ":
                    return ﾕ;
                case "ﾖ":
                    return ﾖ;
                case "ﾗ":
                    return ﾗ;
                case "ﾘ":
                    return ﾘ;
                case "ﾙ":
                    return ﾙ;
                case "ﾚ":
                    return ﾚ;
                case "ﾛ":
                    return ﾛ;
                case "ﾜ":
                    return ﾜ;
                case "ﾝ":
                    return ﾝ;
                case "ﾞ":
                    return ﾞ;
                case "ﾟ":
                    return ﾟ;
                default:
                    if (is1byteOnly)
                    {
                        throw new Exception(String.Format("想定していない文字です。[%s]", unicodeChar));
                    }
                    else
                    {
                        return new EbcdicCharacter(unicodeChar, markOf2byteCharacter);
                        // 2Byte character 
                    }
            }
        }

        public static int GetByteLength(string target)
        {
            int sum = 0;
            foreach (char c in target.ToCharArray())
            {
                sum = (sum + OfO(c).ByteLength());
            }
            return sum;
        }

        private static IComparer<string> StringComposedOfxbyteCharacterComparator(bool is1byteOnly)
        {
            return new StringComposedOfxbyteCharacterComparator(is1byteOnly);
        }

        public static IComparer<string> stringComposedOf1byteCharacterComparator = StringComposedOfxbyteCharacterComparator(true);

        public static IComparer<string> stringComposedOf2byteCharacterComparator = StringComposedOfxbyteCharacterComparator(false);

        public static IComparer<string> stringComparator = StringComposedOfxbyteCharacterComparator(false);

    }

    public class StringComposedOfxbyteCharacterComparator : IComparer<string>
    {

        private static bool is1byteOnlyPrivate;

        public StringComposedOfxbyteCharacterComparator(bool is1byteOnlyPublic)
        {
            is1byteOnlyPrivate = is1byteOnlyPublic;
        }

        public int Compare(string Left, string Right)
        {
            char[] leftChars = Left.ToCharArray();
            char[] rightChars = Right.ToCharArray();
            bool is1byteOnly = is1byteOnlyPrivate;
            for (int i = 0; (i < System.Math.Min(leftChars.Length, rightChars.Length)); i++)
            {
                if ((leftChars[i] == rightChars[i]))
                {
                    continue;
                    // TODO: Continue For... Warning!!! not translated
                }
                EbcdicCharacter leftChar = EbcdicCharacter.Of(leftChars[i], is1byteOnly);
                EbcdicCharacter rightChar = EbcdicCharacter.Of(rightChars[i], is1byteOnly);
                return leftChar.CompareTo(rightChar);
            }

            return leftChars.Length - rightChars.Length;

        }
    }



}
