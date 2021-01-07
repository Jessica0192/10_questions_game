/*
*   FILE: MainPage.xaml.cs
*   Project: A4
*   PROGRAMMER: Jessica Sim
*   FIRST VERSION: 2020-12-03
*   DESCRIPTION:
*	    In this file, it only displays the login page which gets the user name that will be used in Leaderboard when the 
*	    user finished the game. When the user press the button on the corner, it navigates to the next page, "Questions", which
*	    will begin the quiz.
*/
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace A4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /*
         * FUCNTION: nameBtn_Click
         * DESCRIPTION:
         *      This is a function that is going to be called when the user click the button.
         *      And it will navigate to next page and it also passes the user name that user input on the textblock
         * PARAMETER:
         *      object sender: it is the control that action is for OnClick
         *      RoutedEventArgs e: it handles various events regarding to button
         * RETURNS:
         *      void: there's no return value
         */
        private void nameBtn_Click(object sender, RoutedEventArgs e)
        {
            string name;
            if(String.IsNullOrEmpty(txtName.Text))
            {
                name = "Unknown";
            }
            else
            {
                name = txtName.Text;
            }
            this.Frame.Navigate(typeof(Questions), name);
        }
    }
}
