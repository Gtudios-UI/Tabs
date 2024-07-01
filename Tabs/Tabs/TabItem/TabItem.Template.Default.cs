using Get.Data.Bindings;
using Get.Data.Bindings.Linq;
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
            var a = MotionDragItem<T>.DefaultTemplate(@this, border);
            var tmp = border.Child;
            border.BorderThickness = default;
            border.Child = null;
            border.Child = new Border
            {
                Child = tmp
            }.AssignTo(out var BackgroundPlace);
            BackgroundPlace = (Border)border.Child;
            BackgroundPlace.Margin = new(6, 0, 6, 0);
            BackgroundPlace.CornerRadius = new(6, 6, 0, 0);
            var ContentControl = (ContentBundleControl)BackgroundPlace.Child;
            var BackgroundPlaceBackgroundProperty = Border.BackgroundProperty.AsProperty<Border, Brush>(BackgroundPlace);
            var BackgroundPlaceBorderProperty = Border.BorderBrushProperty.AsProperty<Border, Brush>(BackgroundPlace);
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
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Bottom
                    }
                    .WithCustomCode(x => Shape.FillProperty.AsProperty<Path, Brush>(x).Bind(BackgroundPlaceBackgroundProperty, ReadOnlyBindingModes.OneWay))
                    .AssignTo(out var corner1),
                    new Path
                    {
                        Data = (Geometry)XamlBindingHelper.ConvertValue(
                            typeof(Geometry),
                            "F1 M 6 6 C 3 6 0 3 0 0 L 0 6 Z"
                        ),
                        Margin = new(0),
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Bottom
                    }
                    .WithCustomCode(x => Shape.FillProperty.AsProperty<Path, Brush>(x).Bind(BackgroundPlaceBackgroundProperty, ReadOnlyBindingModes.OneWay))
                    .AssignTo(out var corner2)
                }
            };

            Property<PointerState> pointerstates = new(PointerState.Normal);

            var binding = from selected in @this.IsSelectedProperty
                          from pointerState in @pointerstates
                          select (pointerState, selected);
            var TabViewItemHeaderBackgroundSelected = ThemeResources.Create<Brush>("TabViewItemHeaderBackgroundSelected", BackgroundPlace);
            var TabViewSelectedItemBorderBrush = ThemeResources.Create<Brush>("TabViewSelectedItemBorderBrush", BackgroundPlace);
            var ControlFillColorTertiaryBrush = ThemeResources.Create<Brush>("ControlFillColorTertiaryBrush", BackgroundPlace);
            var ControlFillColorDisabledBrush = ThemeResources.Create<Brush>("ControlFillColorDisabledBrush", BackgroundPlace);
            var Transparent = new ReadOnlyProperty<Brush>(new SolidColorBrush(Colors.Transparent));
            Set(binding.CurrentValue.pointerState, binding.CurrentValue.selected);
            binding.ValueChanged += (_, x) => Set(x.pointerState, x.selected);
            void Set(PointerState state, bool isSelected)
            {
                if (isSelected)
                {
                    BackgroundPlaceBackgroundProperty.Bind(TabViewItemHeaderBackgroundSelected, ReadOnlyBindingModes.OneWay);
                    BackgroundPlaceBorderProperty.Bind(TabViewSelectedItemBorderBrush, ReadOnlyBindingModes.OneWay);
                } else
                {
                    switch (state)
                    {
                        case PointerState.Normal:
                            BackgroundPlaceBackgroundProperty.Bind(Transparent, ReadOnlyBindingModes.OneWay);
                            break;
                        case PointerState.PointerOver:
                            BackgroundPlaceBackgroundProperty.Bind(ControlFillColorTertiaryBrush, ReadOnlyBindingModes.OneWay);
                            break;
                        case PointerState.Pressed:
                            BackgroundPlaceBackgroundProperty.Bind(ControlFillColorDisabledBrush, ReadOnlyBindingModes.OneWay);
                            break;
                    }
                }
            }
            return new(CloseButton, a);
        };
    enum PointerState
    {
        Normal,
        PointerOver,
        Pressed
    }
}