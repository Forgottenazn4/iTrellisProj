using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTrellisProj
{
    public class PeopleViewModel : ViewModelBase
    {

        List<Person> people_ = new List<Person>
        {
            new Person() { Name = "Bob"},
            new Person() { Name = "Jim"},
            new Person()
        };

        public PeopleViewModel People {get; set;
        //OnPropertyChanged("People");
            }

    }
}
