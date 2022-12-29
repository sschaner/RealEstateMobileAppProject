using RealEstateApp.Models;
using RealEstateApp.Services;
using System.Reflection;

namespace RealEstateApp.Pages;

public partial class PropertyDetailPage : ContentPage
{
    private string phoneNumber;
    private int propertyID;
    private int bookmarkId;
    private bool isBookmarkEnabled;

	public PropertyDetailPage(int propertyId)
	{
		InitializeComponent();
		GetPropertyDetail(propertyId);
        this.propertyID = propertyId;
	}

    private async void GetPropertyDetail(int propertyId)
    {
        var property = await ApiService.GetPropertyDetail(propertyId);
		LblPrice.Text = $"{property.Price:C2}";
		LblDescription.Text = property.Detail;
		LblAddress.Text = property.Address;
		ImgProperty.Source = property.FullImageUrl;
        phoneNumber = property.Phone;

        if (property.Bookmark is null)
        {
            ImgBookmarkBtn.Source = "bookmark_empty_icon";
            isBookmarkEnabled = false;
        }
        else
        {
            ImgBookmarkBtn.Source = property.Bookmark.Status ? "bookmark_fill_icon" : "bookmark_empty_icon";
            bookmarkId = property.Bookmark.Id;
            isBookmarkEnabled = true;
        }
    }

    private void ImgBackBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void TapMessage_Tapped(object sender, TappedEventArgs e)
    {
        var property = await ApiService.GetPropertyDetail(propertyID);

        var message = new SmsMessage($"Hi. I am interested in your property, {property.Name}.", phoneNumber);
        await Sms.ComposeAsync(message);
    }

    private void TapCall_Tapped(object sender, TappedEventArgs e)
    {
        PhoneDialer.Open(phoneNumber);
    }

    private async void ImgBookmarkBtn_Clicked(object sender, EventArgs e)
    {
        if (isBookmarkEnabled == false)
        {
            // Add a bookmark
            var addBookmark = new AddBookmark()
            {
                User_Id = Preferences.Get("userid", 0),
                PropertyId = propertyID
            };

            var response = await ApiService.AddBookmark(addBookmark);

            if (response)
            {
                ImgBookmarkBtn.Source = "bookmark_fill_icon";
                isBookmarkEnabled = true;
            }
        }
        else
        {
            // Delete a bookmark
            var response = await ApiService.DeleteBookmark(bookmarkId);

            if (response)
            {
                ImgBookmarkBtn.Source = "bookmark_empty_icon";
                isBookmarkEnabled = false;
            }
        }
    }
}