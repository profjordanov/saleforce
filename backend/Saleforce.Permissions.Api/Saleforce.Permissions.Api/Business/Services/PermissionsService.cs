using AutoMapper;
using Saleforce.Permissions.Api.Core.Repositories;
using Saleforce.Permissions.Api.Core.Services;

namespace Saleforce.Permissions.Api.Business.Services
{
    public class PermissionsService : IPermissionsService
    {
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IMapper _mapper;

        public PermissionsService(IPermissionsRepository permissionsRepository, IMapper mapper)
        {
            _permissionsRepository = permissionsRepository;
            _mapper = mapper;
        }
    }
}