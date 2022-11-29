using Delta.AS400.Libraries;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Jobs;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs.CLs.Lines.Libs;
using Delta.Tools.Modernization.Sources.AS400.Programs.CLs.Lines;
using Delta.Tools.Sources.Statements;
using Delta.Utilities.Extensions.SystemString;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CLLineFactory
    {
        ObjectIDFactory ObjectIDFactory;
        public CLLineFactory(ObjectIDFactory ObjectIDFactory)
        {
            this.ObjectIDFactory = ObjectIDFactory;
        }

        public IStatement Of(Library library, string line, int originalLineStartIndex, int originalLineEndIndex)
        {
            if (line.StartsWith("/*")) return CLCommentLine.Of(line, originalLineStartIndex);

            if (line.StartsWith("ADDLIBLE")) return CreateAddlibleLine(library, line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("AUTOSTART"))
            {
                return CreateAutostartLine(library, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CALL")) return CreateCallLine(library, line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("CHGCURLIB "))
            {
                var CURLIB = TextClipper.ClipParameter(line, "CURLIB");
                if (CURLIB == string.Empty) CURLIB = line.Replace("CHGCURLIB ", "").Trim();
                return new ChgcurlibLine(ObjectIDFactory.LibraryFactory.Create(CURLIB), line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CHGDTAARA "))
            {
                //CHGDTAARA  DTAARA(COMNLIB/NIKAIME (1 1)) VALUE('2')
                //CHGDTAARA  DTAARA(OPLIB/DTASNDMAI) VALUE('1')
                var dtaara = TextClipper.ClipParameter(line, "DTAARA");
                var variableName = dtaara.Contains("(") ? dtaara.Substring(0, dtaara.IndexOf('(')).Trim():dtaara;
                var objectID = CreateObjectID(ObjectIDFactory, library, variableName);

                var s_l = dtaara.Replace(variableName, string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Trim().Replace("  ", " ").Split(' ');
                int startingPosition = (s_l.Length > 0 && s_l[0]!=string.Empty) ? int.Parse(s_l[0]):-1;
                int substringLength = s_l.Length > 1 ? int.Parse(s_l[1]) : -1;
                var rtnvar = TextClipper.ClipParameter(line, "VALUE");
                return new ChgdtaaraLine(objectID, startingPosition, substringLength, rtnvar, line, originalLineStartIndex);
            }
            if (line.StartsWith("CHGJOB "))
            {
                var sws = TextClipper.ClipParameter(line, "SWS");
                return new ChgjobLine(sws,line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CHGLIBL ")) return CreateChgliblLine(library, line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("CHGSPLFA ")) return new ChgsplfaLine(line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("CHGVAR"))
            {
                var chgVar = new ChgvarStatement(line, originalLineStartIndex, originalLineEndIndex);
                VarList.Instance.Chgvar(chgVar);
                return chgVar;
            }

            if (line.StartsWith("CHKOBJ "))
            {
                var objType = TextClipper.ClipParameter(line, "OBJTYPE");
                if (objType == "*FILE")
                {
                    var fileName = TextClipper.ClipParameter(line, "OBJ");
                    var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                    return new ChkobjLine(objectID, line, originalLineStartIndex, originalLineEndIndex);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            if (line.StartsWith("CLRPFM "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                return new ClrpfLine(objectID, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CLRSAVF "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                return new ClrsavfLine(objectID, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CPYSPLF ")) return new CpysplfLine(line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("CPYTOIMPF "))
            {
                var fromfileName = TextClipper.ClipParameter(line, "FILE");
                var fromFile = CreateObjectID(ObjectIDFactory, library, fromfileName);
                var tofileName = TextClipper.ClipParameter(line, "TOFILE");
                var tostmfileName = TextClipper.ClipParameter(line, "TOSTMF");
                var haveStmFile = tofileName== string.Empty;
                var targetFileName = haveStmFile ? tostmfileName: tofileName;
                var targetFile = CreateObjectID(ObjectIDFactory, library, targetFileName);
                var mbropt = TextClipper.ClipParameter(line, "MBROPT");
                return new CpytoimpfLine(fromFile, targetFile, haveStmFile, mbropt, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CRTDDMF "))
            {
                var fromfileName = TextClipper.ClipParameter(line, "FILE");
                var fromFile = CreateObjectID(ObjectIDFactory, library, fromfileName);
                var tofileName = TextClipper.ClipParameter(line, "RMTFILE");
                var toFile = CreateObjectID(ObjectIDFactory, library, tofileName);
                return new CrtddmfLine(fromFile, toFile, line, originalLineStartIndex, originalLineEndIndex);
            }
            if (line.StartsWith("CRTLF "))
            {
                var fromfileName = TextClipper.ClipParameter(line, "SRCFILE");
                var fromFile = CreateObjectID(ObjectIDFactory, library, fromfileName);
                var tofileName = TextClipper.ClipParameter(line, "FILE");
                var toFile = CreateObjectID(ObjectIDFactory, library, tofileName);
                return new CrtlfLine(fromFile, toFile, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CRTPF "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                return new CrtpfLine(objectID, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CRTSAVF "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                return new CrtsavfLine(objectID, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("CPYF "))
            {
                var fromfileName = TextClipper.ClipParameter(line, "FROMFILE");
                var fromFile = CreateObjectID(ObjectIDFactory, library, fromfileName);
                var tofileName = TextClipper.ClipParameter(line, "TOFILE");
                var toFile = CreateObjectID(ObjectIDFactory, library, tofileName);
                var mbropt = TextClipper.ClipParameter(line, "MBROPT");
                return new CpyfLine(fromFile, toFile, mbropt, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("DCL ")) return new DclStatement(line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("DCLF "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                return new DclfLine(library.ObjectIDOf(fileName), line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("DLTF "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                return new DltfLine(objectID, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("DLTSPLF ")) return new DltsplfLine(line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("DLTOVR "))
            {
                ObjectIDFactory.DeleteAll();
                return new DltovrLine(line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("DLYJOB "))
            {
                return new DlyjobLine(line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("ELSE ") && line.Contains("CMD(DO)")) return new ElseCmdDoLIne(line, originalLineStartIndex);

            if (line.StartsWith("ENDDO")) return new EnddoStatement(line, originalLineStartIndex);

            if (line.StartsWith("ENDPGM")) return new EndpgmStatement(line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("FMTDTA "))
            {
                //FMTDTA     INFILE((&SON/UMTTBL)) OUTFILE(&SN2/VAA529I1) SRCFILE(&VAA / QFMTSRC) SRCMBR(VAA529I1) OPTION(*NOCHK * NOPRT)

                var infileNames = TextClipper.ClipParameter(line, "INFILE");
                var InFiles = new List<ObjectID>();
                foreach (var inFileName in TextClipper.List(infileNames))
                {
                    InFiles.Add(CreateObjectID(ObjectIDFactory, library, inFileName));
                }
                var outFileName = TextClipper.ClipParameter(line, "OUTFILE");
                var outObjectID = CreateObjectID(ObjectIDFactory, library, outFileName);
                var srcfile = TextClipper.ClipParameter(line, "SRCFILE");
                var srcLibraryName= srcfile.Split('/')[0].TrimEnd();
                var srcmbr = TextClipper.ClipParameter(line, "SRCMBR");
                var srcObjectID = ObjectIDFactory.LibraryFactory.Create(srcLibraryName).ObjectIDOf(srcmbr);
                return new FmtdtaLine(InFiles, outObjectID, srcObjectID, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("FTP "))
            {
                var RMTSYS = TextClipper.ClipParameter(line, "RMTSYS");
                return new FtpLine(RMTSYS, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("GOTO ")) return CLGotoStatement.Of(line, originalLineStartIndex);

            if (line.StartsWith("IF "))
            {
                if (line.EndsWith("THEN(DO)"))
                {
                    return new IfThenDoStatement(line, originalLineStartIndex);
                }
                else
                {
                    return new IfThenStatement(line, originalLineStartIndex);
                }
            }

            if (line.StartsWith("MONMSG "))
            {
                var msgid = TextClipper.ClipParameter(line, "MSGID");
                var exec = TextClipper.ClipParameter(line, "EXEC");
                CLLine clCommand = null;
                if (exec.StartsWith("CRTPF"))
                {
                    var fileName = TextClipper.ClipParameter(exec, "FILE");
                    var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                    clCommand = new CrtpfLine(objectID, exec, originalLineStartIndex, originalLineEndIndex);
                }
                else if (exec != string.Empty)
                {
                    clCommand = CLUnKnownLine.Create(exec, originalLineStartIndex, originalLineEndIndex);
                }
                return new MonmsgLine(msgid, clCommand, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("MVSRC "))
            {
                var frFileName = TextClipper.ClipParameter(line, "FRFILE");
                var frFile = CreateObjectID(ObjectIDFactory, library, frFileName);
                var trFileName = TextClipper.ClipParameter(line, "TOFILE");
                var trFile = CreateObjectID(ObjectIDFactory, library, trFileName);
                return new MvsrcLine(frFile, trFile, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("OVRDBF "))
            {
                var fromfileName = TextClipper.ClipParameter(line, "FILE");
                var fromFile = CreateObjectID(ObjectIDFactory, library, fromfileName);
                var tofileName = TextClipper.ClipParameter(line, "TOFILE");
                var toFile = CreateObjectID(ObjectIDFactory, library, tofileName);

                ObjectIDFactory.Override(fromFile, toFile);

                return new OvrdbfLine(fromFile, toFile, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("OVRPRTF "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                var file = CreateObjectID(ObjectIDFactory, library, fileName);
                var outq = TextClipper.ClipParameter(line, "OUTQ");
                var splfname = TextClipper.ClipParameter(line, "SPLFNAME");

                return new OvrprtfLine(file, outq, splfname, line, originalLineStartIndex, originalLineEndIndex);
            }
            ////0291 OVRPRTF    FILE(QPRINT) DEV(PRTAFP) DEVTYPE(*AFPDS) PAGESIZE(8.25 11.7 *UOM) LPI(9) FRONTMGN(0.96 0.9) CDEFNT(XZK308) PAGRTT(90) DUPLEX(*YES) OUTQ(CLASSZ) HOLD(*YES) SAVE(*YES) USRDTA(&X1PGID) IGCCDEFNT(XZK808)

            if (line.StartsWith("PGM")) return new PgmStatement(line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("RCLRSC")) return new RclrscLine(line, originalLineStartIndex);

            if (line.StartsWith("RCVMSG ")) return new RcvmsgLine(line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("RETURN")) return new ReturnStatement(line, originalLineStartIndex);

            if (line.StartsWith("RGZPFM "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                return new RgzpfmLine(objectID, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("RMVLIBLE ")) return CreateRmvlible(library, line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("RSTOBJ "))
            {
                /*RSTOBJ OBJ(IIFORDAIC IIFORDAICS IIFORDFRC) SAVLIB(IIDLIB) DEV(*SAVF) SAVF(WKLIB/IIDBU) MBROPT(*ALL) ALWOBJDIF(*ALL)*/

                var savlib = TextClipper.ClipParameter(line, "SAVLIB");
                var OBJs = TextClipper.ClipParameter(line, "OBJ").Split(" ");
                var objectIDs=new List<ObjectID>();
                TextClipper.ClipParameter(line, "OBJ").Split(" ").ToList().ForEach(item=> objectIDs.Add(ObjectIDFactory.LibraryFactory.Create(savlib).ObjectIDOf(item)));

                var fileName = TextClipper.ClipParameter(line, "SAVF");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                return new RstobjLine(objectID, objectIDs, line, originalLineStartIndex, originalLineEndIndex);
            }

            //RTVDTAARA
            if (line.StartsWith("RTVDTAARA ")){
                //0103 RTVDTAARA  DTAARA(SRCDTAARA (1 10))   RTNVAR(&ZZZ)
                var dtaara = TextClipper.ClipParameter(line, "DTAARA");
                var dtaaraName = dtaara;
                int startingPosition = 0;
                int substringLength = 10;

                if (dtaara.IndexOf('(') != -1)
                {
                    dtaaraName = dtaara.Substring(0, dtaara.IndexOf('(')).Trim();
                    var s_l =dtaara.Replace(dtaaraName,string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Trim().Split(' ');
                    startingPosition = int.Parse(s_l[0]);
                    substringLength = int.Parse(s_l[1]);
                }

                var objectID = CreateObjectID(ObjectIDFactory, library, dtaaraName);
                var rtnvar= TextClipper.ClipParameter(line, "RTNVAR");

                return new RtvdtaaraLine(objectID, startingPosition, substringLength,rtnvar, line, originalLineStartIndex);
            }

            if (line.StartsWith("RTVJOBA ")) return new RtvjobaStatement(line, originalLineStartIndex);

            if (line.StartsWith("RTVMBRD "))
            {
                var fileName = TextClipper.ClipParameter(line, "FILE");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                ////Kensu = DependencyInjector.Pddat1Repository.Count(); //0012 RTVMBRD    FILE(&LIB/PDDAT1) NBRCURRCD(&KENSU)
                var nbrcurrcd = TextClipper.ClipParameter(line, "NBRCURRCD");

                return new RtvmbrdLine(objectID, nbrcurrcd, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("RTVNETA ")) return new RtvnetaStatement(line, originalLineStartIndex);

            if (line.StartsWith("RTVSYSVAL ")) return new RtvsysvalStatement(line, originalLineStartIndex);

            if (line.StartsWith("RUNSQLSTM "))
            {
                var srcfile = TextClipper.ClipParameter(line, "SRCFILE");
                var srcmbr = TextClipper.ClipParameter(line, "SRCMBR");
                return new RunsqlstmLine(srcfile, srcmbr, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("SAVOBJ "))
            {
                /*
SAVOBJ OBJ(WARIMR BROAD CALEND* PRCALN* PSMSTR* IMDATA* IMMSTR*) LIB(&PRD) DEV(*SAVF) SAVF(WSVFLIB/CIID010BA) SAVACT(*SYSDFN) ACCPTH(*YES) DTACPR(*YES)
*/

                var lib = TextClipper.ClipParameter(line, "LIB");
                var OBJs = TextClipper.ClipParameter(line, "OBJ").Split(" ");
                var objectIDs = new List<ObjectID>();
                TextClipper.ClipParameter(line, "OBJ").Split(" ").ToList().ForEach(item => objectIDs.Add(ObjectIDFactory.LibraryFactory.Create(lib).ObjectIDOf(item)));

                var fileName = TextClipper.ClipParameter(line, "SAVF");
                var objectID = CreateObjectID(ObjectIDFactory, library, fileName);
                return new SavobjLine(objectID, objectIDs, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("SBMJOB")) return CreateSbmjobLine(library, line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("SBMRMTCMD ")) return CreateSbmrmtcmdLine(library, line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("SIGNOFF")) return new SignOffStatement(line, originalLineStartIndex);

            if (line.StartsWith("SPLSPT")) return new SplsptLine(line, originalLineStartIndex, originalLineEndIndex);

            if (line.StartsWith("SNDBRKMSG "))
            {
                var msg = TextClipper.ClipParameter(line.Substring(6), "MSG", '\'', '\'');
                var tomsgq = TextClipper.ClipParameter(line.Substring(6), "TOMSGQ");
                return new SndbrkmsgLine(msg, tomsgq, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("SNDF "))
            {
                var rcdfmt = TextClipper.ClipParameter(line, "RCDFMT");
                return new SndfStatement(rcdfmt, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("SNDMSG "))
            {
                var msg = TextClipper.ClipParameter(line.Substring(6), "MSG",'\'','\'');
                var tomsgq = TextClipper.ClipParameter(line.Substring(6), "TOMSGQ");
                return new SndmsgLine(msg,tomsgq, line, originalLineStartIndex, originalLineEndIndex);
            }

            if (line.StartsWith("SNDPGMMSG "))
            {
                var msgdta = TextClipper.ClipParameter(line, "MSGDTA");
                return new SndpgmmsgLine(msgdta, line, originalLineStartIndex, originalLineEndIndex);
            }
            
            if (line.StartsWith("SNDRCVF ")) return new SndrcvfStatement(line, originalLineStartIndex);

            if (line.StartsWith("SNDUSRMSG ")) return new SndusrmsgStatement(line, originalLineStartIndex);


            if (line.StartsWith("WRKOUTQ ")) return new WrkoutqLine(line, originalLineStartIndex, originalLineEndIndex);

            if (CLTodoLine.IsTodoCommand(line)) return CLTodoLine.Create(line, originalLineStartIndex, originalLineEndIndex);

            return CLUnKnownLine.Create(line, originalLineStartIndex, originalLineEndIndex);

        }

        static ObjectID CreateObjectID(ObjectIDFactory objectIDFactory, Library library, string fileName)
        {
            return objectIDFactory.Create(library, fileName);
        }

        CLLine CreateAddlibleLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {

            //ADDLIBLE   LIB(UT4IPDATO)
            //ADDLIBLE UT400SPM
            var libraryName = string.Empty;
            if (joinedLine.Contains('('))
            {
                libraryName = TextClipper.ClipParameter(joinedLine, "LIB");
            }
            else
            {
                joinedLine = joinedLine.Replace("ADDLIBLE ", string.Empty).Trim();
            }

            var LIB = ObjectIDFactory.LibraryFactory.Create(libraryName);

            var instance = new AddlibleLine(LIB, joinedLine, originalLineStartIndex, originalLineEndIndex);

            Job.Instance.AddLibrary(instance.LIB);

            return instance;
        }
        CLLine CreateChgliblLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {

            //CHGLIBL    LIBL(QEVX QGPL QTEMP CDD0001 EIGYLIB RJELIB WRKLIB) CURLIB(SALELIB)
            var currentLibrary = ObjectIDFactory.LibraryFactory.Create(TextClipper.ClipParameter(joinedLine, "CURLIB"));

            var LIBL = TextClipper.ClipParameter(joinedLine, "LIBL");
            var libraries = new List<Library>();
            LIBL.Split(' ').ToList().ForEach(libraryName => libraries.Add(ObjectIDFactory.LibraryFactory.Create(libraryName)));

            var instance = new ChgliblLine(currentLibrary, libraries, joinedLine, originalLineStartIndex, originalLineEndIndex);

            Job.Instance.ChangeLibrary(instance.LIBL, instance.CURLIB);

            return instance;
        }
        CLLine CreateRmvlible(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {

            //RMVLIBLE LIB(UT400IPDC)
            //RMVLIBLE   GAILIB
            var libraryName = string.Empty;
            if (joinedLine.Contains('('))
            {
                libraryName = TextClipper.ClipParameter(joinedLine, "LIB");
            }
            else
            {
                joinedLine = joinedLine.Replace("RMVLIBLE ", string.Empty).Trim();
            }

            var LIB = ObjectIDFactory.LibraryFactory.Create(libraryName);

            var instance = new RmvlibleLine(LIB, joinedLine, originalLineStartIndex, originalLineEndIndex);

            Job.Instance.RemoveLibrary(instance.LIB);

            return instance;
        }

        static CLLine CreateAutostartLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {
            var pgm = ResolveForAutostartCommand(TextClipper.ClipParameter(joinedLine, "GROUP"));

            //PGM(WRKLIB/BKUPON)
            //PGM(PQEA130)
            Library objLibrary = library;
            //string libraryName = library.Name;
            string programName = string.Empty;
            if (pgm.Contains("/"))
            {
                var lib_pgm = pgm.Split('/');
                var libraryName = lib_pgm[0];
                objLibrary = objLibrary.Of(libraryName);
                programName = lib_pgm[1];
            }
            else
            {
                programName = pgm;
            }

            //var callerPgm = Create(ProgramStructureFactory, objLibrary, programName);
            var parm = string.Empty;// CLCommandFactory.ClipParameter(line, "PARM");
            return new AutostartLine(objLibrary, programName, joinedLine, originalLineStartIndex, originalLineEndIndex);
        }

        static string ResolveForAutostartCommand(string originalProgramName)
        {
            if (originalProgramName == "YJVAA65Y2")
            {
                return "JVAA65Y2";
            }
            if (originalProgramName == "YJVAA06YK")
            {
                return "JVAA06YK";
            }
            return originalProgramName;
        }

        static CLLine CreateCallLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {
            var pgm = TextClipper.ClipParameter(joinedLine, "PGM");

            if (!joinedLine.Contains("PGM") && pgm == string.Empty)
            {
                pgm = joinedLine.Replace("CALL","").Trim();
            }

            if (pgm == "CMENOK")
            {//CALL       PGM(CMENOK) PARM(&PGM &MSG) CRUD
                var job = TextClipper.ClipParameter(joinedLine, "PARM").Split(' ')[0];
                var realJob = VarList.Instance.Find(job) ?? job;
                pgm = realJob.Replace("'", string.Empty);
            }

            //PGM(WRKLIB/BKUPON)
            //PGM(PQEA130)
            var objLibrary = library;
            string programName = string.Empty;
            if (pgm.Contains("/"))
            {
                var lib_pgm = pgm.Split('/');
                var libraryName = lib_pgm[0];
                objLibrary = objLibrary.Of(libraryName);
                programName = lib_pgm[1];
            }
            else
            {
                programName = pgm;
            }



            return new CallLine(objLibrary, programName, joinedLine, originalLineStartIndex, originalLineEndIndex);
        }

        static CLLine CreateSbmjobLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {
            var cmd = TextClipper.ClipParameter(joinedLine, "CMD");
            CLLine clCommand;
            if (cmd.StartsWith("CALL"))
            {
                clCommand = CreateCallLine(library, cmd, originalLineStartIndex, originalLineEndIndex);
            }
            else
            {
                clCommand = CLUnKnownLine.Create(cmd, originalLineStartIndex, originalLineEndIndex);
            }
            var job = TextClipper.ClipParameter(joinedLine, " JOB");
            var jobq = TextClipper.ClipParameter(joinedLine, " JOBQ");
            //var callerPgm = PgmFactory.CreateBy(pgm);
            return new SbmjobLine(clCommand, joinedLine, originalLineStartIndex, originalLineEndIndex);
        }

        static CLLine CreateSbmrmtcmdLine(Library library, string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {
            var cmd = TextClipper.ClipParameter(joinedLine, "CMD").Replace(@"'", string.Empty);
            CLLine clCommand;
            if (cmd.StartsWith("CALL"))
            {
                clCommand = CreateCallLine(library, cmd, originalLineStartIndex, originalLineEndIndex);
            }
            else
            {
                clCommand = CLUnKnownLine.Create(cmd, originalLineStartIndex, originalLineEndIndex);
            }
            return new SbmrmtcmdLine(clCommand, joinedLine, originalLineStartIndex, originalLineEndIndex);
        }
    }
}
