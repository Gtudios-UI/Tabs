using Get.Data.Collections.Update;
using Get.Data.DataTemplates;
using Get.Data.Properties;
using Get.UI.Data;
using Gtudios.UI.Controls;
using Gtudios.UI.MotionDrag;
using Gtudios.UI.MotionDragContainers;

namespace Gtudios.UI.Controls.Tabs;

partial class TabView<T>
{
    public Property<ContentBundle?> HeaderProperty { get; } = new(default);
    public Property<ContentBundle?> InlineHeaderProperty { get; } = new(default);
    public Property<ContentBundle?> FooterProperty { get; } = new(default);
    public Property<ContentBundle?> InlineFooterProperty { get; } = new(default);
    public Property<double?> TabContainerRequestedSizeProperty { get; } = new(default);
    public Property<Orientation> OrientationProperty { get; } = new(Orientation.Horizontal);
    public Property<OrientationNeutralAlignment> AlignmentProperty { get; } = new(default);
    public Property<CenterAlignmentResolvingMode> CenterAlignmentResolvingModeProperty { get; } = new(default);
    /// <summary>The start offset for footer. Note that this does not effect the centered element.</summary>
    public Property<double> TitleBarLeftInset { get; } = new(default);
    /// <summary>The end offset for footer. Note that this does not effect the centered element.</summary>
    public Property<double> TitleBarRightInset { get; } = new(default);
    public ReadOnlyProperty<T?> SelectedValueProperty { get; }
    Property<T?> _SelectedValueProperty { get; } = new(default);
    public Property<int> SelectedIndexProperty { get; } = new(0);
    public Property<MotionDragConnectionContext<T>> ConnectionContextProperty { get; } = new(new());
    public Property<Visibility> AddTabButtonVisibilityProperty { get; } = new(new());
    public Property<IUpdateCollection<T>?> ItemsSourceProperty { get; } = new(default);
    public Property<IDataTemplate<SelectableItem<T>, MotionDragItem<T>>?> ItemTemplateProperty { get; } = new(default);
    public Property<IDataTemplate<T, UIElement>?> ToolbarTemplateProperty { get; } = new(default);
    public Property<IDataTemplate<T, UIElement>?> ContentTemplateProperty { get; } = new(default);
    public Property<Visibility> TabsVisibilityProperty { get; } = new(new());
}