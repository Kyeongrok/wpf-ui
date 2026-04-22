using System.Windows;
using System.Windows.Controls;

namespace MV1000.Support.UI.Units;

public class MagicBar : ListBox
{
    static MagicBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MagicBar), 
            new FrameworkPropertyMetadata(typeof(MagicBar)));
    }
}