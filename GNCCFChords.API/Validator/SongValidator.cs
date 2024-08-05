using FluentValidation;
using GNCCFChords.API.Model.DTO;

namespace GNCCFChords.API.Validator
{
    public sealed class SongValidator: AbstractValidator<SongDTO>
    {
        public SongValidator()
        {
            RuleFor(x => x.SongName).NotEmpty();
        }
    }
}
