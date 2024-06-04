using FluentValidation;
using UserService.Api.PostModels.Roles;

namespace UserService.Infrastructure.Validators
{
	public class RoleUpdateValidator : AbstractValidator<RoleUpdateModel>
	{
		public RoleUpdateValidator()
		{
			RuleFor(r => r.Name).NotNull().NotEmpty().WithMessage("Введите непустое название роли!")
				.MinimumLength(2).WithMessage("Название роли должно быть минимум от 2-х символов!");
		}
	}
}
