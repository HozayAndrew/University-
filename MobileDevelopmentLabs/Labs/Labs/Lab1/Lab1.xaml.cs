using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Labs.Lab1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lab1 : ContentPage
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

        public Lab1()
        {
            InitializeComponent();
            var picker = this.FindByName<Picker>("ColorChooser");

            foreach(var color in nameToColor.Keys)
            {
                picker.Items.Add(color);
            }
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
            await DisplayAlert("Message", "You changed color.", "OK");
        }
    }
}