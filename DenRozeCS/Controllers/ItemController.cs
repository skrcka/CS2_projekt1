using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeCS.Entities;

namespace DenRozeCS.Controllers
{
    class ItemController
    {
        public void DeleteItem(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.Item WHERE iid = @ID");
        }
        public async Task DeleteItemAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.Item WHERE iid = @ID");
        }
        public void UpdateItem(int id, string name, string code, decimal price, decimal dph, int count, int mincount, int weight)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Name", name)
                .AddParamaeter("@Code", code)
                .AddParamaeter("@Price", price)
                .AddParamaeter("@DPH", dph)
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Mincount", mincount)
                .AddParamaeter("@Weight", weight)
                .ExecuteNonQuery("Update DenRoze.Item SET name=@Name, code=@Code, price=@Price, dph=@DPH, count=@Count, mincount=@Mincount, weight=@Weight WHERE iid = @ID");
        }
        public async Task UpdateItemAsync(int id, string name, string code, decimal price, decimal dph, int count, int mincount, int weight)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Name", name)
                .AddParamaeter("@Code", code)
                .AddParamaeter("@Price", price)
                .AddParamaeter("@DPH", dph)
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Mincount", mincount)
                .AddParamaeter("@Weight", weight)
                .ExecuteNonQueryAsync("Update DenRoze.Item SET name=@Name, code=@Code, price=@Price, dph=@DPH, count=@Count, mincount=@Mincount, weight=@Weight WHERE iid = @ID");
        }
        public ObservableCollection<ItemEntity> GetItemById(int id)
        {
            return new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQuery<ItemEntity>("SELECT * FROM DenRoze.Item WHERE iid = @ID");
        }
        public async Task<ObservableCollection<ItemEntity>> GetItemByIdAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQueryAsync<ItemEntity>("SELECT * FROM DenRoze.Item WHERE iid = @ID");
        }
        public ObservableCollection<ItemEntity> GetItemByCode(string code)
        {
            return new Database()
                .AddParamaeter("@Code", code)
                .ExecuteQuery<ItemEntity>("SELECT * FROM DenRoze.Item WHERE Code = @Code");
        }
        public async Task<ObservableCollection<ItemEntity>> GetItemByCodeAsync(string code)
        {
            return await new Database()
                .AddParamaeter("@Code", code)
                .ExecuteQueryAsync<ItemEntity>("SELECT * FROM DenRoze.Item WHERE Code = @Code");
        }

        public ObservableCollection<ItemEntity> GetAllItems()
        {
            return new Database().ExecuteQuery<ItemEntity>("SELECT * FROM DenRoze.Item");
        }

        public async Task<ObservableCollection<ItemEntity>> GetAllItemsAsync()
        {
            return await new Database().ExecuteQueryAsync<ItemEntity>("SELECT * FROM DenRoze.Item");
        }

        public decimal InsertItem(string name, string code, decimal price, decimal dph, int count, int mincount, int weight)
        {
            return new Database().AddParamaeter("@Name", name)
                .AddParamaeter("@Code", code)
                .AddParamaeter("@Price", price)
                .AddParamaeter("@DPH", dph)
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Mincount", mincount)
                .AddParamaeter("@Weight", weight)
                .ExecuteScalar<decimal>(@"INSERT INTO DenRoze.Item VALUES (@Name, @Code, @Price, @DPH, @Count, @MinCount, @Weight); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertItemAsync(string name, string code, decimal price, decimal dph, int count, int mincount, int weight)
        {
            return await new Database().AddParamaeter("@Name", name)
                .AddParamaeter("@Code", code)
                .AddParamaeter("@Price", price)
                .AddParamaeter("@DPH", dph)
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Mincount", mincount)
                .AddParamaeter("@Weight", weight)
                .ExecuteScalarAsync<decimal>(@"INSERT INTO DenRoze.Item VALUES (@Name, @Code, @Price, @DPH, @Count, @MinCount, @Weight); SELECT SCOPE_IDENTITY()");
        }
    }
}
