using DFlow.Budget.Core.Model;

namespace DFlow.Budget.App
{
    public class SessionContext
    {
        public SessionContext(
            Tenant tenant)
        {
            CurrentTenant = tenant;
        }

        public Tenant CurrentTenant { get; }
    }
}
