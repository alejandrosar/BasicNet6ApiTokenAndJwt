


var builder = WebApplication.CreateBuilder(args);




BuilderServices.BuildService(builder);

var app = BuilderApp.BuildApp(builder.Build());

app.Run();
