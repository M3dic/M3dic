using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3dic
{
    class doctorSQL
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public doctorSQL()
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

        internal void UPdateDIAGNOSE(string text1, string text2)
        {
            string query = $"UPDATE patients_appointment SET dignose='{text2}' WHERE patient_id='{text1}' and hospital_id = 1";

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

        internal void UpdateTIME(string text1, string text2, string text3)
        {
            string query = $"UPDATE patients_appointment SET appointment_data='{text2}',appointment_time = '{text3}'  WHERE patient_id='{text1}' and hospital_id = 1";

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
                MessageBox.Show("Successfully updated appointment time", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        internal List<string> checkPRID(string text)
        {
            string query = $"SELECT * FROM prescription where patinet_id = '{text}' and hospital_id = 1";
            List<string> prescriptioninfo = new List<string>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    prescriptioninfo.Add(dataReader["prescription_ID"].ToString());
                    prescriptioninfo.Add(dataReader["prescription_date"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
                  
                //return list to be displayed
                return prescriptioninfo;
            }
            else
            {
                return null;
            }
        }

        internal string lastprid()
        {
            string query = "SELECT prescription_ID FROM prescription where hospital_id = 1 order by prescription_ID DESC limit 1;";
            string id = null;
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    id = dataReader["prescription_ID"].ToString().Remove(0, 2);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();
                if (id == null) id = "100";
                //return list to be displayed
                return "PR"+(int.Parse(id)+1).ToString();
            }
            else
            {
                return null;
            }
        }

        internal void generateprescription(string text1, string text2, string v, string special_id, string name, string text3)
        {
            string query = $"INSERT INTO prescription (prescription_ID, patinet_id, patient_name, doctor_ID, doctor_name, prescription_date, hospital_id) VALUES('{text1}', '{text2}', '{v}','{special_id}','{name}','{text3}',1)";

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

        internal void removePrescription(string text)
        {
            string query = $"DELETE FROM prescription WHERE prescription_ID='{text}' and hospital_id = 1";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        internal void removeDoctor(string text,string text1)
        {
            string query = $"DELETE FROM M3dic.{text1} WHERE staffid='{text}' and hospital_id = 1";
            try
            {
                if (this.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
                    MessageBox.Show("Successfully deleted","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error 44444", "Please contact support", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }
    }
}
