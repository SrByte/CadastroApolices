using Amazon.Runtime.Internal.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using WebMongoAPI.Models;
namespace WebMongoAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

            builder.Services.AddDbContext<MongoContext>(options =>
            {
                var connectionString = builder.Configuration["MongoDbSettings:ConnectionString"];
                var databaseName = builder.Configuration["MongoDbSettings:DatabaseName"];

                if (string.IsNullOrEmpty(connectionString) || string.IsNullOrEmpty(databaseName))
                {
                    throw new InvalidOperationException(
                        "MongoDbSettings não foi definido no arquivo de configurações ou user secrets."
                    );
                }

                options.UseMongoDB(connectionString, databaseName);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(policy =>
                policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithHeaders(HeaderNames.ContentType)
            );
            app.UseHttpsRedirection();

            // app.UseAuthorization();


            // app.MapControllers();
            app.MapPost("/users", (MongoContext context, User user) =>
             {
                 context.Users.Add(user);
                 context.SaveChanges();
                 return user;
             });

            app.MapGet("/users",async (MongoContext context) =>
            {
                var x = context.Users.ToListAsync();
                return x;
            });

            app.MapPost("/api/v1/apolice", async (MongoContext context, Seguro seguro) =>
                       {
                           context.Seguros.Add(seguro);
                           await context.SaveChangesAsync();
                           return seguro;
                       });

            app.MapPut("/api/v1/apolice", async (MongoContext context, Seguro seguro) =>
            {

                // Anexa a entidade ao contexto
                context.Seguros.Attach(seguro);

                // Marca a entidade como modificada
                context.Entry(seguro).State = EntityState.Modified;

                // Salva as alterações no banco de dados
                await context.SaveChangesAsync();

                return seguro;
            });

            app.MapGet("/api/v1/apolice", async (MongoContext context) =>
            {
                return await context.Seguros.ToListAsync();
            });

            app.MapGet("/api/v1/apolice/{id}", async (string id, MongoContext context) =>
            {
                var apolice = await context.Seguros.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (apolice != null)
                {
                    return Results.Json(apolice);
                }
                else
                {
                    return Results.Json(new Seguro());

                    //  return Results.NotFound($"Apolice com ID {numeroApolice} não encontrada.");
                }
            });

            app.MapDelete("/api/v1/apolice/{id}", async (string id, MongoContext context) =>
            {

                var apolice = await context.Seguros.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (apolice != null)
                {
                    context.Seguros.Remove(apolice);
                    await context.SaveChangesAsync();
                }

            });

            app.Run();
        }
    }
}



