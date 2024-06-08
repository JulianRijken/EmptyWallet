using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using EmptyWallet.Model;
using System.Windows.Media.Imaging;

namespace EmptyWallet.ViewModel
{
    internal class GameDetailPageVM : ObservableObject
    {

        private Game _currentGame = new Game()
        {
            Thumbnail = @"https:\/\/www.freetogame.com\/g\/540\/thumbnail.jpg",
            GameUrl = @"https:\/\/www.freetogame.com\/open\/overwatch-2"
        };

        public string SwitchPageButtonText => "Play Game";
        public RelayCommand SwitchedPageCommand => new RelayCommand(OnSwitchPageButton);

        private void OnSwitchPageButton()
        {
            string url = CurrentGame.GameUrl;
            url = url.Replace("\\/", "/");

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        public Game CurrentGame
        {
            get => _currentGame;
            set
            {
                _currentGame = value;
                OnPropertyChanged(nameof(CurrentGame));
            }
        }

    }
}
