using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace DataWarehouseViewer.Mvvm.Menus
{
    [ContentProperty(nameof(Children))]
    public class ObservableMenuItem : DependencyObject , IEnumerable
    {
        public ObservableMenuItem()
        {
            Children = new ObservableMenuItemCollection();
        }

        public string ItemName { get; set; }


        public ICommand Command { get; set; }


        public ObservableMenuItemCollection Children { get; set; }

        public void Add(ObservableMenuItem item)
        {
            Children.Add(item);
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Children).GetEnumerator();
        }
    }
}
