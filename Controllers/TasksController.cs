using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList_API.Models;
using ToDoList_API.Services.Task;

namespace ToDoList_API.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase {
		private readonly ITaskService taskService;

		public TasksController(ITaskService taskService) {
			this.taskService = taskService;
		}

		[HttpGet("get")]
		public async Task<ActionResult<ServerResponse<TaskDto>>> GetTasks() {

			var response = await taskService.GetTasks();
			return Ok(response);

		}

		[HttpGet("get/{id}")]
		public async Task<ActionResult<ServerResponse<TaskDto>>> GetSingleTask(int id) {
			var response = await taskService.GetSingleTask(id);

			if (response.sucess) {
				return Ok(response);
			} else {
				return NotFound(response);
			}
		}
		[HttpPost("changestatus/{id}")]
		public async Task<ActionResult<ServerResponse<TaskDto>>> ChangeTaskStatus(int id) {
			var response = await taskService.ChangeTaskStatus(id);

			if (response.sucess) {
				return Ok(response);
			} else {
				return NotFound(response);
			}
		}

		[HttpPost("edit")]
		public async Task<ActionResult<ServerResponse<TaskDto>>> EditTask(TaskDto task) {
			var response = await taskService.EditTask(task);

			if (response.sucess) {
				return Ok(response);
			} else {
				return NotFound(response);
			}
		}

		[HttpPost("add")]
		public async Task<IActionResult> AddTask(TaskDto task) {
			var response = await taskService.AddTask(task);

			if (response.sucess) {
				return Created("Database", new {message= "Task created"});
			} else {
				return Conflict(response);
			}
		}

		[HttpDelete("delete/{id}")]
		public async Task<IActionResult> AddTask(int id ) {
			var response = await taskService.DeleteTask(id);

			if (response.sucess) {
				return Ok(response);
			} else {
				return BadRequest(response);
			}
		}


	}
}
