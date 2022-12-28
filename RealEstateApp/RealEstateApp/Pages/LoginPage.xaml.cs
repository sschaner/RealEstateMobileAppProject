using RealEstateApp.Services;

namespace RealEstateApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    /// <summary>
    /// Handles the Clicked event of the BtnLogin control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);

        if (response)
        {
            Application.Current.MainPage = new CustomTabbedPage();
        }
        else
        {
            await DisplayAlert("", "Oops. Something went wrong.", "Cancel");
        }
    }

    /// <summary>
    /// Handles the Tapped event of the TapJoinNow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="TappedEventArgs"/> instance containing the event data.</param>
    async void TapJoinNow_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }
}