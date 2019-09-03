using System.Threading;
using System.Threading.Tasks;
using Saleforce.Permissions.Api.Views;

namespace Saleforce.Permissions.Api.Core.Services
{
    public interface IRulesConfigurationService
    {
        Task<RoleRulesView> GetRulesAsync(CancellationToken cancellationToken);
    }
}