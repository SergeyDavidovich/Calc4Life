using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Calc4Life.Models;

namespace Calc4Life.Services.RepositoryServices
{
    public interface IConstantsRepositoryService
    {
        Task<List<Constant>> GetAllAsync();

        Task<Constant> GetByIdAsync(string id);

        Task<List<Constant>> GetAllFavoritesASync();

        Task AddAsync(Constant entity);

        Task DeleteAsync(int id);

        Task UpdateAsync(Constant entity);
    }
}
