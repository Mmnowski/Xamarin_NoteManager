using System;
using Newtonsoft.Json;
using PCLStorage;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using NoteWizard.Models;
using NoteWizard.Services;
using NoteWizard.ViewModels;
using NoteWizard.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Android.Content.Res;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NoteWizard
{

    public partial class App : Application
	{

        TimeSpan goSleep;
        public App ()
		{
			InitializeComponent();
            MainPage = new MainPage();
        }

		protected override async void OnStart ()
		{
            // Handle when your app starts
            IFolder root = FileSystem.Current.LocalStorage;
            IFolder folder = await root.CreateFolderAsync("NotesCache", CreationCollisionOption.OpenIfExists);
            string fileName = "notatki.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            try
            {
                string content = await file.ReadAllTextAsync();               
                if (content.Length == 0)
                {
                    //MockDataStore.items = new List<Item>();
                    MessagingCenter.Send(this, "LoadItems", new List<Note>());
                }
                else
                {
                    MessagingCenter.Send(this, "LoadItems", JsonConvert.DeserializeObject<List<Note>>(content));
                    //MockDataStore.items = JsonConvert.DeserializeObject<List<Item>>(content);
                }  
            }
            catch (System.NullReferenceException e)
            {
                Debug.WriteLine(e);
            }              
        }

        protected override async void OnSleep()
        {
            Console.WriteLine("Uśpiono");
            goSleep = DateTime.Now.TimeOfDay;

        }
        protected override async void OnResume()
        {
            Console.WriteLine("Wznowiono");
            MessagingCenter.Send(this, "SleepingTime", goSleep);
        }
    }
}
