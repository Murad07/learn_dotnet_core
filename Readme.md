## Learning Dotnet Core - start 8th March 2025
### Learning source: https://grok.com/share/bGVnYWN5_3b2b72d0-bcc7-48c6-b772-f71aa1e54c45
### Frontend source code: https://github.com/Murad07/task-manager-frontend

## Day 1 - 8th March 2025: .NET Core Basics & Setup
### Step 1: Install the .NET SDK in Ubuntu
- Update system: sudo apt update && sudo apt upgrade -y
- Install Prerequisites: sudo apt install -y dotnet-sdk-9.0
- Verify installation: dotnet --version

### Step 2: Create a new project
- make directory: mkdir TaskManagerApi
- go to the directory: cd TaskManagerApi

#### Create project: dotnet new webapi
- Run the API: dotnet run

## Day 2 - 9th March 2025: Building a RESTful API with .NET Core
### Step 1: Install Entity Framework Core
- dotnet tool install --global dotnet-ef --version 9.0.0
- dotnet add package Microsoft.EntityFrameworkCore.Sqlite
- dotnet add package Microsoft.EntityFrameworkCore.Design
- verify installation: dotnet ef --version or Open TaskManagerApi.csproj and search for Microsoft.EntityFrameworkCore.Sqlite

### Step 2: Define the Task Model
- Create a new folder: mkdir Models
- Create a new file: touch Models/Task.cs

### Step 3: Set Up the Database Context
- Create a new folder: mkdir Data
- Add Data/TaskContext.cs
- Update Program.cs:
// Add services to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<TaskContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

### run: dotnet ef migrations add InitialCreate
- run: dotnet ef database update

## Day 3 - 10th March 2025: .NET Core Advanced Features
### Step 1: Add Validation with Data Annotations
- Update Models/Task.cs
using System.ComponentModel.DataAnnotations;

namespace TaskManagerApi.Models;

public class Task
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters")]
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}

- Update migrations (since the model changed) run: dotnet ef migrations add AddTaskValidation
dotnet ef database update
- now test by check api from api-testing.http. Pass empty string at title.

### Step 2: Add Global Error Handling
#### Action
- Create Middleware/ExceptionMiddleware.cs
- Register it in Program.cs by adding blew line:
using TaskManagerApi.Middleware;
app.UseExceptionMiddleware();

#### Test
- Temporarily break GetTasks to throw an exception
[HttpGet]
public IActionResult GetTasks()
{
    throw new Exception("Test error");
    var tasks = _context.Tasks.ToList();
    return Ok(tasks);
}
-- Check api-testing.http
GET http://localhost:5134/api/tasks
Content-Type: application/json

### Step 3: Add Logging (Log actions for debugging and monitoring)
#### Action
- Update TasksController to use logging:

public class TasksController : ControllerBase
{
    private readonly TaskContext _context;
    private readonly ILogger<TasksController> _logger;

    public TasksController(TaskContext context, ILogger<TasksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetTasks()
    {
        _logger.LogInformation("Fetching all tasks");
        var tasks = _context.Tasks.ToList();
        return Ok(tasks);
    }

    // Add logging to other methods similarly
}

- Enable logging in Program.cs:

builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Information);
});

#### Test:
- check api-testing.http GET all and check the terminal for “Fetching all tasks”.

## Day 4 - Connect with React Frontend
### Step 1: Handle CORS Issues
- Open TaskManagerApi/Program.cs and update it
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

app.UseCors("AllowReactApp"); // Enable CORS


## Bonus: Fix API Port (Optional)

### Open TaskManagerApi/Properties/launchSettings.json (create it if missing)
{
  "profiles": {
    "TaskManagerApi": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": false,
      "applicationUrl": "https://localhost:7291;http://localhost:5291",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}


