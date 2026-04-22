using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MV1000.Forms.ViewModels;
using MV1000.Support.UI.Units;

namespace MV1000.Forms.UI.Views;

public class MainWindow : MV1000Window
{
    static MainWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MainWindow),
            new FrameworkPropertyMetadata(typeof(MainWindow)));
    }

    public MainWindow(MainWindowViewModel viewModel)
    {
        DataContext = viewModel;
    }

    public static readonly DependencyProperty HistoryTextProperty =
        DependencyProperty.Register(nameof(HistoryText), typeof(string), typeof(MainWindow),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty OpenHistoryCommandProperty =
        DependencyProperty.Register(nameof(OpenHistoryCommand), typeof(ICommand), typeof(MainWindow),
            new PropertyMetadata(null));

    public static readonly DependencyProperty HistoryOffImageProperty =
        DependencyProperty.Register(nameof(HistoryOffImage), typeof(ImageSource), typeof(MainWindow),
            new PropertyMetadata(null));

    public static readonly DependencyProperty HistoryOnImageProperty =
        DependencyProperty.Register(nameof(HistoryOnImage), typeof(ImageSource), typeof(MainWindow),
            new PropertyMetadata(null));

    public string HistoryText
    {
        get => (string)GetValue(HistoryTextProperty);
        set => SetValue(HistoryTextProperty, value);
    }

    public ICommand? OpenHistoryCommand
    {
        get => (ICommand?)GetValue(OpenHistoryCommandProperty);
        set => SetValue(OpenHistoryCommandProperty, value);
    }

    public ImageSource? HistoryOffImage
    {
        get => (ImageSource?)GetValue(HistoryOffImageProperty);
        set => SetValue(HistoryOffImageProperty, value);
    }

    public ImageSource? HistoryOnImage
    {
        get => (ImageSource?)GetValue(HistoryOnImageProperty);
        set => SetValue(HistoryOnImageProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        var minimizeButton = GetTemplateChild("PART_MinimizeButton") as Button;
        if (minimizeButton != null)
            minimizeButton.Click += (s, e) => WindowState = System.Windows.WindowState.Minimized;

        var maximizeButton = GetTemplateChild("PART_MaximizeButton") as Button;
        if (maximizeButton != null)
            maximizeButton.Click += (s, e) =>
                WindowState = WindowState == System.Windows.WindowState.Maximized
                    ? System.Windows.WindowState.Normal
                    : System.Windows.WindowState.Maximized;

        var closeButton = GetTemplateChild("PART_CloseButton") as Button;
        if (closeButton != null)
            closeButton.Click += (s, e) => Close();
    }
}
