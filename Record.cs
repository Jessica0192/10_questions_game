/*
*   FILE: Record.cs
*   Project: A4
*   PROGRAMMER: Jessica Sim
*   FIRST VERSION: 2020-12-03
*   DESCRIPTION:
*	    In this file, there are methods that connects to rdbA4 database to retrieve multiple choices that correspond with each questions.
*	    Another method is connecting to rdbA4 database again to retrieve the correct answer for each questions.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace A4
{
    public class Record
    {
        public string[] choice = new string[4];
        string strconn = "Server=localhost;Database=tenQuestionsGame;Uid=root;Pwd=password123;";
        string answer;

        /*
         * FUCNTION: ConnectDB
         * DESCRIPTION:
         *      This function connects to rdbA4 database and get multiple choices for each questions
         * PARAMETER:
         *      int i: integer that represents the ID of questions
         * RETURNS:
         *      string[]: returns array of string that contains 4 multiple choices for each questions
         */
        public string[] ConnectDB(int i)
        {
            int index = 0;
            MySqlConnection mysqlconn = new MySqlConnection(strconn);
            MySqlCommand mysqlcom = new MySqlCommand("select choice from choices where ID = "+i, mysqlconn);
            mysqlconn.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                choice[index] =  mysqlread.GetString(0);
                //choice[index] = mysqlread.GetString("choice");
                //Debug.WriteLine(choice[index]);
                index++;
            }
            mysqlconn.Close();
            return choice;
        }

        /*
         * FUCNTION: CheckAnswer
         * DESCRIPTION:
         *      This function connect to rdbA4 database to get answerID where questionID is same as the integer that passed as parameter.
         * PARAMETER:
         *      int i = integer that represents questionID
         * RETURNS:
         *      string: returns string of answerID
         */
        public string CheckAnswer(int i)
        {
            MySqlConnection mysqlconn = new MySqlConnection(strconn);
            MySqlCommand mysqlcom = new MySqlCommand("select answerID from answers where questionID = " + i, mysqlconn);
            mysqlconn.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                answer = mysqlread.GetString(0);
            }
            return answer;
        }

    }
}
