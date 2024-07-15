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
		// 		new PieChartSlice { Name = "Dave's fans", Count = 1, Color = Colors.Magenta },
		// 		new PieChartSlice { Name = "Rachel's fans", Count = 5, Color = Colors.Cyan },
		// 		new PieChartSlice { Name = "Maddy's fans", Count = 10, Color = Colors.Blue },
		// 		new PieChartSlice { Name = "Beth's fans", Count = 20, Color = Colors.Purple }
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

	static Random random = new Random();
	static Color randomColor = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

	public Color Color { get; set; } = randomColor;
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
