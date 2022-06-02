using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project_54.Model
{
    public class SubCategoris : INotifyPropertyChanged
    {
        private List<string> _list;

        public List<string> list
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyChanged("list");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
