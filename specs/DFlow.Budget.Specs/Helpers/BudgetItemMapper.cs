using AutoMapper;
using Domion.Base;
using DFlow.Budget.Core.Model;

namespace DFlow.Budget.Specs.Helpers
{
    public class BudgetItemMapper : IDataMapper<BudgetItemData, BudgetItem>
    {
        private static readonly IMapper Mapper;

        static BudgetItemMapper()
        {
            var config = new MapperConfiguration(cfg => 
                cfg.CreateMap<BudgetItemData, BudgetItem>()
                .ForMember(dest => dest.BudgetClass, opt => opt.Ignore()));

            Mapper = config.CreateMapper();
        }

        public BudgetItemData CreateData(BudgetItem entity)
        {
            throw new System.NotImplementedException();
        }

        public BudgetItem CreateEntity(BudgetItemData data)
        {
            return Mapper.Map<BudgetItem>(data);
        }

        public BudgetItem UpdateEntity(BudgetItemData data, BudgetItem entity)
        {
            return Mapper.Map(data, entity);
        }
    }
}
