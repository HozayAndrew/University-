using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TeacherProblemApp.Pages
{
    public partial class SavedDataPage : Page
    {
        public Frame Frame;
        public SavedDataPage()
        {
            InitializeComponent();
            var data = GetFiles();
            ResultsPreviewList.ItemsSource = data;
        }

        private List<FilePreview> GetFiles()
        {
            var files = new List<FilePreview>();

            var currentDirectory = Directory.GetCurrentDirectory();
            var resultsDirectory = System.IO.Path.Combine(currentDirectory, "Results");

            if (Directory.Exists(resultsDirectory))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(resultsDirectory);
                FileInfo[] info = dirInfo.GetFiles("*.*", SearchOption.TopDirectoryOnly);

                if (info.Count() != 0)
                {
                    foreach (var file in info)
                    {
                        try
                        {
                            if (file.Extension == ".json")
                            {
                                var fileStr = File.ReadAllText(file.FullName);
                                var saveFile = JsonConvert.DeserializeObject<SaveFile>(fileStr);

                                var filePreview = new FilePreview
                                {
                                    AlgorithmName = saveFile.Name,
                                    AlgorithmTime = saveFile.OutputData.AlgorithmTime,
                                    Size = saveFile.OutputData.StudentSequence.Count,
                                    LocalPath = file.FullName
                                };

                                files.Add(filePreview);
                            }
                        }
                        catch { }
                    }
                }
                else
                {
                    ShowNoDataYet();
                }
            }
            else
            {
                ShowNoDataYet();
            }

            return files;
        }

        private async void ShowNoDataYet()
        {
            MessageBox.Show("No any saved files yet. " +
                    "To save data open special case tab and click \"Get data\" then wait for results. " +
                    "All results will be saved.");


            await Task.Delay(500);

            Frame.Navigate(new ChoosingPage());
        }

        private void ResultsPreviewList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filePreview = e.AddedItems[0] as FilePreview;
            System.Diagnostics.Process.Start(filePreview.LocalPath);
        }
    }
}
