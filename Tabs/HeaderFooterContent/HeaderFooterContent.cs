using Get.Data.Properties;
using Get.UI.Data;

namespace Gtudios.UI.Controls;

[TemplatePart(Type = typeof(UIElement), Name = "HeaderAndInset")]
[TemplatePart(Type = typeof(UIElement), Name = "ContentAndInline")]
[TemplatePart(Type = typeof(UIElement), Name = "FooterAndInset")]
[TemplatePart(Type = typeof(CustomizablePanel), Name = "PanelHost")]
[ContentProperty(Name = nameof(ContentProperty))]
public partial class HeaderFooterContent : TemplateControl<CustomizablePanel>
{
    public Property<ContentBundle?> HeaderProperty { get; } = new(default);
    public Property<ContentBundle?> InlineHeaderProperty { get; } = new(default);
    public Property<ContentBundle?> FooterProperty { get; } = new(default);
    public Property<ContentBundle?> InlineFooterProperty { get; } = new(default);
    public Property<ContentBundle?> ContentProperty { get; } = new(default);
    public ContentBundle? Content
    {
        get => ContentProperty.Value;
        set => ContentProperty.Value = value;
    }
    public Property<double?> ContentRequestedSizeProperty { get; } = new(default);
    public Property<Orientation> OrientationProperty { get; } = new(default);
    public Property<OrientationNeutralAlignment> AlignmentProperty { get; } = new(default);
    public Property<CenterAlignmentResolvingMode> CenterAlignmentResolvingModeProperty { get; } = new(default);
    /// <summary>The start offset for footer. Note that this does not effect the centered element.</summary>
    public Property<double> StartInsetProperty { get; } = new(default);
    /// <summary>The end offset for footer. Note that this does not effect the centered element.</summary>
    public Property<double> EndInsetProperty { get; } = new(default);
    public double? ContentRequestedSize
    {
        get => ContentRequestedSizeProperty.Value;
        set => ContentRequestedSizeProperty.Value = value;
    }
    public Orientation Orientation
    {
        get => OrientationProperty.Value;
        set => OrientationProperty.Value = value;
    }
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