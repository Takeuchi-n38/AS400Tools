using System;
using System.Collections.Generic;

namespace Delta.TrimSystem.DatAllocatePlans
{
    public partial class DatAllocatePlan
    {
        public string ProcessDate { get; set; } = null!;
        public string DataCode { get; set; } = null!;
        public string TypeCode { get; set; } = null!;
        public string TypeColor { get; set; } = null!;
        public string? TypeName { get; set; }
        public string? SetType { get; set; }
        public string Partno { get; set; } = null!;
        public string DeliveryDate { get; set; } = null!;
        public string Shift { get; set; } = null!;
        public string Supplier { get; set; } = null!;
        public double? Priority { get; set; }
        public string? AllocateCode { get; set; }
        public double? AllocateQuantity { get; set; }
        public double? MakeQuantity { get; set; }
        public string? FirmDiv { get; set; }
        public string? CarType { get; set; }
        public string? Grade { get; set; }
        public string? Color { get; set; }
        public string? OperateDate { get; set; }
        public string? ConfigCompare { get; set; }
        public string? FirmForcastDiv { get; set; }
        public double? PurchasedAmount { get; set; }
        public double? ManHour { get; set; }
        public string? CreateDate { get; set; }
        public string? CreateTime { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateDate { get; set; }
        public string? UpdateTime { get; set; }
        public string? UpdateUser { get; set; }
    }
}
