using System.Collections.Generic;

namespace System.Emulater.Report
{
    public interface IOutputRowsContainer
    {
        void AddOutputRows(List<IOutputRow> outputRows);
    }
}