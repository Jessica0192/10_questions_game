/*
*   FILE: ConnectDB.cs
*   Project: A4
*   PROGRAMMER: Jessica Sim
*   FIRST VERSION: 2020-12-03
*   DESCRIPTION:
*	    In this file, it connects to question table in rdbA4 database and retrieves all 10 questions that is going to be displayed
*	    in Questions page.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace A4
{
    public class ConnectDB
    {
        string strconn = "Server=localhost;Database=tenQuestionsGame;Uid=root;Pwd=password123;";

        /*
         * FUCNTION: Connection
         * DESCRIPTION:
         *      This function connects to rdbA4 database and get the question which is correspond with the number
         *      which is passes as parameter
         * PARAMETER:
         *      int i: integer that represents id in questions table
         * RETURNS:
         *      string: returns string of questions
         */
        public string Connection(int i)
        {
            string str = "";
            List<string> lsChoices = new List<string>();

            MySqlConnection mysqlconn = new MySqlConnection(strconn);
            MySqlCommand mysqlcom = new MySqlCommand("select question from questions where id = "+ i, mysqlconn);

            mysqlconn.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            while (mysqlread.Read())
            {
                str = mysqlread.GetString(0);
            }
            mysqlconn.Close();
            return str;
        }
    }
}
