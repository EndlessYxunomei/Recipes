using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Recipes.Client.Core.ViewModels
{
    public class RecipeDetailViewModel: INotifyPropertyChanged
    {
        //Событие интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        //Метод, который будет вызывать событие
        //[CallerMemberName] убирает необходимость указывать nameof(obj) при вызове метода.
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string Title { get; set; } = "Classic Caesar Salad";

        public IngredientsListViewModel IngredientsList { get; set; } = new();

        //свойство для проверки OneWayToSource binding mode
        private bool _showAllergenInformation;
        public bool ShowAllergenInformation
        {
            get => _showAllergenInformation;
            set
            {
                if(_showAllergenInformation != value) 
                {
                    _showAllergenInformation = value;
                    OnPropertyChanged();
                }
            }
        }
        //Свойство для проверки команд
        private bool _isFavorite = false;
        public bool IsFavorite
        {
            get => _isFavorite;
            private set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    OnPropertyChanged();
                    //Обрабатываем возможность запуска команд
                    ((Command)AddAsFavoriteCommand).ChangeCanExecute();
                    ((Command)RemoveAsFavoriteCommand).ChangeCanExecute();

                    ((Command<bool>)SetFavoriteCommand).ChangeCanExecute();
                }
            }
        }
        //команды через ICommand
        //Так как .NET не содержит конкретной реализации интерфейса ICommand, то используем реализацию из NET MAUI
        //Для чего включаем его использование в конфиге проекта - не очень кросплатформенно, ну и хрен с ним
        //По умолчанию не вносит записи в референсы проекта, приходится копировать вручную!!!!!!
        public ICommand AddAsFavoriteCommand
        {
            get;
        }
        public ICommand RemoveAsFavoriteCommand
        {
            get;
        }
        //необходимые элементы для команд
        private void AddAsFavorite() => IsFavorite = true;
        private void RemoveAsFavorite() => IsFavorite = false;
        private bool CanAddAsFavorite() => !IsFavorite;
        private bool CanRemoveAsFavorite() => IsFavorite;

        //если использовать одну команду для включения и выключения избранного: команда c параметром
        private bool CanSteFavorite(bool isFavorite) => IsFavorite != isFavorite;
        private void SetFavorite(bool isFavorite) => IsFavorite = isFavorite;
        public ICommand SetFavoriteCommand { get; }
        
        public RecipeDetailViewModel()
        {
            //Задаем команды в конструкторе VM
            AddAsFavoriteCommand = new Command(AddAsFavorite,CanAddAsFavorite);
            RemoveAsFavoriteCommand = new Command(RemoveAsFavorite, CanRemoveAsFavorite);

            SetFavoriteCommand = new Command<bool>(SetFavorite, CanSteFavorite);
        }
    }
}
