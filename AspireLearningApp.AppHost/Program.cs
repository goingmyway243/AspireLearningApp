var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
    .WithImageTag("5.0.9-alpine");

var sql = builder.AddSqlServer("sqlserver")
    .WithImageTag("2022-latest")
    .WithDataVolume("mssql-data")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithHttpEndpoint(1466, 1433);

var db = sql.AddDatabase("database", "NorthwindSampleDb");
var identityDb = sql.AddDatabase("identity-database", "IdentityDb");

builder.AddProject<Projects.AspireLearningApp_UserService>("user-service")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.AspireLearningApp_OrderService>("order-service")
    .WithReference(db)
    .WaitFor(db);

builder.AddProject<Projects.AspireLearningApp_Identity>("identity")
    .WithReference(identityDb)
    .WaitFor(identityDb);

builder.Build().Run();
