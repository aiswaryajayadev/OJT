using Microsoft.EntityFrameworkCore;
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
using Microsoft.OpenApi.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
builder.Services.AddMediatR(typeof(LoginCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(LogoutCommandHandler).Assembly);


// Register Repositories
builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
             "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
             "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
             "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    { 
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                           },
                           Scheme = "oauth2",
                           Name = "Bearer",
                           In = ParameterLocation.Header,
                       },
                       new List<string>()
                   }
     });


});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Not used in your case
        ValidateAudience = false, // Not used in your case
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
    options.AddPolicy("RequireAdminOrManagerRole", policy =>
         policy.RequireAssertion(context =>
             context.User.IsInRole("Admin") || context.User.IsInRole("Manager")));

});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await RoleInitializer.InitializeAsync(roleManager);
}

app.UseRouting();

app.UseAuthentication(); // Ensure this is in place if using [Authorize]
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Make sure this is present
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
