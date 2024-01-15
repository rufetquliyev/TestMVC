using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Test.Business.ViewModels.UserVMs
{
    public record UserRegisterVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class UserRegisterValidator : AbstractValidator<UserRegisterVm>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("Name cannot be more than 30 characters.");
            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Surname cannot be more than 50 characters.");
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(60)
                .WithMessage("UserName cannot be more than 60 characters.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .Must(x =>
                {
                    Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                    bool match = regex.IsMatch(x);
                    return match;
                })
                .WithMessage("Password is not in correct format.");
            RuleFor(x => x.Password)
                .NotEmpty()
                .Equal(r => r.ConfirmPassword)
                .WithMessage("Password must be equal to confitm password.");
        }
    }
}
