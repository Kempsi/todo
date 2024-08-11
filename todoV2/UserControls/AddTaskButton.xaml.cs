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

namespace todoV2.UserControls
{
    /// <summary>
    /// Interaction logic for AddTaskButton.xaml
    /// </summary>
    public partial class AddTaskButton : UserControl
    {
        public AddTaskButton()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler ButtonClick;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick?.Invoke(this, e);
        }
    }
}
