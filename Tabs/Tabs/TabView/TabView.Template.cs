using Get.UI.Controls.Panels;
using Get.UI.Data;

namespace Gtudios.UI.Controls.Tabs;
partial class TabView<T>
{
    public readonly record struct TabViewTemplateParts(TabContainer<T> TabContainer);
    public ExternalControlTemplate<TabViewTemplateParts, TabView<T>, OrientedStack> ControlTemplate { get; set; }
    TChild GetTemplateChild<TChild>(string name) where TChild : DependencyObject => (TChild)GetTemplateChild(name);
    TabViewTemplateParts TemplateParts;
    TabContainer<T>? TabContainer => TemplateParts.TabContainer;
    protected override void Initialize(OrientedStack rootElement)
    {
        TemplateParts = ControlTemplate(this, rootElement);
        TabContainer!.AddTabButtonClicked += (o, e) => AddTabButtonClicked?.Invoke(o, e);
    }
}