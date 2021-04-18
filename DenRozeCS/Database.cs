using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenRozeCS
{
    class Database
    {
        public SqlConnection _connection = null;
        public SqlCommand _cmd = null;

        public Database(string connectionString = @"Server=dbsys.cs.vsb.cz\STUDENT;Database=krc0071;User Id=krc0071;Password=ZBH4QBnO33;")
        {
            this._connection = new SqlConnection(connectionString);
            this._cmd = _connection.CreateCommand();
        }

        public Database AddParamaeter<T>(string name, T value)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = name;
            param.Value = value;
            _cmd.Parameters.Add(param);
            return this;
        }

        public int ExecuteNonQuery(string query)
        {
            int noOfAffectedRows = 0;

            using (_connection)
            {
                _cmd.CommandText = query;
                _connection.Open();
                noOfAffectedRows = _cmd.ExecuteNonQuery();
            }

            return noOfAffectedRows;
        }

        public async Task<int> ExecuteNonQueryAsync(string query)
        {
            int rowsAffected = 0;

            using (_connection)
            {
                _cmd.CommandText = query;
                _connection.Open();
                rowsAffected = await _cmd.ExecuteNonQueryAsync();
            }

            return rowsAffected;
        }

        public ObservableCollection<T> ExecuteQuery<T>(string query)
        {
            ObservableCollection<T> list = new ObservableCollection<T>();
            Type t = typeof(T);

            using (_connection)
            {
                _cmd.CommandText = query;
                _connection.Open();
                var reader = _cmd.ExecuteReader();
                while (reader.Read())
                {
                    T obj = (T)Activator.CreateInstance(t);
                    t.GetProperties().ToList().ForEach(p => {
                        p.SetValue(obj, reader[p.Name]);
                    });
                    list.Add(obj);
                }
            }

            return list;
        }

        public async Task<ObservableCollection<T>> ExecuteQueryAsync<T>(string query)
        {
            ObservableCollection<T> list = new ObservableCollection<T>();
            Type t = typeof(T);

            using (_connection)
            {
                _cmd.CommandText = query;
                _connection.Open();
                var reader = await _cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    T obj = (T)Activator.CreateInstance(t);
                    t.GetProperties().ToList().ForEach(p => {
                        p.SetValue(obj, reader[p.Name]);
                    });
                    list.Add(obj);
                }
            }

            return list;
        }

        public T ExecuteScalar<T>(string query)
        {
            T result = default(T);
            using (_connection)
            {
                _cmd.CommandText = query;
                _connection.Open();
                result = (T)_cmd.ExecuteScalar();
            }
            return result;
        }

        public async Task<T> ExecuteScalarAsync<T>(string query)
        {
            T result = default(T);
            using (_connection)
            {
                _cmd.CommandText = query;
                _connection.Open();
                result = (T) await _cmd.ExecuteScalarAsync();
            }
            return result;
        }
    }
}
