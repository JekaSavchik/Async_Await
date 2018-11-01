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

        Dispatcher dispFact = Dispatcher.CurrentDispatcher;
        long Factorial(int n)
        {
            //UpdateProgressBarDelegate updProgress = new UpdateProgressBarDelegate(ProgressBarFact.SetValue);
            //double value = 0;
            dispFact.Invoke(() => { ProgressBarFact.Value = 0; });
            float step = 100 / n;
            long result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
                Thread.Sleep(500);
                dispFact.Invoke(() => { ProgressBarFact.Value += step; });
                //Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, value += step });

                //w.ProgressBarFact.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new DispatcherOperationCallback(delegate
                //{
                //    w.ProgressBarFact.Value = w.ProgressBarFact.Value + 1;
                //    return null;
                //}), null);
            }
            return result;
        }
        async Task<long> FactorialAsync(int n)
        {
            long res = await Task.Run(() => Factorial(n));

            return res;
        }
        private async void ButtonFact_Click(object sender, RoutedEventArgs e)
        {
            long resFact = await FactorialAsync(Convert.ToInt32(TextBoxFact.Text));
            TextBlockFactRes.Text = "Factorial - " + resFact;
        }

        private async void ButtonFib_Click(object sender, RoutedEventArgs e)
        {
            long result = await FibonacciAsync(Convert.ToInt32(TextBoxFib.Text));

            TextBlockFibRes.Text = "Fibonacci - " + result;
        }

        async Task<long> FibonacciAsync(int n)
        {
            long res = await Task.Run(() => Fibonacci(n));

            return res;
        }
        Dispatcher dispFib = Dispatcher.CurrentDispatcher;

        int Fibonacci(int f)
        {
            dispFib.Invoke(() => { ProgressBarFib.Value = 0; });
            float step = 100 / f;

            if (f == 1)
                return f;

            int result = 0, first = 0, second;
            second = 1;
            for (int i = 1; i < f; i++)
            {
                result = first + second;
                first = second;
                second = result;
                dispFib.Invoke(() => { ProgressBarFib.Value += step; });
                Thread.Sleep(300);
            }
            return result;
        }
    }
}
