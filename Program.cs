using InsuranceAPI.Auth;
using InsuranceAPI.Data;
using InsuranceAPI.Interfaces;
using InsuranceAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InsuranceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<InsuranceProductRepository>();
builder.Services.AddScoped<ICustomer, CustomerRepository>();
builder.Services.AddSingleton<Token>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin() 
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
            ValidIssuer = builder.Configuration["JWT:Issuer"]!,
            ValidAudience = builder.Configuration["JWT:Audience"]!,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.
    AddFluentEmail(builder.Configuration["Email:SenderEmail"], builder.Configuration["Email:Sender"])
    .AddSmtpSender(builder.Configuration["Email:Host"], builder.Configuration.GetValue<int>("Email:Port"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
