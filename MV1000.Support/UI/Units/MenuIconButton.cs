using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MV1000.Support.UI.Units;

public class MenuIconButton : ButtonBase
{
    static MenuIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuIconButton),
            new FrameworkPropertyMetadata(typeof(MenuIconButton)));
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(MenuIconButton),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(MenuIconButton),
            new PropertyMetadata(false, (d, e) => ((MenuIconButton)d).OnIsActiveChanged((bool)e.NewValue)));

    public string Title  { get => (string)GetValue(TitleProperty);  set => SetValue(TitleProperty, value); }
    public bool IsActive { get => (bool)GetValue(IsActiveProperty); set => SetValue(IsActiveProperty, value); }

    private static readonly Brush ActiveFore  = new SolidColorBrush(Color.FromArgb(255, 17, 68, 111));
    private static readonly Brush DefaultFore = Brushes.White;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        Foreground = IsActive ? ActiveFore : DefaultFore;
    }

    private void OnIsActiveChanged(bool active)
    {
        Foreground = active ? ActiveFore : DefaultFore;
    }
}
