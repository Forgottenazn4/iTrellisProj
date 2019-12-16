using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTrellisProj
{
    public class Expense
    {
        double value_;
        public Expense(double expense)
        {
            value_ = expense;
        }

        public double Value
        {
            get { return value_; }
            set { value_ = value; }
        }
    }
}
