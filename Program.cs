using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module4HW5;

var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory());
builder.AddJsonFile("config.json");
var config = builder.Build();
var connectionString = config.GetConnectionString("DefaultConnection");

var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
var options = optionsBuilder.
    UseSqlServer(connectionString)
    .Options;
var starter = new Starter();
starter.Run(options);
Console.ReadKey();
