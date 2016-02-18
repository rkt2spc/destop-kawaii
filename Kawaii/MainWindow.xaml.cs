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
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media.Animation;

namespace Kawaii
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double OriginTop;
        double OriginLeft;
        MediaPlayer soundPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Drawing.Rectangle scr = Screen.PrimaryScreen.WorkingArea;
            OriginTop = scr.Height - this.Height;
            OriginLeft = scr.Width - this.Width;

            this.Top = OriginTop + 45;
            this.Left = OriginLeft;

            soundPlayer.Open(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"/Resources/kawaiiVoice.mp3"));
            soundPlayer.MediaEnded += new EventHandler(Song_Ended);
        }
        private void Song_Ended(object sender, EventArgs e)
        {
            soundPlayer.Position = TimeSpan.Zero;
            soundPlayer.Play();
        }
        //==================
        private void MenuItem_Config(object sender, RoutedEventArgs e)
        {
            Window config = new ConfigurationWindow();
            config.Show();
        }
        private void MenuItem_Show(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Normal;
            this.Activate();
        }
        private void MenuItem_Hide(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //==================
        private void Window_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            soundPlayer.Play();
            Caption.Visibility = System.Windows.Visibility.Hidden;
            heart.Visibility = System.Windows.Visibility.Visible;
            kawaii.Source = (BitmapImage)Resources["Kawaii_2"];
            DoubleAnimation animation = new DoubleAnimation(this.Top, OriginTop, new Duration(new TimeSpan(0, 0, 0, 0, 400)));
            //animation.AccelerationRatio = 0.1;
            this.BeginAnimation(Window.TopProperty, animation);
        }
        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            soundPlayer.Pause();
            Caption.Visibility = System.Windows.Visibility.Visible;
            heart.Visibility = System.Windows.Visibility.Hidden;
            kawaii.Source = (BitmapImage)Resources["Kawaii_1"];
            DoubleAnimation animation = new DoubleAnimation(this.Top, OriginTop + 45, new Duration(new TimeSpan(0, 0, 0, 0, 400)));
            //animation.AccelerationRatio = 0.1;
            this.BeginAnimation(Window.TopProperty, animation);
        }
        //==================
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            //clean up notifyicon (would otherwise stay open until application finishes)
            MyNotifyIcon.Dispose();

            base.OnClosing(e);
        }

        private void ShowCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.WindowState != System.Windows.WindowState.Normal)
                this.WindowState = System.Windows.WindowState.Normal;
            this.Activate();
        }
        private void HideCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.WindowState != System.Windows.WindowState.Minimized;
        }    
        private void HideCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
