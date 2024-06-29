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
                new OffsetElement()
                .WithCustomCode(x =>
                {
                    x.OrientationProperty.Bind(OrientationProperty, ReadOnlyBindingModes.OneWay);
                    x.OffsetAmountProperty.Bind(StartInsetProperty, ReadOnlyBindingModes.OneWay);
                }),
                new ContentBundleControl()
                .WithCustomCode(x => x.ContentBundleProperty.Bind(HeaderProperty, ReadOnlyBindingModes.OneWay))
            }
        }.AssignTo(out HeaderAndInset));
        rootElement.Children.Add(new OrientedStack
        {
            Children =
            {
                new ContentBundleControl()
                .WithCustomCode(x => x.ContentBundleProperty.Bind(InlineHeaderProperty, ReadOnlyBindingModes.OneWay))
                .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, GridLength.Auto)),
                new ContentBundleControl()
                .WithCustomCode(x => x.ContentBundleProperty.Bind(ContentProperty, ReadOnlyBindingModes.OneWay))
                .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, new(1, GridUnitType.Star))),
                new ContentBundleControl()
                .WithCustomCode(x => x.ContentBundleProperty.Bind(InlineFooterProperty, ReadOnlyBindingModes.OneWay))
                .WithCustomCode(x => OrientedStack.LengthProperty.SetValue(x, GridLength.Auto)),
            }
        }.AssignTo(out ContentAndInline));
        rootElement.Children.Add(new StackPanel
        {
            Children =
            {
                new ContentBundleControl()
                .WithCustomCode(x => x.ContentBundleProperty.Bind(FooterProperty, ReadOnlyBindingModes.OneWay)),
                new OffsetElement()
                .WithCustomCode(x =>
                {
                    x.OrientationProperty.Bind(OrientationProperty, ReadOnlyBindingModes.OneWay);
                    x.OffsetAmountProperty.Bind(EndInsetProperty, ReadOnlyBindingModes.OneWay);
                })
            }
        }.AssignTo(out FooterAndInset));
    }
}