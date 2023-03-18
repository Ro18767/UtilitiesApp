/*
 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UtilitiesApp.Entities
{
    using Item = Entities.Payment;
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid IdСontract { get; private set; }
        public int Price { get; set; }

        public Сontract? Contract
        {
            get
            {
                if (contract is null)
                {
                    contract = Сontract.List.Find((otherItem) => IdСontract == otherItem.Id);
                }
                return contract;
            }
            set { contract = value; IdСontract = value?.Id ?? IdСontract; }
        }

        private Сontract? contract;

        public Payment(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }

        public static List<Item> List { get; } = null!;
        static Payment()
        {
            SqlConnection connection = new(App.ConnectionString);
            List = new();
            try
            {
                connection.Open();
                String sql = "SELECT Id, IdСontract, Price FROM Payments";
                using var cmd = new SqlCommand(sql, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List.Add(new(reader.GetGuid("Id"))
                    {
                        IdСontract = reader.GetGuid("IdСontract"),
                        Price = reader.GetInt32("Price"),
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

                String sql = "INSERT INTO Payments (Id, IdСontract, Price) VALUES (@id, @idСontract, @price)";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@idСontract", item.IdСontract);
                cmd.Parameters.AddWithValue("@price", item.Price);
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
                String sql = $"DELETE FROM Payments WHERE Id = @id";
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
                String sql = $"UPDATE Payments SET IdСontract = @idСontract, Price = @price WHERE Id = @id";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@idСontract", item.IdСontract);
                cmd.Parameters.AddWithValue("@price", item.Price);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

 */