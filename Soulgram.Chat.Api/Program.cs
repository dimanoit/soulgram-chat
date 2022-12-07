using Soulgram.Chat.Contracts;
using Soulgram.Chat.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("api/chat",
    async (CreateChatRequest request, CancellationToken cancellationToken, IChatManagementService service) =>
        await service.CreateChatAsync(request, cancellationToken));

app.MapPost("message", async () => { });

app.MapGet("message", async () => { });

app.Run();