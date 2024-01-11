using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTfull.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace RESTfull.Infrastructure.Repository
{
    public class AuthorRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public AuthorRepository(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.Include(a => a.Publications).OrderBy(a => a.Name).ToListAsync();
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await _context.Authors.Where(a => a.Id == id)
                .OrderBy(a => a.Name)
                .Include(a => a.Publications)
                .FirstOrDefaultAsync();
        }



        public async Task<Author> GetByNameAsync(string name)
        {
            return await _context.Authors
                .Where(a => a.Name == name)
                .Include(a => a.Publications)
                .FirstOrDefaultAsync();
        }

        public async Task<Author> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task UpdateAsync(Author author)
        {
            var existAuthor = GetByIdAsync(author.Id).Result;
            if (existAuthor != null)
            {
                _context.Entry(existAuthor).CurrentValues.SetValues(author);
                foreach (var publicationName in author.Publications)
                {
                    var existpublicationName = existAuthor.Publications.FirstOrDefault(pb => pb.Id == publicationName.Id);
                    if (existpublicationName == null)
                    {
                        existAuthor.Publications.Add(publicationName);
                    }
                    else
                    {
                        _context.Entry(existpublicationName).CurrentValues.SetValues(publicationName);
                    }
                }
                foreach (var existPub in existAuthor.Publications)
                {
                    if (!author.Publications.Any(pb => pb.Id == existPub.Id))
                    {
                        _context.Remove(existPub);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Author author = await _context.Authors.FindAsync(id);
            _context.Remove(author);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Author>> GetByPlace(string pWork)
        {
            return await _context.Authors.Where(a => a.placeWork == pWork).ToListAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
