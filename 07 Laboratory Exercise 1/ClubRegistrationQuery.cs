using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using Microsoft.SqlServer.Server;
using System.Xml;

namespace _07_Laboratory_Exercise_1
{
    
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;
        public DataTable dataTable;
        public BindingSource bindingSource;
        private string connectionString;
        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        public int _Age;
        public long _StudID;
        public ClubRegistrationQuery()
        {
           
            connectionString = @"Data Source=DESKTOP-H0S7818\\SQLEXPRESS;database=ClubDB;integrated security = True";

            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }
        public bool DisplayList()
        {
            string ViewClubMembers = "SELECT StudentID, FirstName, MiddleName,LastName, Age, Gender, Program FROM ClubMembers";
            sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);
            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingSource.DataSource = dataTable;
            return true;
        }
        
        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            sqlCommand = new SqlCommand("Insert Into ClubMembers VALUES(@Id, @StudentID, @FirstName, " + "@MiddleName, @LastName, @Age, @Gender, @Program)", sqlConnect);
            
            sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
            sqlCommand.Parameters.Add("@RegistrationID", SqlDbType.BigInt).Value = StudentID;
            sqlCommand.Parameters.Add("StudentID", SqlDbType.VarChar).Value = StudentID;
            sqlCommand.Parameters.Add("FirstName", SqlDbType.VarChar).Value = FirstName;
            sqlCommand.Parameters.Add("MiddleName", SqlDbType.VarChar).Value = MiddleName;
            sqlCommand.Parameters.Add("LastName", SqlDbType.VarChar).Value = LastName;
            sqlCommand.Parameters.Add("Age", SqlDbType.Int).Value = Age;
            sqlCommand.Parameters.Add("Gender", SqlDbType.VarChar).Value = Gender;
            sqlCommand.Parameters.Add("Program", SqlDbType.VarChar).Value = Program;
            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }
        public void DisplayStudID(ComboBox comboBox)
        {
            string getId = "SELECT * FROM ClubMembers";
            sqlCommand = new SqlCommand(getId, sqlConnect);
            sqlConnect.Open();
            sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox.Items.Add(sqlReader["StudentId"]);
            }
            sqlConnect.Close();
        }
        public bool UpdateStudData(long StudentID, string LastName, string FirstName, string MiddleName, int Age, string Program, string Gender)
        {
            sqlCommand = new SqlCommand("UPDATE ClubMembers SET StudentID = @StudentID, Age = @Age, Program = @Program, FirstName = @FirstName, LastName = @LastName, MiddleName = @MiddleName, Gender = @Gender  WHERE StudentId = @StudentId", sqlConnect);

            sqlCommand.Parameters.AddWithValue("@StudentId", StudentID);
            sqlCommand.Parameters.AddWithValue("@Age", Age);
            sqlCommand.Parameters.AddWithValue("@Program", Program);
            sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", LastName);
            sqlCommand.Parameters.AddWithValue("@MiddleName", MiddleName);
            sqlCommand.Parameters.AddWithValue("@Gender", Gender);

            sqlConnect.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();
            return true;
        }
        public void GetStudData(long studentId)
        {
            string ConnectionString = "Data Source=DESKTOP-H0S7818\\SQLEXPRESS;database=ClubDB;integrated security = True";

            using (SqlConnection sqlConnect = new SqlConnection(ConnectionString))
            {
                string query = "SELECT * FROM ClubMembers WHERE StudentId = @StudentId";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnect);
                sqlCommand.Parameters.AddWithValue("@StudentId", studentId);

                sqlConnect.Open();

                SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                if (sqlReader.Read())
                {
                    _StudID = Convert.ToInt64(sqlReader["StudentID"]);
                    _FirstName = sqlReader["FirstName"].ToString();
                    _MiddleName = sqlReader["MiddleName"].ToString();
                    _LastName = sqlReader["LastName"].ToString();
                    _Age = Convert.ToInt32(sqlReader["Age"]);
                    _Gender = sqlReader["Gender"].ToString();
                    _Program = sqlReader["Program"].ToString();
                }
                else
                {
                    MessageBox.Show("StudentID not in Record!");
                }
                sqlConnect.Close();
            }
        }
    }

    
}
