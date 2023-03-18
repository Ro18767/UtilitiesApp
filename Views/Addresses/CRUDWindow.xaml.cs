using System;
using System.Collections.Generic;
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

namespace UtilitiesApp.Views.Addresses
{
    using Item = Entities.Address;
    /// <summary>
    /// Interaction logic for CRUDWindow.xaml
    /// </summary>
    public partial class CRUDWindow : Window
    {
        public Item? Item;

        public bool IsReadonly = false;
        public CRUDWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Item is null)
            {
                Item = new();
                ButtonDelete.IsEnabled = false;
            }

            InputId.Text = Item.Id.ToString();
            InputValue.Text = Item.Value;
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы потвержаете удаление из БД?", "Удалиение из БД", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
            {
                return;
            }
            this.Item = null;
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (Item is null) return;

            if (InputValue.Text == String.Empty)
            {
                MessageBox.Show("Ведите Адрес");
                InputValue.Focus();
                return;
            }
            Item.Value = InputValue.Text;
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
