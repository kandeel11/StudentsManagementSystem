# Students Management System

An ASP.NET Core MVC app from the ITI Full Stack .NET program for running a training center: departments, courses, instructors and students, and who is enrolled in which course.

## What's in it

- **Accounts and roles** with ASP.NET Core Identity: sign-up, login, and a page for creating roles and assigning them to users.
- **Departments, courses, instructors and students**, each with its own controller, views and repository behind an interface, registered through dependency injection.
- **Enrollment** of students and instructors in courses, through join entities (`Student_Crs`, `Instructor_Crs`) and their own view models.
- **Validation** with custom attributes, for example a unique-value check and an address that must be in Alexandria, Cairo or Ismailia.
- **Mapster** for mapping between entities and view models, **Serilog** for logging, and session state for small per-user values.

## Stack

.NET 10, ASP.NET Core MVC, Entity Framework Core, SQL Server, ASP.NET Core Identity, Mapster, Serilog, Bootstrap

## Run it

1. Set the `cs` connection string in `MVC01/appsettings.json` to your SQL Server.
2. Apply the migrations:

   ```bash
   dotnet ef database update --project MVC01
   ```

3. Start the app:

   ```bash
   dotnet run --project MVC01
   ```
