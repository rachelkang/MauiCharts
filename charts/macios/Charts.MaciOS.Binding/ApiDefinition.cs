using Foundation;

namespace ChartsMaciOS
{
	// @interface DotnetCharts : NSObject
	[BaseType (typeof(NSObject))]
	interface DotnetCharts
	{
		// +(NSString * _Nonnull)getStringWithMyString:(NSString * _Nonnull)myString __attribute__((warn_unused_result("")));
		[Static]
		[Export ("getStringWithMyString:")]
		string GetString (string myString);
	}
}
