using FluentValidation;
using SocialNetwork.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Validators
{
    public class UserRegistrationFormValidator : AbstractValidator<UserRegistrationForm>
    {
		public UserRegistrationFormValidator()
		{
			RuleFor(x => x.FirstName).Length(0, 20);
			RuleFor(x => x.LastName).Length(0, 20);
			RuleFor(x => x.Username).NotNull();
			RuleFor(x => x.Password).MinimumLength(8);
			RuleFor(x => x.DateOfBirth).Must(x => (DateTime.Now - x) > new TimeSpan(5110, 0, 0, 0));
			RuleFor(x => x.Email).EmailAddress();
		}
	}
}
