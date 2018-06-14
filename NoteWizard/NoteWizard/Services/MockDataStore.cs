using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using NoteWizard.Models;
using System.Net;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(NoteWizard.Services.MockDataStore))]
namespace NoteWizard.Services
{
    public class MockDataStore : IDataStore<Note>
    {
        List<Note> items;
        public MockDataStore()
        {
            MessagingCenter.Subscribe<App, List<Note>>(this, "LoadItems", async (obj, item) =>
            {
                items = item as List<Note>;
                MessagingCenter.Send(this, "Refresh");
                await Task.CompletedTask;
            });
        }
        public async Task<bool> AddItemAsync(Note item)
        {
            items.Add(item);
            await SaveToFile();
            return await Task.FromResult(true);
        }
        public async Task<bool> UpdateItemAsync(Note item)
        {
            var _item = items.Where((Note arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            await SaveToFile();
            items.Add(item);

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteItemAsync(Note item)
        {
            var _item = items.Where((Note arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            return await Task.FromResult(true);
        }
        public async Task<Note> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }
        public async Task<IEnumerable<Note>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
        public async Task SaveToFile()
        {
            IFolder root = FileSystem.Current.LocalStorage;
            IFolder folder = await root.CreateFolderAsync("NotesCache", CreationCollisionOption.OpenIfExists);
            string fileName = "notatki.txt";
            IFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            List<Note> notes = new List<Note>();
            file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            string json = JsonConvert.SerializeObject(items);
            await file.WriteAllTextAsync(json);
        }
    }
}