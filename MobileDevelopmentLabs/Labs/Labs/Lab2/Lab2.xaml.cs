using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Labs.Lab2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Lab2 : ContentPage
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

        StackLayout _grid;
        BoxView _boxView;
        Label _colorLabel;

        public Lab2 ()
		{
			InitializeComponent ();

            var picker = this.FindByName<Picker>("ColorChooser");

            foreach (var color in nameToColor.Keys)
            {
                picker.Items.Add(color);
            }

            InitializeGrid();

            var stackPanel = this.FindByName<StackLayout>("MainStack");
            stackPanel.Children.Add(_grid);
        }

        private void InitializeGrid()
        {
            _grid = new StackLayout();
            _grid.WidthRequest = 200;
            _grid.HeightRequest = 200;

            _boxView = new BoxView();
            _boxView.WidthRequest = 150;
            _boxView.HeightRequest = 150;
            _boxView.Color = Color.Black;
            _boxView.HorizontalOptions = LayoutOptions.Center;

            _colorLabel = new Label();
            _colorLabel.Text = "Black";
            _colorLabel.HorizontalOptions = LayoutOptions.Center;

            _grid.Children.Add(_boxView);
            _grid.Children.Add(_colorLabel);
        }

        private void OK_Clicked(object sender, EventArgs e)
        {
            var picker = this.FindByName<Picker>("ColorChooser");

            if (picker.SelectedIndex < 0) return;

            string colorName = picker.Items[picker.SelectedIndex];
            _boxView.Color = nameToColor[colorName];

            _colorLabel.Text = colorName;
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            var picker = this.FindByName<Picker>("ColorChooser");
            picker.SelectedIndex = -1;

            _boxView.Color = Color.Black;
            _colorLabel.Text = "Black";
        }
    }
}