using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TimeCalculator.Desktop.Views.Controls
{
    public partial class TimeInputControl : UserControl
    {
        private bool _isInternalUpdate = false;
        private string _lastValidHours = "";

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(TimeSpan?), typeof(TimeInputControl),
                new FrameworkPropertyMetadata(null, OnTimeChanged));

        public TimeSpan? Time
        {
            get => (TimeSpan?)GetValue(TimeProperty);
            set => SetValue(TimeProperty, value);
        }

        public TimeInputControl()
        {
            InitializeComponent();
        }

        public void SetInitialFocus()
        {
            HoursTextBox.Focus();
        }

        private static void OnTimeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TimeInputControl)d;
            if (!control._isInternalUpdate && e.NewValue is TimeSpan time)
            {
                control._lastValidHours = time.Hours.ToString("D2");
                control.HoursTextBox.Text = control._lastValidHours;
                control.MinutesTextBox.Text = time.Minutes.ToString("D2");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void HoursTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(HoursTextBox.Text))
            {
                if (int.TryParse(HoursTextBox.Text, out int hours))
                {
                    if (hours >= 0 && hours <= 23)
                    {
                        _lastValidHours = HoursTextBox.Text;

                        if (hours > 2 || HoursTextBox.Text.Length == 2)
                        {
                            MinutesTextBox.Focus();
                            MinutesTextBox.SelectAll();
                        }
                    }
                }
            }
            UpdateTimeValue();
        }

        private void MinutesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(MinutesTextBox.Text))
            {
                if (int.TryParse(MinutesTextBox.Text, out int minutes))
                {
                    if (MinutesTextBox.Text.Length == 2 && minutes >= 0 && minutes <= 59)
                    {
                        SimulateTabKey();
                    }
                }
            }
            UpdateTimeValue();
        }

        private void UpdateTimeValue()
        {
            _isInternalUpdate = true;
            try
            {
                if (int.TryParse(HoursTextBox.Text, out int hours) &&
                    int.TryParse(MinutesTextBox.Text, out int minutes))
                {
                    if (hours >= 0 && hours <= 23 && minutes >= 0 && minutes <= 59)
                    {
                        Time = new TimeSpan(hours, minutes, 0);
                    }
                }
                else
                {
                    Time = null;
                }
            }
            finally
            {
                _isInternalUpdate = false;
            }
        }

        private void SimulateTabKey()
        {
            var tab = new KeyEventArgs(Keyboard.PrimaryDevice,
                                     PresentationSource.FromVisual(this),
                                     0,
                                     Key.Tab)
            {
                RoutedEvent = Keyboard.KeyDownEvent
            };

            InputManager.Current.ProcessInput(tab);
        }
    }
}