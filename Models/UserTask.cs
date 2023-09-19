namespace ToDoList_API.Models {
	public class UserTask {
		public int Id { get; set; }
		public string Name { get; set; } = "";
		public bool Completed { get; set; }= false;
	}
}
