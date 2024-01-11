using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTfull.Domain;
using Microsoft.EntityFrameworkCore;

namespace RESTfull.Infrastructure.Repository
{
    public class publicationRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public publicationRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<List<Publication>> GetAllAsync()
        {
            return await _context.Publications.ToListAsync();
        }

        public async Task<Publication> GetByIdAsync(Guid id)
        {
            return await _context.Publications.Where(a => a.Id == id)
                .OrderBy(a => a.PublicationName)
                .FirstOrDefaultAsync();
        }

        public async Task<Publication> GetByNameAsync(string name)
        {
            return await _context.Publications
                .Where(a => a.PublicationName == name)
                .FirstOrDefaultAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }

    }
}
