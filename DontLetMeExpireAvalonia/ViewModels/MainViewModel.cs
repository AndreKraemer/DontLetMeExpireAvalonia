using CommunityToolkit.Mvvm.ComponentModel;
using DontLetMeExpireAvalonia.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DontLetMeExpireAvalonia.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly IItemService _itemService;

    public MainViewModel(IItemService itemService)
    {
        _itemService = itemService;
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
}

public class DesignTime_MainViewModel : MainViewModel
{
    public DesignTime_MainViewModel(): base(new DummyItemService())
    {
        InitializeAsync();
    }
}
