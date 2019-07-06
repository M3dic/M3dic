using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3dic
{
    class medicines
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public medicines()
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

        public void addmedicine(string name, string description, string price, string quantity, string mgf, string expiry, string manufacturer)
        {
            string query = $"INSERT INTO medicines (name, description, price, quantity, mgf, expiry, manufacturer, hospital_id)" +
                $" VALUES('{name}', '{description}', '{price}','{quantity}', '{mgf}', '{expiry}', '{manufacturer}', '1')";
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
            else
            {
                MessageBox.Show("Connection lost", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal void updatemed(string text1, string text2, string text3, string text4, string text5, string text6)
        {
            string query = $"UPDATE medicines SET price='{text2}', quantity = '{text3}', mgf = '{text4}', expiry = '{text5}', manufacturer = '{text6}' WHERE name='{text1}' and hospital_id = 1";

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
                MessageBox.Show("Medicine successfully uppdated", "Updated med", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        internal void deleteMED(string text)
        {
            string query = $"DELETE FROM medicines WHERE name='{text}' and hospital_id = 1";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
