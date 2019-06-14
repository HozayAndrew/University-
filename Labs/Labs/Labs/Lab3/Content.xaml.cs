﻿using System;
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
	public partial class Content : ContentPage
	{
		public Content ()
		{
			InitializeComponent ();

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "temp.txt");
            TextLabel.Text = File.ReadAllText(fileName);
        }
	}
}