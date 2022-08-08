using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Calc4Life.Models;
using System.Threading.Tasks;
using Calc4Life.Services.RepositoryServices;
using Xamarin.Forms;
using Calc4Life.Helpers;

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
                    Name = "Constant Sample",
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

        public async Task<int> DeleteAsync(Constant item)
        {
            var deletedConstId = await database.DeleteAsync(item);
            MessagingCenter.Send(this, AppConstants.CONSTANTS_UPDATED_MESSAGE);
            return deletedConstId;
        }

        public async Task<int> SaveAsync(Constant item)
        {
            int savedConstId;
            if (item.Id != 0)
            {
                savedConstId = await database.UpdateAsync(item);
            }
            else
            {
                savedConstId = await database.InsertAsync(item);
            }
            MessagingCenter.Send(this, AppConstants.CONSTANTS_UPDATED_MESSAGE);
            return savedConstId;
        }
    }

}
