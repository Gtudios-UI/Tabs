
namespace Gtudios.UI;

public class CustomizablePanel : Panel
{
    public event Func<Size, Size>? OnMeasure;
    public event Func<Size, Size>? OnArrange;
    protected override Size ArrangeOverride(Size finalSize)
    {
        return OnArrange?.Invoke(finalSize) ?? base.ArrangeOverride(finalSize);
    }
    protected override Size MeasureOverride(Size availableSize)
    {
        return OnMeasure?.Invoke(availableSize) ?? base.MeasureOverride(availableSize);
    }
}
