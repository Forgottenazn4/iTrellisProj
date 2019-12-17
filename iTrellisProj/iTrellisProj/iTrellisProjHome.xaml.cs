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
        static List<Person> people_;
        public iTrellisProjHome()
        {
            InitializeComponent();

            peopleListBox.ItemsSource = people_;
            DataContext = this;
        }
        public iTrellisProjHome(object data) : this()
        {
            InitializeComponent();

            peopleListBox.ItemsSource = (List<Person>) data;
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            if (this.peopleListBox.SelectedItem != null)
            {
                ExpensePage expenseReportPage = new ExpensePage(this.peopleListBox.SelectedItem);
                this.NavigationService.Navigate(expenseReportPage);
            }

        }
    }
}
