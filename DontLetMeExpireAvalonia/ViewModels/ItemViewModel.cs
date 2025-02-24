using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DontLetMeExpireAvalonia.Models;
using DontLetMeExpireAvalonia.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DontLetMeExpireAvalonia.ViewModels
{
    public partial class ItemViewModel: ViewModelBase
    {
        private readonly IStorageLocationService _storageLocationService;
        private readonly IItemService _itemService;


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyDataErrorInfo]
        [Required]
        [Length(5, 50)]
        private string _name;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private DateTime _expirationDate = DateTime.Today;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private StorageLocation _selectedLocation;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        [NotifyDataErrorInfo]
        [Range(1, 10000)]
        private decimal _amount = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        private string _image;


        public ItemViewModel(IStorageLocationService storageLocationService,
                             IItemService itemService)
        {
            _storageLocationService = storageLocationService;
            _itemService = itemService;
        }



        public ObservableCollection<StorageLocation> StorageLocations { get; set; } = [];


        override public async Task OnNavigatedToAsync(Dictionary<string, object> parameters)
        {
          await InitializeAsync();
        }

        /// <summary>
        /// Initialisiert das ViewModel asynchron.
        /// </summary>
        public async Task InitializeAsync()
        {
            // Speicherorte laden
            var locations = await _storageLocationService.GetAsync();


            // Die Liste der Speicherorte aktualisieren
            StorageLocations.Clear();
            foreach (var location in locations)
            {
                StorageLocations.Add(location);
            }
            SelectedLocation = StorageLocations.FirstOrDefault();
        }


        /// <summary>
        /// Speichert das Element asynchron.
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanSave))]
        private async Task SaveAsync()
        {
            // Neues Element mit den
            // Daten des ViewModels erstellen
            var item = new Item
            {
                Name = Name,
                ExpirationDate = ExpirationDate,
                StorageLocationId = SelectedLocation.Id,
                Amount = Amount,
                Image = Image
            };

            // Element speichern
            await _itemService.SaveAsync(item);

            // Daten für die Anzeige zurücksetzen
            Name = string.Empty;
            ExpirationDate = DateTime.Today;
            Amount = 0;
            SelectedLocation = StorageLocations.First();
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Name)
              && Amount > 0;
        }
    }

    public class DesignTime_ItemViewModel : ItemViewModel
    {
        public DesignTime_ItemViewModel() : base(new DummyStorageLocationService(), new DummyItemService())
        {
            InitializeAsync();
        }
    }
}
