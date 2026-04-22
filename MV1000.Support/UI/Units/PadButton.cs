using System.Windows;
using System.Windows.Controls.Primitives;

namespace MV1000.Support.UI.Units;

public class PadButton : ToggleButton
{
    static PadButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PadButton),
            new FrameworkPropertyMetadata(typeof(PadButton)));
    }
}
