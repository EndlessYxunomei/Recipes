using CommunityToolkit.Mvvm.ComponentModel;

namespace Recipes.Client.Core.ViewModels
{
    public class IngredientsListViewModel : ObservableObject
    {
        //Событие интерфейса INotifyPropertyChanged
        //public event PropertyChangedEventHandler? PropertyChanged;
        //Метод, который будет вызывать событие
        //[CallerMemberName] убирает необходимость указывать nameof(obj) при вызове метода.
        //public void OnPropertyChanged([CallerMemberName] string?  propertyName = null)
         //   => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private int _numberOfServings = 4;
        //Через тулкит
        public int NumberOfServings
        {
            get => _numberOfServings;
            set
            {
                if (SetProperty(ref _numberOfServings, value))
                {
                    Ingredients.ForEach(i => i.UpdateServings(value));
                }
            }
        }
        //Свойство, которое при изменении вызывает через метод вызывает событие PropertyChanged
        /*public int NumberOfServings
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
        }*/

        public IngredientsListViewModel()
        {
            Ingredients = new()
            {
                new ("Olive oil", .5, "Cup"),
                new ("Garlic (gloves)", 4),
                new ("Baguette", 1),
                new ("Lemon Juice", .25, "Cup"),
                new ("Parmesan cheese (grated)", 4, "Ounce"),
                new ("Anchovies", 1),
                new ("Egg", 2),
                new ("Black pepper", .25, "Teaspoon"),
                new ("Salt", .5,  "Teaspoon"),
                new ("Romaine lettuce", 4)
            };
        }

        public List<RecipeIngredientViewModel> Ingredients
        {
            get;
        }
    }
}
