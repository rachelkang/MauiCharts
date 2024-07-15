//
//  DotnetCharts.swift
//  Charts
//
//  Created by .NET MAUI team on 6/18/24.
//

import Foundation

@objc(DotnetCharts)
public class DotnetCharts : NSObject
{

    @objc
    public static func getString(myString: String) -> String {
        return myString  + " from swift!"
    }

}
