#nullable enable
using CommunityToolkit.WinUI;
using Get.Data.Collections;
using Get.Data.Collections.Linq;
using Get.UI.Data;
using System.Threading.Tasks;

namespace Gtudios.UI.Controls.Tabs;

public partial class TabContainer<T> : TemplateControl<HeaderFooterContent>
{
    public event RoutedEventHandler? AddTabButtonClicked;
    public event EventHandler<TabClosedRequestEventArgs<T>> TabCloseRequest;
    public TabContainer()
    {
        OrientationProperty.ValueChanged += (_, newValue) =>
        {
            UpdateScrollView(newValue);
        };
    }
    void UpdateScrollView(Orientation newValue)
    {
        if (ContainerAreaScrollViewer is { } sv)
        {
            if (newValue is Orientation.Horizontal)
            {
                sv.HorizontalScrollMode = ScrollMode.Auto;
                sv.VerticalScrollMode = ScrollMode.Disabled;
                sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
            else
            {
                sv.VerticalScrollMode = ScrollMode.Auto;
                sv.HorizontalScrollMode = ScrollMode.Disabled;
                sv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }
    }
    internal async Task<bool> InternalCallTabCloseRequestAsync(TabItem<T> tabItem)
    {
        TabClosedRequestEventArgs<T> args = new(tabItem, Container.TargetCollection[Container.ChildContainers.IndexOf(tabItem)]);
        TabCloseRequest?.Invoke(this, args);
        await args.InternalWaitAsync();
        if (args.RemoveRequest)
        {
            Container.TargetCollection.RemoveAt(Container.ChildContainers.IndexOf(tabItem));
            return true;
        }
        return false;
    }
    public async Task<bool> AttemptToCloseAllTabsAsync()
    {
        while (Container.ChildContainers[0] is { } a &&
            a.FindDescendantOrSelf<TabItem<T>>() is { } tabItem)
        {
            var item = Container.TargetCollection[0];
            if (!await tabItem.InternalAttemptToCloseAsync())
            {
                // check if the library consumer removes item from container themselves or not
                if (ReferenceEquals(item, Container.TargetCollection[0]))
                {
                    return false;
                }
            }
        }
        return true;
    }
    protected override Size MeasureOverride(Size availableSize)
    {
        return base.MeasureOverride(availableSize);
    }
}
public class TabClosedRequestEventArgs<T>
{
    internal TabClosedRequestEventArgs(TabItem<T> Tab, T Item)
    {
        this.Tab = Tab;
        this.Item = Item;
    }
    public TabItem<T> Tab { get; }
    public T Item { get; }
    public bool RemoveRequest { get; set; } = false;
    Deferral? def;
    public Deferral GetDeferal() => def ??= new();
    internal Task InternalWaitAsync() => def != null ? Deferral.WaitAsync(def) : Task.CompletedTask;
}
