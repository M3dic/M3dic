using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3dic
{
    class AddStuff
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public AddStuff()
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
        public void AddAdmin(string first_name, string pass, string specID, string phone_number, string email, string salary, int age, string CNIC)
        {
            string query = $"INSERT INTO admins (name, password, special_id, email, phone_number, salary, age, CNIC, hospital_id)" +
                $" VALUES('{first_name}', '{pass}', '{specID}','{email}', '{phone_number}', '{salary}', '{age}', '{CNIC}', '1')";

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

        public void AddPharmacist(string ID, string FName, string LName, string pass, string gender, int age, string phone, string datejoin, string email, string line, string city, string timings, string salary, string CNIC)
        {
            string query = $"INSERT INTO pharmacist (staffid, first_name, last_name, password, Gender, age, phone_number, datejoined, email, line, city, timings, salary, CNIC, hospital_id)" +
                $" VALUES('{ID}', '{FName}', '{LName}','{pass}', '{gender}', '{age}', '{phone}', '{datejoin}', '{email}', '{line}', '{city}', '{timings}', '{salary}', '{CNIC}', '1')";

            //open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        internal void AddDoctor(string ID, string FName, string LName, string pass, string gender, int age, string phone, string datejoin, string email, string line, string city, string qualification, string timings, string salary, string CNIC)
        {
            string query = $"INSERT INTO doctors (staffid, first_name, last_name, password, Gender, age, phone_number, datejoined, email, line, city, qualification, timings, salary, CNIC, hospital_id)" +
               $" VALUES('{ID}', '{FName}', '{LName}','{pass}', '{gender}', '{age}', '{phone}', '{datejoin}', '{email}', '{line}', '{city}', '{qualification}', '{timings}', '{salary}', '{CNIC}', '1')";

            //open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        internal void Addreceptionist(string ID, string FName, string LName, string pass, string gender, int age, string phone, string datejoin, string email, string line, string city, string qualification, string timings, string salary, string CNIC)
        {
            string query = $"INSERT INTO receptionist (staffid, first_name, last_name, password, Gender, age, phone_number, datejoined, email, line, city, qualification, timings, salary, CNIC, hospital_id)" +
               $" VALUES('{ID}', '{FName}', '{LName}','{pass}', '{gender}', '{age}', '{phone}', '{datejoin}', '{email}', '{line}', '{city}', '{qualification}', '{timings}', '{salary}', '{CNIC}', '1')";

            //open connection
            if (this.OpenConnection() == true)
            {
                try
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Execute command
                    cmd.ExecuteNonQuery();
                    //close connection
                    this.CloseConnection();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        public List<string> AdminInformation(string ID)
        {
            string query = $"SELECT * FROM admins WHERE `special_id` = '{ID}' and hospital_id = 1";
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
                    list.Add(dataReader["name"].ToString());
                    list.Add(dataReader["special_id"].ToString());
                    list.Add(dataReader["email"].ToString());
                    list.Add(dataReader["phone_number"].ToString());
                    list.Add(dataReader["salary"].ToString());
                    list.Add(dataReader["age"].ToString());
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

        public List<string> ReceptionistInformation(string ID)
        {
            string query = $"SELECT * FROM receptionist WHERE `staffid` = '{ID}' and hospital_id = 1";
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
                    list.Add(dataReader["staffid"].ToString());
                    list.Add(dataReader["email"].ToString());
                    list.Add(dataReader["phone_number"].ToString());
                    list.Add(dataReader["salary"].ToString());
                    list.Add(dataReader["age"].ToString());
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

        internal void UpdatePharmacy(string text1, string special_id, string text2, int v, string text3, string email)
        {
            string query = $"UPDATE pharmacist " +
                $"SET phone_number = '{text2}'," +
                $"salary = '{text3}'," +
                $"first_name = '{text1}'," +
                $"age = '{v}', " +
                $"email = '{email}' " +
                $"WHERE staffid = '{special_id}' and hospital_id = 1";
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
                MessageBox.Show("Information successfully updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Information unsuccessfully updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        internal List<string> PHINFO(string special_id)
        {
            string query = $"SELECT * FROM pharmacist WHERE `staffid` = '{special_id}' and hospital_id = 1";
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
                    list.Add(dataReader["staffid"].ToString());
                    list.Add(dataReader["email"].ToString());
                    list.Add(dataReader["phone_number"].ToString());
                    list.Add(dataReader["salary"].ToString());
                    list.Add(dataReader["age"].ToString());
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

        public void UpdateAdminInformation(string firstname, string id,string phonenumber, int age, string salary)
        {
            string query = $"UPDATE admins " +
                $"SET phone_number = '{phonenumber}'," +
                $"salary = '{salary}'," +
                $"name = '{firstname}'," +
                $"age = '{age}' " +
                $"WHERE special_id = '{id}' and hospital_id = 1";
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
                MessageBox.Show("Information successfully updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Information unsuccessfully updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void UpdateReceptionistInformation(string id, string phonenumber, string email)
        {
            string query = $"UPDATE receptionist " +
                $"SET phone_number = '{phonenumber}'," +
                $" email = '{email}' " +
                $"WHERE staffid = '{id}' and hospital_id = 1";
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
                MessageBox.Show("Information successfully updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Information unsuccessfully updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public List<string> findallPHID()
        {
            string query = "SELECT StaffID FROM pharmacist where hospital_id = 1";
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
                    list.Add(dataReader["StaffID"].ToString());
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

        public string findLastAdminID()
        {
            string query = "SELECT special_id FROM admins where hospital_id = 1 order by special_id desc limit 1";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                dataReader.Read();
                  string id = dataReader["special_id"].ToString();

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return id;
            }
            else
            {
                return "A100";
            }
        }

        public List<string> findall_D_ID()
        {
            string query = "SELECT staffid FROM doctors where hospital_id = 1";
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
                    list.Add(dataReader["staffid"].ToString());
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

        internal void updatedoctorinformation(string special_id, string text1, string text2, string text3, string text4, string text5, string text6, string text7, string text8)
        {
            string query = $"UPDATE doctors " +
              $"SET first_name = '{text1}'," +
              $"last_name = '{text2}'," +
              $"age = '{int.Parse(text3)}'," +
              $"phone_number = '{text4}', " +
              $"email = '{text5}', " +
              $"line = '{text6}', " +
              $"city = '{text7}', " +
              $"CNIC = '{text8}' " +
              $"WHERE staffid = '{special_id}' and hospital_id = 1";
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
                MessageBox.Show("Information successfully updated", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Information unsuccessfully updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        internal List<string> doctorINFO(string special_id)
        {
            string query = $"SELECT * FROM doctors WHERE `staffid` = '{special_id}' and hospital_id = 1";
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
                    list.Add(dataReader["staffid"].ToString());
                    list.Add(dataReader["first_name"].ToString());
                    list.Add(dataReader["last_name"].ToString());
                    list.Add(dataReader["age"].ToString());
                    list.Add(dataReader["phone_number"].ToString());
                    list.Add(dataReader["email"].ToString());
                    list.Add(dataReader["line"].ToString());
                    list.Add(dataReader["city"].ToString());
                    list.Add(dataReader["CNIC"].ToString());
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

        public List<string> findall_R_ID()
        {
            string query = "SELECT staffid FROM receptionist where hospital_id = 1";
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
                    list.Add(dataReader["staffid"].ToString());
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
    }
}
