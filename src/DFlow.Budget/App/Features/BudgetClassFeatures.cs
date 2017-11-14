using DFlow.Budget.Core.Model;
using DFlow.Budget.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DFlow.Budget.App.Features
{
    public class BudgetClassFeatures
    {
        private readonly Lazy<BudgetDbContext> _lazyDbContext;

        public BudgetClassFeatures(
            SessionContext sessionContext,
            Lazy<BudgetDbContext> lazyDbContext)
        {
            _lazyDbContext = lazyDbContext;
            SessionContext = sessionContext;
        }

        public static List<ValidationResult> NoError => new List<ValidationResult>();

        public SessionContext SessionContext { get; }

        private BudgetDbContext DbContext => _lazyDbContext.Value;

        public async Task<List<ValidationResult>> AddBudgetClassAsync(BudgetClass entity)
        {
            entity.Tenant_Id = SessionContext.CurrentTenant.Id;

            var errors = await ValidateSaveAsync(entity);

            if (errors.Any()) return errors;

            DbContext.Add(entity);

            await DbContext.SaveChangesAsync();

            return NoError;
        }

        public IQueryable<BudgetClass> QueryBudgetClasses(Expression<Func<BudgetClass, bool>> where = null)
        {
            return where == null ? DbContext.BudgetClasses : DbContext.BudgetClasses.Where(where);
        }

        private List<ValidationResult> Error(string message)
        {
            return new List<ValidationResult> { new ValidationResult(message) };
        }

        private async Task<BudgetClass> FindDuplicateByNameAsync(BudgetClass entity)
        {
            IQueryable<BudgetClass> query = DbContext.BudgetClasses.Where(bc => bc.Name == entity.Name);

            if (entity.Id == 0)
            {
                query = query.Where(bc => bc.Id != entity.Id);
            }

            return await query.FirstOrDefaultAsync();
        }

        private async Task<List<ValidationResult>> ValidateSaveAsync(BudgetClass entity)
        {
            BudgetClass duplicateByName = await FindDuplicateByNameAsync(entity);

            if (duplicateByName != null)
            {
                return Error($@"There's another BudgetClass with Name ""{duplicateByName.Name}"" (Id={duplicateByName.Id})");
            }

            return NoError;
        }
    }
}
