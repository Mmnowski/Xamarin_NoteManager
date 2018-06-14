using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NoteWizard.Models;
using NoteWizard.Services;
using System.Net.Mail;

namespace NoteWizard.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Note Note { get; set; }       

        public NewItemPage()
        {
            InitializeComponent();

            Note = new Note
            {
                Id = Guid.NewGuid().ToString(),
                Title = "",
                Description = ""
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            List<string> errors = new List<string>();

            if (Note.Description.Length == 0) errors.Add("Notatka nie może być pusta");
            if (Note.Title.Length == 0) errors.Add("Podaj tytuł notatki");
            string[] array = errors.ToArray();
            if(array.Length == 0)
            {
                MessagingCenter.Send(this, "AddItem", Note);
                await Navigation.PopModalAsync();
            }
            else
            {
                string message ="";
                for(int i =0; i< array.Length; i++)
                {
                    message += array[i] + "\n";
                }
                await DisplayAlert("", message, "Ok");
                errors.Clear();
            }         
            
        }

        async void Anuluj_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}