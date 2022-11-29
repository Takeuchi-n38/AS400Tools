using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DDSs
{
    public interface IRecordFormatRepository
    {
        int Insert(RecordFormatSpecification RecordFormat);

    }

    public static class IRecordFormatRepositoryExtensions
    {
        public static int Insert(this IRecordFormatRepository target, byte[] bytes)
        {
            var recordFormat = RecordFormatSpecification.OfLeftJustified(bytes);
            return target.Insert(recordFormat);
        }

    }
}
