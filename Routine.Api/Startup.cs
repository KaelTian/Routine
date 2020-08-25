using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Routine.Api.Data;
using Routine.Api.Services;
using System;
using System.Linq;

namespace Routine.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();
            //向服务容器注册服务
            services.AddControllers(setup =>
            {
                //支持406 output not acceptable.
                setup.ReturnHttpNotAcceptable = true;
                //增加输出格式xml的支持:XmlDataContractSerializerOutputFormatter,默认OutputFormatters包含一个item并且是json类型的,在json后添加一个xml类型.
                //旧写法
                //setup.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                //修改默认输出格式为xml,输出的数据就不是json的了.
                //setup.OutputFormatters.Insert(0, new XmlDataContractSerializerOutputFormatter());

                //cache 缓存策略
                setup.CacheProfiles.Add("120sCacheProfile", new CacheProfile
                {
                    Duration = 120
                });
            })
            //http patch方法需要通过调用这个方法支持串行化
            .AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            })
            //.net core 3.0的新写法,添加accept-type和content-type支持xml
            //并且这个方法能够支持更多的数据类型,比如:datetimeoffset
            .AddXmlDataContractSerializerFormatters()
            //自定义客户端错误的返回信息,ErrorMessage是由不同的validation验证设置,
            //api body中的内容是自定义的,正常返回400,这里定义返回422
            .ConfigureApiBehaviorOptions(setup =>
            {
                setup.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "http://www.baidu.com",
                        Title = "傻了吧,出错咯!",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "歪比歪比,歪比巴卜!",
                        Instance = context.HttpContext.Request.Path
                    };
                    problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            services.Configure<MvcOptions>(config =>
            {
                var newtonSoftJasonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?
                    .FirstOrDefault();

                newtonSoftJasonOutputFormatter?.SupportedMediaTypes.Add("application/vnd.company.hateoas+json");
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddDbContext<RoutineDbContext>(options =>
            {
                options.UseSqlite("Data Source=routine.db");
            });
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //中间件
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //非开发环境下,自定义处理异常.
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected Error!");
                    });
                });
            }

            app.UseResponseCaching();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
