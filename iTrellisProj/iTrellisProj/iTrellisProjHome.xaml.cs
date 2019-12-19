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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iTrellisProj
{
    /// <summary>
    /// Interaction logic for iTrellisProjHome.xaml
    /// </summary>
    public partial class iTrellisProjHome : Page
    {
        private ObservableCollection<Person> people_ = new ObservableCollection<Person>()
        {
            new Person("Bob"),
            new Person("Jimmy"),
            new Person("")
        };

        ExpenseCalculator calculator_ = new ExpenseCalculator();

        public iTrellisProjHome()
        {
            InitializeComponent();
            peopleListBox.ItemsSource = people_;
            DataContext = this;
        }

        private void Person_Click(object sender, RoutedEventArgs e)
        {
            // View Expense Report
            if (this.peopleListBox.SelectedItem != null)
            {
                ExpensesListBox.ItemsSource = ((Person)this.peopleListBox.SelectedItem).Expenses;
                SelectedNameLabel.Content = ((Person)this.peopleListBox.SelectedItem).Name;
            }

        }
        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            // Safety Check
            if (((Person)this.peopleListBox.SelectedItem).Expenses != null)
            {
                // Don't Change the Item Source Directly
                ExpensesListBox.ItemsSource = null;

                // Add a new expense to the selected Person's Expense List
                ((Person)this.peopleListBox.SelectedItem).Expenses.Add(new Expense(0));

                // Update the expenses to get a current total, not a necessity
                UpdateExpense_Click(sender, e);
            }

        }
        private void UpdateExpense_Click(object sender, RoutedEventArgs e)
        {
            // Safety Check
            if (((Person)this.peopleListBox.SelectedItem).Expenses != null)
            {
                // Re-Bind the Item Source to show new updated expense
                ExpensesListBox.ItemsSource = ((Person)this.peopleListBox.SelectedItem).Expenses;

                // Recalculate Total expenses
                ((Person)this.peopleListBox.SelectedItem).CalculateTotalExpenses();

                // Recalculate all Debts
                calculator_.CalculateAllDebts(people_);
                
                // Prescribe payments to the user
                PrescribedSolutionLabel.Content = calculator_.PrescribePayments(people_);
            }
        }


    }
}
