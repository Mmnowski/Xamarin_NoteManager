using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NoteWizard.Models;
using NoteWizard.ViewModels;
using NoteWizard.Services;

namespace NoteWizard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Note
            {
                Title = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        async void Usun_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("", "Czy na pewno chcesz usunąć tę notkę?", "Tak", "Nie");
         
            if(answer == true)
            {
                MessagingCenter.Send(this, "DeleteItem", viewModel.Item);
                await Navigation.PopAsync();
            }
            
        }
        async void Edytuj_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditItemPage(new EditViewModel(this.viewModel.Item)));      
        }

        async void Share_Clicked(object sender, EventArgs e)
        {
            await DependencyService.Get<IShare>().ShareAsync(this.viewModel.Item);
        }

    }
}