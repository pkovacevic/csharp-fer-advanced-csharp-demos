using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ThreadingDemo2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Use #regions to improve code readability (regions are collapsable).
        // #region
        // your code
        // #endregion


        #region GUI Methods

        private void SetLoadingOn()
        {
            TextBlock.Text = "";
            LoadingAnimation.Visibility = Visibility.Visible;
            RightWayButton.IsEnabled = false;
            WrongWayButton.IsEnabled = false;
            GoogleButton.IsEnabled = false;
        }

        private void SetLoadingOff()
        {
            LoadingAnimation.Visibility = Visibility.Hidden;
            RightWayButton.IsEnabled = true;
            WrongWayButton.IsEnabled = true;
            GoogleButton.IsEnabled = true;
        }
        #endregion

        #region Button click handlers

        private void SyncTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            SetLoadingOn();
            // Bad - get students takes 10 second to finish. Poor internet connection?
            // Main thread is stuck waiting... who will refresh the UI now?
            var students = GetStudents();

            TextBlock.Text = string.Join("\n", students);

            SetLoadingOff();
        }


        private void AsyncTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            // FailedAsyncAttempt();
            SuccessfullAsyncAttempt();
        }


        private async void GetGooglePageButtonClicked(object sender, RoutedEventArgs e)
        {
            SetLoadingOn();

            string page = await GetGooglePageAsync();
            TextBlock.Text = page;

            SetLoadingOff();

        }
        #endregion

        #region Actual logic

        /// <summary>
        /// Method that returns students.
        /// Lasts for 10 seconds!
        /// </summary>
        /// <returns></returns>
        private string[] GetStudents()
        {
            // Simulate long running operation
            Thread.Sleep(10000);
            return new string[] { "Pero", "Ivan", "Luka", "Tamara" };
        }

        private async void SuccessfullAsyncAttempt()
        {
            SetLoadingOn();

            // Fire a new thread that will collect the students.
            // Nice.
            var studentCollectingTask = Task.Run(() => GetStudents());
            
            // Don't just wait. Do the asynchronous wait.
            // Main method returns to its duties while this runs.
            var students = await studentCollectingTask;


            // Main thread continues here after the student collecting task finished.
            TextBlock.Text = string.Join("\n", students);

            SetLoadingOff();
        }


        private void FailedAsyncAttempt()
        {
            // Fire a new thread that will collect the students.
            // Nice.
            var studentCollectingTask = Task.Run(() => GetStudents());

            // But.. at this point, task is still running. How will I get the students??
            // Bad -> accessing Result property will make main thread wait again.
            // Main thread waits for task to finish in order to get the result 
            // (result = return value, what task promised to deliver)
            TextBlock.Text = string.Join(",", studentCollectingTask.Result);

        }


        /// <summary>
        /// 1. We have to add async keyword since we are using await keyword inside the method. 
        /// Few things about that:
        /// a) Convention: Name should be GetGooglePageAsync instead of GetGooglePage
        /// b) We no longer can return a regular string, we have to return promise that we will return string
        /// c) Since we are returning task, others can await us as well!
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetGooglePageAsync()
        {
            var client = new HttpClient();
            // Get string is an async method (one clue is the method name, other that it returns Task<string>)
            // It returns a task that promising us string in the future.
            // We can await tasks.
            Task<string> getPageTask = client.GetStringAsync("http://www.google.com/");
            Task<string> getMapsPageTask = client.GetStringAsync("http://www.google.com/");


            // (await task) -> will get us a string object.
            // We can use the expression as we would with any other string.
            return string.Format("Google response:\n{0}\n\nGoogle maps response: \n{1}", 
                        await getPageTask,  await getMapsPageTask);

        }
    }
    #endregion

}
