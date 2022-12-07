using System.Text.Json.Serialization;
using Soulgram.Chat.FileManagement;
using Soulgram.Chat.Persistence;
using Soulgram.Chat.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddMongoDependencies(builder.Configuration);
services.AddFileManager(builder.Configuration);
services.AddApplicationServices();

services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();