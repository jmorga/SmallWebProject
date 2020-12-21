using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace WebProject.Models
{
    public class Database
    {
        string connectionString;

        public Database()
        {
            connectionString = "Data Source=JMORGA-LT1\\SQLEXPRESS;Initial Catalog=People;Integrated Security=True";
        }

        public Wrapper getPersonList()
        {
            List<Person> personList = new List<Person>();
            string getPersonTableQuery = "SELECT * FROM Person";
            SqlCommand command;
            SqlDataReader reader;
            SqlConnection connection;

            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch(InvalidOperationException exception)
            {
                return new Wrapper(false, $"InvalidOperationException: {exception.Message}");
            }
            catch (ArgumentException exception) 
            {
                return new Wrapper(false, $"ArgumentException: {exception.Message}");
            }
            catch (Exception exception)
            {
                return new Wrapper(false, $"Exception: {exception.Message}");
            }

            command = new SqlCommand(getPersonTableQuery, connection);

            try
            {
                reader = command.ExecuteReader();
            }
            catch (SqlException exception)
            {
                connection.Close();
                return new Wrapper(false, $"SqlException: {exception.Message}");
            }
            catch(Exception exception)
            {
                connection.Close();
                return new Wrapper(false, $"Exception: {exception.Message}");
            }

            while (reader.Read()) //Reads a row from a table, creates a person object with the information and adds it into the person list
            {
                personList.Add(new Person(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }

            connection.Close();

            return new Wrapper(true, JsonConvert.SerializeObject(personList));
        }


        //Updates the Person table from the people database. 
        public Wrapper changePersonData(Person changedPerson)
        {
            SqlConnection connection;
            SqlCommand command;

            if(string.IsNullOrEmpty(changedPerson.firstName) || string.IsNullOrEmpty(changedPerson.lastName)) 
            {
                return new Wrapper(false, "Error: An input field is null or empty");
            }

            try 
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch (SqlException exception) 
            {
                return new Wrapper(false, $"SqlException: {exception.Message}");
            }
            catch (ArgumentException exception) 
            {
                return new Wrapper(false, $"ArgumentException: {exception.Message}");
            }
            catch (Exception exception) 
            {
                return new Wrapper(false, $"Exception: {exception.Message}");
            }

            command = new SqlCommand("UPDATE Person SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID", connection);
            command.Parameters.AddWithValue("FirstName", changedPerson.firstName);
            command.Parameters.AddWithValue("LastName", changedPerson.lastName);
            command.Parameters.AddWithValue("ID", changedPerson.id);

            try {
                command.ExecuteNonQuery();
            }
            catch (SqlException exception) {
                connection.Close();
                return new Wrapper(false, $"Sql Exeption, Error: {exception.Message}");
            }
            catch (Exception exception) {
                connection.Close();
                return new Wrapper(false, $"Exception, Error: {exception.Message}");
            }

            connection.Close();

            return new Wrapper(true, "Data updated successfully");
        }
    }
}