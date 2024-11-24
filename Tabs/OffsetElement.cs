#nullable enable
using Get.Data.Properties;
using Get.UI.Data;

namespace Gtudios.UI.Controls;
[AutoProperty]
public partial class OffsetElement : TemplateControl<Border>
{
    public IProperty<Orientation> OrientationProperty { get; } = Auto<Orientation>(default);
    public IProperty<double> OffsetAmountProperty { get; } = Auto<double>(default);
    

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