using DFlow.Budget.Core.Model;
using DFlow.Budget.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DFlow.Budget.App.Features
{
    public class TenantFeatures
    {
        private readonly Lazy<BudgetDbContext> _lazyDbContext;

        public TenantFeatures(
            Lazy<BudgetDbContext> lazyDbContext)
        {
            _lazyDbContext = lazyDbContext;
        }

        public static List<ValidationResult> NoErrors { get; } = new List<ValidationResult>();

        private BudgetDbContext DbContext => _lazyDbContext.Value;

        public async Task<List<ValidationResult>> AddTenantAsync(Tenant tenant)
        {
            DbContext.Add(tenant);

            await DbContext.SaveChangesAsync();

            return NoErrors;
        }

        public async Task<Tenant> FindTenantByNameAsync(string name)
        {
            return await DbContext.Tenants.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<List<ValidationResult>> RemoveTenantAsync(Tenant tenant)
        {
            DbContext.Remove(tenant);

            await DbContext.SaveChangesAsync();

            return NoErrors;
        }
    }
}
