using CommunityToolkit.Mvvm.ComponentModel;
using EmptyWallet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using EmptyWallet.Repository;
using EmptyWallet.View;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;

namespace EmptyWallet.ViewModel
{
    internal class GamesOverviewPageVM : ObservableObject
    {
        private IFreeToGameRepository _activeRepository = new FreeToGameApiRepository();


        public GamesOverviewPageVM()
        {
            // NOT EMBEDDED
            string pathString = $"pack://application:,,,/Resources/Thumb/fallback.jpg";
            FallbackImage =  new BitmapImage(new Uri(pathString, UriKind.Absolute));

            LoadGames();
        }

        public async Task ChangeRepo(bool local)
        {
            if (local)
                _activeRepository = new FreeToGameLocalRepository();
            else
                _activeRepository = new FreeToGameApiRepository();

            await LoadGames();
        }

        public async Task LoadGames()
        {
            const int fakeDelay = 0;

            LoadingText = "loading data...";
            GamesList = new List<Game>();
            await Task.Delay(fakeDelay);

            LoadingText = "loading games...";
            List<Game> newGames = await _activeRepository.GetAllGames();

            LoadingText = "loading genres...";
            GameGenres = await _activeRepository.GetGenres();
            SelectedGenre = GameGenres[0];
            await Task.Delay(fakeDelay);

            LoadingText = "loading platforms...";
            GamePlatforms = await _activeRepository.GetPlatforms();
            SelectedPlatform = GamePlatforms[0];
            await Task.Delay(fakeDelay);

            LoadingText = "loading sort modes...";
            SortModes = await _activeRepository.GetSortModes();
            SelectedSortMode = SortModes[0];
            await Task.Delay(fakeDelay);

            // Only show games at the end
            GamesList = newGames;

            LoadingText = string.Empty;
        }

        public async Task FilterGames()
        {
            const int fakeDelay = 0;

            LoadingText = "filer changed...";
            GamesList = new List<Game>();
            await Task.Delay(fakeDelay);

            GamesList = await _activeRepository.GetAllGames(_selectedGenre,_selectedPlatform,_selectedSortMode);
            LoadingText = string.Empty;
        }


        private string _selectedGenre;
        public string SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                if (_selectedGenre == value)
                    return;
                SetProperty(ref _selectedGenre, value);
                FilterGames();
            }
        }

        private string _selectedPlatform;
        public string SelectedPlatform
        {
            get => _selectedPlatform;
            set
            {
                if (_selectedPlatform == value)
                    return;
                SetProperty(ref _selectedPlatform, value);
                FilterGames();
            }
        }
        private string _selectedSortMode;
        public string SelectedSortMode
        {
            get => _selectedSortMode;
            set
            {
                if (_selectedSortMode == value)
                    return;

                SetProperty(ref _selectedSortMode, value);
                FilterGames();
            }
        }

        private Game _selectedGame;
        public Game SelectedGame
        {
            get => _selectedGame;
            set => SetProperty(ref _selectedGame, value);
        }


        private List<string> _gameGenres;
        public List<string> GameGenres
        {
            get => _gameGenres;
            private set => SetProperty(ref _gameGenres, value);
        }

        private List<string> _gamePlatforms;
        public List<string> GamePlatforms
        {
            get => _gamePlatforms;
            private set => SetProperty(ref _gamePlatforms, value);
        }

        private List<string> _sortModes;
        public List<string> SortModes
        {
            get => _sortModes;
            private set => SetProperty(ref _sortModes, value);
        }


        private List<Game>? _gamesList;
        public List<Game>? GamesList
        {
            get => _gamesList;
            private set => SetProperty(ref _gamesList, value);
        }

        private BitmapImage _fallbackImage;
        public BitmapImage FallbackImage
        {
            get => _fallbackImage;
            private set => SetProperty(ref _fallbackImage, value);
        }

        private string _loadingText = string.Empty;
        public string LoadingText
        {
            get { return _loadingText; }
            set
            {
                // SetProperty DOES BOTH

                _loadingText = value;
                OnPropertyChanged(nameof(LoadingText));
            }
        }

    }
}
