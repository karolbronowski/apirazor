using FluentValidation;
using FinalLabProject.Domain.ValueObjects;

namespace FinalLabProject.Application.Artists.Commands.CreateArtist;

public class CreateArtistCommandValidator : AbstractValidator<CreateArtistCommand>
{
    public CreateArtistCommandValidator()
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(v => v.UserName)
            .NotEmpty()
            .WithMessage("Username is required.");
        RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage("Email is required.");
        RuleFor(v => v.Bio)
            .MaximumLength(500);
        RuleFor(v => v.PayoutTier)
            .NotEmpty();
        RuleFor(v => v.PasswordHash)
            .NotEmpty()
            .WithMessage("Password hash is required.");
    }
}