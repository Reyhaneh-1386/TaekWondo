using api.Interfaces;
using api.Repositorys;
using api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

#region MongoDbSettings
///// get values from this file: appsettings.Development.json /////
// get section
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection(nameof(MongoDbSettings)));

// get values
builder.Services.AddSingleton<IMongoDbSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);


// get connectionString to the db
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    MongoDbSettings uri = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;


    return new MongoClient(uri.ConnectionString);
});
#endregion MongoDbSettings

builder.Services.AddScoped<AccountRepository, AccountRepository>();
builder.Services.AddScoped<UserRepository,UserRepository>();
builder.Services.AddScoped<MemberRepository,MemberRepository>();

#region Cors: baraye ta'eede Angular HttpClient requests
builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
    });
#endregion Cors

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();