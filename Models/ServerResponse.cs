namespace ToDoList_API.Models {
	public class ServerResponse<T> {

		public T? Data { get; set; }
		public bool sucess { get; set; } = true;
		public string message { get; set; } = "";
	}
}
