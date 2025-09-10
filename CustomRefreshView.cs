using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace LanguageInUse.Components.CustomRefresh
{
    public class CustomRefreshView : ContentView
    {
        public static readonly BindableProperty IndicatorPositionProperty =
            BindableProperty.Create(
                nameof(IndicatorPosition),
                typeof(Position),
                typeof(CustomRefreshView),
                Position.Middle,
                propertyChanged: OnIndicatorPositionChanged);

        public static readonly BindableProperty IndicatorTextProperty =
            BindableProperty.Create(
                nameof(IndicatorText),
                typeof(string),
                typeof(CustomRefreshView),
                string.Empty,
                propertyChanged: OnIndicatorTextChanged);

        public static readonly BindableProperty IsRefreshingProperty =
            BindableProperty.Create(
                nameof(IsRefreshing),
                typeof(bool),
                typeof(CustomRefreshView),
                false,
                propertyChanged: OnIsRefreshingChanged);

        public static readonly BindableProperty RefreshColorProperty =
            BindableProperty.Create(nameof(RefreshColor), typeof(Color), typeof(CustomRefreshView), Colors.Gray);

        public static readonly BindableProperty RefreshCommandProperty =
            BindableProperty.Create(nameof(RefreshCommand), typeof(Command), typeof(CustomRefreshView));

        public static readonly BindableProperty RefreshContentProperty =
            BindableProperty.Create(nameof(RefreshContent), typeof(View), typeof(CustomRefreshView), propertyChanged: OnRefreshContentChanged);

        public static readonly BindableProperty IndicatorBackgroundProperty =
            BindableProperty.Create(
                nameof(IndicatorBackground),
                typeof(Color),
                typeof(CustomRefreshView),
                Colors.Transparent,
                propertyChanged: OnIndicatorBackgroundChanged);

        public static readonly BindableProperty IndicatorTextColorProperty =
            BindableProperty.Create(
                nameof(IndicatorTextColor),
                typeof(Color),
                typeof(CustomRefreshView),
                Colors.Gray,
                propertyChanged: OnIndicatorTextColorChanged);

        public static readonly BindableProperty IndicatorMarginProperty =
            BindableProperty.Create(
                nameof(IndicatorMargin),
                typeof(Thickness),
                typeof(CustomRefreshView),
                new Thickness(10, 10, 10, 10),
                propertyChanged: OnIndicatorPaddingChanged);

        public static readonly BindableProperty IndicatorMinimumWidthRequestProperty =
            BindableProperty.Create(
                nameof(IndicatorMinimumWidthRequest),
                typeof(double),
                typeof(CustomRefreshView),
                200.0,
                propertyChanged: OnIndicatorMinimumWidthRequestChanged);

        public static readonly BindableProperty IndicatorMinimumHeightRequestProperty =
            BindableProperty.Create(
                nameof(IndicatorMinimumHeightRequest),
                typeof(double),
                typeof(CustomRefreshView),
                150.0,
                propertyChanged: OnIndicatorMinimumHeightRequestChanged);

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(
                nameof(BorderColor),
                typeof(Color),
                typeof(CustomRefreshView),
                Colors.Transparent,
                propertyChanged: OnBorderPropertyChanged);

        public static readonly BindableProperty BorderThicknessProperty =
            BindableProperty.Create(
                nameof(BorderThickness),
                typeof(float),
                typeof(CustomRefreshView),
                0f,
                propertyChanged: OnBorderPropertyChanged);

        public static readonly BindableProperty BorderCornerRadiusProperty =
            BindableProperty.Create(
                nameof(BorderCornerRadius),
                typeof(float),
                typeof(CustomRefreshView),
                0f,
                propertyChanged: OnBorderPropertyChanged);

        private readonly ActivityIndicator _activityIndicator;
        private readonly Label _indicatorLabel;
        private readonly Grid _grid;
        private readonly VerticalStackLayout _indicatorStack;
        private double _totalY;
        private readonly Border _border;

        public CustomRefreshView()
        {
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(panGesture);

            _activityIndicator = new ActivityIndicator
            {
                IsVisible = false,
                IsRunning = false,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                InputTransparent = true,
                Margin = IndicatorMargin
            };
            _activityIndicator.SetBinding(ActivityIndicator.ColorProperty, new Binding(nameof(RefreshColor), source: this));

            _indicatorLabel = new Label
            {
                IsVisible = false,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = IndicatorTextColor,
                Padding = IndicatorMargin
            };

            _indicatorStack = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                IsVisible = false,
                MinimumWidthRequest = IndicatorMinimumWidthRequest,
                MinimumHeightRequest = IndicatorMinimumHeightRequest,
                Children = { _activityIndicator, _indicatorLabel }
            };

            _grid = new Grid();
            _grid.BackgroundColor = IndicatorBackground;
            _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            _grid.Add(_indicatorStack, 0, 0);

            _border = new Border
            {
                Stroke = BorderColor,
                StrokeThickness = BorderThickness,
                Background = IndicatorBackground,
                Content = _grid,
                StrokeShape = new RoundRectangle { CornerRadius = BorderCornerRadius }
            };

            Content = _border;
        }

        public Position IndicatorPosition
        {
            get => (Position)GetValue(IndicatorPositionProperty);
            set => SetValue(IndicatorPositionProperty, value);
        }

        public string IndicatorText
        {
            get => (string)GetValue(IndicatorTextProperty);
            set => SetValue(IndicatorTextProperty, value);
        }

        public bool IsRefreshing
        {
            get => (bool)GetValue(IsRefreshingProperty);
            set => SetValue(IsRefreshingProperty, value);
        }

        public Color RefreshColor
        {
            get => (Color)GetValue(RefreshColorProperty);
            set => SetValue(RefreshColorProperty, value);
        }

        public Command RefreshCommand
        {
            get => (Command)GetValue(RefreshCommandProperty);
            set => SetValue(RefreshCommandProperty, value);
        }

        public View RefreshContent
        {
            get => (View)GetValue(RefreshContentProperty);
            set => SetValue(RefreshContentProperty, value);
        }

        public Color IndicatorBackground
        {
            get => (Color)GetValue(IndicatorBackgroundProperty);
            set => SetValue(IndicatorBackgroundProperty, value);
        }

        public Color IndicatorTextColor
        {
            get => (Color)GetValue(IndicatorTextColorProperty);
            set => SetValue(IndicatorTextColorProperty, value);
        }

        public Thickness IndicatorMargin
        {
            get => (Thickness)GetValue(IndicatorMarginProperty);
            set => SetValue(IndicatorMarginProperty, value);
        }

        public double IndicatorMinimumWidthRequest
        {
            get => (double)GetValue(IndicatorMinimumWidthRequestProperty);
            set => SetValue(IndicatorMinimumWidthRequestProperty, value);
        }

        public double IndicatorMinimumHeightRequest
        {
            get => (double)GetValue(IndicatorMinimumHeightRequestProperty);
            set => SetValue(IndicatorMinimumHeightRequestProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public float BorderThickness
        {
            get => (float)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public float BorderCornerRadius
        {
            get => (float)GetValue(BorderCornerRadiusProperty);
            set => SetValue(BorderCornerRadiusProperty, value);
        }

        private static void OnIndicatorPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomRefreshView)bindable;
            var position = (Position)newValue;

            switch (position)
            {
                case Position.Top:
                    control._indicatorStack.VerticalOptions = LayoutOptions.Start;
                    break;

                case Position.Bottom:
                    control._indicatorStack.VerticalOptions = LayoutOptions.End;
                    break;

                default:
                    control._indicatorStack.VerticalOptions = LayoutOptions.Center;
                    break;
            }
        }

        private static void OnIsRefreshingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRefreshView control)
            {
                bool isRefreshing = (bool)newValue;
                control._activityIndicator.IsRunning = isRefreshing;
                control._activityIndicator.IsVisible = isRefreshing;
                control._indicatorLabel.IsVisible = isRefreshing && !string.IsNullOrEmpty(control.IndicatorText);
                control._indicatorStack.IsVisible = isRefreshing;
            }
        }

        private static void OnIndicatorTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRefreshView control)
            {
                control._indicatorLabel.Text = newValue as string ?? string.Empty;
                control._indicatorLabel.IsVisible = control.IsRefreshing && !string.IsNullOrEmpty(control._indicatorLabel.Text);
            }
        }

        private static void OnIndicatorBackgroundChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRefreshView control)
            {
                control._indicatorStack.BackgroundColor = (Color)newValue;
            }
        }

        private static void OnIndicatorTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRefreshView control)
            {
                control._indicatorLabel.TextColor = (Color)newValue;
            }
        }

        private static void OnRefreshContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomRefreshView)bindable;
            if (oldValue is View oldView)
                control._grid.Children.Remove(oldView);
            if (newValue is View newView)
                control._grid.Children.Insert(0, newView);
        }

        private static void OnIndicatorPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRefreshView control)
            {
                control._indicatorStack.Padding = (Thickness)newValue;
            }
        }

        private static void OnIndicatorMinimumWidthRequestChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRefreshView control)
            {
                control._indicatorStack.MinimumWidthRequest = (double)newValue;
            }
        }

        private static void OnIndicatorMinimumHeightRequestChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRefreshView control)
            {
                control._indicatorStack.MinimumHeightRequest = (double)newValue;
            }
        }

        private static void OnBorderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is CustomRefreshView control && control._border != null)
            {
                control._border.Stroke = control.BorderColor;
                control._border.StrokeThickness = control.BorderThickness;
                control._border.StrokeShape = new RoundRectangle { CornerRadius = control.BorderCornerRadius };
            }
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _totalY = 0;
                    break;

                case GestureStatus.Running:
                    _totalY += e.TotalY;
                    break;

                case GestureStatus.Completed:
                    if (_totalY > 50)
                        RefreshCommand?.Execute(null);
                    break;
            }
        }
    }
}