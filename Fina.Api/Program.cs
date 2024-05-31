using Fina.Api;
using Fina.Api.Endpoints;
using Fina.Api.Utils.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPoliceName);
//app.UseSecurity();
app.MapEndPoints();

app.Run();
