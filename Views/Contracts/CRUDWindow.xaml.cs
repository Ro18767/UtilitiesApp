using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UtilitiesApp.Entities;

namespace UtilitiesApp.Views.Contracts
{
    using Item = Entities.Contract;
    /// <summary>
    /// Interaction logic for CRUDWindow.xaml
    /// </summary>
    public partial class CRUDWindow : Window
    {
        public Item? Item;
        public ObservableCollection<Utility> UtilityList = new(Utility.List);
        public ObservableCollection<Address> AddressList = new(Address.List);

        public bool IsReadonly = false;
        public CRUDWindow()
        {
            InitializeComponent();
            InputUtility.ItemsSource = UtilityList;
            InputAddress.ItemsSource = AddressList;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Item is null)
            {
                Item = new();
                ButtonDelete.IsEnabled = false;
            }

            InputUtility.SelectedValue = Item.Utility ?? Utility.List.First();
            InputAddress.SelectedValue = Item.Address ?? Address.List.First();

            InputId.Text = Item.Id.ToString();
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


            if (InputUtility.SelectedValue == null)
            {
                MessageBox.Show("Выберете Услугу");
                InputUtility.Focus();
                return;
            }

            if (InputAddress.SelectedValue == null)
            {
                MessageBox.Show("Выберете Адрес");
                InputAddress.Focus();
                return;
            }

            Item.Address = InputAddress.SelectedValue as Address;
            Item.Utility = InputUtility.SelectedValue as Utility;

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

