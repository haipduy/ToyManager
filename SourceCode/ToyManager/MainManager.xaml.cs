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
using System.Windows.Shapes;

namespace ToyManager
{
    /// <summary>
    /// Interaction logic for MainManager.xaml
    /// </summary>
    public partial class MainManager : Window
    {
        public MainManager()
        {
            InitializeComponent();
            GridPrincifal.Children.Add(new FormController.HomeController());
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            switch (index)
            {
                case 0:
                    GridPrincifal.Children.Clear();
                    GridPrincifal.Children.Add(new FormController.HomeController());
                    break;
                case 1:
                    GridPrincifal.Children.Clear();
                    GridPrincifal.Children.Add(new FormController.ProductController());
                    break;
                case 2:
                    GridPrincifal.Children.Clear();
                    GridPrincifal.Children.Add(new FormController.EmployeeController());
                    break;
                case 3:
                    GridPrincifal.Children.Clear();
                    GridPrincifal.Children.Add(new MiTTLibrary.PresentationLayer.UserControls.OrderManagementUC());
                    break;
                case 4:
                    GridPrincifal.Children.Clear();
                    GridPrincifal.Children.Add(new FormController.CategoryController());
                    break;
                case 5:
                    GridPrincifal.Children.Clear();
                    GridPrincifal.Children.Add(new FormController.SupllierContoller());
                    break;
                

            }
        }
    }
}
