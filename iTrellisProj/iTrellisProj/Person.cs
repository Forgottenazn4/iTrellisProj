using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTrellisProj
{
    public class Person : ViewModelBase
    {
        string name_ = "";
        List<Expense> expenses_ = new List<Expense>
        {
            new Expense(0)
        };

        double totalExpenses_ = 0;
        public string Name
        {
            get { return name_; }
            set { 
                name_ = value;
                OnPropertyChanged("Name");
            }
        }

        public List<Expense> Expenses
        {
            get { return expenses_; }
            set
            {
                expenses_ = value;
                // Set Total expenses every time expenses_ updates
                totalExpenses_ = 0;
                foreach (var i in expenses_)
                {
                    totalExpenses_ += i.Value;
                }

                OnPropertyChanged("Expenses");
                OnPropertyChanged("TotalExpenses");
            }
        }

        public double TotalExpenses
        {
            get { return totalExpenses_; }
        }
    }
}
