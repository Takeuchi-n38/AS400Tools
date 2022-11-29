using Delta.AS400.DataTypes.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Delta.AS400.DDSs.Printers
{
    [Obsolete("廃止予定です。Printerを使用してください。")]
    public class IPrinter
    {
        public readonly int CapacityEachLine;
        public readonly ReportPages ReportPages;

        public IPrinter(int capacityEachLine) 
        {
            CapacityEachLine = capacityEachLine;
            InOF = "0";
            ReportPages = new ReportPages(CapacityEachLine);
        }

        protected string _inOf;
        public string InOF { get { return _inOf; } set { _inOf = value; } }

        public virtual void Excpt(OutputRowsContainer outputRowsContainer)
        {
            var outputRows = new List<OutputRow>();
            outputRowsContainer.AddOutputRows(outputRows);
            Excpt(outputRows);
        }

        public virtual void Excpt(IEnumerable<OutputRow> outputRows)
        {
            outputRows.ToList().ForEach(row => Excpt(row));
        }

        public virtual void Excpt(OutputRow row)
        {
            ReportPages.Excpt(row);

            if (ReportPages.currentPage.Lines.IsOverFlow())
            {
                InOF = "1";
            }
        }

        public virtual void Excpt(OutputRow row1, OutputRow row2)
        {
            Excpt(row1);
            Excpt(row2);
        }

        public IEnumerable<(int, IEnumerable<string>)> AllPageLines()
        {
            var allPageLines = new List<(int, IEnumerable<string>)>();

            ReportPages.Pages.ForEach(page =>
            {
                var lines = new string[66];

                var pageLines = page.Lines.Lines;

                for (int i = 0; i <= 65; i++)
                {
                    if (pageLines.ContainsKey(i + 1))
                    {
                        if(pageLines.TryGetValue(i + 1,out var line))
                        {
                            lines[i] = line.ToLine();
                        }
                    }
                }
                //OF 60 66
                allPageLines.Add((page.Number, lines));
            });
            return allPageLines;
        }
    }

    public class Printer : IPrinter
    {
        public Printer(int capacityEachLine):base(capacityEachLine)
        {
        }

        public Printer() : this(132)
        {

        }
        
    }
}
