namespace ToDoList_API.Models {
	public class TaskDto {

		public int Id { get; set; }
		public string? Name { get; set; } = "task";
		public bool Completed { get; set; } = false;
	}
}
