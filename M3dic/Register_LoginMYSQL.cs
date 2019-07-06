using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace M3dic
{
    class Register_LoginMYSQL
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public Register_LoginMYSQL()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "35.205.24.161";
            database = "M3dic";
            uid = "m3dic";
            password = "M3dic";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert(string first_name, string last_name, string pass, string gender, int age, string phone_number, string time, string email, string address, string city, string number, string CNIC)
        {
            string query = $"INSERT INTO register (first_name, last_name, password, Gender, age, phone_number, date, email, address, city, sequre_number, CNIC, hospital_id)" +
                $" VALUES('{first_name}', '{last_name}', '{pass}','{gender}', '{age}', '{phone_number}', '{time}', '{email}', '{address}', '{city}', '{number}', '{CNIC}', 1)";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Execute command
                cmd.ExecuteNonQuery();
                //close connection
                this.CloseConnection();
            }
        }
     
        public List<string> SearchEmails()
        {
            string query = "SELECT email FROM register where hospital_id = 1";
            List<string> list = new List<string>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(dataReader["email"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        public List<List<string>> FindPassword()
        {
            string query = "SELECT sequre_number,password,email FROM register where hospital_id = 1";
            List<List<string>> list = new List<List<string>>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    List<string> vs = new List<string>();
                    vs.Add(dataReader["sequre_number"].ToString());
                    vs.Add(dataReader["password"].ToString());
                    vs.Add(dataReader["email"].ToString());
                    list.Add(vs);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        //Select statement
        public List<int> FindID()
        {
            string query = "SELECT sequre_number FROM register where hospital_id = 1";
            List<int> list = new List<int>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(int.Parse(dataReader["sequre_number"].ToString().Remove(0, 1)));
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        internal List<List<string>> chackPharmacyLogin()
        {
            string query = "SELECT staffid,password,first_name,email FROM pharmacist where hospital_id = 1";
            List<List<string>> list = new List<List<string>>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    List<string> vs = new List<string>();
                    vs.Add(dataReader["staffid"].ToString());
                    vs.Add(dataReader["password"].ToString());
                    vs.Add(dataReader["first_name"].ToString());
                    vs.Add(dataReader["email"].ToString());
                    list.Add(vs);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        internal List<List<string>> checkAdminLogin()
        {
            string query = "SELECT special_id,password,name,email FROM admins where hospital_id = 1";
            List<List<string>> list = new List<List<string>>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    List<string> vs = new List<string>();
                    vs.Add(dataReader["special_id"].ToString());
                    vs.Add(dataReader["password"].ToString());
                    vs.Add(dataReader["name"].ToString());
                    vs.Add(dataReader["email"].ToString());
                    list.Add(vs);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        internal List<List<string>> checkDoctorLogin()
        {
            string query = "SELECT staffid,password,concat(first_name,' ',last_name) as first_name,email,qualification FROM doctors where hospital_id = 1";
            List<List<string>> list = new List<List<string>>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    List<string> vs = new List<string>();
                    vs.Add(dataReader["staffid"].ToString());
                    vs.Add(dataReader["password"].ToString());
                    vs.Add(dataReader["first_name"].ToString());
                    vs.Add(dataReader["email"].ToString());
                    vs.Add(dataReader["qualification"].ToString());
                    list.Add(vs);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        internal List<List<string>> checkReceptionistLogin()
        {
            string query = "SELECT staffid,password,first_name,email,qualification FROM receptionist where hospital_id = 1";
            List<List<string>> list = new List<List<string>>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    List<string> vs = new List<string>();
                    vs.Add(dataReader["staffid"].ToString());
                    vs.Add(dataReader["password"].ToString());
                    vs.Add(dataReader["first_name"].ToString());
                    vs.Add(dataReader["email"].ToString());
                    list.Add(vs);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        internal List<string> patientINFO(string text)
        {
            string query = $"SELECT first_name,last_name,Gender,phone_number,CNIC,email,date FROM register where sequre_number = '{text}' and hospital_id = 1";
            List<string> list = new List<string>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(dataReader["first_name"].ToString());
                    list.Add(dataReader["last_name"].ToString());
                    list.Add(dataReader["Gender"].ToString());
                    list.Add(dataReader["phone_number"].ToString());
                    list.Add(dataReader["CNIC"].ToString());
                    list.Add(dataReader["email"].ToString());
                    list.Add(dataReader["date"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        internal List<string> patientINFOtt(string text)
        {
            string query = $"SELECT first_name,last_name,phone_number,CNIC,email FROM register where sequre_number = '{text}' and hospital_id = 1";
            List<string> list = new List<string>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(dataReader["first_name"].ToString());
                    list.Add(dataReader["last_name"].ToString());
                    list.Add(dataReader["phone_number"].ToString());
                    list.Add(dataReader["CNIC"].ToString());
                    list.Add(dataReader["email"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
        internal void updateinformation(string text1, string text2, string text3, string text4, string text5, string text6, string text7)
        {

            string query = $"UPDATE register SET first_name='{text2}', last_name='{text3}', Gender='{text4}', phone_number='{text5}', CNIC='{text6}', email='{text7}' WHERE sequre_number='{text1}' and hospital_id = 1";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        internal void DeletePerson(string text)
        {
            string query = $"DELETE FROM patients_appointment WHERE patient_id='{text}' and hospital_id = 1";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }


    }
}
