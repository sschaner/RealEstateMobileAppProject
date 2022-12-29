using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();

		LblUserName.Text = $"Hi {Preferences.Get("username", string.Empty)}!";

		GetCategories();
		GetTrendingProperties();
	}

	private async void GetCategories()
	{
		var categories = await ApiService.GetCategories();
		CvCategories.ItemsSource = categories;
	}

	private async void GetTrendingProperties()
	{
		var trendingProperties =  await ApiService.GetTrendingProperties();
		CvTopPicks.ItemsSource = trendingProperties;
	}

    private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;

		if (currentSelection is null)
			return;

		Navigation.PushAsync(new PropertiesListPage(currentSelection.Id, currentSelection.Name));

		((CollectionView)sender).SelectedItem = null;
    }

    private void CvTopPicks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as TrendingProperty;

		if (currentSelection is null)
			return;

		Navigation.PushModalAsync(new PropertyDetailPage(currentSelection.Id));

		((CollectionView)sender).SelectedItem = null;
	}

    private void TapSearch_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PushModalAsync(new SearchPage());
    }
}