﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Calc4Life.Models;
using System.Threading.Tasks;

namespace Calc4Life.Data
{
    public class ConstItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ConstItemDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Constant>().Wait();
        }

        public Task<List<Constant>> GetItemsAsync()
        {
            return database.Table<Constant>().ToListAsync();
        }

        public Task<List<Constant>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Constant>("SELECT * FROM [Constant] WHERE [Done] = 0");
        }

        public Task<Constant> GetItemAsync(int id)
        {
            return database.Table<Constant>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Constant item)
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

        public Task<int> DeleteItemAsync(Constant item)
        {
            return database.DeleteAsync(item);
        }
    }

}