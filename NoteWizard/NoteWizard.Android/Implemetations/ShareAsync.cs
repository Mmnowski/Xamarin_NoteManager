using System.Threading.Tasks;
using NoteWizard.Models;
using NoteWizard.Services;
using Plugin.Share;

[assembly: Xamarin.Forms.Dependency(typeof(NoteWizard.Droid.Implementations.ShareImplementation))]
namespace NoteWizard.Droid.Implementations
{
    class ShareImplementation : IShare
    {
        public async Task ShareAsync(Note item)
        {
            await CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Title = item.Title,
                Text = item.Description
            });
        }
    }
}
