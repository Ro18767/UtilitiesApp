using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UtilitiesApp.Entities
{
    using Item = Entities.Consumption;
    public class Consumption
    {
        public Guid Id { get; set; }
        public Guid IdСontract { get; private set; }
        public int Amount { get; set; }

        public int Price { get; set; }

        public Contract? Contract
        {
            get
            {

                if (contract is null)
                {

                    contract = Contract.List.Find((otherItem) => IdСontract == otherItem.Id);
                }
                return contract;
            }
            set { contract = value; IdСontract = value?.Id ?? IdСontract; }
        }

        private Contract? contract;

        public Consumption(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }
        public static List<Item> List { get; } = null!;
        static Consumption()
        {
            SqlConnection connection = new(App.ConnectionString);
            List = new();
            try
            {
                connection.Open();
                String sql = "SELECT Id, IdСontract, Amount, Price FROM Consumptions";
                using var cmd = new SqlCommand(sql, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List.Add(new(reader.GetGuid("Id"))
                    {
                        IdСontract = reader.GetGuid("IdСontract"),
                        Amount = reader.GetInt32("Amount"),
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

                String sql = "INSERT INTO Consumptions (Id, IdСontract, Amount, Price) VALUES (@id, @idСontract, @amount, @price)";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@idСontract", item.IdСontract);
                cmd.Parameters.AddWithValue("@amount", item.Amount);
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
                String sql = $"DELETE FROM Consumptions WHERE Id = @id";
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
                connection.Open();//Id, IdСontract, Amount, Price
                String sql = $"UPDATE Consumptions SET IdСontract = @idСontract, Amount = @amount, Price = @price WHERE Id = @id";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@idСontract", item.IdСontract);
                cmd.Parameters.AddWithValue("@amount", item.Amount);
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
