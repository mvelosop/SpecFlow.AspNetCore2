using DFlow.Budget.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace DFlow.Budget.App.Features
{
    public class BudgetFeatures
    {
        public BudgetFeatures()
        {
            
        }

        public async Task<List<ValidationResult>> AddBudgetClass(BudgetClass budgetClass)
        {
            throw new NotImplementedException();
        }

        internal async Task PurgeBudgetClasses()
        {
            throw new NotImplementedException();
        }

    }
}
