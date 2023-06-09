using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using OnlineCinema.Data;
using OnlineCinema.WebApi.ApiDescriptors;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using OnlineCinema.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using OnlineCinema.Logic.Mapper;
using OnlineCinema.Logic.Services.IServices;
using OnlineCinema.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineCinema.Data.Repositories;
using OnlineCinema.Data.Repositories.IRepositories;
using OnlineCinema.Logic.Response.IResponse;
using OnlineCinema.Logic.Response;

namespace OnlineCinema.WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", ApiDescriptor.GetApiInfo(builder.Configuration));
                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });

            builder.Services.AddIdentity<UserEntity, RoleEntity>(config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequiredLength = 6;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true; //???
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!))
                };
            }).AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["Google:Authentication:ClientId"]!;
                googleOptions.ClientSecret = builder.Configuration["Google:Authentication:ClientSecret"]!;
            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IGenreRepository, GenreRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
         
            builder.Services.AddTransient<IEmailSender, EmailSenderService>();
            builder.Services.AddTransient<IMessageService, MessageService>();
            builder.Services.AddTransient<IMovieService, MovieService>();
            builder.Services.AddTransient<ISeasonService, SeasonService>();
            builder.Services.AddTransient<IEpisodeService, EpisodeService>();

            builder.Services.AddTransient<IBlobService, BlobService>();
            builder.Services.AddTransient<ILikeService, LikeService>();
            builder.Services.AddTransient<IFavoriteMovieService, FavoriteMovieService>();
            builder.Services.AddTransient<IViewedService, ViewedService>();

            builder.Services.AddTransient<IMovieRepository, MovieRepository>();
            builder.Services.AddTransient<ISeasonRepository, SeasonRepository>();
            builder.Services.AddTransient<IEpisodeRepository, EpisodeRepository>();

            builder.Services.AddTransient<IMovieTagRepository, MovieTagRepository>();
            builder.Services.AddTransient<IMovieGenreRepository, MovieGenreRepository>();

            builder.Services.AddTransient<IUserManagerResponse, UserManagerResponse>();
            builder.Services.AddTransient<IOperationResponse, OperationResponse>();
            builder.Services.AddTransient<IGenreService, GenreService>();
            builder.Services.AddTransient<ITagService, TagService>();

            builder.Services.AddAutoMapper(typeof(MapperConfig));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            app.Run();
        }
    }
}