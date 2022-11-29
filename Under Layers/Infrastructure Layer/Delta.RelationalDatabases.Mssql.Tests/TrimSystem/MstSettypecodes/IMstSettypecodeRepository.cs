using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.TrimSystem.MstSettypecodes
{
    public interface IMstSettypecodeRepository
    {
        IQueryable<MstSettypecode> FindAll();

        public List<MstSettypecode> FindAllForImport()
        {
            return FindAll()
                .OrderBy(item => item.DistinctTypeCode)
                .ThenBy(item => item.DistinctTypeColor)
                .ThenBy(item => item.SeqNo)
                .ToList();
        }

    }
}
