/*
*   FILE: Questions.xaml.cs
*   Project: A4
*   PROGRAMMER: Jessica Sim
*   FIRST VERSION: 2020-12-03
*   DESCRIPTION:
*	    In this file, it displays 10 questions and 4 multiple choices for each quesition. There is only one answer
*	    and there will be 20 seconds to solve for each question. The total point for each question is 5 points and 
*	    everytime 5 seconds passed, 1 point will be drop. If the user got wrong, she/he will get 0 point for that question.
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace A4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Questions : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        private string question;
        private int questionNum = 1;
        private string[] choices = new string[4];
        private int i = 1;
        private int j = 0;
        private int point = 0;
        private string[] nameAndPoint = new string[2];

        private int timeIncrement = 0;
        private int second = 0;
        private string minSec;


        public Questions()
        {
            this.InitializeComponent();
            timeIncrement = 0;
            second = 0;
            minSec = null;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            int index = 0;
            timeTxtBlock.Text = "00";
            Record record = new Record();

            choices = record.ConnectDB(i);
            foreach (var str in choices)
            {
                choices[index] = str;
                index++;
            }
            
            ConnectDB connectDB = new ConnectDB();


            question = connectDB.Connection(questionNum);
            txtQuestion.Text = "1. "+question;
            choice1.Content = choices[0];
            choice2.Content = choices[1];
            choice3.Content = choices[2];
            choice4.Content = choices[3];
        }


        /*
         * FUCNTION: timer_Tick
         * DESCRIPTION:
         *      This is a function that is going to be called every one second to increase the time every one second.
         *      After 20 second is passed, it resets to 0 and start the timer again.
         * PARAMETER:
         *      object sender: it supports all classes in .NET framework class and provided low-level services to derived classes
         *      object e: it supports all classes in .NET framework class and provided low-level services to derived classes
         * RETURNS:
         *      void: there's no return value
         */
        private void timer_Tick(object sender, object e)
        {
            second = timeIncrement;
            if (second == 21)
            {
                j++;
                second = 0;
                timeIncrement = 0;
                timer.Stop();
                NextQuestion();
            }
            timeTxtBlock.Text = string.Format("{0, 0:D2}", second);
            timeIncrement++;
        }


        /*
         * FUCNTION: OnNavigatedTo
         * DESCRIPTION:
         *      This function is called when the it is navigated from MainPage to this page
         * PARAMETER:
         *      NavigationEventArgs e: provides data for navigation methods and event handlers
         * RETURNS:
         *      void: there's no return value
         */
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            nameAndPoint[0] = e.Parameter.ToString();
        }


        /*
         * FUCNTION: choice_Checked
         * DESCRIPTION:
         *      This function is called when one of multiple choices is checked or selected by user and it stops the timer
         *      to go to next question. Before it moves to the next question, it gets the answer from database and compare the answer
         *      if it is right. Base on the time that user select one of the choices, it stores the point and move to next function
         *      which goes to next question.
         * PARAMETER:
         *      object sender: it is the control that action is for Checked
         *      RoutedEventArgs e: it handles various events regarding to radiobutton
         * RETURNS:
         *      void: there's no return value
         */
        private void choice_Checked(object sender, RoutedEventArgs e)
        {
            timeTxtBlock.Text = string.Format("{0, 0:D2}", 0);
            timer.Stop();
            j++;
            string answer;
            int val;
            bool correct = false;


            RadioButton rb = sender as RadioButton;
            string choice = rb.Tag.ToString();
            Record record = new Record();
            answer = record.CheckAnswer(j);

            switch(choice)
            {
                case "1":
                    val = String.Compare("1", answer);
                    if(val == 0)
                    {
                        correct = true;
                    }
                    break;
                case "2":
                    val = String.Compare("2", answer);
                    if (val == 0)
                    {
                        correct = true;
                    }
                    break;
                case "3":
                    val = String.Compare("3", answer);
                    if (val == 0)
                    {
                        correct = true;
                    }
                    break;
                case "4":
                    val = String.Compare("4", answer);
                    if (val == 0)
                    {
                        correct = true;
                    }
                    break;
                default:
                    correct = false;
                    break;
            }

            if(correct == true)
            {
                int time = second / 4;
                switch(time)
                {
                    case 0:
                    case 1:
                        point += 5;
                        break;
                    case 2:
                        point += 4;
                        break;
                    case 3:
                        point += 3;
                        break;
                    case 4:
                        point += 2;
                        break;
                    case 5:
                        point += 1;
                        break;
                }
            }
            else
            {
                point += 0;
            }
            timeIncrement = 0;
            second = 1;
            NextQuestion();
            rb.IsChecked = false;
        }

        /*
         * FUCNTION: NextQuestion
         * DESCRIPTION:
         *      This function is called when the user select one of the choice in the question and would like to move on the next question.
         *      It starts the timer and displays the next question with 4 multiple choices
         * PARAMETER:
         *      Radiobutton rb: radio button
         * RETURNS:
         *      void: there's no return value
         */
        public void NextQuestion()
        {
            int index = 0;
            if (questionNum == 10)
            {
                nameAndPoint[1] = point.ToString();
                this.Frame.Navigate(typeof(AnswersWindow), nameAndPoint);
            }
            questionNum++;
            i++;

            Record record = new Record();
            choices = record.ConnectDB(i);
            foreach (var str in choices)
            {
                choices[index] = str;
                index++;
            }

            ConnectDB connectDB = new ConnectDB();
            question = connectDB.Connection(questionNum);
            txtQuestion.Text = i +". "+question;
            choice1.Content = choices[0];
            choice2.Content = choices[1];
            choice3.Content = choices[2];
            choice4.Content = choices[3];
            timer.Start();
        }
    }
}
