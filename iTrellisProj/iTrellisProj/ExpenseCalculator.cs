using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTrellisProj
{
    public class ExpenseCalculator
    {
        public void CalculateAllDebts(ObservableCollection<Person> people_)
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

        public string PrescribePayments(ObservableCollection<Person> people_)
        {
            // Clone people_ for calculating the prescribed payments
            var clonedList = people_.Select(p => (Person)p.Clone()).ToList();
            ObservableCollection<Person> temp = new ObservableCollection<Person>(clonedList);

            List<Person> PeopleToAccountFor = new List<Person>();
            string MessageToUser = "";
            foreach (Person PersonToSettle in temp)
            {
                // We don't prescribe anything to people that are settled
                if (PersonToSettle.Name.Length > 0 && PersonToSettle.TotalOwed != 0)
                {
                    // No one to settle against, so prepare to settle in the future
                    if (PeopleToAccountFor.Count == 0)
                    {
                        PeopleToAccountFor.Add(PersonToSettle);
                    }
                    // Settle against people that aren't settled up yet
                    else if (PeopleToAccountFor.Count > 0)
                    {
                        // This will be updated when we figure out how much is owed
                        double DebtToPay = 0;
                        foreach (Person PersonToSettleAgainst in PeopleToAccountFor)
                        {
                            // PersonToSettle owes PersonToSettleAgainst
                            if (PersonToSettle.TotalOwed < 0 && PersonToSettleAgainst.TotalOwed > 0)
                            {
                                // Both parties can only settle with the smallest amount between them
                                DebtToPay = Math.Min(Math.Abs(PersonToSettle.TotalOwed), Math.Abs(PersonToSettleAgainst.TotalOwed));
                                if (DebtToPay > 0)
                                {
                                    // Prescribe payment
                                    MessageToUser += String.Format("{0} owes {1} ${2}\n", PersonToSettle.Name, PersonToSettleAgainst.Name, DebtToPay);

                                    // Reduce debt the the amount possible for both parties
                                    PersonToSettle.TotalOwed += DebtToPay;
                                    PersonToSettleAgainst.TotalOwed -= DebtToPay;
                                }
                            }
                            // PersonToSettleAgainst owes PersonToSettle
                            else if (PersonToSettle.TotalOwed > 0 && PersonToSettleAgainst.TotalOwed < 0)
                            {
                                DebtToPay = Math.Min(Math.Abs(PersonToSettle.TotalOwed), Math.Abs(PersonToSettleAgainst.TotalOwed));
                                if (DebtToPay > 0)
                                {
                                    MessageToUser += String.Format("{0} owes {1} ${2}\n", PersonToSettleAgainst.Name, PersonToSettle.Name, DebtToPay);

                                    PersonToSettle.TotalOwed -= DebtToPay;
                                    PersonToSettleAgainst.TotalOwed += DebtToPay;
                                }
                            }
                        }
                        // If they're not done settling
                        // then add them to the list to settle debts with
                        if (PersonToSettle.TotalOwed != 0)
                        {
                            PeopleToAccountFor.Add(PersonToSettle);
                        }
                    }
                }
            }
            // PrescribedSolutionLabel.Content
            return MessageToUser;
        }
    }
}
