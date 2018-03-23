using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Calc4Life.Models;
using System.Threading.Tasks;
using Calc4Life.Services.RepositoryServices;

namespace Calc4Life.Data
{
    public class ConstItemDatabase : IConstantsRepositoryService
    {
        readonly SQLiteAsyncConnection database;

        public ConstItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Constant>().Wait();
        }

        public Task<List<Constant>> GetItemsAsync()
        {
            var list = database.Table<Constant>().ToListAsync().Result;

            if (list.Count == 0)
            {
                SaveAsync(new Constant()
                {
                    Name = "Example of constant",
                    Value = 1234567.89m,
                    Note = "This is the constant example. You can edit or delete this one. Also you can add another constants in the list. Select and press green button to use"
                });
                list = database.Table<Constant>().ToListAsync().Result;
            }

            return database.Table<Constant>().ToListAsync();
        }

        public Task<Constant> GetItemAsync(int id)
        {
            return database.Table<Constant>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Constant>> GetItemsFavoriteAsync()
        {
            return database.QueryAsync<Constant>("SELECT * FROM [Constant] WHERE [IsFavorite] = 0");
        }

        public Task<int> DeleteAsync(Constant item)
        {
            return database.DeleteAsync(item);
        }

        public Task<int> SaveAsync(Constant item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
    }

}
