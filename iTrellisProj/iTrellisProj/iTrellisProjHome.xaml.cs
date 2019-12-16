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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iTrellisProj
{
    /// <summary>
    /// Interaction logic for iTrellisProjHome.xaml
    /// </summary>
    public partial class iTrellisProjHome : Page
    {
        List<Person> people_ = new List<Person>
        {
            new Person() { Name = "Bob"},
            new Person() { Name = "Jim"},
            new Person()
        };
        public iTrellisProjHome()
        {
            InitializeComponent();

            peopleListBox.ItemsSource = people_;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            ExpensePage expenseReportPage = new ExpensePage(this.peopleListBox.SelectedItem);
            this.NavigationService.Navigate(expenseReportPage);

        }
    }
}
