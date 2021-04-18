using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeCS.Entities;

namespace DenRozeCS.Controllers
{
    class UserController
    {
        public void DeleteUser(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.\"User\" WHERE uid = @ID");
        }
        public async Task DeleteUserAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.\"User\" WHERE uid = @ID");
        }
        public void UpdateUser(int id, string password, string phone, string email, string address)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Password", password)
                .AddParamaeter("@Phone", phone)
                .AddParamaeter("@Email", email)
                .AddParamaeter("@Address", address)
                .ExecuteNonQuery("Update DenRoze.\"User\" SET password=@Password, phone=@Phone, email=@Email, address=@Address WHERE uid = @ID");
        }
        public async Task UpdateUserAsync(int id, string password, string phone, string email, string address)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Password", password)
                .AddParamaeter("@Phone", phone)
                .AddParamaeter("@Email", email)
                .AddParamaeter("@Address", address)
                .ExecuteNonQueryAsync("Update DenRoze.\"User\" SET password=@Password, phone=@Phone, email=@Email, address=@Address WHERE uid = @ID");
        }
        public ObservableCollection<UserEntity> GetUserById(int id)
        {
            return new Database()
                .AddParamaeter("@Id", id)
                .ExecuteQuery<UserEntity>("SELECT * FROM DenRoze.\"User\" WHERE uid = @Id");
        }
        public async Task<ObservableCollection<UserEntity>> GetUserByIdAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@Id", id)
                .ExecuteQueryAsync<UserEntity>("SELECT * FROM DenRoze.\"User\" WHERE uid = @Id");
        }
        public ObservableCollection<UserEntity> GetUserByEmail(string email)
        {
            return new Database()
                .AddParamaeter("@Email", email)
                .ExecuteQuery<UserEntity>("SELECT * FROM DenRoze.\"User\" WHERE email = @Email");
        }
        public async Task<ObservableCollection<UserEntity>> GetUserByEmailAsync(string email)
        {
            return await new Database()
                .AddParamaeter("@Email", email)
                .ExecuteQueryAsync<UserEntity>("SELECT * FROM DenRoze.\"User\" WHERE email = @Email");
        }

        public ObservableCollection<UserEntity> GetAllUsers()
        {
            return new Database().ExecuteQuery<UserEntity>("SELECT * FROM DenRoze.\"User\"");
        }

        public async Task<ObservableCollection<UserEntity>> GetAllUsersAsync()
        {
            return await new Database().ExecuteQueryAsync<UserEntity>("SELECT * FROM DenRoze.\"User\"");
        }

        public decimal InsertUser(string login, string full_name, string password, string phone, string email, string address)
        {
            return new Database().AddParamaeter("@Login", login)
                .AddParamaeter("@Full_name", full_name)
                .AddParamaeter("@Password", password)
                .AddParamaeter("@Phone", phone)
                .AddParamaeter("@Email", email)
                .AddParamaeter("@Address", address)
                .ExecuteScalar<decimal>("INSERT INTO DenRoze.\"User\" VALUES (@Login, @Full_name, @Password, @Phone, @Email, @Address); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertUserAsync(string login, string full_name, string password, string phone, string email, string address)
        {
            return await new Database().AddParamaeter("@Login", login)
                .AddParamaeter("@Full_name", full_name)
                .AddParamaeter("@Password", password)
                .AddParamaeter("@Phone", phone)
                .AddParamaeter("@Email", email)
                .AddParamaeter("@Address", address)
                .ExecuteScalarAsync<decimal>("INSERT INTO DenRoze.\"User\" VALUES (@Login, @Full_name, @Password, @Phone, @Email, @Address); SELECT SCOPE_IDENTITY()");
        }
    }
}
