using Microsoft.EntityFrameworkCore;

namespace ToDoList_API.Models {
	public class ApplicationDatabase : DbContext{

        public ApplicationDatabase(DbContextOptions<ApplicationDatabase> dbContext): base(dbContext) {
        }

       public DbSet<UserTask> Tasks { get; set; }
       
    }
}
