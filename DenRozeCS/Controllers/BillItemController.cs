using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeCS.Entities;

namespace DenRozeCS.Controllers
{
    class BillItemController
    {
        public void DeleteBillItemByOrderId(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.BillItem WHERE oid = @ID");
        }
        public async Task DeleteBillItemByOrderIdAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.BillItem WHERE oid = @ID");
        }
        public void DeleteBillItemByBillId(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.BillItem WHERE bid = @ID");
        }
        public async Task DeleteBillItemByBillIdAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.BillItem WHERE bid = @ID");
        }
        public void DeleteBillItem(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.BillItem WHERE biid = @ID");
        }
        public async Task DeleteBillItemAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.BillItem WHERE biid = @ID");
        }
        public void UpdateBillItem(int id, int count, int iid, int bid)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Iid", iid)
                .AddParamaeter("@Bid", bid)
                .ExecuteNonQuery("Update DenRoze.BillItem SET biid=@ID, count=@Count, iid=@Iid, bid=@Bid");
        }
        public async Task UpdateBillItemAsync(int id, int count, int iid, int bid)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Iid", iid)
                .AddParamaeter("@Bid", bid)
                .ExecuteNonQueryAsync("Update DenRoze.BillItem SET biid=@ID, count=@Count, iid=@Iid, bid=@Bid");
        }
        public ObservableCollection<BillItemEntity> GetBillItemByBill(int id)
        {
            return new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQuery<BillItemEntity>("SELECT * FROM DenRoze.BillItem WHERE bid = @ID");
        }
        public async Task<ObservableCollection<BillItemEntity>> GetBillItemByBillAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQueryAsync<BillItemEntity>("SELECT * FROM DenRoze.BillItem WHERE bid = @ID");
        }
        public ObservableCollection<BillItemEntity> GetBillItemByOrder(int id)
        {
            return new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQuery<BillItemEntity>("SELECT * FROM DenRoze.BillItem WHERE oid = @ID");
        }
        public async Task<ObservableCollection<BillItemEntity>> GetBillItemByOrderAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQueryAsync<BillItemEntity>("SELECT * FROM DenRoze.BillItem WHERE oid = @ID");
        }
        public ObservableCollection<BillItemEntity> GetAllBillItems()
        {
            return new Database().ExecuteQuery<BillItemEntity>("SELECT * FROM DenRoze.BillItem");
        }

        public async Task<ObservableCollection<BillItemEntity>> GetAllBillItemsAsync()
        {
            return await new Database().ExecuteQueryAsync<BillItemEntity>("SELECT * FROM DenRoze.BillItem");
        }

        public decimal InsertBillItem(int count, int iid, int bid, int oid)
        {
            return new Database()
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Iid", iid)
                .AddParamaeter("@Bid", bid)
                .AddParamaeter("@Oid", oid)
                .ExecuteScalar<decimal>(@"INSERT INTO DenRoze.BillItem VALUES (@Count, @Iid, @bID, @Oid); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertBillItemAsync(int count, int iid, int bid, int oid)
        {
            return await new Database()
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Iid", iid)
                .AddParamaeter("@Bid", bid)
                .AddParamaeter("@Oid", oid)
                .ExecuteScalarAsync<decimal>(@"INSERT INTO DenRoze.BillItem VALUES (@Count, @Iid, @bID, @Oid); SELECT SCOPE_IDENTITY()");
        }
    }
}
