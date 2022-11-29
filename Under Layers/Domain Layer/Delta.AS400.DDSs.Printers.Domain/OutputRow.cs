using System;

namespace Delta.AS400.DDSs.Printers
{
    [Obsolete("廃止予定です。OutputRowを使用してください。")]

    public abstract class IOutputRow : OutputRow
    { }

    public abstract class OutputRow
    {
        public abstract void AddReportItem(ReportLine line);

        //public virtual bool IsPageBreakAfterPrinting()
        //{
        //    return PositionOfLineToBePrintedAfterPrinting() > 0;
        //}

        //public virtual bool IsPageBreakBeforePrinting()
        //{
        //    return PositionOfLineToBePrintedBeforePrinting() > 0;
        //}

        //17桁 SPACEB 印刷前の改行数
        public virtual int NumberOfLineBreaksBeforePrinting()
        {
            return 0;
        }

        //18桁 SPACEA 印刷後の改行数
        public virtual int NumberOfLineBreaksAfterPrinting()
        {
            return 0;
        }

        //19～20桁 SKIPB 印刷前の改ページと位置付けの行番号
        public virtual int PositionOfLineToBePrintedBeforePrinting()
        {
            return 0;
        }

        //21～22桁 SKIPA 印刷後の改ページと位置付けの行番号
        public virtual int PositionOfLineToBePrintedAfterPrinting()
        {
            return 0;
        }


    }
}