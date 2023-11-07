using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace _2023._11._07._wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Organisation> organisations = new List<Organisation>();

        private void LoadFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                Organisation organisation = new Organisation(sr.ReadLine().Split(';'));
                organisations.Add(organisation);
            }

            sr.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
            LoadFile("organizations-100000.csv");

            dgOrganizations.ItemsSource = organisations;

            var countriesList = organisations.Select(x => x.Country).OrderBy(x => x).Distinct().ToList();
            countriesList.Insert(0, "Mindegyik");
            cbCountries.ItemsSource = countriesList;
            cbCountries.SelectedIndex = 0;

            var yearsList = organisations.Select(x => x.Founded.ToString()).OrderBy(x => x).Distinct().ToList();
            yearsList.Insert(0, "Mindegyik");
            cbYears.ItemsSource = yearsList;
            cbYears.SelectedIndex = 0;
            lblTotalEmployers.Content = organisations.Sum(x => x.EmployeesNumber);
        }

        private void dgOrganizations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgOrganizations.SelectedItem is Organisation)
            {
                Organisation organisation = dgOrganizations.SelectedItem as Organisation;
                lblId.Content = organisation.Id;
                lblWebsite.Content = organisation.Website;
                lblDescription.Content = organisation.Description;
            }
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            Filter();
        }

        private void cbYears_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        public void Filter()
        {
            // Todo
            if(cbCountries.SelectedIndex == 0 && cbYears.SelectedIndex == 0) {
                dgOrganizations.ItemsSource = organisations;
            } 
            else if(cbCountries.SelectedIndex == 0)
            {
                dgOrganizations.ItemsSource = organisations.Where(x => x.Country == cbCountries.SelectedItem.ToString());
            } 
            else if (cbYears.SelectedIndex == 0)
            {
                dgOrganizations.ItemsSource = organisations.Where(x => x.Founded.ToString() == cbYears.SelectedItem.ToString());
            }
            else
            {
                dgOrganizations.ItemsSource = organisations.Where(x => x.Country == cbCountries.SelectedItem.ToString() && x.Founded.ToString() == cbYears.SelectedItem.ToString());
            }
           
        }
    }
}
