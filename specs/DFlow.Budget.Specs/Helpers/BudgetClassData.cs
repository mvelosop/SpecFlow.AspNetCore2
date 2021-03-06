﻿using System.Collections.Generic;
using DFlow.Budget.Core.Model;

namespace DFlow.Budget.Specs.Helpers
{
    public class BudgetClassData
    {
        public string FindName { get; set; }

        public string Name { get; set; }

        public int SortOrder { get; set; }

        public TransactionType TransactionType { get; set; }

        public List<BudgetItemData> BudgetItems { get; set; } = new List<BudgetItemData>();
    }
}
