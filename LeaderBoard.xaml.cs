/*
*   FILE: LeaderBoard.xaml.cs
*   Project: A4
*   PROGRAMMER: Jessica Sim
*   FIRST VERSION: 2020-12-03
*   DESCRIPTION:
*	    In this file, it displays the leaderboard from highest score on the top and it also stores the record of scores and user name
*	    in the users table from rdbA4 database.
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Toolkit.Uwp.UI.Controls;
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
    public sealed partial class LeaderBoard : Page
    {
        private string[] nameAnswer = new string[2];
        private SortedList sortedLs;
        string strconn = "Server=localhost;Database=tenQuestionsGame;Uid=root;Pwd=password123;";

        public LeaderBoard()
        {
            this.InitializeComponent();
        }

        string[] topLeaderBoard = { "Rank", "Player", "Time" };


        /*
         * FUCNTION: OnNavigatedTo
         * DESCRIPTION:
         *      This function is called when the page is navigated from previous page to this page
         * PARAMETER:
         *      NavigationEventArgs e: provides data for navigation methods and event handlers
         * RETURNS:
         *      void: there's no return value
         */
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            nameAnswer = (string[])e.Parameter;
            DataTable dt = LayoutLeaderBoard();
            FillDataGrid(dt, DataGrid1);
        }

        /*
         * FUCNTION: LayoutLeaderBoard
         * DESCRIPTION:
         *      This function builds the DataTable that will be used later to put data in DataGrid. To build DataTable, it reads all data from 
         *      users table in rdbA4 database and build DataTable. Then, it stores new user and his/her score to the table.
         * PARAMETER:
         *      There's no parameters
         * RETURNS:
         *      void: there's no return value
         */
        private DataTable LayoutLeaderBoard()
        {
            int j = 1;
            sortedLs = new SortedList();
            DataTable dt = new DataTable();
            Dictionary<string, int> dict= new Dictionary<string, int>();
            int index =0;

            sortedLs.Add(nameAnswer[1] + "." + index, nameAnswer[0]);
            dict.Add(nameAnswer[1] + "." + index, Convert.ToInt32(nameAnswer[1]));
            index++;

            dt.Columns.Add("Rank", typeof(int));
            dt.Columns.Add("User Name", typeof(string));
            dt.Columns.Add("Point", typeof(string));

            MySqlConnection mysqlconn = new MySqlConnection(strconn);
            MySqlCommand mysqlcom = new MySqlCommand("select * from users",mysqlconn);

            mysqlconn.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                sortedLs.Add(mysqlread.GetString(2)+"."+index, mysqlread.GetString(1));
                dict.Add(mysqlread.GetString(2) + "." + index, Convert.ToInt32(mysqlread.GetString(2)));
                index++;
            }
            mysqlconn.Close();

            MySqlConnection mysqlconn2 = new MySqlConnection(strconn);
            string insertQuery = "INSERT INTO users(userName,score) VALUES( "+ "'"+nameAnswer[0].ToString()+ "'" + "," + nameAnswer[1].ToString()+")";
            mysqlconn2.Open();
            MySqlCommand mySqlCommand = new MySqlCommand(insertQuery, mysqlconn2);
            mySqlCommand.ExecuteNonQuery();
            mysqlconn2.Close();


            IList lsKey = sortedLs.GetKeyList();
            IList lsValue = sortedLs.GetValueList();

            for (int i = sortedLs.Count -1; i >= 0; i--)
            {
                int score = dict[lsKey[i].ToString()];
                dt.Rows.Add(j, lsValue[i], score);
                j++;
            }

            return dt;
        }

        /*
         * FUCNTION: FillDataGrid
         * DESCRIPTION:
         *      This function is to fill the DataGrid of leaderBoard with DataTable.
         *      It will show all the users and their scores from highest to lowest in the window
         * PARAMETER:
         *      DataTable table: represents one table of in-memory data
         *      DataGrid grid: control to represent data in columns and row
         * RETURNS:
         *      void: there's no return value
         */
        public static void FillDataGrid(DataTable table, DataGrid grid)
        {
            grid.Columns.Clear();
            grid.AutoGenerateColumns = false;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                grid.Columns.Add(new DataGridTextColumn()
                {
                    Header = table.Columns[i].ColumnName,

                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") }
                });
            }

            var collection = new ObservableCollection<object>();
            foreach (DataRow row in table.Rows)
            {
                collection.Add(row.ItemArray);
            }

            grid.ItemsSource = collection;
        }

        /*
         * FUCNTION: goBackToFirstPage_Click
         * DESCRIPTION:
         *      This function is called when the user click the button to go back to first page of this program.
         * PARAMETER:
         *      object sender: it is the control that action is for OnClick
         *      RoutedEventArgs e: it handles various events regarding to button
         * RETURNS:
         *      void: there's no return value
         */
        private void goBackToFirstPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
