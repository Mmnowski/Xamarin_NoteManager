using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteWizard.Models;
using NoteWizard.Services;
using Newtonsoft.Json;
using PCLStorage;
using Xamarin.Forms;

namespace NoteWizard.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Note Item { get; set; }
        public Command FavItemCommand { get; set; }
        public Command ShareItemCommand { get; set; }
        public string _favfext;
        public string FavText
        {
            get
            { return _favfext; }
            set
            { SetProperty(ref _favfext, value); }
        }
        public ItemDetailViewModel(Note item = null, string favText = "F")
        {
            Title = item?.Title;
            Item = item;
            ShareItemCommand = new Command(async () => await ShareItem());
        }
        public async Task ShareItem()
        {
            await DependencyService.Get<IShare>().ShareAsync(this.Item);
        }
    }
}