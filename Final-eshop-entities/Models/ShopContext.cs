using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Final_eshop_entities.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext() : base("MyConnectionString")
        //public ProductContext() : base("AzureConnectionString")
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Member> MemberShips { get; set; }
        public virtual DbSet<OrderSummary> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            

            modelBuilder.Entity<Member>().HasMany(m => m.Orders)
                                         .WithRequired(o => o.Member)
                                         .WillCascadeOnDelete(true);
            

            modelBuilder.Entity<OrderSummary>().HasMany(o => o.OrderDetails)
                                              .WithRequired(od => od.OrderSummary)
                                              .WillCascadeOnDelete(true);
        }
    }
}