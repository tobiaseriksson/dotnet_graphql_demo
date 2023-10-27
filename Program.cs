

using System.Net.Http.Headers;

using com.nkt.npt.api.fake;
using com.nkt.npt.api.graphql;
using com.nkt.npt.api.model;
using com.nkt.npt.GraphQL.ErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
.AddGraphQLServer()
.AddQueryType(q => q.Name("Query"))
.AddType<Queries>()
.AddType<ServiceQueries>()
.AddType<SpecialDetails>()
.AddType<ToolDetails>()
.AddErrorFilter<GraphQLErrorHandler>()
.AddTypeExtension<ResourceResolvers>()
.AddTypeExtension<ProjectResolvers>()
.AddTypeExtension<EventXResolvers>();

builder.Services.AddSingleton<FakeDB>();

var app = builder.Build();

var fakeDB = app.Services.GetService<FakeDB>();
fakeDB.createFakeData();

app.MapGet("/", () => {
    var response = new HttpResponseMessage();
    response.Content = new StringContent("Go to <a href=\"/graphql\">graphql-ui</a>");
    response.Content.Headers.ContentType=new MediaTypeHeaderValue("text/html");
    return response;
});
app.UseRouting();
app.UseEndpoints( endpoints => {
    endpoints.MapGraphQL();
});

string sep = new('*',30);
Console.WriteLine(sep);
Console.WriteLine("Started!");
Console.WriteLine(sep);
app.Run();