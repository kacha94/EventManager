using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System.Data;

namespace EventManager
{
    public class TaskManagerDB : ITaskManager
    {
        private static NpgsqlConnection _connection = null;
        private static string _eventsTableName = "events2";

        public TaskManagerDB()
        {
            if (!TableExists(_eventsTableName))
            {
                CreateTable();
            }
        }

        public void Add(TaskModel task)
        {

            openConnection();

            try
            {
                var sql = "INSERT INTO "+ _eventsTableName + "(title, description, IsCompleted, create_date) VALUES(@title, @description, @IsCompleted, @create_date)";
                using var cmd = new NpgsqlCommand(sql, _connection);

                cmd.Parameters.AddWithValue("title", task.Title);
                cmd.Parameters.AddWithValue("description", task.Description);
                cmd.Parameters.AddWithValue("IsCompleted", task.IsCompleted);
                cmd.Parameters.AddWithValue("create_date", task.Create_Date);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                closeConnection();
            }
        }

        public void Edit(TaskModel task)
        {
            openConnection();

            try
            {
                var sql = "UPDATE "+ _eventsTableName + " SET title = '" + task.Title + "', description = '" + task.Description + "', IsCompleted = '" + task.IsCompleted + "' WHERE id = '" + task.Id + "'";
                using var cmd = new NpgsqlCommand(sql, _connection);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex);
            }
            finally
            {
                closeConnection();
            }
        }

        public void Delete(TaskModel task)
        {
            openConnection();

            try
            {
                var sql = "DELETE FROM "+ _eventsTableName + " WHERE id = '" + task.Id + "'";
                using var cmd = new NpgsqlCommand(sql, _connection);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex);
            }
            finally
            {
                closeConnection();
            }
        }

        public IEnumerable<TaskModel> Tasks()
        {

            List<TaskModel> _taskList = new List<TaskModel>();

            openConnection();

            try
            {
                string sql = "SELECT * FROM " + _eventsTableName;
                using var cmd = new NpgsqlCommand(sql, _connection);

                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Int32 id = rdr.GetInt32(0);
                    String title = rdr.GetString(1);
                    String description = rdr.GetString(2);
                    bool isCompleted = rdr.GetBoolean(3);
                    DateTime create_date = rdr.GetDateTime(4);

                    TaskModel task = new TaskModel(id.ToString(), title, description, isCompleted, create_date);
                    _taskList.Add(task);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                closeConnection();
            }

            return _taskList;
        }

        private void openConnection()
        {
            try
            {
                _connection = new NpgsqlConnection("Host=localhost;Username=eventmanager;Password=root;Database=eventmanagerdb");
                _connection.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void closeConnection()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }

        private void CreateTable()
        {

            openConnection();
            try
            {
                using var cmd = new NpgsqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = "DROP TABLE IF EXISTS events";
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"CREATE TABLE " + _eventsTableName + "(id SERIAL PRIMARY KEY, Title VARCHAR(255), Description VARCHAR(255), IsCompleted BOOL, create_date DATE)";
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                closeConnection();
            }
        }

        private bool TableExists(string tableName)
        {
            openConnection();
            string sql = "SELECT * FROM information_schema.tables WHERE table_name = '" + tableName + "'";
            using (var cmd = new NpgsqlCommand(sql))
            {
                if (cmd.Connection == null)
                    cmd.Connection = _connection;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                lock (cmd)
                {
                    using (NpgsqlDataReader rdr = cmd.ExecuteReader())
                    {
                        try
                        {
                            if (rdr != null && rdr.HasRows)
                                return true;
                            return false;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
            }
        }
    }
}
