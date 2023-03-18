using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UtilitiesApp.Entities
{
    using Item = Entities.Utility;
    public class Utility
    {
        public Guid Id { get; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public string UnitName { get; set; } = null!;

        public Utility(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }
        public static List<Item> List { get; } = null!;
        static Utility()
        {
            SqlConnection connection = new(App.ConnectionString);
            List = new();
            try
            {
                connection.Open();
                String sql = "SELECT Id, Name, Price, UnitName FROM Utilities";
                using var cmd = new SqlCommand(sql, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List.Add(new(reader.GetGuid("Id"))
                    {
                        Name = reader.GetString("Name"),
                        Price = reader.GetInt32("Price"),
                        UnitName = reader.GetString("UnitName"),
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Add(Item item)
        {
            if (item is null) return;
            SqlConnection connection;
            connection = new(App.ConnectionString);
            try
            {
                connection.Open();

                    String sql = "INSERT INTO Utilities (Id, Name, Price, UnitName) VALUES (@id, @name, @price, @unitName)";
                    using var cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@id", item.Id);
                    cmd.Parameters.AddWithValue("@name", item.Name);
                    cmd.Parameters.AddWithValue("@price", item.Price);
                    cmd.Parameters.AddWithValue("@unitName", item.UnitName);
                    cmd.ExecuteNonQuery();
                    List.Add(item);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Remove(Item item)
        {
            if (item is null) return;
            SqlConnection connection;
            connection = new(App.ConnectionString);
            try
            {
                connection.Open();
                String sql = $"DELETE FROM Utilities WHERE Id = @id";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void Update(Item item)
        {
            if (item is null) return;
            SqlConnection connection;
            connection = new(App.ConnectionString);
            try
            {
                connection.Open();
                String sql = $"UPDATE Utilities SET Name = @name, Price = @price, UnitName = @unitName WHERE Id = @id";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@name", item.Name);
                cmd.Parameters.AddWithValue("@price", item.Price);
                cmd.Parameters.AddWithValue("@unitName", item.UnitName);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
