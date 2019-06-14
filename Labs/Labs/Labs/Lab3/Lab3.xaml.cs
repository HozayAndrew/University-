using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Labs.Lab3
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Lab3 : ContentPage
	{
        Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Blue", Color.Blue }, { "Fucshia", Color.AliceBlue },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
            { "Navy", Color.Navy }, { "Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, { "Teal", Color.Teal },
            { "White", Color.White }, { "Yellow", Color.Yellow }
        };

        public Lab3 ()
		{
			InitializeComponent ();

            var picker = this.FindByName<Picker>("ColorChooser");

            foreach (var color in nameToColor.Keys)
            {
                picker.Items.Add(color);
            }
        }

        private void SaveToFile(string data)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
            File.AppendAllText(fileName, data + "\n");
        }

        private async Task<bool> GetData()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");

            if (!File.Exists(fileName))
            {
                await DisplayAlert("Message", "No data yet.", "OK");
                return false;
            }

            return true;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            var picker = this.FindByName<Picker>("ColorChooser");
            picker.SelectedIndex = -1;

            var box = this.FindByName<BoxView>("ColorBox");
            box.Color = Color.Default;

            var colorName = this.FindByName<Label>("ColorName");
            colorName.Text = "Black";
        }

        private async void OK_Clicked(object sender, EventArgs e)
        {
            var picker = this.FindByName<Picker>("ColorChooser");
            var box = this.FindByName<BoxView>("ColorBox");

            if (picker.SelectedIndex < 0) return;

            string colorName = picker.Items[picker.SelectedIndex];
            box.Color = nameToColor[colorName];

            var colorNameLabel = this.FindByName<Label>("ColorName");
            colorNameLabel.Text = colorName;

            await box.RotateTo(box.Rotation + 90);
            await Task.Delay(200);

            SaveToFile(colorName);
            await DisplayAlert("Message", "Color added to database.", "OK");
        }

        private async void Open_Clicked(object sender, EventArgs e)
        {
            var data = await GetData();
            if (!data) return;

            await Navigation.PushModalAsync(new Content());
        }

        private void ClearData_Clicked(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
            File.Delete(fileName);
        }
    }
}