using Get.UI.Data;
using Gtudios.UI.MotionDragContainers;

namespace Gtudios.UI.Controls.Tabs;
partial class TabItem<T>
{
    public readonly record struct TabItemTemplateParts(Button CloseButton, MotionDragItemTempalteParts Parent);
    public new ExternalControlTemplate<TabItemTemplateParts, TabItem<T>, Border> ControlTemplate { get; set; }
    protected override void Initialize(Border rootElement)
    {
        var TemplateParts = ControlTemplate(this, rootElement);
        TemplateParts.CloseButton.Click += (_, _) => _ = InternalAttemptToCloseAsync();
        var template = base.ControlTemplate;
        base.ControlTemplate = (_, _) => TemplateParts.Parent;
        base.Initialize(rootElement);
        base.ControlTemplate = template;
    }
}