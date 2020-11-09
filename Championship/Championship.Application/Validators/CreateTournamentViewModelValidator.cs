using Championship.Application.ViewModels;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Championship.Application.Validators
{
    public class CreateTournamentViewModelValidator : AbstractValidator<CreateTournamentViewModel>
	{
		public CreateTournamentViewModelValidator()
		{
			RuleFor(x => x.MoviesIds)
				.Must(x => x.Count == 8)
				.WithMessage("No more or less than 8 movies are allowed");
		}
	}
}
