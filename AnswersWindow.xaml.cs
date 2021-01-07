/*
*   FILE: AnswersWindow.xaml.cs
*   Project: A4
*   PROGRAMMER: Jessica Sim
*   FIRST VERSION: 2020-12-03
*   DESCRIPTION:
*	    In this file, it displays all 10 questions and correct answer for each question. It connects to rdbA4 database and
*	    find the answer for each question with questionID and displays the answers in TextBlock.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MySql.Data.MySqlClient;
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
    public sealed partial class AnswersWindow : Page
    {
        string strconn = "Server=localhost;Database=tenQuestionsGame;Uid=root;Pwd=password123;";
        private string[] questions = new string[10];
        private string[] questionNum = new string[10];
        private string[] answerID = new string[10];
        private string[] nameAndPoint = new string[2];
        private List<string> answerStr = new List<string>();

        /*
         * FUCNTION: OnNavigatedTo
         * DESCRIPTION:
         *      This function is called when it is navigated from Questions page to this page.
         * PARAMETER:
         *      NavigationEventArgs e: provides data for navigation methods and event handlers
         * RETURNS:
         *      void: there's no return value
         */
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            nameAndPoint = (string[])e.Parameter;
            RetrieveAnswers();
        }

        public AnswersWindow()
        {
            this.InitializeComponent();
        }

        /*
         * FUCNTION: RetrieveAnswers
         * DESCRIPTION:
         *      This function connects to rdbA4 database and get all questions from questions table. And then it gets answerID from answers table
         *      to check the answer. Finally, it calls GetAnswer function to get the answer with answerID parameter and displays those to the TextBlock
         * PARAMETER:
         *      There's no parameters
         * RETURNS:
         *      void: there's no return value
         */
        public void RetrieveAnswers()
        {
            int index = 0;

            MySqlConnection mysqlconn = new MySqlConnection(strconn);
            MySqlCommand mysqlcom = new MySqlCommand("select * from questions", mysqlconn);

            mysqlconn.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                questionNum[index] = mysqlread.GetString(0);
                questions[index] = mysqlread.GetString(1);
                index++;
            }
            mysqlconn.Close();

            index = 0;

            MySqlConnection mysqlconn2 = new MySqlConnection(strconn);
            MySqlCommand mysqlcom2 = new MySqlCommand("select answerID from answers", mysqlconn2);
            mysqlconn2.Open();
            MySqlDataReader mysqlread2 = mysqlcom2.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread2.Read())
            {
                answerID[index] = mysqlread2.GetString(0);
                index++;
            }
            mysqlconn2.Close();

            Answers answers = new Answers();

            for (int i = 0; i < 10; i++)
            {
                answerStr.Add(answers.GetAnswer(i, answerID));
            }

            for (int j = 0; j < 10; j++)
            {
                int num = j + 1;
                txtAnswers.Text += num + ". " + questions[j];
                txtAnswers.Text += Environment.NewLine;
                txtAnswers.Text += "-> " + answerStr[j];
                txtAnswers.Text += Environment.NewLine;
                txtAnswers.Text += Environment.NewLine;
            }
        }

        /*
         * FUCNTION: nextBtn_Click
         * DESCRIPTION:
         *      This function is called when the user clicked button to go to next page
         * PARAMETER:
         *      object sender: it is the control that action is for OnClick
         *      RoutedEventArgs e: it handles various events regarding to button
         * RETURNS:
         *      void: there's no return value
         */
        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LeaderBoard), nameAndPoint);
        }
    }
}
