using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saleforce.Common.Api;
using Saleforce.Common.Hateoas.Core;

namespace Saleforce.Customers.Api.Controllers
{
    public class CustomersController : ApiController
    {
        public CustomersController(
            IResourceMapper resourceMapper,
            IMediator mediator) : base(resourceMapper, mediator)
        {
        }


    }
}