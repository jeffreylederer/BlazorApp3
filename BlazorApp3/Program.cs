using BlazorApp3.Components;
using BlazorApp3.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BlazerApp1.Services;
using Microsoft.AspNetCore.Components.Authorization;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using BlazerApp1.Models.Mappings;
using Blazored.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using BlazerApp3.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("LeagueApp") ?? throw new InvalidOperationException("Connection string 'LeagueApp' not found.");
builder.Services.AddDbContextFactory<TournamentContext>(opt =>
    opt.UseSqlServer(connectionString));

builder.Services.AddQuickGridEntityFrameworkAdapter();

#region Authentication
//for use SecurityTrimming
builder.Services.AddDNTCommonWeb();
//Authentication
builder.Services.AddScoped<IUnitOfWork, TournamentContext>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRolesService, RolesService>();
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<ICookieValidatorService, CookieValidatorService>();
//builder.Services.AddScoped<IDbInitializerService, DbInitializerService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserInfoService, UserInfoService>();
builder.Services.AddHttpContextAccessor();
// Needed for cookie auth.
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.SlidingExpiration = false;
        options.LoginPath = "/";
        options.LogoutPath = "/";
        //options.AccessDeniedPath = new PathString("/Home/Forbidden/");
        options.Cookie.Name = ".my.app1.cookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = context =>
            {
                var cookieValidatorService = context.HttpContext.RequestServices.GetRequiredService<ICookieValidatorService>();
                return cookieValidatorService.ValidateAsync(context);
            }
        };
    });
#endregion
//AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddBlazoredSessionStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

#region authentication

app.UseAuthentication();
#endregion

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
