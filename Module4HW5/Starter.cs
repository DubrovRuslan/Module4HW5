using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                await Request01();
                await Request02();
                await Request03();
                await Request04();
                await Request05();
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
                    await _context.Titles.AddAsync(new Title { Id = 4, Name = "Tilt" });
                    await _context.Projects.AddAsync(new Project { Id = 6, Name = "Pffff", Budget = 100000, StartedDate = new DateTime(2000, 1, 1), ClientId = 1 });
                    await _context.SaveChangesAsync();
                    await _context.Employees.AddAsync(new Employee { Id = 6, FirstName = "Ya", LastName = "VShoke", OfficeId = 1, TitleId = 4, HiredDate = new DateTime(2020, 1, 1), DateOfBirth = new DateTime(2000, 1, 1) });
                    await _context.SaveChangesAsync();
                    await _context.EmployeeProjects.AddAsync(new EmployeeProject { Id = 6, EmployeeId = 6, ProjectId = 6, Rate = 0, StartDate = new DateTime(2020, 1, 1) });
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
