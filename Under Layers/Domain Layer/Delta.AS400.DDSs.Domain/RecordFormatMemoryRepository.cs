using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DDSs
{
    public class RecordFormatMemoryRepository : IRecordFormatRepository
    {
        List<RecordFormatSpecification> RecordFormats = new List<RecordFormatSpecification>();
        int IRecordFormatRepository.Insert(RecordFormatSpecification RecordFormat)
        {
            RecordFormats.Add(RecordFormat);
            return 1;
        }

        public IEnumerable<byte[]> AllBytes() => RecordFormats.Select(r => r.ToBytes());

        public int Count()
        {
            return RecordFormats.Count();
        }

    }
}
