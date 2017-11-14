//------------------------------------------------------------------------------
//  BudgetDbContext.cs
//
//  Implementation of: BudgetDbContext (Class) <<dbcontext>>
//  Generated by Domion-MDA - http://www.coderepo.blog/domion
//
//  Created on     : 02-jun-2017 10:49:08
//  Original author: Miguel
//------------------------------------------------------------------------------

using DFlow.Budget.Core.Model;
using DFlow.Budget.Data.Config;
using Domion.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace DFlow.Budget.Data.Services
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext()
            : base()
        {
        }

        public BudgetDbContext(DbContextOptions<BudgetDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BudgetClass> BudgetClasses { get; set; }

        public virtual DbSet<Tenant> Tenants { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        ///
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureLocalModel(modelBuilder);

            ConfigureExternalModel(modelBuilder);
        }

        ///
        /// <param name="modelBuilder"></param>
        private void ConfigureExternalModel(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new TenantConfiguration());
        }

        ///
        /// <param name="modelBuilder"></param>
        private void ConfigureLocalModel(ModelBuilder modelBuilder)
        {
            // Database schema is "Budget"

            modelBuilder.AddConfiguration(new BudgetClassConfiguration());
            modelBuilder.AddConfiguration(new BudgetLineConfiguration());
        }
    }
}
