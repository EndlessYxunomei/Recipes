using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.Client.Core.ViewModels
{
    public class IngredientsListViewModel : INotifyPropertyChanged
    {
        //Событие интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        //Метод, который будет вызывать событие
        //[CallerMemberName] убирает необходимость указывать nameof(obj) при вызове метода.
        public void OnPropertyChanged([CallerMemberName] string?  propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private int _numberOfServings = 4;
        //Свойство, которое при изменении вызывает через метод вызывает событие PropertyChanged
        public int NumberOfServings
        {
            get => _numberOfServings;
            set
            {
                if (_numberOfServings != value)
                {
                    _numberOfServings = value;
                    OnPropertyChanged();//можно указать nameof(NumberOfServings), но мы внедрили [CallerMemberName]
                }
            }
        }
    }
}
