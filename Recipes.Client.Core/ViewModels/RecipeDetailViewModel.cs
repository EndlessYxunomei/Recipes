using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Recipes.Client.Core.ViewModels
{
    public class RecipeDetailViewModel: ObservableObject
    {
        
        //нЕ нужно, так как используется тулкит
        //Событие интерфейса INotifyPropertyChanged
        //public event PropertyChangedEventHandler? PropertyChanged;
        //Метод, который будет вызывать событие
        //[CallerMemberName] убирает необходимость указывать nameof(obj) при вызове метода.
        //public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        //    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string Title { get; set; } = "Classic Caesar Salad";
        public string[] Allergens { get; set; } = ["Milk", "Eggs", "Nuts", "Sesame"];
        public int? Calories { get; set; } = 240;
        public int? ReadyInMinutes { get; set; } = 35;
        public DateTime LastUpdated { get; set; } = new DateTime(2020, 7, 3);
        public RecipeRatingsSummaryViewModel RatingSummary { get; } = new(15, 3.6d, 4);
        //Картинкка к рецепту для проверки нулевой подстановки
        public string Image { get; } = "caesarsalad.png";
        //автор рецепта для проверки мултибиндинга
        public string Author { get; set; } = "Sally Burton";

        public List<InstructionBaseViewModel> Instructions { get; }
        public IngredientsListViewModel IngredientsList { get; set; } = new();

        //свойство для проверки OneWayToSource binding mode
        private bool _hideAllergenInformation = true;
        public bool HideAllergenInformation
        {
            
            get => _hideAllergenInformation;
            set => SetProperty(ref _hideAllergenInformation, value);
            /*set
            {
                if(_hideAllergenInformation != value) 
                {
                    _hideAllergenInformation = value;
                    OnPropertyChanged();


                }
            }*/
        }
        //Свойство для проверки команд
        private bool _isFavorite = false;
        public bool IsFavorite
        {
            get => _isFavorite;
            private set
            {
                if (SetProperty(ref _isFavorite, value))
                {
                    AddAsFavoriteCommand.NotifyCanExecuteChanged();
                    RemoveAsFavoriteCommand.NotifyCanExecuteChanged();
                }


                /*if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged();
                    //Обрабатываем возможность запуска команд
                    ((Command)AddAsFavoriteCommand).ChangeCanExecute();
                    ((Command)RemoveAsFavoriteCommand).ChangeCanExecute();

                    ((Command<bool>)SetFavoriteCommand).ChangeCanExecute();
                }*/
            }
        }
        //команды через ICommand
        //Так как .NET не содержит конкретной реализации интерфейса ICommand, то используем реализацию из NET MAUI
        //Для чего включаем его использование в конфиге проекта - не очень кросплатформенно, ну и хрен с ним
        //По умолчанию не вносит записи в референсы проекта, приходится копировать вручную!!!!!!
        /*public ICommand AddAsFavoriteCommand
        {
            get;
        }
        public ICommand RemoveAsFavoriteCommand
        {
            get;
        }*/
        public IRelayCommand AddAsFavoriteCommand { get; }
        public IRelayCommand RemoveAsFavoriteCommand { get; }
        public IRelayCommand UserIsBrowsingCommand { get; }
        //необходимые элементы для команд
        private void AddAsFavorite() => IsFavorite = true;
        private void RemoveAsFavorite() => IsFavorite = false;
        private bool CanAddAsFavorite() => !IsFavorite;
        private bool CanRemoveAsFavorite() => IsFavorite;

        //если использовать одну команду для включения и выключения избранного: команда c параметром
        //private bool CanSteFavorite(bool isFavorite) => IsFavorite != isFavorite;
        //private void SetFavorite(bool isFavorite) => IsFavorite = isFavorite;
        //public ICommand SetFavoriteCommand { get; }
        
        public RecipeDetailViewModel()
        {
            //Задаем команды в конструкторе VM
            //AddAsFavoriteCommand = new Command(AddAsFavorite,CanAddAsFavorite);
            //RemoveAsFavoriteCommand = new Command(RemoveAsFavorite, CanRemoveAsFavorite);
            AddAsFavoriteCommand = new RelayCommand(AddAsFavorite, CanAddAsFavorite);
            RemoveAsFavoriteCommand = new RelayCommand(RemoveAsFavorite, CanRemoveAsFavorite);
            UserIsBrowsingCommand = new RelayCommand(UserIsBrowsing);
            //SetFavoriteCommand = new Command<bool>(SetFavorite, CanSteFavorite);

            Instructions =
        [
            new InstructionViewModel(1, "Preheat your oven to 350°F (175°C). Place the baguette slices on a baking sheet and drizzle them with olive oil. Bake for about 10 minutes or until they are crispy and golden brown. Set aside to cool."),
            new InstructionViewModel(2, "In a small skillet, heat 2 tablespoons of olive oil over medium heat. Add the minced garlic and cook for about 1-2 minutes until fragrant. Remove from heat and let it cool."),
            new InstructionViewModel(3, "In a small bowl, whisk together the lemon juice, grated Parmesan cheese, minced anchovies (if using), and the cooled garlic-oil mixture. Set aside."),
            new InstructionViewModel(4, "Fill a medium-sized saucepan with water and bring it to a boil. Gently place the eggs into the boiling water and cook for 4-5 minutes for soft-boiled eggs or 9-10 minutes for hard-boiled eggs. Once cooked, remove the eggs from the boiling water and place them in a bowl of ice water to cool. Once cool, peel the eggs and set them aside."),
            new InstructionViewModel(5, "In a large salad bowl, add the torn romaine lettuce leaves. Pour the dressing over the lettuce and toss to coat evenly. Season with salt and freshly ground black pepper to taste."),
            new NoteViewModel("To add a smoky flavor to your Caesar Salad, try grilling the romaine lettuce for a few minutes on each side before tearing it into bite-sized pieces."),
            new InstructionViewModel(6, "Break the baguette slices into smaller pieces and add them to the salad. Toss gently to combine."),
            new InstructionViewModel(7, "Cut the peeled eggs into halves or quarters and place them on top of the salad."),
            new InstructionViewModel(8, "Finally, sprinkle some additional grated Parmesan cheese on top as a garnish.")
        ];
        }

        private void UserIsBrowsing()
        {
            //Do Logging
        }
    }
}
