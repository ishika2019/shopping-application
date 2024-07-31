using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using project.Data;
using project.Entities.identity;
using project.Errors;
using project.Implementation;
using project.Interface;
using project.Middleware;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<StoreDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

});
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidateIssuer=true,
        ValidateAudience=false

    };

}

    );
builder.Services.AddAuthorization();
builder.Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>().AddSignInManager<SignInManager<AppUser>>();




builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITokenInterface, TokenInterface>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray(); ;

        var errorResponse = new ApiValidationError
        {
            Errors = errors
         };
        return new BadRequestObjectResult(errorResponse);
    };
}); 

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyMethod()
           .AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();
using var scope=app.Services.CreateScope();
var service=scope.ServiceProvider;
var context=service.GetRequiredService<StoreDbContext>();
var identitycontext = service.GetRequiredService<AppDbContext>();
var usermanager=service.GetRequiredService<UserManager<AppUser>>();
var logger=service.GetRequiredService<ILogger<Program>>();
try
{
    context.Database.Migrate();
    identitycontext.Database.Migrate(); 
    await StoreContextSeed.seedData(context);
    await AppIdentityDbContextSeed.SeedUserAsync(usermanager);
    Console.WriteLine(usermanager);
    identitycontext.Database.Migrate();


}
catch(Exception ex)
{
    logger.LogError(ex, "An error occur during migration");
}
app.MapControllers();

app.Run();
