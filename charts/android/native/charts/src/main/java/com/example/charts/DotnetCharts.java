package com.example.charts;

import android.content.Context;

import com.github.mikephil.charting.charts.PieChart;
import com.github.mikephil.charting.data.PieData;
import com.github.mikephil.charting.data.PieDataSet;
import com.github.mikephil.charting.data.PieEntry;
import com.github.mikephil.charting.utils.ColorTemplate;

import android.graphics.Color;
import android.graphics.Typeface;
import android.os.Build;
import android.view.View;

import java.util.ArrayList;
import java.util.LinkedHashMap;
import java.util.stream.Collectors;

public class DotnetCharts {
    public String getString(String myString)
    {
        return myString;
    }

    public static View createPieChart(Context context, LinkedHashMap<String, Integer> data, ArrayList<Integer> colors) {

        PieChart pieChartView = renderPieChart(context);

        setChartData(pieChartView, data, colors);

        return pieChartView;

//        return (View)renderPieChart(context);
    }

    static PieChart renderPieChart(Context context) {
        PieChart pieChart = new PieChart(context);

        pieChart.setBackgroundColor(Color.TRANSPARENT);
        pieChart.setHoleColor(Color.TRANSPARENT);
        pieChart.getLegend().setEnabled(false);
        pieChart.getDescription().setEnabled(false);
        pieChart.setRotationAngle(270);

        pieChart.setEntryLabelTextSize(18f);
        pieChart.setEntryLabelColor(Color.WHITE);
        pieChart.setEntryLabelTypeface(Typeface.DEFAULT_BOLD);

        return pieChart;
    }

    static void setChartData(PieChart pieChart, LinkedHashMap<String, Integer> data, ArrayList<Integer> colors) {

        ArrayList<PieEntry> entries = new ArrayList<>();

        if (colors == null || colors.size() == 0) {
            colors = getDefaultChartColors();
        }

        if (data != null && data.size() > 0) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
                entries = new ArrayList<PieEntry>(data.entrySet().stream().map(x -> new PieEntry(x.getValue(), x.getKey())).collect(Collectors.toList()));
            }
        }

        PieDataSet dataSet = new PieDataSet(entries, "Data");
        dataSet.setDrawIcons(false);

        dataSet.setColors(colors);

        PieData pieData = new PieData(dataSet);
//        pieData.setValueFormatter(new com.github.mikephil.charting.formatter.DefaultValueFormatter(0));

        pieData.setValueTextSize(18f);
        pieData.setValueTextColor(Color.WHITE);
        pieData.setValueTypeface(Typeface.DEFAULT_BOLD);

        pieChart.setData(pieData);
        pieChart.highlightValues(null);
        pieChart.invalidate();
    }

    static ArrayList<Integer> getDefaultChartColors() {
        ArrayList<Integer> chartColors = new ArrayList<>();

        for (int color : ColorTemplate.MATERIAL_COLORS) {
            chartColors.add(color);
        }

        return  chartColors;
    }
}
