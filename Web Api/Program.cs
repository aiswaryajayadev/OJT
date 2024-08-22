using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Application.Command_Handler;
using Application.Validators;
using Infrastructure.Repository.IRepository;
using Infrastructure.Repository;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<VisitorManagementDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(typeof(CreateVisitorCommandHandler).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(CreateVisitorCommandValidator).Assembly);
builder.Services.AddIdentity<User, IdentityRole<int>>()
        .AddEntityFrameworkStores<VisitorManagementDbContext>()
        .AddDefaultTokenProviders();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddMediatR(typeof(CreateUserCommandHandler).Assembly);

// Register Repositories
builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
