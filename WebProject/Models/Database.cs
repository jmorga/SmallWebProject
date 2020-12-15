using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace WebProject.Models
{
    public class Database
    {
        string connectionString;

        public Database()
        {
            connectionString = "Data Source=JMORGA-LT1\\SQLEXPRESS;Initial Catalog=People;Integrated Security=True";
        }

        public List<Person> getPersonList()
        {
            List<Person> personList = new List<Person>();
            string getPersonTableQuery = "SELECT * FROM Person";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(getPersonTableQuery, connection);

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read()) //Reads a row from a table, creates a person object with the information and adds it into the person list
            {
                personList.Add(new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }

            connection.Close();

            return personList;
        }


        //Updates the Person table from the people database. 
        public void changePersonData(Person changedPerson)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;

            connection.Open();

            command = new SqlCommand("UPDATE Person SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID", connection);
            command.Parameters.AddWithValue("FirstName", changedPerson.firstName);
            command.Parameters.AddWithValue("LastName", changedPerson.lastName);
            command.Parameters.AddWithValue("ID", changedPerson.id);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}