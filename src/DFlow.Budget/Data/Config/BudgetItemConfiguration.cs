//------------------------------------------------------------------------------
//  BudgetLineConfiguration.cs
//
//  Implementation of: BudgetLineConfiguration (Class) <<entity-configuration>>
//  Generated by Domion-MDA - http://www.coderepo.blog/domion
//
//  Created on     : 06-ago-2017 20:04:43
//  Original author: Miguel
//------------------------------------------------------------------------------

using DFlow.Budget.Core.Model;
using Domion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFlow.Budget.Data.Config
{
    public class BudgetItemConfiguration : EntityTypeConfiguration<BudgetItem>
    {
        public override void Map(EntityTypeBuilder<BudgetItem> builder)
        {
            builder.ToTable("BudgetItems", schema: "Budget");

            builder.HasKey(bl => bl.Id);

            builder.Property(bl => bl.RowVersion)
                .IsRowVersion();

            builder.HasOne<BudgetClass>(bl => bl.BudgetClass)
                .WithMany(bc => bc.BudgetItems)
                .HasForeignKey(bl => bl.BudgetClass_Id)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes

            builder.HasIndex(bl => new { bl.BudgetClass_Id, bl.Name })
                .IsUnique();
        }
    }
}
