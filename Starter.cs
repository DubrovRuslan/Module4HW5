using Microsoft.EntityFrameworkCore;
using Module4HW5.Entities;

namespace Module4HW5
{
    public class Starter
    {
        private ApplicationContext _context;

        public async void Run(DbContextOptions<ApplicationContext> options)
        {
            using (_context = new ApplicationContext(options))
            {
                await Console.Out.WriteLineAsync("Request 1");
                await Request01();
                await Console.Out.WriteLineAsync("Request 2");
                await Request02();
                await Console.Out.WriteLineAsync("Request 3");
                await Request03();
                await Console.Out.WriteLineAsync("Request 4");
                await Request04();
                await Console.Out.WriteLineAsync("Request 5");
                await Request05();
                await Console.Out.WriteLineAsync("Request 6");
                await Request06();
            }

            System.Console.ReadLine();
        }

        public async Task Request01()
        {
            var firstQuery = await _context.Employees
                .Include(i => i.Title)
                .Include(i => i.Office)
                .ToListAsync();
        }

        public async Task<List<int>> Request02()
        {
            return await _context.Employees
                .Select(e => EF.Functions.DateDiffDay(e.HiredDate, DateTime.UtcNow))
                .ToListAsync();
        }

        public async Task Request03()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var first = await _context.Employees.FirstAsync();
                    first.FirstName = "Tilt";
                    await _context.SaveChangesAsync();
                    var second = await _context.Employees.Skip(1).FirstAsync();
                    second.FirstName = "Tiltoson";
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task Request04()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Titles.AddAsync(new Title { Name = "Tilt" });
                    await _context.SaveChangesAsync();
                    var titleId = _context.Titles.Where(t => t.Name == "Tilt").First().Id;
                    await _context.Offices.AddAsync(new Office { Location = "Kharkiv", Title = "Tilt" });
                    await _context.SaveChangesAsync();
                    var officeId = _context.Offices.Where(o => o.Title == "Tilt").First().Id;
                    await _context.Employees.AddAsync(new Employee { FirstName = "Ya", LastName = "VShoke", OfficeId = officeId, TitleId = titleId, HiredDate = new DateTime(2020, 1, 1), DateOfBirth = new DateTime(2000, 1, 1) });
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task Request05()
        {
            await using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var deleteEmployee = await _context.Employees.FirstAsync(e => e.Id == 4);
                    _context.Employees.Remove(deleteEmployee);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                }
            }
        }

        public async Task Request06()
        {
            await _context.Employees
                        .Include(t => t.Title)
                        .GroupBy(g => g.Title.Name)
                        .Select(s => s.Key)
                        .Where(w => EF.Functions.Like(w, "^a"))
                        .ToListAsync();
        }
    }
}
