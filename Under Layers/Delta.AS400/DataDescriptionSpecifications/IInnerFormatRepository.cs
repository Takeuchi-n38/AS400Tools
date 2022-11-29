using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.DataDescriptionSpecifications
{
    public interface IInnerFormatRepository<T>
    {
        IEnumerable<T> FindAll();

    }
}
