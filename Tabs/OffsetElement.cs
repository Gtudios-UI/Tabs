#nullable enable
using Get.Data.Properties;
using Get.UI.Data;

namespace Gtudios.UI.Controls;
public partial class OffsetElement : TemplateControl<Border>
{
    public Property<Orientation> OrientationProperty { get; } = new(default);
    public Orientation Orientation
    {
        get => OrientationProperty.Value;
        set => OrientationProperty.Value = value;
    }
    public Property<double> OffsetAmountProperty { get; } = new(default);
    public double OffsetAmount
    {
        get => OffsetAmountProperty.Value;
        set => OffsetAmountProperty.Value = value;
    }

    public OffsetElement()
    {
        OrientationProperty.ValueChanged += (_, _) => Update();
        OffsetAmountProperty.ValueChanged += (_, _) => Update();
    }
    FrameworkElement? RootElement;
    protected override void Initialize(Border rootElement)
    {
        RootElement = rootElement;
        Update();
    }
    void Update()
    {
        if (RootElement is { } ele)
        {
            if (Orientation is Orientation.Horizontal)
                (ele.Width, ele.Height) = (OffsetAmount, 1);
            else
                (ele.Width, ele.Height) = (1, OffsetAmount);
        }
    }
}