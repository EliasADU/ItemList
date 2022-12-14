using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemsAPI.Models;
using System.Data.SqlClient;

namespace ItemsAPI
{
    public class DBHelper
    {
        public static List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();
            string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = "select * from [dbo].ItemsTable";
                var cmd = new SqlCommand(sql, connection);

                connection.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader == null) throw new ArgumentNullException("reader");

                    if (reader["deadline"] != System.DBNull.Value || reader["Deadline"] != System.DBNull.Value)
                    {
                        Models.Task newTask = new Models.Task();

                        newTask.name = (string)reader["name"];
                        newTask.description = (string)reader["description"];
                        newTask.priority = (string)reader["priority"];
                        newTask.deadline = (string)reader["deadline"];
                        newTask.isCompleted = (bool)reader["isCompleted"];
                        newTask.id = (int)reader["Id"];

                        items.Add(newTask);
                    }
                    else if (reader["start"] != System.DBNull.Value || reader["Start"] != System.DBNull.Value)
                    {
                        Models.Appointment newAppointment = new Models.Appointment();

                        newAppointment.name = (string)reader["name"];
                        newAppointment.description = (string)reader["description"];
                        newAppointment.priority = (string)reader["priority"];
                        newAppointment.start = new DateTimeOffset((DateTime)reader["start"]);
                        newAppointment.end = new DateTimeOffset((DateTime)reader["end"]);
                        newAppointment.attendees = ListConverter.StringToList((string)reader["attendees"]);
                        newAppointment.id = (int)reader["Id"];

                        items.Add(newAppointment);
                    }
                    else
                    {
                        throw new Exception("invalid row");
                    }
                }
                connection.Close();
            }
            return items;
        }

        public static List<Item> GetAllOutstanding()
        {
            List<Item> items = new List<Item>();
            string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = "select * from [dbo].ItemsTable where isCompleted=0";
                var cmd = new SqlCommand(sql, connection);

                connection.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Models.Task newTask = new Models.Task();

                    newTask.name = (string)reader["name"];
                    newTask.description = (string)reader["description"];
                    newTask.priority = (string)reader["priority"];
                    newTask.deadline = (string)reader["deadline"];
                    newTask.isCompleted = (bool)reader["isCompleted"];
                    newTask.id = (int)reader["Id"];

                    items.Add(newTask);
                }
                connection.Close();
            }
            return items;
        }

        public static void InsertItem(Item i)
        {
            try
            {
                Models.Task newTask = (Models.Task)i;

                string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int completed = newTask.isCompleted ? 1 : 0;

                    var sql = "INSERT INTO ITEMSTABLE(Name, description, priority, deadline, isCompleted)" +
                        $" VALUES ('{newTask.name}', '{newTask.description}', '{newTask.priority}', '{newTask.deadline}', '{completed}')";
                    var cmd = new SqlCommand(sql, connection);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                Models.Appointment newAppointment = (Models.Appointment)i;
                DateTime mystart = newAppointment.start.DateTime;

                string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var sql = "INSERT INTO ITEMSTABLE(Name, description, priority, start, [end], attendees)" +
                        $" VALUES ('{newAppointment.name}', '{newAppointment.description}', '{newAppointment.priority}', '{newAppointment.start.DateTime}', '{newAppointment.end.DateTime}', '{ListConverter.ListToString(newAppointment.attendees)}')";
                    var cmd = new SqlCommand(sql, connection);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static void UpdateItem(Item i)
        {
            try
            {
                Models.Task newTask = (Models.Task)i;

                string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    int completed = newTask.isCompleted ? 1 : 0;

                    var sql = "UPDATE [dbo].ItemsTable " +
                        $"set name = '{newTask.name}', description = '{newTask.description}', priority = '{newTask.priority}', deadline = '{newTask.deadline}', isCompleted = {completed} " +
                        $"where Id = {newTask.id}";
                    var cmd = new SqlCommand(sql, connection);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception)
            {
                Models.Appointment newAppointment = (Models.Appointment)i;

                string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var sql = "UPDATE [dbo].ItemsTable " +
                        $"set name = '{newAppointment.name}', description = '{newAppointment.description}', priority = '{newAppointment.priority}', start = '{newAppointment.start.DateTime}', [end] = '{newAppointment.end.DateTime}', attendees = '{ListConverter.ListToString(newAppointment.attendees)}' " +
                        $"where Id = {newAppointment.id}";
                    var cmd = new SqlCommand(sql, connection);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static void DeleteItem(int id)
        {
            string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = $"DELETE FROM [dbo].ItemsTable where Id='{id}'";
                var cmd = new SqlCommand(sql, connection);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static List<Item> GetFromSearch(string searchString)
        {
            List<Item> items = new List<Item>();
            string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var sql = "select * from [dbo].ItemsTable " +
                    $"where name like '%{searchString}%'" +
                    $" OR description like '%{searchString}%'" +
                    $" OR priority like '%{searchString}%'" +
                    $" OR deadline like '%{searchString}%'" +
                    $" OR attendees like '%{searchString}%'";
                var cmd = new SqlCommand(sql, connection);

                connection.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader == null) throw new ArgumentNullException("reader");

                    if (reader["deadline"] != System.DBNull.Value || reader["Deadline"] != System.DBNull.Value)
                    {
                        Models.Task newTask = new Models.Task();

                        newTask.name = (string)reader["name"];
                        newTask.description = (string)reader["description"];
                        newTask.priority = (string)reader["priority"];
                        newTask.deadline = (string)reader["deadline"];
                        newTask.isCompleted = (bool)reader["isCompleted"];
                        newTask.id = (int)reader["Id"];

                        items.Add(newTask);
                    }
                    else if (reader["start"] != System.DBNull.Value || reader["Start"] != System.DBNull.Value)
                    {
                        Models.Appointment newAppointment = new Models.Appointment();

                        newAppointment.name = (string)reader["name"];
                        newAppointment.description = (string)reader["description"];
                        newAppointment.priority = (string)reader["priority"];
                        newAppointment.start = new DateTimeOffset((DateTime)reader["start"]);
                        newAppointment.end = new DateTimeOffset((DateTime)reader["end"]);
                        newAppointment.attendees = ListConverter.StringToList((string)reader["attendees"]);
                        newAppointment.id = (int)reader["Id"];

                        items.Add(newAppointment);
                    }
                    else
                    {
                        throw new Exception("invalid row");
                    }
                }
                connection.Close();
            }
            return items;
        }
    }
}
