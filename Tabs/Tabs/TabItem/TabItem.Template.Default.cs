using Get.Data.Bindings;
using Get.Data.Helpers;
using Get.Data.Properties;
using Get.UI.Controls.Panels;
using Get.UI.Data;
using Gtudios.UI.MotionDragContainers;
namespace Gtudios.UI.Controls.Tabs;
using Path = Platform.UI.Xaml.Shapes.Path;
using Shape = Platform.UI.Xaml.Shapes.Shape;
partial class TabItem<T>
{
    public new readonly static ExternalControlTemplate<TabItemTemplateParts, TabItem<T>, Border> DefaultTemplate =
        (@this, border) =>
        {
            var a = MotionDragSelectableItem<T>.DefaultTemplate(@this, border);
            var BackgroundPlace = (Border)border.Child;
            var ContentControl = (TypedContentControl<T>)BackgroundPlace.Child;
            var BackgroundPlacePBackgroundroperty = Border.BackgroundProperty.AsProperty<Border, SolidColorBrush>(BackgroundPlace);
            BackgroundPlace.Child = null; // removes ContentControl from background place
            BackgroundPlace.Child = new OrientedStack
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    ContentControl
                    .WithCustomCode(x =>
                    {
                        x.MinWidth = 150;
                        x.Margin = new(10, 5, 5, 5);
                        OrientedStack.LengthProperty.SetValue(x, GridLength.Auto);
                        x.VerticalAlignment = VerticalAlignment.Center;
                        x.CornerRadius = new(4, 4, 0, 0);
                    }),
                    new Button
                    {
                        Background = new SolidColorBrush(Colors.Transparent),
                        BorderBrush = new SolidColorBrush(Colors.Transparent),
                        Padding = new(0),
                        Width = 35,
                        Margin = new(0, 5, 5, 5),
                        Content = new SymbolIcon(Symbol.Clear)
                        {
                            RenderTransformOrigin = new(0.5, 0.5),
                            RenderTransform = new ScaleTransform { ScaleX = 0.65, ScaleY = 0.65 }
                        }
                    }.WithCustomCode(x =>
                    {
                        OrientedStack.LengthProperty.SetValue(x, GridLength.Auto);
                    })
                    .AssignTo(out var CloseButton),
                    new Path
                    {
                        Data = (Geometry)XamlBindingHelper.ConvertValue(
                            typeof(Geometry),
                            "F1 M 0 6 C 3 6 6 3 6 0 L 6 6 Z"
                        ),
                        Margin = new(0),
                        Fill = SelfNote.ThrowNotImplemented<Brush>(), // Binding of BackgroundPlace.Background
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Bottom
                    },
                    new Path
                    {
                        Data = (Geometry)XamlBindingHelper.ConvertValue(
                            typeof(Geometry),
                            "F1 M 6 6 C 3 6 0 3 0 0 L 0 6 Z"
                        ),
                        Margin = new(0),
                        Fill = SelfNote.ThrowNotImplemented<Brush>(), // Binding of BackgroundPlace.Background
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Bottom
                    }
                    .WithCustomCode(x => Shape.FillProperty.AsProperty<Path, SolidColorBrush>(x).Bind(BackgroundPlacePBackgroundroperty, ReadOnlyBindingModes.OneWay))
                }
            };
            return new(CloseButton, a);
        };
}