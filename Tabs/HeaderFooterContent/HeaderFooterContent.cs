using Get.Data.Bundles;
using Get.Data.Properties;
using Get.UI.Data;

namespace Gtudios.UI.Controls;

[TemplatePart(Type = typeof(UIElement), Name = "HeaderAndInset")]
[TemplatePart(Type = typeof(UIElement), Name = "ContentAndInline")]
[TemplatePart(Type = typeof(UIElement), Name = "FooterAndInset")]
[TemplatePart(Type = typeof(CustomizablePanel), Name = "PanelHost")]
[ContentProperty(Name = nameof(ContentProperty))]
[AutoProperty]
public partial class HeaderFooterContent : TemplateControl<CustomizablePanel>
{
    public IProperty<UIElement?> HeaderProperty { get; } = Auto<UIElement?>(default);
    public IProperty<UIElement?> InlineHeaderProperty { get; } = Auto<UIElement?>(default);
    public IProperty<UIElement?> FooterProperty { get; } = Auto<UIElement?>(default);
    public IProperty<UIElement?> InlineFooterProperty { get; } = Auto<UIElement?>(default);
    public IProperty<UIElement?> ContentProperty { get; } = Auto<UIElement?>(default);
    public IProperty<double?> ContentRequestedSizeProperty { get; } = Auto<double?>(default);
    public IProperty<Orientation> OrientationProperty { get; } = Auto<Orientation>(default);
    public IProperty<OrientationNeutralAlignment> AlignmentProperty { get; } = Auto<OrientationNeutralAlignment>(default);
    public IProperty<CenterAlignmentResolvingMode> CenterAlignmentResolvingModeProperty { get; } = Auto<CenterAlignmentResolvingMode>(default);
    /// <summary>The start offset for footer. Note that this does not effect the centered element.</summary>
    public IProperty<double> StartInsetProperty { get; } = Auto(0d);
    /// <summary>The end offset for footer. Note that this does not effect the centered element.</summary>
    public IProperty<double> EndInsetProperty { get; } = Auto(0d);
    public HeaderFooterContent()
    {
        ContentRequestedSizeProperty.ValueChanged += (_, _) => UpdatePanel();
        OrientationProperty.ValueChanged += (_, _) => UpdatePanel();
        AlignmentProperty.ValueChanged += (_, _) => UpdatePanel();
        CenterAlignmentResolvingModeProperty.ValueChanged += (_, _) => UpdatePanel();
    }
    void UpdatePanel()
    {
        PanelHost?.InvalidateMeasure();
        PanelHost?.InvalidateArrange();
        PanelHost?.UpdateLayout();
    }
}
public enum CenterAlignmentResolvingMode
{
    /// <summary>
    /// Force the element to center no matter what happended. The element resizes down to still keep centering.
    /// </summary>
    AbsoluteCenterResizeDown,
    /// <summary>
    /// If there are still space left but can no longer be centered, the element will align left or right to keep the current size first and resize down only when necessary.
    /// </summary>
    AlignLeftOrRight
}