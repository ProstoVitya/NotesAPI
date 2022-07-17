using Notes.Persistance;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;

namespace Notes.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();


        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();
            context.Notes.AddRange(
                    new Note
                    {
                        CreatedDate = DateTime.Today,
                        Details = "Details1",
                        EditDate = null,
                        Id = Guid.Parse("2AAC2DBA-C235-4D57-A17F-76E59CCBBBAD"),
                        Title = "Title1",
                        UserId = UserAId
                    },
                    new Note
                    {
                        CreatedDate = DateTime.Today,
                        Details = "Details2",
                        EditDate = null,
                        Id = Guid.Parse("E07A8ACB-0055-4A69-AD4A-10BF4F7F4A9A"),
                        Title = "Title2",
                        UserId = UserBId
                    },
                    new Note
                    {
                        CreatedDate = DateTime.Today,
                        Details = "Details3",
                        EditDate = null,
                        Id = NoteIdForDelete,
                        Title = "Title3",
                        UserId = UserAId
                    },
                    new Note
                    {
                        CreatedDate = DateTime.Today,
                        Details = "Details4",
                        EditDate = null,
                        Id = NoteIdForUpdate,
                        Title = "Title4",
                        UserId = UserBId
                    }
                );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
