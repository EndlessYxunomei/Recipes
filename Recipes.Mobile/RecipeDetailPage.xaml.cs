using CommunityToolkit.Maui.Behaviors;
using Recipes.Client.Core.ViewModels;
using Recipes.Mobile.Converters;
namespace Recipes.Mobile;


public partial class RecipeDetailPage : ContentPage
{

	public RecipeDetailPage()
	{
		InitializeComponent();
		BindingContext = new RecipeDetailViewModel();
		//binding with c#
		//lblTitle.SetBinding(Label.TextProperty, nameof(RecipeDetailViewModel.Title), BindingMode.OneWay);
	}
    private void Ratings_Tapped(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync("RecipeRating");
    }
}