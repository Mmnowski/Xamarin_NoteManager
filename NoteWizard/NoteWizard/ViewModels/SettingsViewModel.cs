using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace NoteWizard.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public bool _toggled;
        public bool _darkMode;
        public static string _SortType;
        public bool Toggled
        {
            get
            {
                return _toggled;
            }
            set
            {
                SetProperty(ref _toggled, value);
                
            }
        }

        public bool DarkMode
        {
            get
            {
                return _darkMode;
            }
            set
            {
                SetProperty(ref _darkMode, value);
                
            }
        }

        public string SortType
        {
            get
            {
                return _SortType;
            }
            set
            {
                SetProperty(ref _SortType, value);
            }
        }

        public SettingsViewModel()
        {
            Title = "Ustawienia";
            
            if (Application.Current.Properties.ContainsKey("Title"))
            {                
                Toggled = (bool)Application.Current.Properties["Title"];
                Debug.WriteLine("\n\n\n\n\n WCZYTANO NAZWE: " + Toggled);
            }
            else {
                //Toggled = false;                
                Application.Current.Properties["Title"] = false;
            }

            if (Application.Current.Properties.ContainsKey("DarkMode"))
            {              
                DarkMode = (bool)Application.Current.Properties["DarkMode"];
                Debug.WriteLine("\n\n\n\n\n WCZYTANO DARKMODE: " + DarkMode);
            }
            else {
                //DarkMode = false;                
                Application.Current.Properties["DarkMode"] = false;
            }

            if (Application.Current.Properties.ContainsKey("Sort"))
            {
                SortType = (string)Application.Current.Properties["Sort"];
            }
            else { SortType = "Name"; }
        }        

        public Task ToggleName()
        {
            //_toggled = !_toggled;
            Application.Current.Properties["Title"] = !(bool)Application.Current.Properties["Title"];
            MessagingCenter.Send(this, "ChangedNameEvent");
            //Debug.WriteLine(_toggled);
            return Task.CompletedTask;
        }

        public Task ToggleDarkMode()
        {
            //_darkMode = !_darkMode;
            Application.Current.Properties["DarkMode"] = !(bool)Application.Current.Properties["DarkMode"];

            if (!DarkMode)
            {
                Application.Current.Resources["backgroundColor"] = Color.White;
                Application.Current.Resources["textColor"] = Color.Black;
                Application.Current.Resources["backgroundButtonColor"] = Color.LightGray;

                Application.Current.Resources["barBackgroundColor"] = Color.FromHex("2196F3");
                Application.Current.Resources["navigationTextColor"] = Color.White;        
                
                Debug.WriteLine(Application.Current.Resources);
            }
            else
            {
                Application.Current.Resources["backgroundColor"] = Color.FromHex("33302E");
                Application.Current.Resources["textColor"] = Color.White;
                Application.Current.Resources["backgroundButtonColor"] = Color.DarkGray;

                Application.Current.Resources["barBackgroundColor"] = Color.DarkBlue;
                Application.Current.Resources["navigationTextColor"] = Color.White;

            }

            //Debug.WriteLine(_darkMode);
            return Task.CompletedTask;
        }

    }
}