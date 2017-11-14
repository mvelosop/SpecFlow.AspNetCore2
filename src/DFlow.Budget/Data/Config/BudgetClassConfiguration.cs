//------------------------------------------------------------------------------
//  BudgetClassConfiguration.cs
//
//  Implementation of: BudgetClassConfiguration (Class) <<entity-configuration>>
//  Generated by Domion-MDA - http://www.coderepo.blog/domion
//
//  Created on     : 02-jun-2017 10:49:07
//  Original author: Miguel
//------------------------------------------------------------------------------

using DFlow.Budget.Core.Model;
using Domion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFlow.Budget.Data.Config
{
    public class BudgetClassConfiguration : EntityTypeConfiguration<BudgetClass>
    {
        public override void Map(EntityTypeBuilder<BudgetClass> builder)
        {
            builder.ToTable("BudgetClasses", schema: "Budget");

            builder.HasKey(bc => bc.Id);

            builder.Property(bc => bc.RowVersion)
                .IsRowVersion();

            // External entities

            builder.HasOne<Tenant>(bc => bc.Tenant)
                .WithMany()
                .HasForeignKey(bc => bc.Tenant_Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes

            builder.HasIndex(bc => new { bc.Tenant_Id, bc.Name})
                .IsUnique();
        }
    }
}
