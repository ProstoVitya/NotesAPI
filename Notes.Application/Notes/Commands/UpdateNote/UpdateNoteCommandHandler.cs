using MediatR;
using Notes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly INotesDbContext _dbContext;

        public UpdateNoteCommandHandler(INotesDbContext dbContext)
            => _dbContext = dbContext;


        public async Task<Unit> Handle(UpdateNoteCommand reqiest, CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Notes.FirstOrDefaultAsync(note =>
                    note.Id == reqiest.Id, cancellationToken);

            if(entity == null || entity.UserId != reqiest.UserId)
            {
                throw new NotFoundException(nameof(Note), reqiest.Id);
            }

            entity.Details = reqiest.Deatils;
            entity.Title = reqiest.Title;
            entity.EditDate = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
