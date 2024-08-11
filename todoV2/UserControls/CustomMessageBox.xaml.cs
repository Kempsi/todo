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

namespace todoV2.UserControls
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public bool Result { get; private set; }

        public CustomMessageBox(string message, string title)
        {
            InitializeComponent();
            SetWindowIcon();

            MessageTextBlock.Text = CheckMessageLength(message);
            this.Title = title;

            SetWindowToMiddle();
        }

        #region Window icon

        private void SetWindowIcon()
        {
            this.Icon = new BitmapImage(new Uri("F:\\Visual Studio Enterprise 2022 projektit\\Oman ajan extra tehtäviä\\todoV2\\todoV2\\Icons\\todoIconV2.ico"));
        }

        #endregion Window icon

        private void SetWindowToMiddle()
        {
            Owner = Application.Current.MainWindow;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        private string CheckMessageLength(string msg)
        {
            if (msg.Length > 20)
            {
                msg = msg.Substring(0, 20) + "...?";

                return msg;
            }

            else 
            {
                return msg;
            }

        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            this.Close();
        }

        public static bool Show(string message, string title)
        {
            CustomMessageBox messageBox = new CustomMessageBox(message, title);
            messageBox.ShowDialog();
            return messageBox.Result;
        }
    }
}
