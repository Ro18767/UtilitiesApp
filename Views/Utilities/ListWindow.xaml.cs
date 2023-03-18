using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UtilitiesApp.Views.Utilities
{
    using Item = Entities.Utility;
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        public ObservableCollection<Item> List { get; set; } = new();
        public ListWindow()
        {
            InitializeComponent();
            DataContext = this;
            SqlConnection connection = new(App.ConnectionString);

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

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var window = new CRUDWindow();
            if (window.ShowDialog().GetValueOrDefault() && window.Item is not null)
            {

                SqlConnection connection;
                connection = new(App.ConnectionString);
                try
                {
                    connection.Open();

                    Item.Add(window.Item);
                    List.Add(window.Item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            this.ShowDialog();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListViewItem)?.Content as Item;
            if (item is null) return;
            this.Hide();
            var window = new CRUDWindow()
            {
                Item = item
            };
            var index = List.IndexOf(item);

            List.Remove(item);
            var result = window.ShowDialog().GetValueOrDefault();

            if (!result)
            {
                List.Insert(index, window.Item);
                this.Show();
                return;
            }
            SqlConnection connection = new(App.ConnectionString);
            if (result && window.Item is null)
            {
                Item.Remove(item);
                this.ShowDialog();
                return;
            }

            Item.Update(item);

            List.Insert(index, window.Item);
            this.ShowDialog();

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
