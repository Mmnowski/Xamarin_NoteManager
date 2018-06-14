using System;
using System.Diagnostics;
using NoteWizard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteWizard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
        SettingsViewModel viewModel;        
        public SettingsPage()
		{
			InitializeComponent ();
            BindingContext = viewModel = new SettingsViewModel();
        }

         async void ToggleName(object sender, EventArgs e)            
        {
            await viewModel.ToggleName();            
        }

         async void ToggleDarkMode(object sender, EventArgs e)
        {
            await viewModel.ToggleDarkMode();
        }

    }
}