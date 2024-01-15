using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Business.ViewModels.Common;

namespace Test.Business.ViewModels.BlogVMs
{
    public record UpdateBlogVm : BaseAuditableEntityVm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogVm>
    {
        public UpdateBlogValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Title cannot be more than 50 characters");
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("Description cannot be more than 150 characters");
        }
    }
}
