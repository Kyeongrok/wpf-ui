using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MV1000.Support.UI.Units;

[TemplatePart(Name = "PART_Image", Type = typeof(Image))]
public class MenuIconButton : ButtonBase
{
    static MenuIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuIconButton),
            new FrameworkPropertyMetadata(typeof(MenuIconButton)));
    }

    private Image? _image;

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(MenuIconButton),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty OnImageSourceProperty =
        DependencyProperty.Register(nameof(OnImageSource), typeof(ImageSource), typeof(MenuIconButton),
            new PropertyMetadata(null, (d, _) => ((MenuIconButton)d).UpdateImage()));

    public static readonly DependencyProperty OffImageSourceProperty =
        DependencyProperty.Register(nameof(OffImageSource), typeof(ImageSource), typeof(MenuIconButton),
            new PropertyMetadata(null, (d, _) => ((MenuIconButton)d).UpdateImage()));

    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(MenuIconButton),
            new PropertyMetadata(false, (d, e) => ((MenuIconButton)d).OnIsActiveChanged((bool)e.NewValue)));

    public string Title          { get => (string)GetValue(TitleProperty);          set => SetValue(TitleProperty, value); }
    public ImageSource? OnImageSource  { get => (ImageSource?)GetValue(OnImageSourceProperty);  set => SetValue(OnImageSourceProperty, value); }
    public ImageSource? OffImageSource { get => (ImageSource?)GetValue(OffImageSourceProperty); set => SetValue(OffImageSourceProperty, value); }
    public bool IsActive         { get => (bool)GetValue(IsActiveProperty);         set => SetValue(IsActiveProperty, value); }

    private static readonly Brush ActiveFore  = new SolidColorBrush(Color.FromArgb(255, 17, 68, 111));
    private static readonly Brush DefaultFore = Brushes.White;

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _image = GetTemplateChild("PART_Image") as Image;
        UpdateImage();
        Foreground = IsActive ? ActiveFore : DefaultFore;
    }

    private void OnIsActiveChanged(bool active)
    {
        Foreground = active ? ActiveFore : DefaultFore;
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (_image == null) return;
        _image.Source = IsActive ? OnImageSource : OffImageSource;
    }
}
