using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DAL.Models;

namespace WebShop.DAL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDatabaseContext _context;

        public ClientRepository(IDatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<Client> GetAll()
        {
            return _context.Clients;
        }

        public Client Add(Client entity)
        {
            return _context.Clients.Add(entity);
        }

        public void Delete(Client entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Client entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
