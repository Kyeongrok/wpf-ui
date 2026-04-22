using System.Windows;
using System.Windows.Controls;

namespace MV1000.Support.UI.Units;

public class MaximizeButton : Button
{
    static MaximizeButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MaximizeButton),
            new FrameworkPropertyMetadata(typeof(MaximizeButton)));
    }
}
