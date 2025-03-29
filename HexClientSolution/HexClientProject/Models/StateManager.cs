using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Automation.Peers;
using LcuApi;
using ReactiveUI;

namespace HexClientProject.Models
{
    public class StateManager : ReactiveObject
    {
        private LcuApi.ILeagueClient _api;

        public LcuApi.ILeagueClient Api
        {
            get => _api;
            set => this.RaiseAndSetIfChanged(ref _api, value);
        }

        public static StateManager Instance { get; } = new();
    }
}
