using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;

namespace Komiwojazer
{
    class View
    {
        public void AddCity(ListBox listBox, City city)
        {
            StackPanel newCity = new StackPanel();
            Label name = new Label();
            name.Content = city.Name;
            name.Height = 0;
            Label x = new Label();
            x.Content = city.X;
            x.Visibility = Visibility.Hidden;
            x.Height = 0;
            Label y = new Label();
            y.Content = city.Y;
            y.Visibility = Visibility.Hidden;
            y.Height = 0;
            Label content = new Label();
            content.Content = city;
            newCity.Children.Add(name);
            newCity.Children.Add(x);
            newCity.Children.Add(y);
            newCity.Children.Add(content);
            listBox.Items.Add(newCity);
        }
        public void RemoveCity(ListBox listBox, int index)
        {
            listBox.Items.RemoveAt(index);
        }
        public void AddAllCities(ListBox listBox, List<City> cities)
        {
            foreach (City city in cities)
            {
                AddCity(listBox, city);
            }
        }
        public void RemoveAllCities(ListBox listBox)
        {
            listBox.Items.Clear();
        }
    }
}
