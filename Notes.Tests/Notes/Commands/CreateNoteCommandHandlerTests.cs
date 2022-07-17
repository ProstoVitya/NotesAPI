using Notes.Tests.Common;
using Notes.Application.Notes.Commands.CreateNote;
using Microsoft.EntityFrameworkCore;

namespace Notes.Tests.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandGandlerSuccess()
        {
            //Arrange
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "note name";
            var noteDetails = "note details";

            //Act
            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Details = noteDetails,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None);

            //Assert
            Assert.NotNull(
               await Context.Notes.SingleOrDefaultAsync(note =>
                   note.Id == noteId && note.Title == noteName &&
                   note.Details == noteDetails));

        }
    }
}
