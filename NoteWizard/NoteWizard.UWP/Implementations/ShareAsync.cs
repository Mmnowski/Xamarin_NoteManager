using NoteWizard.Models;
using NoteWizard.Services;
using NoteWizard.UWP.Implementations;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

[assembly: Xamarin.Forms.Dependency(typeof(ShareImplementation))]
namespace NoteWizard.UWP.Implementations
{
    class ShareImplementation : IShare
    {
        public async Task ShareAsync(Note item)
        {

            await Task.Delay(1);
            var dataPackage = new DataPackage();
            dataPackage.SetText(item.Description);
            Clipboard.SetContent(dataPackage);

        }
    }
}
