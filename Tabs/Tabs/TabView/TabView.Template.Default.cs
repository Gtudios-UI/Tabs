using Get.Data.Bindings;
using Get.Data.Bindings.Linq;
using Get.Data.Bundles;
using Get.Data.Helpers;
using Get.Data.Properties;
using Get.UI.Controls.Panels;
using Get.UI.Data;
using Gtudios.UI.MotionDragContainers;
namespace Gtudios.UI.Controls.Tabs;
partial class TabView<T>
{
    public readonly static ExternalControlTemplate<TabViewTemplateParts, TabView<T>, OrientedStack> DefaultTemplate =
        (@this, os) =>
        {
            //os.Tag = "Debug";
            os.OrientationProperty.Bind(@this.OrientationProperty.Select(x => x is Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal), ReadOnlyBindingModes.OneWay);
            os.HorizontalAlignment = HorizontalAlignment.Stretch;
            os.VerticalAlignment = VerticalAlignment.Stretch;
            var selectedValueBinding = @this.SelectionManagerProperty.SelectPath(x => x.SelectedValueProperty);
            var shouldBeVisible = selectedValueBinding.Select(x => x is not null ? Visibility.Visible : Visibility.Collapsed);
            os.Children.Add(
                new TabContainer<T>
                {
                    HeaderBinding = OneWay(@this.HeaderProperty),
                    InlineHeaderBinding = OneWay(@this.InlineHeaderProperty),
                    FooterBinding = OneWay(@this.FooterProperty),
                    InlineFooterBinding = OneWay(@this.InlineFooterProperty),
                    TabContainerRequestedSizeBinding = OneWay(@this.TabContainerRequestedSizeProperty),
                    OrientationBinding = OneWay(@this.OrientationProperty),
                    AlignmentBinding = OneWay(@this.AlignmentProperty),
                    CenterAlignmentResolvingModeBinding = OneWay(@this.CenterAlignmentResolvingModeProperty),
                    StartInsetBinding = OneWay(@this.TitleBarLeftInset),
                    EndInsetBinding = OneWay(@this.TitleBarRightInset),
                    SelectionManagerBinding = OneWay(@this.SelectionManagerProperty),
                    AddTabButtonVisibilityBinding = OneWay(@this.AddTabButtonVisibilityProperty),
                    ItemTemplateBinding = OneWay(@this.ItemTemplateProperty.Select(x => x.DataTemplateHelper().As<MotionDragItem<T>>(x => x))),
                    ConnectionContextBinding = OneWay(@this.ConnectionContextProperty),
                }
                .WithCustomCode(x =>
                {
                    VisibilityProperty.AsProperty<TabContainer<T>, Visibility>(x).BindOneWay(@this.TabsVisibilityProperty);
                    OrientedStack.LengthProperty.SetValue(x, GridLength.Auto);
                })
                .AssignTo(out var TabContainer)
            );
            os.Children.Add(new OrientedStack
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    new ContentBundleControl {
                        //Tag = "Debug",
                        ContentBundle = new ContentBundle<T?, UIElement>(default)
                        {
                            ContentBinding = OneWay(selectedValueBinding),
                            ContentTemplateBinding = OneWay(@this.ToolbarTemplateProperty)
                        }
                    }
                    .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, GridLength.Auto))
                    .WithCustomCode(x => OrientedStack.VisibilityTrackingProperty.SetValue(x, true))
                    .WithCustomCode(x =>
                        VisibilityProperty.AsProperty<ContentBundleControl, Visibility>(x)
                        .Bind(shouldBeVisible, ReadOnlyBindingModes.OneWay)
                    ),
                    new ContentBundleControl {
                        //Tag = "Debug",
                        ContentBundle = new ContentBundle<T?, UIElement>(default)
                        {
                            ContentBinding = OneWay(selectedValueBinding),
                            ContentTemplateBinding = OneWay(@this.ContentTemplateProperty)
                        }
                    }
                    .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, new(1, GridUnitType.Star)))
                    .WithCustomCode(x => OrientedStack.VisibilityTrackingProperty.SetValue(x, true))
                    .WithCustomCode(x =>
                        VisibilityProperty.AsProperty<ContentBundleControl, Visibility>(x)
                        .Bind(shouldBeVisible, ReadOnlyBindingModes.OneWay)
                    )
                }
            }
            .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, new(1, GridUnitType.Star))));
            return new(TabContainer);
        };
}