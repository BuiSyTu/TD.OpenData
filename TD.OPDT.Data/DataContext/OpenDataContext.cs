using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.OPDT.Data.Models;

namespace TD.OPDT.Data.DataContext
{
    public class OpenDataContext: DbContext
    {
        public OpenDataContext() : base("OpenDataContext")
        {
        }

        public DbSet<Field> Fields { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Field>().ToTable("Field");

            base.OnModelCreating(modelBuilder);
        }
    }
}
