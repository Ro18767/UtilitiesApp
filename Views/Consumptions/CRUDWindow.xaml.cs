using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UtilitiesApp.Entities;

namespace UtilitiesApp.Views.Consumptions
{
    using Item = Entities.Consumption;
    /// <summary>
    /// Interaction logic for CRUDWindow.xaml
    /// </summary>
    public partial class CRUDWindow : Window
    {
        public Item? Item;
        public ObservableCollection<Contract> СontractList = new(Contract.List);

        public bool IsReadonly = false;
        public CRUDWindow()
        {
            InitializeComponent();
            InputContract.ItemsSource = СontractList;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var contract = Item?.Contract ?? Contract.List.First();
            if (Item is null)
            {
                Item = new()
                {
                    Price = contract.Utility?.Price ?? 0,
                };
                ButtonDelete.IsEnabled = false;
            }
            InputContract.SelectedValue = contract;

            UnitName.Content = contract.Utility?.UnitName;
            InputPrice.Text = Item.Price.ToString();
            InputPrice.IsEnabled = false;

            InputContract.SelectedValue = contract;
            InputAmount.Text = Item.Amount.ToString();

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


            if (InputContract.SelectedValue == null)
            {
                MessageBox.Show("Выберете Контракт");
                InputContract.Focus();
                return;
            }

            try
            {
                Convert.ToInt32(InputAmount.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Ведите Количество");
                InputAmount.Focus();
                return;
            }

            try
            {
                Convert.ToInt32(InputPrice.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Почемуто цена неправильная");
                InputPrice.Focus();
                return;
            }
            Item.Contract = InputContract.SelectedValue as Contract;
            Item.Amount = Convert.ToInt32(InputAmount.Text);
            Item.Price = Convert.ToInt32(InputPrice.Text);

            this.DialogResult = true;
            this.Close();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void InputContract_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateContractData();
        }

        private void UpdateContractData()
        {
            var contract = InputContract.SelectedValue as Contract ?? Contract.List.First();

            UnitName.Content = contract.Utility?.UnitName;
            InputPrice.Text = contract.Utility?.Price.ToString();
        }
    }
}
