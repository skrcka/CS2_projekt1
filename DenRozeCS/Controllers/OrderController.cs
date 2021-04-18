using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeCS.Entities;

namespace DenRozeCS.Controllers
{
    class OrderController
    {
        public void DeleteOrder(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.\"Order\" WHERE oid = @ID");
        }
        public async Task DeleteOrderAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.\"Order\" WHERE oid = @ID");
        }
        public void UpdateOrder(int id, string note, DateTime created_at, DateTime edited_at, int uid)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Note", note)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", edited_at)
                .AddParamaeter("@Uid", uid)
                .ExecuteNonQuery("Update DenRoze.\"Order\" SET note=@Note, created_at=@Created_at, edited_at=@Edited_at, uid=@Uid WHERE oid = @ID");
        }
        public async Task UpdateOrderAsync(int id, string note, DateTime created_at, DateTime edited_at, int uid)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Note", note)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", edited_at)
                .AddParamaeter("@Uid", uid)
                .ExecuteNonQueryAsync("Update DenRoze.\"Order\" SET note=@Note, created_at=@Created_at, edited_at=@Edited_at, uid=@Uid WHERE oid = @ID");
        }
        public ObservableCollection<OrderEntity> GetOrderByDate(DateTime date)
        {
            return new Database()
                .AddParamaeter("@Date", date.Date)
                .ExecuteQuery<OrderEntity>("SELECT * FROM DenRoze.\"Order\" WHERE CONVERT(date, created_at) = @Date");
        }
        public async Task<ObservableCollection<OrderEntity>> GetOrderByDateAsync(DateTime date)
        {
            return await new Database()
                .AddParamaeter("@Date", date.Date)
                .ExecuteQueryAsync<OrderEntity>("SELECT * FROM DenRoze.\"Order\" WHERE CONVERT(date, created_at) = @Date");
        }

        public ObservableCollection<OrderEntity> GetAllOrders()
        {
            return new Database().ExecuteQuery<OrderEntity>("SELECT * FROM DenRoze.\"Order\"");
        }

        public async Task<ObservableCollection<OrderEntity>> GetAllOrdersAsync()
        {
            return await new Database().ExecuteQueryAsync<OrderEntity>("SELECT * FROM DenRoze.\"Order\"");
        }

        public decimal InsertOrder(string note, DateTime created_at, DateTime edited_at, int uid)
        {
            return new Database()
                .AddParamaeter("@Note", note)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", edited_at)
                .AddParamaeter("@Uid", uid)
                .ExecuteScalar<decimal>("INSERT INTO DenRoze.\"Order\" VALUES (@Note, @Created_at, @Edited_at, @Uid); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertOrderAsync(string note, DateTime created_at, DateTime edited_at, int uid)
        {
            return await new Database()
                .AddParamaeter("@Note", note)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", edited_at)
                .AddParamaeter("@Uid", uid)
                .ExecuteScalarAsync<decimal>("INSERT INTO DenRoze.\"Order\" VALUES (@Note, @Created_at, @Edited_at, @Uid); SELECT SCOPE_IDENTITY()");
        }
    }
}
