using Get.Data.Bindings;
using Get.Data.Bindings.Linq;
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
            os.OrientationProperty.Bind(@this.OrientationProperty.Select(x => x is Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal), ReadOnlyBindingModes.OneWay);
            os.HorizontalAlignment = HorizontalAlignment.Stretch;
            os.VerticalAlignment = VerticalAlignment.Stretch;
            var shouldBeVisible = @this.SelectedValueProperty.Select(x => x is not null ? Visibility.Visible : Visibility.Collapsed);
            os.Children.Add(
                new TabContainer<T>()
                .WithCustomCode(x =>
                {
                    x.HeaderProperty.Bind(@this.HeaderProperty, ReadOnlyBindingModes.OneWay);
                    x.InlineHeaderProperty.Bind(@this.InlineHeaderProperty, ReadOnlyBindingModes.OneWay);
                    x.FooterProperty.Bind(@this.FooterProperty, ReadOnlyBindingModes.OneWay);
                    x.InlineFooterProperty.Bind(@this.InlineFooterProperty, ReadOnlyBindingModes.OneWay);
                    x.TabContainerRequestedSizeProperty.Bind(@this.TabContainerRequestedSizeProperty, ReadOnlyBindingModes.OneWay);
                    x.OrientationProperty.Bind(@this.OrientationProperty, ReadOnlyBindingModes.OneWay);
                    x.AlignmentProperty.Bind(@this.AlignmentProperty, ReadOnlyBindingModes.OneWay);
                    x.CenterAlignmentResolvingModeProperty.Bind(@this.CenterAlignmentResolvingModeProperty, ReadOnlyBindingModes.OneWay);
                    x.StartInsetProperty.Bind(@this.TitleBarLeftInset, ReadOnlyBindingModes.OneWay);
                    x.EndInsetProperty.Bind(@this.TitleBarRightInset, ReadOnlyBindingModes.OneWay);
                    x.SelectedValueProperty.BindOneWayToSource(@this._SelectedValueProperty);
                    x.SelectedIndexProperty.Bind(@this.SelectedIndexProperty, BindingModes.TwoWay);
                    x.ConnectionContextProperty.Bind(@this.ConnectionContextProperty, ReadOnlyBindingModes.OneWay);
                    x.AddTabButtonVisibilityProperty.Bind(@this.AddTabButtonVisibilityProperty, ReadOnlyBindingModes.OneWay);
                    x.ItemsSourceProperty.Bind(@this.ItemsSourceProperty, ReadOnlyBindingModes.OneWay);
                    x.ItemTemplateProperty.Bind(@this.TabItemTemplateProperty, ReadOnlyBindingModes.OneWay);
                    x.PreferAlwaysSelectItemProperty.Bind(@this.PreferAlwaysSelectItemProperty, ReadOnlyBindingModes.OneWay);
                    VisibilityProperty.AsProperty<TabContainer<T>, Visibility>(x).Bind(@this.TabsVisibilityProperty, ReadOnlyBindingModes.OneWay);
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
                        ContentBundle = new ContentBundle<T, UIElement>()
                        .WithCustomCode(x =>
                        {
                            x.ContentProperty.Bind(@this.SelectedValueProperty, ReadOnlyBindingModes.OneWay);
                            x.ContentTemplateProperty.Bind(@this.ToolbarTemplateProperty, ReadOnlyBindingModes.OneWay);
                        })
                    }
                    .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, GridLength.Auto))
                    .WithCustomCode(x =>
                        VisibilityProperty.AsProperty<ContentBundleControl, Visibility>(x)
                        .Bind(shouldBeVisible, ReadOnlyBindingModes.OneWay)
                    ),
                    new ContentBundleControl
                    {
                        ContentBundle = new ContentBundle<T, UIElement>()
                        .WithCustomCode(x =>
                        {
                            x.ContentProperty.Bind(@this.SelectedValueProperty, ReadOnlyBindingModes.OneWay);
                            x.ContentTemplateProperty.Bind(@this.TabContentTemplateProperty, ReadOnlyBindingModes.OneWay);
                        })
                    }
                    .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, new(1, GridUnitType.Star)))
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