using Recipes.Client.Core.ViewModels;

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
}