using Delta.AS400.Libraries;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Selects;
using Delta.Tools.Modernization.Sources.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.Modernization.Sources.AS400.Programs.RPGs.Forms.Calculations.Dos;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class CalculationLineFactory
    {

        public CalculationLine Create(bool IsRPG4notRPG3, Library library, string line, int originalLineStartIndex, int originalLineEndIndex)
        {

            var lineC = new CalculationLine(IsRPG4notRPG3, line, originalLineStartIndex, originalLineEndIndex);

            var operationExtender = lineC.OperationExtender;

            if (operationExtender == "CALL" || operationExtender == "CALLP")
            {
                return new RPGCallLine(library, lineC);
            }

            if (operationExtender == "KLIST") return new KlistLine(lineC);
            if (operationExtender == "KFLD") return new KfldLine(lineC);
            if (operationExtender == "PLIST") return new PlistLine(lineC);
            if (operationExtender == "PARM") return new ParmLine(lineC);

            if (operationExtender == "EVAL") return new EvalLine(lineC);
            if (operationExtender == "IF") return new IfLine(lineC);
            if (operationExtender == "IFEQ") return new IfeqLine(lineC);
            if (operationExtender == "IFGE") return new IfgeLine(lineC);
            if (operationExtender == "IFLE") return new IfleLine(lineC);
            if (operationExtender == "IFGT") return new IfgtLine(lineC);
            if (operationExtender == "IFLT") return new IfltLine(lineC);
            if (operationExtender == "IFNE") return new IfneLine(lineC);
            if (operationExtender == "ANDEQ") return new AndeqLine(lineC);
            if (operationExtender == "ANDNE") return new AndneLine(lineC);
            if (operationExtender == "ANDGT") return new AndgtLine(lineC);
            if (operationExtender == "ANDLT") return new AndltLine(lineC);
            if (operationExtender == "ANDGE") return new AndgeLine(lineC);
            if (operationExtender == "ANDLE") return new AndleLine(lineC);
            if (operationExtender == "OREQ") return new OreqLine(lineC);
            if (operationExtender == "ORLT") return new OrltLine(lineC);
            if (operationExtender == "ORNE") return new OrneLine(lineC);
            if (operationExtender == "ORGE") return new OrgeLine(lineC);
            if (operationExtender == "ORGT") return new OrgtLine(lineC);
            if (operationExtender == "END") return new EndLine(lineC);
            if (operationExtender == "ENDIF") return new EndifLine(lineC);
            if (operationExtender == "WRITE") return new WriteLine(lineC);
            if (operationExtender == "CHAIN") return new ChainLine(lineC);
            if (operationExtender == "CHAIN(N)") return new ChainnLine(lineC);
            if (operationExtender == "READE") return new ReadeLine(lineC);
            if (operationExtender == "READE(N)") return new ReadenLine(lineC);
            if (operationExtender == "READPE" || operationExtender == "REDPE") return new ReadpeLine(lineC);
            if (operationExtender == "READPE(N)") return new ReadpenLine(lineC);
            if (operationExtender == "READ") return new ReadLine(lineC);
            if (operationExtender == "READ(N)") return new ReadnLine(lineC);
            if (operationExtender == "READP") return new ReadpLine(lineC);
            if (operationExtender == "READP(N)") return new ReadpnLine(lineC);
            if (operationExtender == "UPDATE" || operationExtender == "UPDAT") return new UpdateLine(lineC);
            if (operationExtender.StartsWith("DELET")) return new DeleteLine(lineC);
            if (operationExtender == "BEGSR") return new BegsrLine(lineC);
            if (operationExtender == "ENDSR") return new EndsrLine(lineC);
            if (operationExtender == "EXSR") return new ExsrLine(lineC);
            if (operationExtender == "ELSE") return new ElseLine(lineC);
            if (operationExtender == "MOVEA") return new MoveaLine(lineC);
            if (operationExtender == "DO")
            {
                if (lineC.Factor1.Trim() == string.Empty && lineC.Factor2.Trim() == string.Empty)
                {
                    return new DoLine(lineC);
                }
                else
                if (lineC.Factor1.Trim() != string.Empty && lineC.Factor2.Trim() == string.Empty)
                {
                    return new DoWithFactor1Line(lineC);
                }
                else
                {
                    return new DoForLine(lineC);
                }
            }
            if (operationExtender == "DOU") return new DouLine(lineC);
            if (operationExtender == "DOUEQ") return new DoueqLine(lineC);
            if (operationExtender == "DOWEQ") return new DoweqLine(lineC);
            if (operationExtender == "DOWGT") return new DowgtLine(lineC);
            if (operationExtender == "ENDDO") return new EnddoLine(lineC);
            if (operationExtender == "ITER") return new IterLine(lineC);
            if (operationExtender == "LEAVE") return new LeaveLine(lineC);
            if (operationExtender == "MOVE") return new MoveLine(lineC);
            if (operationExtender == "MOVEL") return new MovelLine(lineC);
            if (operationExtender == "Z-ADD") return new ZaddLine(lineC);
            if (operationExtender == "Z-SUB") return new ZsubLine(lineC);
            if (operationExtender == "CASEQ") return new CaseqLine(lineC);
            if (operationExtender == "CAS") return new CasLine(lineC);
            if (operationExtender == "CAT") return new CatLine(lineC);
            if (operationExtender == "ENDCS") return new EndcsLine(lineC);
            if (operationExtender.StartsWith("SELEC")) return new SelectLine(lineC);
            if (operationExtender == "WHEN") return new WhenLine(lineC);
            if (operationExtender == "WHEQ") return new WheqLine(lineC);
            if (operationExtender == "OTHER") return new OtherLine(lineC);
            if (operationExtender == "ENDSL") return new EndslLine(lineC);
            if (operationExtender == "SETGT") return new SetgtLine(lineC);
            if (operationExtender == "SETLL") return new SetllLine(lineC);
            if (operationExtender == "TESTN") return new TestnLine(lineC);
            if (operationExtender == "TIME") return new TimeLine(lineC);
            if (operationExtender == "BITOFF") return new BitoffLine(lineC);
            if (operationExtender == "SETON") return new SetonLine(lineC);
            if (operationExtender == "SETOF") return new SetofLine(lineC);
            if (operationExtender == "RETURN" || operationExtender == "RETRN") return new ReturnLine(lineC);
            if (operationExtender == "CLEAR") return new ClearLine(lineC);
            if (operationExtender == "TAG") return new TagLine(lineC);
            if (operationExtender == "GOTO") return new GotoLine(lineC);
            if (operationExtender == "ADD") return new AddLine(lineC);
            if (operationExtender == "SUB") return new SubLine(lineC);
            if (operationExtender == "MULT") return new MultLine(lineC);
            if (operationExtender == "MULT(H)") return new MulthLine(lineC);
            if (operationExtender == "DIV") return new DivLine(lineC);
            if (operationExtender == "MVR") return new MvrLine(lineC);
            if (operationExtender == "ADDDUR") return new AdddurLine(lineC);
            if (operationExtender == "SUBDUR") return new SubdurLine(lineC);

            if (operationExtender == "DEFINE" || operationExtender == "DEFN") return new DefineLine(lineC);

            if (operationExtender == "EXCEPT" || operationExtender == "EXCPT") return new ExcptLine(lineC);

            if (operationExtender == "EXFMT") return new ExfmtLine(lineC);

            if (operationExtender == "COMP") return new CompLine(lineC);

            if (operationExtender == "LOKUP") return new LokupLine(lineC);

            return new UnKnownCalculationLine(lineC);
            //LOCK/UNLOCK
            //DOWLE
            //OUT
            //IFLT
            //ANDLE
        }

    }
}
