using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if(!await context.Users.AnyAsync()) {

            List<IdentityUser> users = new List<IdentityUser>();
            users.Add(new IdentityUser{
                UserName = "Paulo", 
                Email = "Paulo@ufs.ac.za"
            });
             users.Add(new IdentityUser{
                UserName = "Sanele", 
                Email = "Sanele@ufs.ac.za"
            });

            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));

                context.Users.Add(user);
            }
           }

            if(!await context.Todo.AnyAsync()) 
              {
                   var userData = await File.ReadAllTextAsync("Data/SeedData.json");
                   var todos = JsonSerializer.Deserialize<List<TodoItem>>(userData);

                   foreach(var todo in todos)
                   {
                       context.Todo.Add(todo);
                   }

              }
         await context.SaveChangesAsync();


        }
    }
}