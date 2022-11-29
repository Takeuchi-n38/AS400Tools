using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.TrimSystem.DatAllocatePlans
{
    public interface IDatAllocatePlanRepository
    {
        IQueryable<DatAllocatePlan> FindAll();

        public List<DatAllocatePlan> FindAllForImport(string aProcessDate)
        {
            return FindAll().Where(item => item.TypeCode.Length == 4 && item.ProcessDate== aProcessDate)
                .OrderBy(item=>item.TypeCode)
                .ThenBy(item => item.TypeColor)
                .ThenBy(item => item.DeliveryDate)
                .ThenBy(item => item.Priority)
                .ThenBy(item => item.Supplier)
                .ToList();
        }
    }
}
