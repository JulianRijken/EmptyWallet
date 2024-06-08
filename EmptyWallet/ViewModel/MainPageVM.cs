using CommunityToolkit.Mvvm.ComponentModel;
using EmptyWallet.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Input;
using EmptyWallet.Model;

namespace EmptyWallet.ViewModel
{
    internal class MainPageVM : ObservableObject
    {
        public MainPageVM()
        {
            CurrentPage = OverViewPage;
        }



        private OverViewPage _overViewPage = new OverViewPage();
        public OverViewPage OverViewPage => _overViewPage;

        private DetailPage _detailPage = new DetailPage();
        public DetailPage DetailPage => _detailPage;

        private Page _currentPage;
        public Page CurrentPage
        {
            get => _currentPage;
            set => SetProperty(ref _currentPage, value);
        }

        public string SwitchPageButtonText => (CurrentPage is OverViewPage) ? "Show Selected" : "Go Back";
        public RelayCommand SwitchedPageCommand => new RelayCommand(SwitchPage);

        private void SwitchPage()
        {
            if(CurrentPage is OverViewPage)
            {
                Game selectedGame = (OverViewPage.DataContext as GamesOverviewPageVM).SelectedGame;
                if (selectedGame == null)
                    return;

                (DetailPage.DataContext as GameDetailPageVM).CurrentGame = selectedGame;

                CurrentPage = DetailPage;
            }
            else
            {
                CurrentPage = OverViewPage;
            }

            OnPropertyChanged(nameof(SwitchPageButtonText));
        }


        private bool _isToggled;
        public bool IsToggled => _isToggled;

        public string ToggleButtonText => (IsToggled) ? "Loading Local" : "Loading Online";
        public AsyncRelayCommand ToggleCommand => new AsyncRelayCommand(Toggle);

        private async Task Toggle()
        {
            _isToggled = !_isToggled;
            OnPropertyChanged(nameof(ToggleButtonText));
            await (OverViewPage.DataContext as GamesOverviewPageVM).ChangeRepo(_isToggled);
        }
    }
}
