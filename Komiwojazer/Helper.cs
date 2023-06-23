using System;
using System.Windows.Controls;
using System.Windows;
using System.Collections.Generic;

namespace Komiwojazer
{
    public static class Helper
    {
        public static string GetStringFromStackPanel(object stackPanel, int childIndex)
        {
            return ((stackPanel as StackPanel).Children[childIndex] as Label).Content.ToString();
        }
        public static int GetIntFromStackPanel(object stackPanel, int childIndex)
        {
            return Convert.ToInt32(((stackPanel as StackPanel).Children[childIndex] as Label).Content);
        }
        public static double GetPointsDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }
        public static double GetMinValue(double[] values)
        {
            double minValue = values[0];
            for (int i = 1; i < values.Length; i++)
                if (values[i] < minValue)
                    minValue = values[i];
            return minValue;
        }
        public static (City City, double Distance) GetClosestCity(City startCity, List<City> cities)
        {
            City closestCity = new City();
            double minValue = 0;
            if (cities.Count > 0)
            {
                minValue = GetPointsDistance(startCity.X, startCity.Y, cities[0].X, cities[0].Y);
                for (int i = 0; i < cities.Count; i++)
                    if (GetPointsDistance(startCity.X, startCity.Y, cities[i].X, cities[i].Y) < minValue)
                        minValue = GetPointsDistance(startCity.X, startCity.Y, cities[i].X, cities[i].Y);
                for (int i = 0; i < cities.Count; i++)
                    if (GetPointsDistance(startCity.X, startCity.Y, cities[i].X, cities[i].Y) == minValue)
                        closestCity = cities[i];
            }
            return (closestCity, minValue);
        }
    }
}
