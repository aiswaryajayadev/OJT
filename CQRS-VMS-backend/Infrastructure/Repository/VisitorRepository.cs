using Infrastructure.Data;
using Infrastructure.Models;
using Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorManagementDbContext _context;

        public VisitorRepository(VisitorManagementDbContext context)
        {
            _context = context;
        }

      

        public async Task<Visitor> CreateVisitorAsync(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();
            return visitor;
        }

    

       

        public async Task<IEnumerable<Visitor>> GetVisitorDetailsAsync()
        {
            return await _context.Visitors.ToListAsync();
        }
    }

}