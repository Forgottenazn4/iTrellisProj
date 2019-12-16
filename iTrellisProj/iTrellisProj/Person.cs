using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTrellisProj
{
    public class Person
    {
        string name_ = "not here";
        List<Expense> expenses_ = new List<Expense>
        {
            new Expense(100)
        };
        double totalExpenses_ = 0;

        public string Name
        {
            get { return name_; }
            set { name_ = value; }
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
            }
        }

        public double TotalExpenses
        {
            get { return totalExpenses_; }
        }
    }
}
