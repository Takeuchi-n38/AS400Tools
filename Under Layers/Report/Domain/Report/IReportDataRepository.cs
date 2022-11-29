namespace Domain.IReport
{
    public interface IReportDataRepository
    {
        void Output<T>(object reportData);
    }
}
