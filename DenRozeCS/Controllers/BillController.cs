using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeCS.Entities;

namespace DenRozeCS.Controllers
{
    class BillController
    {
        public void DeleteBill(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.Bill WHERE bid = @ID");
        }
        public async Task DeleteBillAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.Bill WHERE bid = @ID");
        }
        public void UpdateBill(int id, decimal total, string eetinfo, DateTime created_at, DateTime edited_at, string transactionType)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Total", total)
                .AddParamaeter("@Eetinfo", eetinfo)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", edited_at)
                .AddParamaeter("@TransactionType", transactionType)
                .ExecuteNonQuery("Update DenRoze.Bill SET total=@Total, eetinfo=@Eetinfo, created_at=@Created_at, edited_at=@Edited_at, transactiontype=@TransactionType WHERE bid = @ID");
        }
        public async Task UpdateBillAsync(int id, decimal total, string eetinfo, DateTime created_at, DateTime edited_at, string transactionType)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Total", total)
                .AddParamaeter("@Eetinfo", eetinfo)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", edited_at)
                .AddParamaeter("@TransactionType", transactionType)
                .ExecuteNonQueryAsync("Update DenRoze.Bill SET total=@Total, eetinfo=@Eetinfo, created_at=@Created_at, edited_at=@Edited_at, transactiontype=@TransactionType WHERE bid = @ID");
        }
        public ObservableCollection<BillEntity> GetBillByDate(DateTime date)
        {
            return new Database()
                .AddParamaeter("@Date", date.Date)
                .ExecuteQuery<BillEntity>("SELECT * FROM DenRoze.Bill WHERE CONVERT(date, created_at) = @Date");
        }
        public async Task<ObservableCollection<BillEntity>> GetBillByDateAsync(DateTime date)
        {
            return await new Database()
                .AddParamaeter("@Date", date.Date)
                .ExecuteQueryAsync<BillEntity>("SELECT * FROM DenRoze.Bill WHERE CONVERT(date, created_at) = @Date");
        }

        public ObservableCollection<BillEntity> GetAllBills()
        {
            return new Database().ExecuteQuery<BillEntity>("SELECT * FROM DenRoze.Bill");
        }

        public async Task<ObservableCollection<BillEntity>> GetAllBillsAsync()
        {
            return await new Database().ExecuteQueryAsync<BillEntity>("SELECT * FROM DenRoze.Bill");
        }

        public decimal InsertBill(decimal total, string eetinfo, DateTime created_at, DateTime edited_at, string transactionType)
        {
            return new Database()
                .AddParamaeter("@Total", total)
                .AddParamaeter("@Eetinfo", eetinfo)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", edited_at)
                .AddParamaeter("@TransactionType", transactionType)
                .ExecuteScalar<decimal>(@"INSERT INTO DenRoze.Bill VALUES (@Total, @Eetinfo, @Created_at, @Edited_at, @TransactionType); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertBillAsync(decimal total, string eetinfo, DateTime created_at, DateTime edited_at, string transactionType)
        {
            return await new Database()
                .AddParamaeter("@Total", total)
                .AddParamaeter("@Eetinfo", eetinfo)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", edited_at)
                .AddParamaeter("@TransactionType", transactionType)
                .ExecuteScalarAsync<decimal>(@"INSERT INTO DenRoze.Bill VALUES (@Total, @Eetinfo, @Created_at, @Edited_at, @TransactionType); SELECT SCOPE_IDENTITY()");
        }
    }
}
