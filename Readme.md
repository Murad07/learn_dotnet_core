## Learning Dotnet Core - start 8th March 2025
### Learning source: https://grok.com/share/bGVnYWN5_3b2b72d0-bcc7-48c6-b772-f71aa1e54c45

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