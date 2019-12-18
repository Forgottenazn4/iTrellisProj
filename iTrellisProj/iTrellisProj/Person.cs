using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTrellisProj
{
    public class Person : PropertyChangeViewModel, ICloneable
    {
        string name_ = "";
        List<Expense> expenses_ = new List<Expense>
        {
            new Expense(0)
        };
        double totalExpenses_ = 0;
        double totalOwed_ = 0;

        public Person(string name)
        {
            Name = name;
        }
        public string Name
        {
            get { return name_; }
            set
            {
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
                OnPropertyChanged("Expenses");
            }
        }
        public double CalculateTotalExpenses()
        {
            // Set Total expenses every time expenses_ updates
            totalExpenses_ = 0;
            foreach (var i in expenses_)
            {
                totalExpenses_ += i.Value;
            }

            totalExpenses_ = Math.Round(totalExpenses_, 2);
            OnPropertyChanged("TotalExpenses");
            return TotalExpenses;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public double TotalExpenses
        {
            get { return totalExpenses_; }
        }

        public double TotalOwed
        {
            get { return totalOwed_; }
            set
            {
                totalOwed_ = Math.Round(value, 2);
                OnPropertyChanged("TotalOwed");
            }

        }
    }
}
