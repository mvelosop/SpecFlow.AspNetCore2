//------------------------------------------------------------------------------
//  TenantConfiguration.cs
//
//  Implementation of: TenantConfiguration (Class) <<entity-configuration>>
//  Generated by Domion-MDA - http://www.coderepo.blog/domion
//
//  Created on     : 06-ago-2017 19:26:07
//  Original author: Miguel
//------------------------------------------------------------------------------

using DFlow.Budget.Core.Model;
using Domion.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DFlow.Budget.Data.Config
{
	public class TenantConfiguration : EntityTypeConfiguration<Tenant>
	{
		public override void Map(EntityTypeBuilder<Tenant> builder)
		{
			builder.ToTable("Tenants", schema: "Tenants");

			builder.HasKey(t => t.Id);

			builder.Property(t => t.RowVersion)
				.IsRowVersion();

			// Indexes

			builder.HasIndex(t => t.Name)
				.IsUnique();
		}
	}
}