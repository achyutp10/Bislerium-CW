
using Application;
using Application.Interfaces;
using Domain;
using Infrastructures;
using Infrastructures.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Cqrs.Hosts;
namespace Presentation
{
    public class Program
    {
        public static async  Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>();
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
                };
            });
            //Add Authorization
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

           
            //Add Authorization
            builder.Services.AddAuthorizationBuilder();
            builder.Services.AddScoped<IComment, CommentServices>();
            builder.Services.AddScoped<ILikeComment, CommentLikeServices>();
            builder.Services.AddScoped<IBlog,BlogServices>();
            builder.Services.AddScoped<ILike, LikeServices>();
            builder.Services.AddScoped<IAdmin, AdminServices>();
            builder.Services.AddIdentity<AppUser,IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
            builder.Services.AddHttpClient();

            //
            var app = builder.Build();
             using( var scope = app.Services.CreateScope() )
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = ["Admin", "Blogger"];

                foreach (var roleName in roles)
                {
                    if (!await roleManager.RoleExistsAsync(roleName))
                    {
                        await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
             }
            app.MapIdentityApi<AppUser>();

            //add Authentication
          

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();

            //
            //CreateHostBuilder(args).Build().Run();

        }
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //Host.CreateDefaultBuilder(args)
        //    .ConfigureWebHostDefaults(webBuilder =>
        //    {
        //        webBuilder.UseStartup<StartUp>();
        //        webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        //        {
        //            // Other configurations...
        //        });
        //        webBuilder.ConfigureServices(services =>
        //        {
        //            services.AddCors(options =>
        //            {
        //                options.AddPolicy("AllowSpecificOrigin", builder =>
        //                {
        //                    builder.WithOrigins("https://localhost:7119")
        //                           .AllowAnyMethod()
        //                           .AllowAnyHeader();
        //                });
        //            });
        //        });
        //    });
    }
}
