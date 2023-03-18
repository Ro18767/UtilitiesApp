using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for CRUDWindow.xaml
    /// </summary>
    public partial class CRUDWindow : Window
    {
        public Item? Item;
        public CRUDWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Item is null)  // режим "C" - добавление отдела
            {
                Item = new();
                ButtonDelete.IsEnabled = false;
            }
            InputId.Text = Item.Id.ToString();
            InputName.Text = Item.Name;
            InputPrice.Text = Item.Price.ToString();
            InputUnitName.Text = Item.UnitName;
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

            if (InputName.Text == String.Empty)
            {
                MessageBox.Show("Ведите Название Услуги");
                InputName.Focus();
                return;
            }
            try
            {
                Convert.ToInt32(InputPrice.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ведите Цену");
                InputPrice.Focus();
                return;
            }

            Item.Name = InputName.Text;
            Item.Price = Convert.ToInt32(InputPrice.Text);
            Item.UnitName = InputUnitName.Text;
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
