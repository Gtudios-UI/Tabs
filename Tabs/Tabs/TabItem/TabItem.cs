using CommunityToolkit.WinUI;
using Get.Data.Bindings;
using Get.Data.Collections;
using Get.Data.Properties;
using Gtudios.UI.MotionDragContainers;
using System.Threading.Tasks;

namespace Gtudios.UI.Controls.Tabs;
public partial class TabItem<T> : MotionDragSelectableItem<T>
{
    public ReadOnlyProperty<Orientation> TabOrientationStyleProperty { get; }
    Property<Orientation> _TabOrientationStyleProperty = new(default);
    Button CloseButton => (Button)GetTemplateChild(nameof(CloseButton));
    public event EventHandler<TabClosingHandledEventArgs> Closing;
    public TabItem()
    {
        ControlTemplate = DefaultTemplate;
        TabOrientationStyleProperty = new(_TabOrientationStyleProperty);
        Loaded += TabItem_Loaded;
    }

    private void TabItem_Loaded(object sender, RoutedEventArgs e)
    {
        var targetOrientation = (Owner?.OrientationProperty as IReadOnlyProperty<Orientation>) ?? new ReadOnlyProperty<Orientation>(Orientation.Horizontal);
        _TabOrientationStyleProperty.Bind(targetOrientation, ReadOnlyBindingModes.OneWay);
    }

    MotionDragSelectableContainer<T>? OwnerContainer => this.FindAscendant<MotionDragSelectableContainer<T>>();
    TabContainer<T>? Owner => this.FindAscendant<TabContainer<T>>();
    internal async Task<bool> InternalAttemptToCloseAsync()
    {
        TabClosingHandledEventArgs args = new();
        Closing(this, args);
        await args.InternalWaitAsync();
        if (args.Handled)
        {
            if (args.RemoveRequest)
            {
                if (OwnerContainer is { } con)
                    con.ItemsSourceProperty.RemoveAt(con.ChildContainers.IndexOf(this));
                return true;
            } else return false;
        }
        return await (Owner?.InternalCallTabCloseRequestAsync(this) ?? Task.FromResult(false));
    }
}
public class TabClosingHandledEventArgs
{
    internal TabClosingHandledEventArgs() { }
    public bool Handled { get; set; } = false;
    public bool RemoveRequest { get; set; } = false;
    Deferral? def;
    public Deferral GetDeferal() => def ??= new();
    internal Task InternalWaitAsync() => Deferral.WaitAsync(def);
}