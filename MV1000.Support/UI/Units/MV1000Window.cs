using System.Windows;

namespace MV1000.Support.UI.Units;

public class MV1000Window : Window
{
    static MV1000Window()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MV1000Window),
            new FrameworkPropertyMetadata(typeof(MV1000Window)));
    }

    protected override void OnStateChanged(EventArgs e)
    {
        base.OnStateChanged(e);
        if (WindowState == WindowState.Maximized)
            MaxHeight = SystemParameters.WorkArea.Height;
        else
            MaxHeight = double.PositiveInfinity;
    }
}
