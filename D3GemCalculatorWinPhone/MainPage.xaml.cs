using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace D3GemCalculatorWinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        Gem Calculator = new Gem();

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string[] output;

            Output.Items.Clear();

            output = Calculator.SanityCheck(GemUsed.SelectedIndex, GemWanted.SelectedIndex,Amount.Text ,((ComboBoxItem)GemUsed.SelectedItem).Content.ToString(), ((ComboBoxItem)GemWanted.SelectedItem).Content.ToString());

            foreach (string message in output)
            {
                if (message == "")
                {
                    ; //no deathsbreath text, so do nothing.
                }
                else
                    Output.Items.Add(message);
            }
        }

        private void Amount_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter)
            {
                CalculateButton_Click(sender, e);
                Windows.UI.ViewManagement.InputPane.GetForCurrentView().TryHide();
                e.Handled = true;
            }
        }

        private async void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog AboutMsg = new MessageDialog("Diablo 3 Gem Calculator by Jose A. Araujo.\nThanks to William for all the help.");
            await AboutMsg.ShowAsync();
        }
    }
}
