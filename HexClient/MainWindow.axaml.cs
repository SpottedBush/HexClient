using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HexClient
{
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnFetchApiClick(object? sender, RoutedEventArgs e)
        {
            string url = "https://wttr.in/London?format=%C+%t";
            HttpResponseMessage response = await client.GetAsync(url);
            string weather = await response.Content.ReadAsStringAsync();
            ApiResponseText.Text = "Weather in London: " + weather;
        }
    }
}