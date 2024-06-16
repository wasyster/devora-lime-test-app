namespace Solution.Validators;

public class AddHeroesToNewArenaRequestValidator : AbstractValidator<AddHeroesToNewArenaRequest>
{
    public AddHeroesToNewArenaRequestValidator()
    {
        RuleFor(x => x.NumberOfHeros).NotEmpty().WithName("Number of hereos are miisng")
                                     .GreaterThan(1).WithMessage("Numbur of heroes can't be less then 1")
                                     .LessThan(int.MaxValue).WithMessage($"Numbur of heroes can't be more then {int.MaxValue}");
    }
}
