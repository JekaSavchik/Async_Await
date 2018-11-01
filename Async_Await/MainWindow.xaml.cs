using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Async_Await
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    //delegate void UpdateProgressBarDelegate(DependencyProperty dp, object value);
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Dispatcher disp = Dispatcher.CurrentDispatcher;
        static long Factorial(int n, MainWindow w)
        {
            //UpdateProgressBarDelegate updProgress = new UpdateProgressBarDelegate(w.ProgressBarFact.SetValue);
            //double value = 0;
            float step = 100 / n;
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
                Thread.Sleep(500);
                w.disp.Invoke(() => { w.ProgressBarFact.Value += step; });
                //w.Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, value += step });

                //w.ProgressBarFact.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(delegate
                //{
                //    w.ProgressBarFact.Value = w.ProgressBarFact.Value + 1;
                //    return null;
                //}), null);
            }
            return result;
        }
        static async void FactorialAsync(int n, MainWindow w)
        {
            long res = await Task.Run(() => Factorial(n, w));

            w.TextBlockFactRes.Text = "Factorial - " + res;
        }
        private void ButtonFact_Click(object sender, RoutedEventArgs e)
        {
            FactorialAsync(Convert.ToInt32(TextBoxFact.Text), this);
        }

        private void ButtonFib_Click(object sender, RoutedEventArgs e)
        {
            long result = 1;

            for (int i = 1; i <= Convert.ToInt32(TextBoxFib.Text); i++)
            {
                result *= i;
                Thread.Sleep(300);
            }
            TextBlockFibRes.Text = "Fibonacci - " + result;
        }
    }
}
