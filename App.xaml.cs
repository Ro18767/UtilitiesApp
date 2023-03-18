using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace UtilitiesApp
{
    
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly string ConnectionString;

        static App()
        {
            string path = Directory.GetCurrentDirectory();

            ConnectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={path}\Utilities.mdf;Integrated Security = True";
        }
    }
}
