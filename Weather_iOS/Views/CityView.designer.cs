// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using Syncfusion.SfChart;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Weather_iOS.Views
{
    [Register ("CityView")]
    partial class CityView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Syncfusion.SfChart.iOS.SFChart Chart { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TempLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel WeatherDescriptionLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (Chart != null) {
                Chart.Dispose ();
                Chart = null;
            }

            if (TempLabel != null) {
                TempLabel.Dispose ();
                TempLabel = null;
            }

            if (WeatherDescriptionLabel != null) {
                WeatherDescriptionLabel.Dispose ();
                WeatherDescriptionLabel = null;
            }
        }
    }
}