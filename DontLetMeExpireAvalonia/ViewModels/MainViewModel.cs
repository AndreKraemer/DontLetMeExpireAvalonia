using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DontLetMeExpireAvalonia.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontLetMeExpireAvalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IItemService _itemService;
    private readonly INavigationService _navigationService;

    public MainViewModel(INavigationService navigationService, IItemService itemService)
    {
        _itemService = itemService;
        _navigationService = navigationService;
    }

    [ObservableProperty]
    private int _stockCount;

    [ObservableProperty]
    private int _expiringSoonCount;

    [ObservableProperty]
    private int _expiresTodayCount;

    [ObservableProperty]
    private int _expiredCount;

    public override async Task OnNavigatedToAsync(Dictionary<string, object> parameters)
    {
        await InitializeAsync();
    }

    /// <summary>
    /// Initialisiert das ViewModel asynchron.
    /// </summary>
    public async Task InitializeAsync()
    {
        StockCount = (await _itemService.GetAsync()).Count();
        ExpiringSoonCount = (await _itemService.GetExpiresSoonAsync()).Count();
        ExpiresTodayCount = (await _itemService.GetExpiresTodayAsync()).Count();
        ExpiredCount = (await _itemService.GetExpiredAsync()).Count();
    }

    [RelayCommand]
    private async Task NavigateToAddItem()
    {
        await _navigationService.NavigateTo<ItemViewModel>();
    }
}

public class DesignTime_MainViewModel : MainViewModel
{
    public DesignTime_MainViewModel(): base(new NavigationService(), new DummyItemService())
    {
        InitializeAsync();
    }
}
