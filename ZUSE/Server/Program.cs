using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZUSE.Server.Data;
using ZUSE.Server.Managers;
using ZUSE.Server.Mappers;
using ZUSE.Server.Models;
using ZUSE.Server.Services;
using ZUSE.Shared;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
        options.JsonSerializerOptions.Converters.Add(new DefaultDateTimeConverter());
    }
);

builder.Services.AddDbContext<ZUSE_dbContext>(
        options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
        }
    );

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.Authority = "https://zuse.us.auth0.com/";
    options.Audience = "https://client.zuse.solutions";
});

//builder.WebHost.UseUrls("http://127.0.0.1:8080");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapRazorPages();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseCors(
        builder => builder.WithOrigins("*")
                                   .AllowAnyMethod()
                                   .WithHeaders("Authorization")
    );

UserCommunicationPipe.StartConnection("Admin",
    dissconnected: () => { }, connected: () => { }, client_id: new Random().Next().ToString(), onlyPublish: true).Wait();

app.Run();

