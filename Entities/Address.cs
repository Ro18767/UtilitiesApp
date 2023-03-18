using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace UtilitiesApp.Entities
{
    using Item = Entities.Address;
    public class Address
    {
        public Guid Id { get; }
        public string Value { get; set; } = null!;

        public Address(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }
        public static List<Item> List { get; } = null!;
        static Address()
        {
            SqlConnection connection = new(App.ConnectionString);
            List = new();
            try
            {
                connection.Open();
                String sql = "SELECT Id, Value FROM Addresses";
                using var cmd = new SqlCommand(sql, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List.Add(new(reader.GetGuid("Id"))
                    {
                        Value = reader.GetString("Value"),
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

                String sql = "INSERT INTO Addresses (Id, Value) VALUES (@id, @value)";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@value", item.Value);
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
                String sql = $"DELETE FROM Addresses WHERE Id = @id";
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
                String sql = $"UPDATE Addresses SET Value = @value WHERE Id = @id";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@value", item.Value);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
