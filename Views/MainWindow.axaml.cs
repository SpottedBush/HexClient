using Avalonia.Controls;

using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Linq;
using System.Net.Http;
using LCUSharp;

using System.Threading.Tasks;
using HttpMethod = LCUSharp.HttpMethod;

namespace HexClient.Views
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
            Console.WriteLine("OnFetchApiClick");
            var api = await LCUSharp.LeagueClient.Connect();
            Console.WriteLine(api);
            var response = await api.MakeApiRequest(HttpMethod.Get, "LoggingGetEntries");
            ApiResponseText.Text = "API response with " + response;
        }
    }
}