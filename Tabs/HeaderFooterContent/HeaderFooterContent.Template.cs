using Get.Data.Bindings;
using Get.Data.Helpers;
using Get.UI.Controls.Panels;
using Get.UI.Data;
using Microsoft.UI.Xaml.Controls;
using WinRT;

namespace Gtudios.UI.Controls;
partial class HeaderFooterContent
{
    T GetTemplateChild<T>(string name) where T : DependencyObject => (T)GetTemplateChild(name);
    FrameworkElement? HeaderAndInset;
    FrameworkElement? ContentAndInline;
    FrameworkElement? FooterAndInset;
    CustomizablePanel? PanelHost;
    protected override void Initialize(CustomizablePanel rootElement)
    {
        PanelHost = rootElement;
        rootElement.OnMeasure += OnPanelMeasure;
        rootElement.OnArrange += OnPanelArrange;
        rootElement.Children.Add(new StackPanel
        {
            Children =
            {
                new OffsetElement
                {
                    OrientationBinding = OneWay(OrientationProperty),
                    OffsetAmountBinding = OneWay(StartInsetProperty)
                },
                new UIElementContentControl
                {
                    ContentBinding = OneWay(HeaderProperty)
                }
            }
        }.AssignTo(out HeaderAndInset));
        rootElement.Children.Add(new OrientedStack
        {
            Children =
            {
                new UIElementContentControl
                {
                    ContentBinding = OneWay(InlineHeaderProperty)
                }
                .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, GridLength.Auto)),
                new UIElementContentControl
                {
                    ContentBinding = OneWay(ContentProperty)
                }
                .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, new(1, GridUnitType.Star))),
                new UIElementContentControl
                {
                    ContentBinding = OneWay(InlineFooterProperty)
                }
                .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, GridLength.Auto)),
            }
        }.AssignTo(out ContentAndInline));
        rootElement.Children.Add(new StackPanel
        {
            Children =
            {
                new UIElementContentControl
                {
                    ContentBinding = OneWay(FooterProperty)
                },
                new OffsetElement
                {
                    OrientationBinding = OneWay(OrientationProperty),
                    OffsetAmountBinding = OneWay(EndInsetProperty)
                }
            }
        }.AssignTo(out FooterAndInset));
    }
}