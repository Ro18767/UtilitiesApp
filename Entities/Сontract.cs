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
    using Item = Entities.Contract;
    public class Contract
    {

        public Guid Id { get; }
        
        public Guid IdAddress { get; private set; }
        public Guid IdUtility { get; private set; }
        // Address.List.Find((otherItem) => value == otherItem.Id); 
        public Address? Address
        {
            get
            {
                if (address is null)
                {
                    address = Address.List.Find((otherItem) => IdAddress == otherItem.Id);
                }
                return address;
            }
            set { address = value; IdAddress = value?.Id ?? IdAddress; }
        }

        private Address? address;

        public Utility? Utility { 
            get {
                if(utility is null)
                {
                    utility = Utility.List.Find((otherItem) => IdUtility == otherItem.Id);
                }
                return utility;
            }
            set { utility = value; IdUtility = value?.Id ?? IdUtility; }
        }

        private Utility? utility;
        public Contract(Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
        }
        public static List<Item> List { get; } = null!;
        static Contract()
        {
            SqlConnection connection = new(App.ConnectionString);
            List = new();
            try
            {
                connection.Open();
                String sql = "SELECT Id, IdUtility, IdAddress FROM Сontracts";
                using var cmd = new SqlCommand(sql, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    List.Add(new(reader.GetGuid("Id"))
                    {
                        IdUtility = reader.GetGuid("IdUtility"),
                        IdAddress = reader.GetGuid("IdAddress"),
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

                String sql = "INSERT INTO Сontracts (Id, IdUtility, IdAddress) VALUES (@id, @idUtility, @idAddress)";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@idUtility", item.IdUtility);
                cmd.Parameters.AddWithValue("@idAddress", item.IdAddress);
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
                String sql = $"DELETE FROM Сontracts WHERE Id = @id";
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
                String sql = $"UPDATE Сontracts SET IdUtility = @idUtility, IdAddress = @idAddress WHERE Id = @id";
                using var cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);
                cmd.Parameters.AddWithValue("@idUtility", item.IdUtility);
                cmd.Parameters.AddWithValue("@idAddress", item.IdAddress);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
