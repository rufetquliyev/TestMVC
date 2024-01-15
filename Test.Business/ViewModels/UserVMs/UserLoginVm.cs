using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test.Business.ViewModels.UserVMs
{
    public record UserLoginVm
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginValidator : AbstractValidator<UserLoginVm>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Username / Email cannot be more than 50 characters.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .Must(x =>
                {
                    Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                    Match match = regex.Match(x);
                    return match.Success;
                });
        }
    }
}
