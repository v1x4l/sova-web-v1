using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddDbContext<SovaContext>(options => options.UseMySql("server=localhost;database=sova;uid=root;pwd=password"));
            services.AddSingleton<IDataService<User>, UserDataService>();
            services.AddSingleton<IDataService<Answer>, AnswerDataService>();
            services.AddSingleton<IDataService<Comment>, CommentDataService>();
            services.AddSingleton<IDataService<History>, HistoryDataService>();
            services.AddSingleton<IDataService<Marked>, MarkedDataService>();
            services.AddSingleton<IDataService<Topic>, TopicDataService>();
            services.AddSingleton<IDataService<PostTopic>, PostTopicDataService>();
            services.AddSingleton<IDataService<Question>, QuestionDataService>();
            services.AddSingleton<IDataService<SovaUser>, SovaUserDataService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod());

            app.UseDefaultFiles();
            app.UseStaticFiles();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
