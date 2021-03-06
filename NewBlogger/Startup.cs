﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewBlogger.Application;
using NewBlogger.Application.Interface;
using NewBlogger.Repository.RedisImpl;

namespace NewBlogger
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            // Add framework services.
            services.AddMvc();

            services.AddTransient<IBlogService, BlogService>();

            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<ICommentService, CommentService>();

            services.AddTransient<ITagService, TagService>();

            services.AddTransient(typeof(RedisRepositoryBase), typeof(DefaultRedisRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                //注册area路由
                routes.MapRoute("areaRoute","{area:exists}/{controller=Home}/{action=Index}/{id?}");

                //注册普通路由
                routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
