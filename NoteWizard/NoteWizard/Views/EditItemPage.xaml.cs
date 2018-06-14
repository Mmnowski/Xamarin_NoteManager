using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NoteWizard.Models;
using NoteWizard.Services;
using NoteWizard.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteWizard.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditItemPage : ContentPage
	{
        EditViewModel viewModel;
        public Note Note { get; set; }      

        public EditItemPage(EditViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
            Note = this.viewModel.Note;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();
            if (Note.Title.Length == 0) errors.Add("Nazwa nie może być pusta");
            if (Note.Description.Length == 0) errors.Add("Imie nie może być puste");
            string[] array = errors.ToArray();
            if (array.Length == 0)
            {
                var answer = await DisplayAlert("", "Czy na pewno chcesz nadpisać tę wizytowkę?", "Tak", "Nie");
                if (answer)
                {
                    MessagingCenter.Send(this, "EditItem", Note);
                    await Navigation.PopToRootAsync();
                }
            }
            else
            {
                string message = "";
                for (int i = 0; i < array.Length; i++)
                {
                    message += array[i] + "\n";
                }
                await DisplayAlert("", message, "Ok");
                errors.Clear();
            }
        }
        async void Anuluj_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}