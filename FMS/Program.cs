using FMS.Domain.Entities.Users;
using FMS.Platform.AppConfiguration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Configure basic settings
builder.AddHttpSecurity();
services.AddDataProtection();

// Configure authentication and authorization
builder.ConfigureAuthentication();
services.AddAuthorizationPolicies();

// Add UI services
services.AddRazorPages();

// Configure bundling and minification
services.AddWebOptimizer();

// Add data stores and initialize the database
await builder.ConfigureDataPersistenceAsync();

// Build the application
var app = builder.Build();

// Configure the application pipeline
app.UseStatusCodePagesWithReExecute("/Error/{0}");
app.UseHttpsRedirection();
app.UseWebOptimizer();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.MapRazorPages().RequireAuthorization();
app.MapControllers();

// Run the app
await app.RunAsync();
