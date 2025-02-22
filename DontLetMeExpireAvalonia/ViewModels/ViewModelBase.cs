using CommunityToolkit.Mvvm.ComponentModel;

namespace DontLetMeExpireAvalonia.ViewModels;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private string _title;
}
