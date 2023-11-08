using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using CourseWork.Application.Abstractions;
using CourseWork.Application.Services;
using CourseWork.Domain.Abstractions;
using CourseWork.Persistence.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using CourseWork.Persistence.Data;
using CourseWork.Domain.Entities;
using CourseWork.UI.Pages;
using CourseWork.UI.ViewModels;

namespace CourseWork.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        string settingsStream = "CourseWork.UI.appsettings.json";
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream(settingsStream);
        builder.Configuration.AddJsonStream(stream);

#if DEBUG
        builder.Logging.AddDebug();
#endif
        AddDbContext(builder);
        SetupServices(builder.Services);
        //SeedData(builder.Services);
        return builder.Build();
    }
    private static void AddDbContext(MauiAppBuilder builder)
    {
        var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
        string dataDirectory = String.Empty;
#if ANDROID
        dataDirectory = FileSystem.AppDataDirectory + "/";
#endif
        connStr = String.Format(connStr, dataDirectory);
        var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connStr).Options;
        builder.Services.AddSingleton<AppDbContext>((s) => new AppDbContext(options));
    }
    public async static void SeedData(IServiceCollection services)
    {
        using var provider = services.BuildServiceProvider();
        var unitOfWork = provider.GetService<IUnitOfWork>();
        await unitOfWork.RemoveDatabaseAsync();
        await unitOfWork.CreateDatabaseAsync();
        IReadOnlyList<User> Users = new List<User>()
        {
            new User(){ Id=1, Name="Vitya", Email="ss@gmail.com", Password = "789", Tasks =new List<UserTask>()},
            new User(){ Id=2,  Name="Jopa", Email="zz@gmail.com", Password = "345", Tasks = new List<UserTask>()}
        };
        foreach (var user in Users)
        {
            await unitOfWork.UserRepository.AddAsync(user);
        }
        await unitOfWork.SaveAllAsync();

        Random rand = new Random();
        int k = 1;
        foreach (var user in Users)
            for (int j = 0; j < 10; j++)
                await unitOfWork.UserTaskRepository.AddAsync(new UserTask()
                {
                    Id = k,
                    Name = $"Attraction {k++}",
                    UserId = user.Id,
                    Location = "Minsk",
                    Description = "iiiiii",
                    StartDateTime = DateTime.Now,
                    EndDateTime = DateTime.Now,
                    IsCompleted = false
                });
        await unitOfWork.SaveAllAsync();
    }

    private static void SetupServices(IServiceCollection services)
    {
        services.AddSingleton<SignInPage>();
        services.AddTransient<AddTaskPage>();
        services.AddSingleton<SignUpPage>();
        services.AddSingleton<LoginManagerViewModel>();

        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        services.AddSingleton<IUserTaskService, UserTaskService>();
        services.AddSingleton<IUserService, UserService>();

        services.AddSingleton<TasksManager>();
        services.AddSingleton<TasksManagerViewModel>();

        services.AddTransient<TaskDetailsPage>();
        services.AddTransient<TaskDetailsViewModel>();

        services.AddTransient<TaskEditPage>();
        services.AddTransient<TaskEditViewModel>();

        services.AddTransient<UserPage>();
        services.AddTransient<UserViewModel>();
    }
}
