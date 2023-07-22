using MediatR;
using Microsoft.EntityFrameworkCore;
using YourBuddyPull.Application.Contracts.Data;
using YourBuddyPull.Application.Contracts.EmailSender;
using YourBuddyPull.Application.Contracts.Security;
using YourBuddyPull.Infraestructure.AuthenticationProvider;
using YourBuddyPull.Infraestructure.EmailSender;
using YourBuddyPull.Repository.SQLServer;
using YourBuddyPull.Repository.SQLServer.Repositories;
using YourBuddyPull.Repository.SQLServer.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProyectoLuisRContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IRoutineRepository, RoutineRepository>();
builder.Services.AddScoped<ITrainingSessionRepository, TrainingSessionRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddScoped<IAuthenticationProvider, AuthenticationProvider>();

builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(IMediator).Assembly));
builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(IExerciseRepository).Assembly));


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));

var app = builder.Build();


app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
