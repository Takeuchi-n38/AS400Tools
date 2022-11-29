namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CLTodoLine : CLLine
    {
        public readonly string Command;
        public CLTodoLine(string command, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            Command = command;
        }

        public static bool IsTodoCommand(string joinedLine)
        {
            foreach (var todoCommand in todoCommands)
            {
                if (joinedLine.StartsWith(todoCommand)) return true;
            }
            return false;
        }

        public static CLTodoLine Create(string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {
            string command = string.Empty;
            if (joinedLine.Contains(" "))
            {
                command = joinedLine.Substring(0, joinedLine.IndexOf(' '));
            }
            else
            {
                command = joinedLine;
            }
            return new CLTodoLine(command, joinedLine, originalLineStartIndex, originalLineEndIndex);
        }

        internal static string[] todoCommands = new string[] {
            "DCL ",
            "CHGVAR ",
            "DO","IF ","ELSE ","ENDDO",
            "GOTO ","RETURN",
            "SNDRCVF  ",
            "DSPMSG","SNDUSRMSG ","SNDPGMMSG ","SNDMSG ","SNDBRKMSG ",
            "CHGDTAARA ","RTVDTAARA ",
            "RTVJOBA ",//RTVJOBA    JOB(&J) USER(&U) NBR(&B)
            "RTVNETA ",//RTVNETA    SYSNAME(&SYSTEM)
            "RTVSYSVAL ",//RTVSYSVAL  SYSVAL(QDATE) RTNVAR(&YMD) //RTVSYSVAL  SYSVAL(QTIME) RTNVAR(&HMS)
            "DSPJOBLOG ",//DSPJOBLOG  OUTPUT(*PRINT)
            "WRKSPLF ",
            "WRKACTJOB ",
            "RUNQRY ",//RUNQRY     QRY(UKAILIB/SUMLST) RCDSLT(*YES)//SALELIB/URIBFL, SALELIB/URIHFL, SALELIB/TKMSTR
            "RMVM ",//RMVM       FILE(DDMLIB/QDDSSRC) MBR(CHATBL)
            "CHGJOB ",//CHGJOB     OUTQ(EIGYOQ)
            "OVRPRTF ",//OVRPRTF FILE(PQEA040P) OUTQ(EIGYOQ) HOLD(*YES)  USRDTA(&DSP) SPLFNAME(NOHIN)
            "CRTSPLPDF1 ",// CRTSPLPDF1 SPLF(NOHIN) PDF(&FNAM) SPLNBR(*LAST) OVRNAME(NOHIN) BASEFONT(G) PAGEORT(*PORTRAIT) SPLFLEN(80) SPLFWID(110) ATODST(Y & UTPRT)
            "CLRSAVF ",//CLRSAVF    FILE(WSVFLIB/JVAA03MUSV)
            "SAVOBJ ",//SAVOBJ     OBJ(UMTTBL UIMMST UTMMST UGMMST UUMMST UKWTBL KMAINTFL VAA540DK VAA529I1 VAA529I2 VAA529DK VAA530IN) LIB(&VAA) DEV(*SAVF) SAVF(WSVFLIB / JVAA03MUSV) SAVACT(*SYSDFN) ACCPTH(*YES) DTACPR(*YES)
            "RSTOBJ ",//RSTOBJ     OBJ(*ALL) SAVLIB(&VAA) DEV(*SAVF) SAVF(&VAA / JVAA40MKS1) MBROPT(*ALL) ALWOBJDIF(*ALL)
            "MAI/SRCTOMAIL ",//MAI/SRCTOMAIL TO('NYAGISHITA@KK-DCS.CO.JP') SUBJECT('TACHIESU HANDI SIJI')  FILE(YAGILIB / QFTPSRC) MBR(TACH2MAIL) FROM('AS-400@KK-DCS.CO.JP') SERVER('192.168.100.200')
            "ALCOBJ ",///*処理重複チェック*/ ALCOBJ OBJ((SALELIB/ CQEA051 * PGM * EXCL)) WAIT(0)
            "DLCOBJ ",//DLCOBJ     OBJ((SALELIB/CQEA051 *PGM *EXCL))
            "SIGNOFF",
            "RCLRSC",//RCLRSC 資源再利用 
            };
    }
}
