using Get.Data.Bundles;
using Get.Data.Collections;
using Get.Data.Collections.Update;
using Get.Data.DataTemplates;
using Get.Data.Properties;
using Get.Data.UIModels;
using Get.UI.Data;
using Gtudios.UI.Controls;
using Gtudios.UI.MotionDrag;
using Gtudios.UI.MotionDragContainers;

namespace Gtudios.UI.Controls.Tabs;
[AutoProperty]
partial class TabContainer<T>
{
    public IProperty<UIElement?> HeaderProperty { get; } = Auto<UIElement?>(default);
    public IProperty<UIElement?> InlineHeaderProperty { get; } = Auto<UIElement?>(default);
    public IProperty<UIElement?> FooterProperty { get; } = Auto<UIElement?>(default);
    public IProperty<UIElement?> InlineFooterProperty { get; } = Auto<UIElement?>(default);
    public IProperty<double?> TabContainerRequestedSizeProperty { get; } = Auto<double?>(default);
    public IProperty<Orientation> OrientationProperty { get; } = Auto<Orientation>(default);
    public IProperty<OrientationNeutralAlignment> AlignmentProperty { get; } = Auto<OrientationNeutralAlignment>(default);
    public IProperty<CenterAlignmentResolvingMode> CenterAlignmentResolvingModeProperty { get; } = Auto<CenterAlignmentResolvingMode>(default);
    /// <summary>The start offset for footer. Note that this does not effect the centered element.</summary>
    public IProperty<double> StartInsetProperty { get; } = Auto(0d);
    /// <summary>The end offset for footer. Note that this does not effect the centered element.</summary>
    public IProperty<double> EndInsetProperty { get; } = Auto(0d);
    public IProperty<SelectionManagerMutable<T>> SelectionManagerProperty { get; } = Auto<SelectionManagerMutable<T>>(default);
    public IProperty<MotionDragConnectionContext<T>?> ConnectionContextProperty { get; } = Auto<MotionDragConnectionContext<T>?>(new());
    public IProperty<Visibility> AddTabButtonVisibilityProperty { get; } = Auto(Visibility.Visible);
    public IProperty<IDataTemplate<T, MotionDragItem<T>>> ItemTemplateProperty { get; }
        = Auto<IDataTemplate<T, MotionDragItem<T>>>(new DataTemplate<T, MotionDragItem<T>>(x =>
            new MotionDragItem<T>
            {
                ContentBundle = new ContentBundle<T, UIElement>(x.CurrentValue)
                {
                    ContentTemplate = DataTemplates.TextBlockUIElement<T>(),
                    ContentBinding = OneWay(x)
                }
            }
        ));

}