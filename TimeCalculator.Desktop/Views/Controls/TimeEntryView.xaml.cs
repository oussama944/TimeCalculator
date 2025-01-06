using System;
using System.Windows;
using System.Windows.Controls;
using TimeCalculator.Application.Services.Interfaces;
using TimeCalculator.Desktop.ViewModels;

namespace TimeCalculator.Desktop.Views.Controls
{
    public partial class TimeEntryView : UserControl
    {
        public TimeEntryView()
        {
            InitializeComponent();
            Loaded += TimeEntryView_Loaded;
        }

        private void TimeEntryView_Loaded(object sender, RoutedEventArgs e)
        {
            MorningStartTime.SetInitialFocus();
        }
    }
}