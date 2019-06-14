using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Labs
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void LabButton_Clicked(object sender, EventArgs e)
        {
            var buttonName = (sender as Button).Text;

            var element = this.FindByName("MainGrid") as Grid;
            if (buttonName == "Lab 1") 
            {
                await Navigation.PushModalAsync(new Lab1.Lab1());
            }
            else if(buttonName == "Lab 2")
            {
                await Navigation.PushModalAsync(new Lab2.Lab2());
            }
            else if (buttonName == "Lab 3")
            {
                await Navigation.PushModalAsync(new Lab3.Lab3());
            }
            else 
            {
                await Navigation.PushModalAsync(new Lab4.Lab4());
            }
        }
    }
}
