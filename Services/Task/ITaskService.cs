using ToDoList_API.Models;

namespace ToDoList_API.Services.Task {
	public interface ITaskService {
		public Task<ServerResponse<List<TaskDto>>> GetTasks();
		public Task<ServerResponse<string>> AddTask(TaskDto task);
		public Task<ServerResponse<string>> DeleteTask(int id);
		public Task<ServerResponse<TaskDto>> GetSingleTask(int id);
		public Task<ServerResponse<string>> EditTask(TaskDto task);
		public Task<ServerResponse<string>> ChangeTaskStatus(int taskId);
	}
}
