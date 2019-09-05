using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saleforce.Common.Api;
using Saleforce.Common.Hateoas.Core;

namespace Saleforce.Permissions.Api.Controllers
{
    [ApiController]
    public class RolesController : ApiController
    {
        public RolesController(IResourceMapper resourceMapper, IMediator mediator)
            : base(resourceMapper, mediator)
        {
        }
    }
}