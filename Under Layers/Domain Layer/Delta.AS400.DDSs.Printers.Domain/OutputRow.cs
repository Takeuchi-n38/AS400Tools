using System;

namespace Delta.AS400.DDSs.Printers
{
    [Obsolete("�p�~�\��ł��BOutputRow���g�p���Ă��������B")]

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

        //17�� SPACEB ����O�̉��s��
        public virtual int NumberOfLineBreaksBeforePrinting()
        {
            return 0;
        }

        //18�� SPACEA �����̉��s��
        public virtual int NumberOfLineBreaksAfterPrinting()
        {
            return 0;
        }

        //19�`20�� SKIPB ����O�̉��y�[�W�ƈʒu�t���̍s�ԍ�
        public virtual int PositionOfLineToBePrintedBeforePrinting()
        {
            return 0;
        }

        //21�`22�� SKIPA �����̉��y�[�W�ƈʒu�t���̍s�ԍ�
        public virtual int PositionOfLineToBePrintedAfterPrinting()
        {
            return 0;
        }


    }
}