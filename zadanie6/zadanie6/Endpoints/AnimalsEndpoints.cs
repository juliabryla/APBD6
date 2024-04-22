using Microsoft.AspNetCore.WebUtilities;
using zadanie6.DTOs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace zadanie6.Endpoints
{
    public class AnimalsEndpoints
    {
        public static void RegisterAnimalsDapperEndpoints(this WebApplication app)
        {
            app.MapGet("/api/animals", async (IConfiguration configuration, HttpRequest request) =>
            {
          
                var query = request.Query;
                var orderBy = query["orderBy"];
                
                var validColumns = new List<string> { "name", "description", "category", "area" };
                if (string.IsNullOrEmpty(orderBy) || !validColumns.Contains(orderBy))
                {
                    orderBy = "name";
                }

                var animals = new List<AnimalDTO.GetAllAnimals>();
                using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
                {
                    var sqlCommand = new SqlCommand($"SELECT * FROM Animals ORDER BY {orderBy}", sqlConnection);
                    sqlCommand.Connection.Open();
                    var reader = await sqlCommand.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        animals.Add(new AnimalDTO.GetAllAnimals(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4))
                        );
                    }
                }

                return Results.Ok(animals);
            });
            app.MapPut("/api/animals/{idAnimal}", async (IConfiguration configuration, HttpRequest request, int idAnimal) =>
            {
                var updatedAnimal = await request.DeserializeJsonBody<AnimalDTO.UpdateAnimal>();
                using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
                {
                    var sqlCommand = new SqlCommand(@"
                        UPDATE Animals
                        SET Name = @Name, Description = @Description, Category = @Category, Area = @Area
                        WHERE IdAnimal = @IdAnimal", sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@Name", updatedAnimal.Name);
                    sqlCommand.Parameters.AddWithValue("@Description", updatedAnimal.Description);
                    sqlCommand.Parameters.AddWithValue("@Category", updatedAnimal.Category);
                    sqlCommand.Parameters.AddWithValue("@Area", updatedAnimal.Area);
                    sqlCommand.Parameters.AddWithValue("@IdAnimal", idAnimal);

                    sqlCommand.Connection.Open();
                    var rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return Results.NotFound($"Animal with IdAnimal {idAnimal} not found.");
                    }
                }

                return Results.Ok();
            });
        }
    }
}