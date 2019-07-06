using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3dic
{
    class MedicinesINFOGIVE
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public MedicinesINFOGIVE()
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

        public List<string> takeMEDS()
        {
            string query = $"SELECT name FROM medicines order by name";
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

        public List<string> takeIDSNAMES()
        {
            string query = $"SELECT patient_id FROM patients_appointment where hospital_id = 1 order by patient_id desc";
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
                    list.Add(dataReader["patient_id"].ToString());
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

        internal string generatebillid()
        {
            string query = $"SELECT billid FROM M3dic.billreceipt where hospital_id = 1 order by billid desc limit 1";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                string billid = "B0";
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    billid = dataReader["billid"].ToString();
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return billid;
            }
            else
            {
                return "B0";
            }
        }

        public List<string> medINFO(string text)
        {
            string query = $"select name,price,quantity from M3dic.medicines where name = '{text}' and hospital_id = 1";
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
                    list.Add(dataReader["price"].ToString());
                    list.Add(dataReader["quantity"].ToString());
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

        internal void registerBILL(string text1, string text2, string text3, string text4, string appointment_fee, string v)
        {
            string query = $"INSERT INTO M3dic.billreceipt (billid, patientid,patientname,medicineexpense,hospitalexpense,total, hospital_id) VALUES('{text1}', '{text2}', '{text3}', '{text4}', '{appointment_fee}', '{v}', 1)";

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

        internal void INcreasequantity(string text, int quntit)
        {
            string query = $"UPDATE medicines SET quantity='{quntit}' WHERE name='{text}' and hospital_id = 1";

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

        internal List<string> patientINFO(string text)
        {
            string query = $"select patient_name,appontment_fee from M3dic.patients_appointment where patient_id = '{text}' and hospital_id = 1";
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
                    list.Add(dataReader["patient_name"].ToString());
                    list.Add(dataReader["appontment_fee"].ToString());
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

        internal List<string> medIDs()
        {
            string query = $"select billid from M3dic.billreceipt where amouuntpaid is null and hospital_id = 1";
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
                    list.Add(dataReader["billid"].ToString());
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

        internal List<string> GETINFOID(string text)
        {
            string query = $"select * from M3dic.billreceipt where billid = '{text}' and hospital_id = 1";
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
                    list.Add(dataReader["patientid"].ToString());
                    list.Add(dataReader["patientname"].ToString());
                    list.Add(dataReader["medicineexpense"].ToString());
                    list.Add(dataReader["hospitalexpense"].ToString());
                    list.Add(dataReader["total"].ToString());
                    list.Add(dataReader["discount"].ToString());
                    list.Add(dataReader["amounttopay"].ToString());
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

        internal void payBill(string text, string text1)
        {
            string query = $"UPDATE billreceipt SET amouuntpaid = '{text1}' WHERE billid='{text}' and hospital_id = 1";

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
    }
}
