using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using ContactManagerApp;
using Microsoft.AspNetCore.Mvc;

namespace SubsContactManagerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubsContactManagerAPIController : ControllerBase
    {


        [HttpGet("add")]
        public ActionResult<int> Add(int a, int b)
        {
            return a + b;
        }

        [HttpPost("SavePerson")]
        public int SavePerson(string FirstName, string LastName, string Phone, string Email)
        {
            
            using (SqlConnection connection = new SqlConnection(AppSettings.getConnectionString()))
            {
                connection.Open();
                string sql = "INSERT INTO Persons (firstname, lastname, Email, Phone) VALUES (@FirstName, @LastName, @Email, @Phone)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("Phone", Phone);

                    return command.ExecuteNonQuery();
                }
            }
        }

        [HttpPost("EditPerson")]
        public int EditPerson(int ID, string FirstName, string LastName, string Phone, string Email)
        {

            using (SqlConnection connection = new SqlConnection(AppSettings.getConnectionString()))
            {
                connection.Open();
                string sql = "UPDATE Persons Set firstName=@FirstName, lastName=@LastName, Email=@Email, Phone=@Phone where ID=@ID";
                Console.WriteLine(sql);

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@ID", ID);

                    return command.ExecuteNonQuery();
                }
            }
        }


        [HttpPost("DeletePerson")]
        public int DeletePerson(int ID)
        {

            using (SqlConnection connection = new SqlConnection(AppSettings.getConnectionString()))
           
            {
                connection.Open();
                string sql = "DELETE Persons WHERE ID=@ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", ID);
                    return command.ExecuteNonQuery();
                }
            }
        }


        [HttpGet("GetAllPersons")]
        public List<Person> GetAllPersons()
        {
            var persons = new List<Person>();
            SqlConnection sqlConnection = new SqlConnection(AppSettings.getConnectionString());
            using (SqlConnection connection = sqlConnection)
            {
                connection.Open();
                string sql = "SELECT ID, FirstName, LastName,Email,Phone FROM Persons";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var person = new Person
                            {
                                ID = reader.GetInt32(0),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Phone = reader["Phone"].ToString()
                            };
                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }



    }


}
