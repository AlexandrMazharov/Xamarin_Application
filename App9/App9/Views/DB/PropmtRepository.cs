using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App9.Views.DB
{
    public class PropmtRepository
    {
        SQLiteConnection database;
        public PropmtRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Prompt>();
        }
        public IEnumerable<Prompt> GetItems()
        {
            return database.Table<Prompt>().ToList();
        }
        public Prompt GetItem(int id)
        {
            return database.Get<Prompt>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Prompt>(id);
        }
        public int Uploaditem(Prompt item) {
            
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
        public int SaveItem(Prompt item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
