using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteWizard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : TabbedPage
	{
		public MainPage ()
		{
			InitializeComponent ();

            MessagingCenter.Subscribe<App, TimeSpan>(this, "SleepingTime", async (obj, goSleep) =>
            {
                await DisplayAlert("", "Sleeping Time: " + (DateTime.Now.TimeOfDay - goSleep).ToString(), "Ok");
            });
        }
	}
}