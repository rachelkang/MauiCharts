using Foundation;
using UIKit;

namespace ChartsMaciOS
{
	// @interface DotnetCharts : NSObject
	[BaseType (typeof(NSObject))]
	interface DotnetCharts
	{
		// +(NSString * _Nonnull)getStringWithMyString:(NSString * _Nonnull)myString __attribute__((warn_unused_result("")));
		[Static]
		[Export ("getStringWithMyString:")]
		string GetStringWithMyString (string myString);

		// +(UIView * _Nonnull)createPieChartWithData:(NSDictionary<NSString *,NSNumber *> * _Nonnull)data colors:(NSArray<UIColor *> * _Nullable)colors __attribute__((warn_unused_result("")));
		[Static]
		[Export ("createPieChartWithData:colors:")]
		UIView CreatePieChartWithData (NSDictionary<NSString, NSNumber> data, [NullAllowed] UIColor[] colors);
	}
}
