using Plugin.FilePicker;
using Rox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Labs.Lab4
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lab4 : ContentPage
    {
        public Lab4()
        {
            InitializeComponent();

        }

        private async void Open_Clicked(object sender, EventArgs e)
        {
            var file = await CrossFilePicker.Current.PickFile();

            var filePath  = file.FilePath;
            Player.Source = filePath;
        }

        private void OpenSite_Clicked(object sender, EventArgs e)
        {
            var text = this.FindByName<Entry>("OpenSite");
            var url = text.Text;

            Player.Source = url;
        }

        private async void Play_Clicked(object sender, EventArgs e)
        {
            await Player.Start();
        }

        private async void Stop_Clicked(object sender, EventArgs e)
        {
            await Player.Stop();
        }

        private async void Pause_Clicked(object sender, EventArgs e)
        {
            await Player.Pause();
        }

        private void Mute_Clicked(object sender, EventArgs e)
        {
            Player.Muted = !Player.Muted;
            if (Player.Muted)
            {
                Mute.Text = "Unmute";
            }
            else
            {
                Mute.Text = "Mute";
            }

        }

        private void FullScreen_Clicked(object sender, EventArgs e)
        {
            Player.FullScreen = true;
        }
    }
}