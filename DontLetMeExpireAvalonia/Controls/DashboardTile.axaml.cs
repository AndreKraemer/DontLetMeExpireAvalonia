using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace DontLetMeExpireAvalonia.Controls;

public partial class DashboardTile : UserControl
{

    public static readonly StyledProperty<IImage?> IconProperty = 
        AvaloniaProperty.Register<DashboardTile, IImage?>(nameof(Icon));

    public static readonly StyledProperty<string> TextProperty = 
        AvaloniaProperty.Register<DashboardTile, string>(nameof(Text));

    public static readonly StyledProperty<int> CountProperty = 
        AvaloniaProperty.Register<DashboardTile, int>(nameof(Count), default(int));


    public static readonly StyledProperty<IBrush?> TextColorProperty = 
        AvaloniaProperty.Register<DashboardTile, IBrush?>(nameof(TextColor), Brushes.Black);

    public static readonly StyledProperty<IBrush?> CountColorProperty =
        AvaloniaProperty.Register<DashboardTile, IBrush?>(nameof(CountColor), Brushes.Black);

    public static readonly StyledProperty<IBrush?> BorderColorProperty = 
        AvaloniaProperty.Register<DashboardTile, IBrush?>(nameof(BorderColor), Brushes.Black);

    public DashboardTile()
    {
        InitializeComponent();
    }

    public IBrush? TextColor
    {
        get => GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public IBrush? CountColor
    {
        get => GetValue(CountColorProperty);
        set => SetValue(CountColorProperty, value);
    }

    public IBrush? BorderColor
    {
        get => GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public IImage? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public int Count
    {
        get => GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }
}