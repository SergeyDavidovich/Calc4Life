using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Calc4Life.Models;

namespace Calc4Life.Services.RepositoryServices
{
    public class ConstantsRepositoryServiceSQLite : IConstantsRepositoryService
    {
        public Task AddAsync(Constant entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Constant>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Constant>> GetAllFavoritesASync()
        {
            throw new NotImplementedException();
        }

        public Task<Constant> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Constant entity)
        {
            throw new NotImplementedException();
        }
    }
}
