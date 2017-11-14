using DFlow.Budget.Core.Model;
using DFlow.Budget.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DFlow.Budget.App.Features
{
    public class AppFeatures
    {
        public AppFeatures(
            BudgetDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public static List<ValidationResult> NoError => new List<ValidationResult>();

        public BudgetDbContext DbContext { get; }

        public async Task<List<ValidationResult>> PurgeTenantAsync(Tenant tenant)
        {
            List<BudgetClass> budgetClassList = await DbContext.BudgetClasses.Where(bc => bc.Tenant_Id == tenant.Id).ToListAsync();

            DbContext.RemoveRange(budgetClassList);

            DbContext.Remove(tenant);

            await DbContext.SaveChangesAsync();

            return NoError;
        }
    }
}
