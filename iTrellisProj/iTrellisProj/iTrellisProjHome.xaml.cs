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

                // Re-Bind the Item Source to the Selected Person's Expenses
                ExpensesListBox.ItemsSource = ((Person)this.peopleListBox.SelectedItem).Expenses;

                // Recalculate Total expenses
                ((Person)this.peopleListBox.SelectedItem).CalculateTotalExpenses();

                // Recalculate all Debts
                CalculateAllDebts();
                PrescribePayments();
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
                CalculateAllDebts();
                PrescribePayments();
            }

        }

        private void CalculateAllDebts()
        {
            double TotalDebt = 0;
            double TotalPeople = 0;
            foreach (Person p in people_)
            {
                if (p.Name.Length != 0)
                {
                    TotalDebt += p.TotalExpenses;
                    ++TotalPeople;
                }
            }
            foreach (Person p in people_)
            {
                if (p.Name.Length > 0)
                {
                    p.TotalOwed = p.TotalExpenses - (TotalDebt / TotalPeople);
                }
                else
                {
                    p.TotalOwed = 0;
                }
            }

        }

        private void PrescribePayments()
        {
            // Clone people_ for calculating the prescribed payments
            var clonedList = people_.Select(p => (Person)p.Clone()).ToList();
            ObservableCollection<Person> temp = new ObservableCollection<Person>(clonedList);

            List<Person> PeopleToAccountFor = new List<Person>();
            string MessageToUser = "";
            foreach (Person p in temp)
            {
                if (p.Name.Length > 0 && p.TotalOwed != 0)
                {
                    if (PeopleToAccountFor.Count == 0)
                    {
                        PeopleToAccountFor.Add(p);
                    }
                    else if (PeopleToAccountFor.Count > 0)
                    {
                        double DebtToPay = 0;
                        foreach (Person PersonToSettle in PeopleToAccountFor)
                        {
                            if (p.TotalOwed < 0 && PersonToSettle.TotalOwed > 0)
                            {
                                DebtToPay = Math.Min(Math.Abs(p.TotalOwed), Math.Abs(PersonToSettle.TotalOwed));
                                if (DebtToPay > 0)
                                {
                                    MessageToUser += String.Format("{0} owes {1} ${2}\n", p.Name, PersonToSettle.Name, DebtToPay);

                                    p.TotalOwed += DebtToPay;
                                    PersonToSettle.TotalOwed -= DebtToPay;
                                }
                            }
                            else if (p.TotalOwed > 0 && PersonToSettle.TotalOwed < 0)
                            {
                                DebtToPay = Math.Min(Math.Abs(p.TotalOwed), Math.Abs(PersonToSettle.TotalOwed));
                                if (DebtToPay > 0)
                                {
                                    MessageToUser += String.Format("{0} owes {1} ${2}\n", PersonToSettle.Name, p.Name, DebtToPay);

                                    p.TotalOwed -= DebtToPay;
                                    PersonToSettle.TotalOwed += DebtToPay;
                                }
                            }
                        }
                        // If they're not done settling
                        // then add them to the list to settle debts with
                        if (p.TotalOwed != 0)
                        {
                            PeopleToAccountFor.Add(p);
                        }
                    }
                }
            }

            PrescribedSolutionLabel.Content = MessageToUser;
        }

    }
}
