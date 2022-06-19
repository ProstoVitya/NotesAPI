using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteNote
{
    public class DeleteNoteValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(deleteNoteCommand => deleteNoteCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
