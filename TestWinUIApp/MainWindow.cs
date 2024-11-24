using Get.Data;
using Get.Data.Bindings;
using Get.Data.Bindings.Linq;
using Get.Data.Collections;
using Get.Data.DataTemplates;
using Get.Data.Helpers;
using Get.Data.Properties;
using Get.Data.UIModels;
using Get.UI.Controls.Panels;
using Get.UI.Data;
using Gtudios.UI.Controls.Tabs;
using Gtudios.UI.MotionDragContainers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;

namespace TestWinUIApp;

class MainWindow : Window
{
    int i = 4;
    readonly UpdateCollectionInitializer<string> Items = [
        "Item 0",
        "Item 1",
        "Item 2",
        "Item 3"
    ];
    public MainWindow()
    {
        ExtendsContentIntoTitleBar = true;
        SystemBackdrop = new MicaBackdrop();
        var sm = new SelectionManagerMutable<string>(Items);
        sm.PreferAlwaysSelectItem = true;
        Content = new TabView<string>
        {
            // didn't make title bar stuff yet so here's temporary one
            Margin = new(0, 30, 0, 0),
            TargetCollection = Items,
            SelectionManager = sm,
            ItemTemplate = new DataTemplate<string, MotionDragItem<string>>(root =>
            {
                return new TabItem<string>
                {
                    ContentBundle = new(root.CurrentValue)
                    {
                        ContentBinding = OneWay(root),
                        ContentTemplate = DataTemplates.TextBlockUIElement<string>()
                    }
                };
            }),
            ContentTemplate = new DataTemplate<string, UIElement>(
                root => new TextBlock
                {
                    Margin = new(16)
                }
                .WithCustomCode(x =>
                    TextBlock.TextProperty.AsProperty<TextBlock, string>(x)
                    .BindOneWay(root.Select(x => $"You are currently selecting {x}."))
                )
            )
        }.WithCustomCode(x => x.AddTabButtonClicked += (_, _) =>
        {
            Items.Add($"Item {i++}");
            sm.SelectedIndex = Items.Count - 1;
        });
    }
}
