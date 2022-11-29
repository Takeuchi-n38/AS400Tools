using System;
using System.Collections.Generic;
using Delta.TrimSystem.MstControls;
using Delta.TrimSystem.DatAllocatePlans;
using Delta.TrimSystem.MstSettypecodes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Delta
{
    public partial class Honsha01IidlibMssqlDbContext : DbContext
    {
        string ConnectionString;
        Honsha01IidlibMssqlDbContext(string aConnectionString)
        {
            ConnectionString = aConnectionString;
        }
        /*
         ID:  sa
Pass:  Delta110.Dcs0330
DBName:  TRIM_SYSTEM
        //MSSQLSERVER
         */
        public static Honsha01IidlibMssqlDbContext Of(string aConnectionString)//"Data Source=.\\SQLEXPRESS;Initial Catalog=h1iid;User ID=sa;Password=dcs0330"
        {
            return new Honsha01IidlibMssqlDbContext(aConnectionString);
        }

        public Honsha01IidlibMssqlDbContext()
        {
        }

        public Honsha01IidlibMssqlDbContext(DbContextOptions<Honsha01IidlibMssqlDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DatAllocatePlan> DatAllocatePlans { get; set; } = null!;
        public virtual DbSet<MstControl> MstControls { get; set; } = null!;
        public virtual DbSet<MstSettypecode> MstSettypecodes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=192.168.100.235;Initial Catalog=TRIM_SYSTEM;User ID=sa;Password=Delta110.Dcs0330");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatAllocatePlan>(entity =>
            {
                entity.HasKey(e => new { e.ProcessDate, e.DataCode, e.TypeCode, e.TypeColor, e.Partno, e.DeliveryDate, e.Shift, e.Supplier });

                entity.ToTable("DAT_ALLOCATE_PLAN");

                entity.HasIndex(e => new { e.ProcessDate, e.DataCode, e.Partno, e.DeliveryDate }, "IX_DAT_ALLOCATE_PLAN_DataCodePartNoDeliveryDate");

                entity.HasIndex(e => new { e.ProcessDate, e.DeliveryDate, e.Supplier, e.CarType, e.Grade, e.Color }, "IX_DAT_ALLOCATE_PLAN_DeliveryDateSupplier");

                entity.HasIndex(e => new { e.ProcessDate, e.TypeCode, e.TypeColor, e.Partno, e.DeliveryDate }, "IX_DAT_ALLOCATE_PLAN_TypeCodeTypeColorPartNoDeliveryDate");

                entity.Property(e => e.ProcessDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PROCESS_DATE");

                entity.Property(e => e.DataCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DATA_CODE");

                entity.Property(e => e.TypeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_CODE");

                entity.Property(e => e.TypeColor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_COLOR");

                entity.Property(e => e.Partno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARTNO");

                entity.Property(e => e.DeliveryDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DELIVERY_DATE");

                entity.Property(e => e.Shift)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SHIFT");

                entity.Property(e => e.Supplier)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SUPPLIER");

                entity.Property(e => e.AllocateCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ALLOCATE_CODE");

                entity.Property(e => e.AllocateQuantity).HasColumnName("ALLOCATE_QUANTITY");

                entity.Property(e => e.CarType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CAR_TYPE");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COLOR");

                entity.Property(e => e.ConfigCompare)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONFIG_COMPARE");

                entity.Property(e => e.CreateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_TIME");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.FirmDiv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRM_DIV");

                entity.Property(e => e.FirmForcastDiv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRM_FORCAST_DIV");

                entity.Property(e => e.Grade)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GRADE");

                entity.Property(e => e.MakeQuantity).HasColumnName("MAKE_QUANTITY");

                entity.Property(e => e.ManHour).HasColumnName("MAN_HOUR");

                entity.Property(e => e.OperateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("OPERATE_DATE");

                entity.Property(e => e.Priority).HasColumnName("PRIORITY");

                entity.Property(e => e.PurchasedAmount).HasColumnName("PURCHASED_AMOUNT");

                entity.Property(e => e.SetType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SET_TYPE");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_NAME");

                entity.Property(e => e.UpdateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UPDATE_TIME");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UPDATE_USER");
            });

            modelBuilder.Entity<MstControl>(entity =>
            {
                entity.HasKey(e => new { e.ControlId, e.Userid });

                entity.ToTable("MST_CONTROL");

                entity.Property(e => e.ControlId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CONTROL_ID");

                entity.Property(e => e.Userid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERID");

                entity.Property(e => e.Memo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("MEMO");

                entity.Property(e => e.Value1)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("VALUE1");

                entity.Property(e => e.Value2)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("VALUE2");

                entity.Property(e => e.Value3)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("VALUE3");

                entity.Property(e => e.Value4)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("VALUE4");

                entity.Property(e => e.Value5)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("VALUE5");
            });

            modelBuilder.Entity<MstSettypecode>(entity =>
            {
                entity.ToTable("MST_SETTYPECODE");

                entity.HasIndex(e => new { e.DistinctTypeCode, e.DistinctTypeColor, e.SeqNo }, "IX_MST_SETTYPECODE")
                    .IsUnique();

                entity.HasIndex(e => new { e.TypeCode, e.TypeColor }, "IX_MST_SETTYPECODE_1");

                entity.Property(e => e.CreateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_DATE");

                entity.Property(e => e.CreateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_TIME");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CREATE_USER");

                entity.Property(e => e.DistinctTypeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISTINCT_TYPE_CODE");

                entity.Property(e => e.DistinctTypeColor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DISTINCT_TYPE_COLOR");

                entity.Property(e => e.SeqNo).HasColumnName("SEQ_NO");

                entity.Property(e => e.TypeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_CODE");

                entity.Property(e => e.TypeColor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPE_COLOR");

                entity.Property(e => e.UpdateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UPDATE_DATE");

                entity.Property(e => e.UpdateTime)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UPDATE_TIME");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("UPDATE_USER");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
