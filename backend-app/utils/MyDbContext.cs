using Microsoft.EntityFrameworkCore;
using MyApp.Models;
using System.Text.Json;

namespace MyApp.Utils;

public class MyDbContext: DbContext {
    public DbSet<Customer> Customers { get; set; } 

    public MyDbContext( DbContextOptions<MyDbContext> options ) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.Entity<Customer>((table) => {
            System.Console.WriteLine("building Customer table");
            
            table.Property(c => c.Id).ValueGeneratedOnAdd();
            table.Property(t => t.CreatedAt).HasDefaultValue(DateTime.Now);

            List<Customer> source = new List<Customer>();
            using (StreamReader r = new StreamReader("data/customers.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<List<Customer>>(json);
                table.HasData(source);
            }

        });
    }

}
