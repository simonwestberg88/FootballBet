using FootballBet.Infrastructure.DependencyInjection;
using TestProject.TestApi;

var builder = WebApplication.CreateBuilder(args);
builder.AddFootballBetDatabase();
builder.Services.AddFootballBetServices();
builder.AddFootballBetConfiguration();
var app = builder.Build();
app.AddTestApi();

app.Run();