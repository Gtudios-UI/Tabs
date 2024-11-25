using Get.Data.Bindings;
using Get.Data.Helpers;
using Get.Data.Properties;
using Get.Data.XACL;
using Get.UI.Controls.Panels;
using Get.UI.Data;
using Gtudios.UI.MotionDragContainers;

namespace Gtudios.UI.Controls.Tabs;
partial class TabContainer<T>
{
    static ExternalControlTemplate<TabContainerTemplateParts<T>, TabContainer<T>, HeaderFooterContent> DefaultTemplate { get; } =
        (@this, item) =>
        {
            item.Content =
                new ScrollViewer
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Content = new OrientedStack
                    {
                        OrientationBinding = OneWay(@this.OrientationProperty),
                        Children =
                        {
                            new MotionDragSelectableContainer<T>
                            {
                                MinWidth = 100,
                                VerticalAlignment = VerticalAlignment.Center,
                                // up to user
                                // need to set in SelectionManager
                                // PreferAlwaysSelectItem = true,
                                SelectionManagerBinding = OneWay(@this.SelectionManagerProperty),
                                ItemTemplateBinding = OneWay(@this.ItemTemplateProperty),
                                ReorderOrientationBinding = OneWay(@this.OrientationProperty),
                                ConnectionContextBinding = OneWay(@this.ConnectionContextProperty),
                            }
                            .WithCustomCode(x =>
                                HeightProperty.AsProperty<MotionDragSelectableContainer<T>, double>(x).Bind(
                                    HeightProperty.AsProperty<TabContainer<T>, double>(@this),
                                    ReadOnlyBindingModes.OneWay
                            ))
                            .AssignTo(out var Container),
                            new Button
                            {
                                Padding = new(5),
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                Background = new SolidColorBrush(Colors.Transparent),
                                BorderBrush = new SolidColorBrush(Colors.Transparent),
                                Content =
                                new PathIcon {
                                    VerticalAlignment = VerticalAlignment.Center,
                                    HorizontalAlignment = HorizontalAlignment.Center,
                                    RenderTransformOrigin = new(0.5, 0.5),
                                    Margin = new(-1, -1, 0, 0),
                                    RenderTransform = new ScaleTransform { ScaleX = 0.8, ScaleY = 0.8 },
                                    Data = (Geometry)XamlBindingHelper.ConvertValue(
                                        typeof(Geometry),
                                        "F1 M 17.5 9.375 C 17.5 9.544271 17.43815 9.690756 17.314453 9.814453 C 17.190754 9.938151 17.04427 10 16.875 10 L 10 10 L 10 16.875 C 10 17.044271 9.93815 17.190756 9.814453 17.314453 C 9.690755 17.43815 9.544271 17.5 9.375 17.5 C 9.205729 17.5 9.059244 17.43815 8.935547 17.314453 C 8.811849 17.190756 8.75 17.044271 8.75 16.875 L 8.75 10 L 1.875 10 C 1.705729 10 1.559245 9.938151 1.435547 9.814453 C 1.311849 9.690756 1.25 9.544271 1.25 9.375 C 1.25 9.205729 1.311849 9.059245 1.435547 8.935547 C 1.559245 8.81185 1.705729 8.75 1.875 8.75 L 8.75 8.75 L 8.75 1.875 C 8.75 1.70573 8.811849 1.559246 8.935547 1.435547 C 9.059244 1.31185 9.205729 1.25 9.375 1.25 C 9.544271 1.25 9.690755 1.31185 9.814453 1.435547 C 9.93815 1.559246 10 1.70573 10 1.875 L 10 8.75 L 16.875 8.75 C 17.04427 8.75 17.190754 8.81185 17.314453 8.935547 C 17.43815 9.059245 17.5 9.205729 17.5 9.375 Z "
                                    )
                                }
                            }
                            .WithOneWayBinding(
                                VisibilityProperty.AsPropertyDefinition<Button, Visibility>(),
                                @this.AddTabButtonVisibilityProperty)
                            .AssignTo(out var AddTabButton)
                        }
                    }
                    .WithCustomCode(x =>
                    {
                        x.Name = $"Hello World! - {x.Name}";
                    })
                }
                .AssignTo(out var ContainerAreaScrollViewer);
            item.WithCustomCode(x =>
            {
                x.HeaderProperty.Bind(@this.HeaderProperty, ReadOnlyBindingModes.OneWay);
                x.InlineHeaderProperty.Bind(@this.InlineHeaderProperty, ReadOnlyBindingModes.OneWay);
                x.FooterProperty.Bind(@this.FooterProperty, ReadOnlyBindingModes.OneWay);
                x.InlineFooterProperty.Bind(@this.InlineFooterProperty, ReadOnlyBindingModes.OneWay);
                x.ContentRequestedSizeProperty.Bind(@this.TabContainerRequestedSizeProperty, ReadOnlyBindingModes.OneWay);
                x.OrientationProperty.Bind(@this.OrientationProperty, ReadOnlyBindingModes.OneWay);
                x.AlignmentProperty.Bind(@this.AlignmentProperty, ReadOnlyBindingModes.OneWay);
                x.CenterAlignmentResolvingModeProperty.Bind(@this.CenterAlignmentResolvingModeProperty, ReadOnlyBindingModes.OneWay);
                x.StartInsetProperty.Bind(@this.StartInsetProperty, ReadOnlyBindingModes.OneWay);
                x.EndInsetProperty.Bind(@this.EndInsetProperty, ReadOnlyBindingModes.OneWay);
            });
            return new(
                Container,
                AddTabButton,
                ContainerAreaScrollViewer
            );
        };
}