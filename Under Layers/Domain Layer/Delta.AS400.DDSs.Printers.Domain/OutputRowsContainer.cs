using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.AS400.DDSs.Printers
{
    [Obsolete("�p�~�\��ł��BOutputRowsContainer���g�p���Ă��������B")]

    public abstract class IOutputRowsContainer : OutputRowsContainer
    { }

    public abstract class OutputRowsContainer
    {
        public abstract void AddOutputRows(List<OutputRow> outputRows);
        //{
        //    this.OutputRows().ToList().ForEach(outputRow=>outputRows.Add(outputRow));
        //}

        //IEnumerable<IOutputRow> OutputRows();

    }
}