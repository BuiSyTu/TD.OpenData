using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Core.Api.Mvc;
using TD.OPDT.Data.Models;

namespace TD.OPDT.Data.Data
{
    public class OpenDataContext : TDCoreDbContext
    {
        public OpenDataContext() : base("DbContext")
        {
        }

        public DbSet<DataSet> DataSets { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Office> Offices { get; set; } 
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataSet>().ToTable("DataSet");
            modelBuilder.Entity<Field>().ToTable("Field");
            modelBuilder.Entity<Office>().ToTable("Office");
            modelBuilder.Entity<User>().ToTable("User");

            base.OnModelCreating(modelBuilder);
        }
    }
}
