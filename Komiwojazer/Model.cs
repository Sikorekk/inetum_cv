using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Windows.Controls;
using System.Windows;

namespace Komiwojazer
{
    class Model
    {
        public void LoadCities(ListBox listBox, string url)
        {
            StreamReader reader = new StreamReader(url);
            string xml = reader.ReadToEnd();
            XDocument doc = XDocument.Parse(xml);
            listBox.Items.Clear();
            List<City> cities = doc.Root.Elements("city").Select(element => new City(element.Element("name").Value, Convert.ToInt32(element.Element("x").Value), Convert.ToInt32(element.Element("y").Value))).ToList();
            foreach (City city in cities)
            {
                StackPanel newCity = new StackPanel();
                Label name = new Label();
                name.Content = city.Name;
                name.Height = 0;
                Label x = new Label();
                x.Content = city.X;
                x.Height = 0;
                Label y = new Label();
                y.Content = city.Y;
                y.Height = 0;
                Label content = new Label();
                content.Content = city;
                newCity.Children.Add(name);
                newCity.Children.Add(x);
                newCity.Children.Add(y);
                newCity.Children.Add(content);
                listBox.Items.Add(newCity);
            }
        }
        public void SaveCities(ListBox listBox, string url)
        {
            XDocument doc = XDocument.Load(url);
            doc.Element("cities").RemoveAll();
            foreach (StackPanel item in listBox.Items)
            {
                string name = (item.Children[0] as Label).Content.ToString();
                int x = Convert.ToInt32((item.Children[1] as Label).Content);
                int y = Convert.ToInt32((item.Children[2] as Label).Content);
                XElement newCity = new XElement("city");
                newCity.Add(new XElement("name", name));
                newCity.Add(new XElement("x", x));
                newCity.Add(new XElement("y", y));
                doc.Element("cities").Add(newCity);
            }
            doc.Save(url);
        }
        public (List<City> CitiesList, double TotalWay) CountBestWay(ListBox listBox)
        {          
            List<City> cities = new List<City>();
            List<City> sortedCities = new List<City>();
            double totalWay = 0;
            foreach (StackPanel item in listBox.Items)
                cities.Add(new City(Helper.GetStringFromStackPanel(item, 0), Helper.GetIntFromStackPanel(item, 1), Helper.GetIntFromStackPanel(item, 2)));
            int citiesAmount = cities.Count();
            if (citiesAmount > 0)
            {
                City startCity = cities[0];
                for (int i = 0; i < citiesAmount - 1; i++)
                {
                    sortedCities.Add(startCity);
                    cities.Remove(startCity);
                    totalWay += Helper.GetClosestCity(startCity, cities).Distance;
                    startCity = Helper.GetClosestCity(startCity, cities).City;
                }
                sortedCities.Add(startCity);
            }
            return (sortedCities, Math.Round(totalWay, 2));
        }
    }
}
