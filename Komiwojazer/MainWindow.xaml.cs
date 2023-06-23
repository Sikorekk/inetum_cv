using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using System;

namespace Komiwojazer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string xmlUrl = "../../Properties/Cities.xml";
        private View view = new View();
        private Model model = new Model();
        private MessageBoxResult messageBoxCaption;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CityAddButton_Click(object sender, RoutedEventArgs e)
        {
            int x, y;
            if (string.IsNullOrWhiteSpace(CityNameTextBox.Text))
            {
                messageBoxCaption = MessageBox.Show("Pole 'Nazwa' nie może być puste!");
                CityNameTextBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(CityXTextBox.Text))
            {
                messageBoxCaption = MessageBox.Show("Pole 'X' nie może być puste!");
                CityXTextBox.Focus();
            }
            else if (string.IsNullOrWhiteSpace(CityYTextBox.Text))
            {
                messageBoxCaption = MessageBox.Show("Pole 'Y' nie może być puste!");
                CityYTextBox.Focus();
            }
            else if (!int.TryParse(CityXTextBox.Text, out x))
            {
                messageBoxCaption = MessageBox.Show("Pozycja X została podana w złym formacie!");
                CityXTextBox.Text = string.Empty;
                CityXTextBox.Focus();
            }
            else if (!int.TryParse(CityYTextBox.Text, out y))
            {
                messageBoxCaption = MessageBox.Show("Pozycja Y została podana w złym formacie!");
                CityYTextBox.Text = string.Empty;
                CityYTextBox.Focus();
            }
            else
            {
                view.AddCity(CitiesListBox, new City(CityNameTextBox.Text, x, y));
                CityNameTextBox.Text = string.Empty;
                CityXTextBox.Text = string.Empty;
                CityYTextBox.Text = string.Empty;
                messageBoxCaption = MessageBox.Show("Miasto dodano pomyślnie!");
                CityNameTextBox.Focus();
            }
        }

        private void CitiesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (CitiesListBox.SelectedIndex != -1)
            {
                view.RemoveCity(CitiesListBox, CitiesListBox.SelectedIndex);
                messageBoxCaption = MessageBox.Show("Usuwanie zakończone pomyślnie!");
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            model.LoadCities(CitiesListBox, xmlUrl);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            model.SaveCities(CitiesListBox, xmlUrl);
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime start = DateTime.Now;
            view.RemoveAllCities(SortedCitiesListBox);
            view.AddAllCities(SortedCitiesListBox, model.CountBestWay(CitiesListBox).CitiesList);
            DateTime end = DateTime.Now;
            TimeSpan executionTime = end - start;
            if (CitiesListBox.Items.Count > 0)
                CompilationTimeRun.Text = Math.Round(executionTime.TotalMilliseconds, 2).ToString();
            messageBoxCaption = MessageBox.Show("Całkowita droga: " + model.CountBestWay(CitiesListBox).TotalWay);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && messageBoxCaption != MessageBoxResult.OK && (CityNameTextBox.IsFocused || CityXTextBox.IsFocused || CityYTextBox.IsFocused))
                CityAddButton_Click(null, null);
            else
                messageBoxCaption = MessageBoxResult.None;

        }
    }
}
