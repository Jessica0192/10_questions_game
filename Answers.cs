/*
*   FILE: Answers.cs
*   Project: A4
*   PROGRAMMER: Jessica Sim
*   FIRST VERSION: 2020-12-03
*   DESCRIPTION:
*	    In this file, it connects to database and retrieves the answer for each question to compare with the answer of user.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace A4
{
    public class Answers
    {
        string strconn = "Server=localhost;Database=tenQuestionsGame;Uid=root;Pwd=password123;";

        /*
         * FUCNTION: GetAnswer
         * DESCRIPTION:
         *      This function is called when the user click the radio button and want to compare with the correct answer
         * PARAMETER:
         *      int i: integer which will identify ID in database to retrieve answer
         *      string[] str: string which will identify choiceID in choices table
         * RETURNS:
         *      string: returns string of answer for each questions
         */
        public string GetAnswer(int i, string[] str)
        {
            int num = i + 1;
            string answer = "";
            MySqlConnection mysqlconn = new MySqlConnection(strconn);
            MySqlCommand mysqlcom = new MySqlCommand("select choice from choices where ID="+num+" and choiceID = " + str[i], mysqlconn);
            mysqlconn.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                answer = mysqlread.GetString(0);
            }
            mysqlconn.Close();

            return answer;
        }
    }

    
}
