using BlazerApp1.Models.Mappings;
using BlazerApp3.Services;
using BlazerApp3.Services;
using BlazorApp3.Components;
using BlazorApp3.Model;
using Blazored.SessionStorage;
using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

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
//builder.Services.AddScoped<IRolesService, RolesService>();
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
        options.LoginPath = "/NotAuthorized/RedirectToLogin";
        options.LogoutPath = "/logout";
        //options.AccessDeniedPath = new PathString("/Home/Forbidden/");
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

builder.Services.AddAntiforgery(options =>
{
    // Set Cookie properties using CookieBuilder properties†.
    options.FormFieldName = "AntiforgeryFieldname";
    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
    options.SuppressXFrameOptionsHeader = false;
    options.Cookie.Name ="AFT";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});



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
