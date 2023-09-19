using Microsoft.EntityFrameworkCore;
using ToDoList_API.Models;
using ToDoList_API.Services.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDatabase>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options => {
	options.AddPolicy(name: "TaskPolicies",
		policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
		);
});

builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("TaskPolicies");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
