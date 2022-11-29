
using Delta.Guis;

namespace Delta.AS400.Workstations
{
    public interface ISubFileRecordViewModel<VM, R> : IIsVisible
        where VM : new()
        where R : new()

    {
        void Write(R recordFormat);

        R Read();

        bool IsModified(R originalRecordFormat);

        void Clear();

        int Rrn { get; set; }
        bool IsNull { get => Rrn == 0; }
        void SetNull() { Rrn = 0; Clear(); }
    }

}
