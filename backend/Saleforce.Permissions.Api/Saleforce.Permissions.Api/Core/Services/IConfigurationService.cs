using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Saleforce.Permissions.Api.Views;

namespace Saleforce.Permissions.Api.Core.Services
{
    public interface IConfigurationService
    {
        Task<List<ConfigurationView>> GetAssignableRoleConfigurationByUserIdAsync(
            string userId,
            CancellationToken cancellationToken);
    }
}