using System;
using System.Collections.Generic;

namespace Delta.TrimSystem.MstSettypecodes
{
    public partial class MstSettypecode
    {
        public int Id { get; set; }
        public string? DistinctTypeCode { get; set; }
        public string? DistinctTypeColor { get; set; }
        public double? SeqNo { get; set; }
        public string? TypeCode { get; set; }
        public string? TypeColor { get; set; }
        public string? CreateDate { get; set; }
        public string? CreateTime { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateDate { get; set; }
        public string? UpdateTime { get; set; }
        public string? UpdateUser { get; set; }
    }
}
