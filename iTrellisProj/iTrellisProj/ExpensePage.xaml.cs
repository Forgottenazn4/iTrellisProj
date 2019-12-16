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
    /// Interaction logic for ExpensePage.xaml
    /// </summary>
    public partial class ExpensePage : Page
    {
        public ExpensePage()
        {
            InitializeComponent();
        }
        // Custom constructor to pass expense report data
        public ExpensePage(object data) : this()
        {
            // Bind to expense report data.
            this.DataContext = data;
        }
    }
}
