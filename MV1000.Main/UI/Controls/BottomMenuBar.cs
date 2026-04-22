using MV1000.Support.UI.Units;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MV1000.Main.UI.Controls;

[TemplatePart(Name = "PART_ButtonHistory",    Type = typeof(MenuIconButton))]
[TemplatePart(Name = "PART_ButtonMonitoring", Type = typeof(MenuIconButton))]
[TemplatePart(Name = "PART_ButtonTool",       Type = typeof(MenuIconButton))]
[TemplatePart(Name = "PART_ButtonSystem",     Type = typeof(MenuIconButton))]
public class BottomMenuBar : Control
{
    static BottomMenuBar()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(BottomMenuBar),
            new FrameworkPropertyMetadata(typeof(BottomMenuBar)));
    }

    private MenuIconButton? _btnHistory, _btnMonitoring, _btnTool, _btnSystem;

    // ── SelectedMenu (0=none, 1=History, 2=Monitoring, 3=Tool, 4=System) ─
    public static readonly DependencyProperty SelectedMenuProperty =
        DependencyProperty.Register(nameof(SelectedMenu), typeof(int), typeof(BottomMenuBar),
            new PropertyMetadata(0, (d, e) => ((BottomMenuBar)d).UpdateMenuState((int)e.NewValue)));

    public static readonly DependencyProperty IsToolVisibleProperty =
        DependencyProperty.Register(nameof(IsToolVisible), typeof(bool), typeof(BottomMenuBar),
            new PropertyMetadata(true, (d, e) => ((BottomMenuBar)d).UpdateToolVisibility((bool)e.NewValue)));

    // ── 상태 정보 영역 ────────────────────────────────────────────────────
    public static readonly DependencyProperty StateInfoImageProperty =
        DependencyProperty.Register(nameof(StateInfoImage), typeof(ImageSource), typeof(BottomMenuBar));
    public static readonly DependencyProperty StateInfoTextProperty =
        DependencyProperty.Register(nameof(StateInfoText), typeof(string), typeof(BottomMenuBar), new PropertyMetadata(string.Empty));
    public static readonly DependencyProperty DateTimeTextProperty =
        DependencyProperty.Register(nameof(DateTimeText), typeof(string), typeof(BottomMenuBar), new PropertyMetadata(string.Empty));
    public static readonly DependencyProperty DebugTextProperty =
        DependencyProperty.Register(nameof(DebugText), typeof(string), typeof(BottomMenuBar), new PropertyMetadata(string.Empty));

    // ── 버튼 텍스트 ──────────────────────────────────────────────────────
    public static readonly DependencyProperty HistoryTextProperty    = DependencyProperty.Register(nameof(HistoryText),    typeof(string), typeof(BottomMenuBar), new PropertyMetadata("이 력"));
    public static readonly DependencyProperty MonitoringTextProperty = DependencyProperty.Register(nameof(MonitoringText), typeof(string), typeof(BottomMenuBar), new PropertyMetadata("모니터링"));
    public static readonly DependencyProperty ToolTextProperty       = DependencyProperty.Register(nameof(ToolText),       typeof(string), typeof(BottomMenuBar), new PropertyMetadata("금 형"));
    public static readonly DependencyProperty SystemTextProperty     = DependencyProperty.Register(nameof(SystemText),     typeof(string), typeof(BottomMenuBar), new PropertyMetadata("시스템"));

    // ── ON/OFF 이미지 (부모가 주입) ──────────────────────────────────────
    public static readonly DependencyProperty HistoryOnImageProperty    = DependencyProperty.Register(nameof(HistoryOnImage),    typeof(ImageSource), typeof(BottomMenuBar));
    public static readonly DependencyProperty HistoryOffImageProperty   = DependencyProperty.Register(nameof(HistoryOffImage),   typeof(ImageSource), typeof(BottomMenuBar));
    public static readonly DependencyProperty MonitoringOnImageProperty  = DependencyProperty.Register(nameof(MonitoringOnImage),  typeof(ImageSource), typeof(BottomMenuBar));
    public static readonly DependencyProperty MonitoringOffImageProperty = DependencyProperty.Register(nameof(MonitoringOffImage), typeof(ImageSource), typeof(BottomMenuBar));
    public static readonly DependencyProperty ToolOnImageProperty    = DependencyProperty.Register(nameof(ToolOnImage),    typeof(ImageSource), typeof(BottomMenuBar));
    public static readonly DependencyProperty ToolOffImageProperty   = DependencyProperty.Register(nameof(ToolOffImage),   typeof(ImageSource), typeof(BottomMenuBar));
    public static readonly DependencyProperty SystemOnImageProperty  = DependencyProperty.Register(nameof(SystemOnImage),  typeof(ImageSource), typeof(BottomMenuBar));
    public static readonly DependencyProperty SystemOffImageProperty = DependencyProperty.Register(nameof(SystemOffImage), typeof(ImageSource), typeof(BottomMenuBar));

    // ── 커맨드 ───────────────────────────────────────────────────────────
    public static readonly DependencyProperty OpenHistoryCommandProperty    = DependencyProperty.Register(nameof(OpenHistoryCommand),    typeof(ICommand), typeof(BottomMenuBar));
    public static readonly DependencyProperty OpenMonitoringCommandProperty = DependencyProperty.Register(nameof(OpenMonitoringCommand), typeof(ICommand), typeof(BottomMenuBar));
    public static readonly DependencyProperty OpenToolCommandProperty       = DependencyProperty.Register(nameof(OpenToolCommand),       typeof(ICommand), typeof(BottomMenuBar));
    public static readonly DependencyProperty OpenSystemCommandProperty     = DependencyProperty.Register(nameof(OpenSystemCommand),     typeof(ICommand), typeof(BottomMenuBar));
    public static readonly DependencyProperty ShowStateInfoCommandProperty  = DependencyProperty.Register(nameof(ShowStateInfoCommand),  typeof(ICommand), typeof(BottomMenuBar));

    // CLR wrappers
    public int SelectedMenu      { get => (int)GetValue(SelectedMenuProperty);      set => SetValue(SelectedMenuProperty, value); }
    public bool IsToolVisible    { get => (bool)GetValue(IsToolVisibleProperty);    set => SetValue(IsToolVisibleProperty, value); }
    public ImageSource? StateInfoImage  { get => (ImageSource?)GetValue(StateInfoImageProperty);  set => SetValue(StateInfoImageProperty, value); }
    public string StateInfoText  { get => (string)GetValue(StateInfoTextProperty);  set => SetValue(StateInfoTextProperty, value); }
    public string DateTimeText   { get => (string)GetValue(DateTimeTextProperty);   set => SetValue(DateTimeTextProperty, value); }
    public string DebugText      { get => (string)GetValue(DebugTextProperty);      set => SetValue(DebugTextProperty, value); }
    public string HistoryText    { get => (string)GetValue(HistoryTextProperty);    set => SetValue(HistoryTextProperty, value); }
    public string MonitoringText { get => (string)GetValue(MonitoringTextProperty); set => SetValue(MonitoringTextProperty, value); }
    public string ToolText       { get => (string)GetValue(ToolTextProperty);       set => SetValue(ToolTextProperty, value); }
    public string SystemText     { get => (string)GetValue(SystemTextProperty);     set => SetValue(SystemTextProperty, value); }
    public ImageSource? HistoryOnImage    { get => (ImageSource?)GetValue(HistoryOnImageProperty);    set => SetValue(HistoryOnImageProperty, value); }
    public ImageSource? HistoryOffImage   { get => (ImageSource?)GetValue(HistoryOffImageProperty);   set => SetValue(HistoryOffImageProperty, value); }
    public ImageSource? MonitoringOnImage  { get => (ImageSource?)GetValue(MonitoringOnImageProperty);  set => SetValue(MonitoringOnImageProperty, value); }
    public ImageSource? MonitoringOffImage { get => (ImageSource?)GetValue(MonitoringOffImageProperty); set => SetValue(MonitoringOffImageProperty, value); }
    public ImageSource? ToolOnImage    { get => (ImageSource?)GetValue(ToolOnImageProperty);    set => SetValue(ToolOnImageProperty, value); }
    public ImageSource? ToolOffImage   { get => (ImageSource?)GetValue(ToolOffImageProperty);   set => SetValue(ToolOffImageProperty, value); }
    public ImageSource? SystemOnImage  { get => (ImageSource?)GetValue(SystemOnImageProperty);  set => SetValue(SystemOnImageProperty, value); }
    public ImageSource? SystemOffImage { get => (ImageSource?)GetValue(SystemOffImageProperty); set => SetValue(SystemOffImageProperty, value); }
    public ICommand? OpenHistoryCommand    { get => (ICommand?)GetValue(OpenHistoryCommandProperty);    set => SetValue(OpenHistoryCommandProperty, value); }
    public ICommand? OpenMonitoringCommand { get => (ICommand?)GetValue(OpenMonitoringCommandProperty); set => SetValue(OpenMonitoringCommandProperty, value); }
    public ICommand? OpenToolCommand       { get => (ICommand?)GetValue(OpenToolCommandProperty);       set => SetValue(OpenToolCommandProperty, value); }
    public ICommand? OpenSystemCommand     { get => (ICommand?)GetValue(OpenSystemCommandProperty);     set => SetValue(OpenSystemCommandProperty, value); }
    public ICommand? ShowStateInfoCommand  { get => (ICommand?)GetValue(ShowStateInfoCommandProperty);  set => SetValue(ShowStateInfoCommandProperty, value); }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _btnHistory    = GetTemplateChild("PART_ButtonHistory")    as MenuIconButton;
        _btnMonitoring = GetTemplateChild("PART_ButtonMonitoring") as MenuIconButton;
        _btnTool       = GetTemplateChild("PART_ButtonTool")       as MenuIconButton;
        _btnSystem     = GetTemplateChild("PART_ButtonSystem")     as MenuIconButton;

        UpdateMenuState(SelectedMenu);
        UpdateToolVisibility(IsToolVisible);
    }

    private void UpdateMenuState(int selected)
    {
        if (_btnHistory == null) return;
        _btnHistory.IsActive    = selected == 1;
        _btnMonitoring!.IsActive = selected == 2;
        _btnTool!.IsActive       = selected == 3;
        _btnSystem!.IsActive     = selected == 4;
    }

    private void UpdateToolVisibility(bool visible)
    {
        if (_btnTool != null)
            _btnTool.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
    }
}
