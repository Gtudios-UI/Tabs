using Get.UI.Data;
using Gtudios.UI.MotionDragContainers;

namespace Gtudios.UI.Controls.Tabs;
public readonly record struct TabContainerTemplateParts<T>(
    MotionDragSelectableContainer<T> Container,
    Button AddTabButton,
    ScrollViewer ContainerAreaScrollViewer
);
partial class TabContainer<T>
{
    TabContainerTemplateParts<T> TemplateParts;
    internal MotionDragSelectableContainer<T> Container => TemplateParts.Container;
    Button? AddTabButton => TemplateParts.AddTabButton;
    ScrollViewer? ContainerAreaScrollViewer => TemplateParts.ContainerAreaScrollViewer;
    public ExternalControlTemplate<TabContainerTemplateParts<T>, TabContainer<T>, HeaderFooterContent> ControlTemplate { get; set; } = DefaultTemplate;
    protected override void Initialize(HeaderFooterContent rootElement)
    {
        TemplateParts = ControlTemplate(this, rootElement);
        AddTabButton!.Click += (o, e) => AddTabButtonClicked?.Invoke(o, e);
        UpdateScrollView(Orientation);
    }
}