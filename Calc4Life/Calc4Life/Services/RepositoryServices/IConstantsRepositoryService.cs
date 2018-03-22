using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Calc4Life.Models;

namespace Calc4Life.Services.RepositoryServices
{
    public interface IConstantsRepositoryService
    {
        Task<List<Constant>> GetItemsAsync();

        Task<Constant> GetItemAsync(int id);

        Task<List<Constant>> GetItemsFavoriteAsync();

        Task<int> DeleteAsync(Constant item);

        Task<int> SaveAsync(Constant item);


        


        //Task AddAsync(Constant entity);
        //Task UpdateAsync(Constant entity);
    }
}
