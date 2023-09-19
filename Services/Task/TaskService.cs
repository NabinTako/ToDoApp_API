using Microsoft.EntityFrameworkCore;
using ToDoList_API.Models;

namespace ToDoList_API.Services.Task {
	public class TaskService : ITaskService {

		private readonly ApplicationDatabase dbContext;

		public TaskService(ApplicationDatabase dbContext)
        {
			this.dbContext = dbContext;
		}
		public async Task<ServerResponse<List<TaskDto>>> GetTasks() {
			var response = new ServerResponse<List<TaskDto>>();
			var tasks = await dbContext.Tasks.ToListAsync();
			response.Data = TaskToTaskDtoConverter(tasks);
			response.message = $"A total of {tasks.FindAll(t => t.Completed == false).Count} task Left";

			return response;

		}
		public async Task<ServerResponse<string>> AddTask(TaskDto task) {
			var response = new ServerResponse<string>();
			var data = TaskDtoToTaskConverter(task);

			response.Data = "";

			try {
				await dbContext.Tasks.AddAsync(data);
				await dbContext.SaveChangesAsync();
				response.message = "Task Added";
			} catch (OperationCanceledException ex) {

				response.sucess = false;
				response.message = "Operation Calcled";

			}
			return response;
		}

		public async Task<ServerResponse<string>> ChangeTaskStatus(int taskId) {
			var response = new ServerResponse<string>();
			var tasks = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
			response.Data = "";

			if (tasks != null) {
				tasks.Completed = !tasks.Completed;
				response.message = "Task status sucessfuly changed";
			} else {
				response.sucess = false;
				response.message = "Task not found";
			}

			await dbContext.SaveChangesAsync();
			return response;
		}
		public async Task<ServerResponse<TaskDto>> GetSingleTask(int id) {
			var response = new ServerResponse<TaskDto>();
			var savedTask = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
			if (savedTask != null) {
				response.Data = TaskToTaskDtoConverter(savedTask);
				response.message = "Task Edited";
			} else {
				response.sucess = false;
				response.message = "Task not found";
			}

			return response;
		}
		public async Task<ServerResponse<string>> DeleteTask(int id) {

			var response = new ServerResponse<string>();
			var taskTodelete = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);

			response.Data = "";

			if (taskTodelete != null) {
				dbContext.Tasks.Remove(taskTodelete);
				await dbContext.SaveChangesAsync();
				response.message = "Task Deleted";
			} else {
				response.sucess = false;
				response.message = "Task not found";
			}

			return response;
		}

		public async Task<ServerResponse<string>> EditTask(TaskDto task) {
			var response = new ServerResponse<string>();
			var savedTask = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id);
			response.Data = "";
			if (savedTask != null) {
				savedTask.Name = task.Name!.Trim();
				savedTask.Completed = task.Completed;
				response.message = "Task Edited";
			} else {
				response.sucess = false;
				response.message = "Task not found";
			}

			await dbContext.SaveChangesAsync();
			return response;
		}

		// Converter Functions >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		private List<TaskDto> TaskToTaskDtoConverter(List<UserTask> tasks) {
			var data = new List<TaskDto>();
			foreach (var task in tasks) {
				data.Add(new TaskDto {
					Id = task.Id,
					Name = task.Name,
					Completed = task.Completed,
				});
			}
			return data;
		}
		private TaskDto TaskToTaskDtoConverter(UserTask task) {

			var data = new TaskDto {
				Id = task.Id,
				Name = task.Name,
				Completed = task.Completed,
			};

			return data;
		}
		private UserTask TaskDtoToTaskConverter(TaskDto task) {

			var data = new UserTask {
				Name = task.Name!,
				Completed = false,
			};

			return data;
		}


	}
}
