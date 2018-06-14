using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using NoteWizard.Models;
using NoteWizard.Views;
using NoteWizard.Services;

//using Algorithmia;

namespace NoteWizard.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Note> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public bool _NameVisibility;
        public bool NameVisibility
        {
            get
            {
                return _NameVisibility;
            }
            set
            {
                SetProperty(ref _NameVisibility, value);

            }
        }

        public ItemsViewModel()
        {
            Title = "Notatka";
            Items = new ObservableCollection<Note>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            if (Application.Current.Properties.ContainsKey("Title"))
            {
                NameVisibility = !(bool)Application.Current.Properties["Title"];
            }

            MessagingCenter.Subscribe<NewItemPage, Note>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Note;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<ItemDetailPage, Note>(this, "DeleteItem", async (obj, item) =>
            {
                var _item = item as Note;
                Items.Remove(_item);
                await DataStore.DeleteItemAsync(_item);
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<EditItemPage, Note>(this, "EditItem", async (obj, item) =>
            {
                var _item = item as Note;
                await DataStore.UpdateItemAsync(_item);
                await ExecuteLoadItemsCommand();
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<MockDataStore>(this, "Refresh", async (obj) =>
            {
                await ExecuteLoadItemsCommand();
            });

            MessagingCenter.Subscribe<App>(this, "Refresh", async (obj) =>
            {
                await ExecuteLoadItemsCommand();
            }); 

            MessagingCenter.Subscribe<SettingsViewModel>(this, "ChangedNameEvent", async (obj) =>
            {
                NameVisibility = !NameVisibility;
                await ExecuteLoadItemsCommand();
            });

        }

        public async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
    //            var client = new Client("simoMkaAzhoQ4DC14QZr/Z340yd1");
  //              var algorithm = client.algo("nlp/Summarizer/0.1.7");
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
    //                var response = algorithm.pipe<object>(item.Description);
   //                 item.Description = Convert.ToString(response.result);
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}