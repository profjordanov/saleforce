using FluentValidation;

namespace Saleforce.Customers.Api.Core.Commands
{
    public class RegisterNewCustomerValidator : AbstractValidator<RegisterNewCustomer>
    {
        public RegisterNewCustomerValidator()
        {
            RuleFor(customer => customer.Name).NotEmpty();
            RuleFor(customer => customer.Name).NotNull();           
        }
    }
}