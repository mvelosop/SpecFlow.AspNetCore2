using DFlow.Budget.Core.Model;

namespace DFlow.Budget.Specs.Helpers
{
    public class BudgetItemDataMapper
    {
        public BudgetItem CreateEntity(BudgetItemData data)
        {
            return new BudgetItem
            {
                BaseAmount = data.BaseAmount,
                Name = data.Name,
                SortOrder = data.SortOrder
            };
        }
    }
}
