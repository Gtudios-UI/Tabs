using Get.Data;
using Get.Data.Bindings;
using Get.Data.Bindings.Linq;
using Get.Data.Collections;
using Get.Data.DataTemplates;
using Get.Data.Helpers;
using Get.Data.Properties;
using Get.UI.Controls.Panels;
using Get.UI.Data;
using Gtudios.UI.Controls.Tabs;
using Gtudios.UI.MotionDragContainers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace TestWinUIApp;

class MainWindow : Window
{
    readonly UpdateCollectionInitializer<string> Items = [
        "Item 1",
        "Item 2",
        "Item 3"
    ];
    public MainWindow()
    {
        Content = new TabView<string>
        {
            ItemsSourceProperty =
            {
                Value = Items
            },
            ItemTemplateProperty =
            {
                Value = new DataTemplate<SelectableItem<string>, MotionDragItem<string>>(root =>
                {
                    var tab = new TabItem<string>();
                    ContentBundle<string, UIElement> cb = new();
                    cb.ContentProperty.Bind(root.SelectPath(x => x.IndexItemBinding).Select(x => x.Value), ReadOnlyBindingModes.OneWay);
                    cb.ContentTemplateProperty.Value = new DataTemplate<string, UIElement>(
                        str => new TextBlock()
                        .WithCustomCode(x =>
                            TextBlock.TextProperty.AsProperty<TextBlock, string>(x)
                            .Bind(str, ReadOnlyBindingModes.OneWay)
                        )
                    );
                    tab.ContentBundle = cb;
                    return tab;
                })
            }
        };
    }
}
