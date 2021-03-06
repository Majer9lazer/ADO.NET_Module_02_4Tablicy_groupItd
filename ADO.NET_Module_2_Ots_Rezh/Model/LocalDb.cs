namespace ADO.NET_Module_2_Ots_Rezh.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LocalDb : DbContext
    {
        public LocalDb()
            : base("name=LocalDb")
        {
        }

        public virtual DbSet<dic_Group> dic_Group { get; set; }
        public virtual DbSet<dic_Model> dic_Model { get; set; }
        public virtual DbSet<dic_Pavilion> dic_Pavilion { get; set; }
        public virtual DbSet<dic_Status> dic_Status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
