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
using Microsoft.EntityFrameworkCore;

namespace Task_4
{
    /// <summary>
    /// Логика взаимодействия для table.xaml
    /// </summary>
    public partial class TableWindow : Window
    {        
        public TableWindow()
        {
            InitializeComponent();
            updateTable();
        }
        private void addUserBtn_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var login = new Microsoft.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@login",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = loginTextBox.Text
                };
                var password = new Microsoft.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@password",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Input,
                    Size = 50,
                    Value = passwordTextBox.Text
                };
                var result = new Microsoft.Data.SqlClient.SqlParameter
                {
                    ParameterName = "@result",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = 250
                };
                db.Database.ExecuteSqlRaw("AddAccount @login, @password, @result OUTPUT", login, password, result);

                resultTextBlock.Text = result.Value.ToString();
            }
            updateTable();
        }

        private void updateTable()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.accounts.Load();
                accountsDG.ItemsSource = db.accounts.Local.ToBindingList();
            }
        }
    }
}
