using Get.Data.Properties;
using Get.UI.Controls.Panels;
using Get.UI.Data;
using System.Threading.Tasks;

namespace Gtudios.UI.Controls.Tabs;

//[DependencyProperty<bool>("AllowUserTabGroupping")]
//[DependencyProperty<TabAlignment>("TabsAlignment")]
//[DependencyProperty<Visibility>("ToolbarVisibility")]
public partial class TabView<T> : TemplateControl<OrientedStack>
{
    public TabView()
    {
        ControlTemplate = DefaultTemplate;
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
        SelectedValueProperty = new(_SelectedValueProperty);
    }
    public event RoutedEventHandler? AddTabButtonClicked;
    public Task<bool> AttemptToCloseAllTabsAsync() => TabContainer?.AttemptToCloseAllTabsAsync() ?? Task.FromResult(false);
}
public enum TabAlignment
{
    /// <summary>
    /// Left or Top
    /// </summary>
    Start,
    Center,
    /// <summary>
    /// Bottom or Right
    /// </summary>
    End
}