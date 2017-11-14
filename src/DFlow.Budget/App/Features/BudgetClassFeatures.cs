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
        public static readonly string DuplicateByNameError = @"There's another BudgetClass with Name ""{0}"", can't duplicate! (Id={1})";
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

        public async Task<List<ValidationResult>> AddBudgetItemsRangeAsync(BudgetClass budgetClass, IEnumerable<BudgetItem> items)
        {
            foreach (BudgetItem item in items)
            {
                budgetClass.BudgetItems.Add(item);
            }

            budgetClass.Calculate();

            DbContext.Update(budgetClass);

            await DbContext.SaveChangesAsync();

            return NoError;
        }

        public async Task<BudgetClass> FindBudgetClassByNameAsync(string name)
        {
            return await QueryBudgetClasses(bc => bc.Name == name)
                .Include(bc => bc.BudgetItems)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ValidationResult>> ModifyBudgetClassAsync(BudgetClass entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();

            return NoError;
        }

        public IQueryable<BudgetClass> QueryBudgetClasses(Expression<Func<BudgetClass, bool>> where = null)
        {
            IQueryable<BudgetClass> query = DbContext.BudgetClasses
                .Where(bc => bc.Tenant_Id == SessionContext.CurrentTenant.Id);

            if (where != null)
            {
                query = query.Where(where);
            }

            return query;
        }

        public async Task<List<ValidationResult>> RemoveBudgetClassAsync(BudgetClass entity)
        {
            DbContext.Remove(entity);

            await DbContext.SaveChangesAsync();

            return NoError;
        }

        private List<ValidationResult> Error(string message, params object[] values)
        {
            return new List<ValidationResult> { new ValidationResult(string.Format(message, values)) };
        }

        private async Task<BudgetClass> FindDuplicateByNameAsync(BudgetClass entity)
        {
            IQueryable<BudgetClass> query = QueryBudgetClasses(bc => bc.Name == entity.Name);

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
                return Error(DuplicateByNameError, duplicateByName.Name, duplicateByName.Id);
            }

            return NoError;
        }
    }
}
