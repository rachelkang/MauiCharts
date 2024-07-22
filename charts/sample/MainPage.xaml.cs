using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace MauiSample;

#if IOS || MACCATALYST
using Charts = ChartsMaciOS.DotnetCharts;
#elif ANDROID
using Charts = ChartsAndroid.DotnetCharts;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using Charts = System.Object;
#endif

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

		// Pie Chart implementation in C#

		// var pieChart = new MauiPieChart()
		// {
		// 	Slices = new List<PieChartSlice>
		// 	{
		// 		new PieChartSlice { Name = "Dave's fans", Count = 1 },
		// 		new PieChartSlice { Name = "Rachel's fans", Count = 5 },
		// 		new PieChartSlice { Name = "Maddy's fans", Count = 10 },
		// 		new PieChartSlice { Name = "Beth's fans", Count = 20 }
		// 	}
		// };
		// pieChart.WidthRequest = 300;
		// pieChart.HeightRequest = 300;
		// myStackLayout.Children.Add(pieChart);
	}
}

public class MauiPieChart : View
{
	public List<PieChartSlice> Slices { get; set; } = new List<PieChartSlice>();
}

public class PieChartSlice
{
	public string Name { get; set; } = string.Empty;

	public int Count { get; set; }
	
    public Color Color
    {
        get
        {
            return _color ??= GenerateRandomColor();
        }
        set
        {
            _color = value;
        }
    }

	private Color? _color = null;

    private Color GenerateRandomColor()
    {
        Random random = new Random();
        return new Color(random.Next(256), random.Next(256), random.Next(256));
    }
}

public partial class MauiPieChartHandler
{
	public static IPropertyMapper<MauiPieChart, MauiPieChartHandler> PropertyMapper = new PropertyMapper<MauiPieChart, MauiPieChartHandler>(ViewHandler.ViewMapper)
	{
	};

	public MauiPieChartHandler() : base(PropertyMapper)
	{
	}
}

#if IOS || MACCATALYST
public partial class MauiPieChartHandler : ViewHandler<MauiPieChart, UIKit.UIView>
{
	protected override UIKit.UIView CreatePlatformView()
	{	
		var data = Foundation.NSDictionary<Foundation.NSString, Foundation.NSNumber>.FromObjectsAndKeys (
			VirtualView.Slices.Select(s => new Foundation.NSNumber(s.Count)).ToArray(),
			VirtualView.Slices.Select(s => s.Name).ToArray()
		);
		var colors = VirtualView.Slices.Select(s => s.Color.ToPlatform()).ToArray();

		var pieChart = Charts.CreatePieChartWithData(data, colors);
		return pieChart;
	}
}

#elif ANDROID
public partial class MauiPieChartHandler : ViewHandler<MauiPieChart, Android.Views.View>
{
	protected override Android.Views.View CreatePlatformView()
	{
		var data = new Java.Util.LinkedHashMap();
		var colors = new List<Java.Lang.Integer>();
		foreach (var slice in VirtualView.Slices) {
			data.Put(slice.Name, slice.Count);
			colors.Add(new Java.Lang.Integer(slice.Color.ToPlatform().ToArgb()));
		}

		var pieChart = Charts.CreatePieChart(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity, data, colors);
		return pieChart;
	}
}
#endif
