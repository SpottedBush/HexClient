using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Skia;
using HttpMethod = LcuApi.HttpMethod;
using HexClientProject.Models;

namespace HexClientProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnPlayButtonClick(object? sender, RoutedEventArgs e)
        {
            Content = new Views.GameModeSelectionView();

        }

        private async void OnDebugClick(object? sender, RoutedEventArgs e)
        {
            LcuApi.ILeagueClient api = await LcuApi.LeagueClient.Connect();
            System.Net.Http.HttpResponseMessage response = await api.MakeApiRequest(HttpMethod.Get, "lol-summoner/v1/current-summoner");
            String responseStr = await response.Content.ReadAsStringAsync();
            DebugTXT.Text = "API resp : " + responseStr;
        }
    }
}