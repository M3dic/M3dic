using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3dic
{
    class hospitalwards
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public hospitalwards()
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

        internal string numberofbeds(int floor, int room)
        {
            string query = $"SELECT Count(bed_number) as beds FROM M3dic.beds where room_n = {room} and floor_n = {floor} and hospital_id = 1;";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                string s = null;
                while (dataReader.Read())
                {
                    s = dataReader["beds"].ToString();
                }
                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();
                //return list to be displayed
                return s;
            }
            return null;
        }

        internal bool searchfloor(string text)
        {
            string query = $"SELECT floor_number FROM M3dic.floors where floor_number = {text} and hospital_id = 1";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                bool s = false;
                s = dataReader.Read();
                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();
                //return list to be displayed
                return s;
            }
            return false;
        }

        internal bool searchroom(string text)
        {
            string query = $"SELECT room_number FROM M3dic.floors where room_number = {text} and hospital_id = 1";
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                bool s = false;
                s = dataReader.Read();
                //close Data Reader
                dataReader.Close();
                //close Connection
                this.CloseConnection();
                //return list to be displayed
                return s;
            }
            return false;
        }

        private string takelastbed(int room, int floor)
        {
            string query = $"SELECT bed_number as l FROM M3dic.beds where room_n = {room} and floor_n = {floor} and hospital_id = 1 order by bed_number desc limit 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    string s = null;
                    try
                    {
                        dataReader.Read();
                        s = dataReader["l"].ToString();
                    }
                    catch (Exception)
                    {

                    }
                    //close Data Reader
                    dataReader.Close();
                    //close Connection
                    this.CloseConnection();
                    //return list to be displayed
                    if (s == null)
                        s = "B100";
                    else s = "B" + (int.Parse((s.Remove(0, 1))) + 1).ToString();
                    return s;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CloseConnection();
                    return null;
                }
            }
            return null;
        }
        internal void addBED(int floor, int room)
        {
            string bednumber = takelastbed(room, floor);
            string query = $"insert into beds (room_n, bed_number, isfree, floor_n, hospital_id) values ('{room}','{bednumber}','true', '{floor}', 1)";
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

        internal void addWard(int room, string room_type, string room_fee, int floor)
        {
            string query = $"insert into floors (room_number, room_type, room_fee, floor_number, hospital_id) values ('{room}','{room_type}','{room_fee}','{floor}', 1)";
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
                    MessageBox.Show("Please take a look at the input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void WardUpdate(int room, string room_type, string room_fee, int floor)
        {
            string query = $"update floors set room_type = '{room_type}', room_fee = '{room_fee}' where room_number = {room} and floor_number = {floor} and hospital_id = 1;";
            if (this.OpenConnection() == true)
            {
                try
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
                catch (Exception)
                {
                    MessageBox.Show("Please check right the input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal string countbeds(string room, string floor)
        {
            string query = $"SELECT count(bed_number) as l FROM M3dic.beds where room_n = {room} and floor_n = {floor} and hospital_id = 1";
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    string s = null;
                    try
                    {
                        dataReader.Read();
                        s = dataReader["l"].ToString();
                    }
                    catch (Exception)
                    {
                        s = "0";
                    }
                    //close Data Reader
                    dataReader.Close();
                    //close Connection
                    this.CloseConnection();
                    return s;
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.CloseConnection();
                    return null;
                }
            }
            return null;
        }

        internal bool checkRom(string text, string text1)
        {
            string query = $"SELECT room_number FROM M3dic.floors where room_number = {text} and floor_number = {text1} and hospital_id = 1;";
            if (this.OpenConnection() == true)
            {
                try
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    //Read the data and store them in the list
                    bool s = false;
                    s = dataReader.Read();
                    //close Data Reader
                    dataReader.Close();
                    //close Connection
                    this.CloseConnection();
                    return s;
                }
                catch (Exception)
                {
                    this.CloseConnection();
                    return false;
                }
            }
            return false;
        }

        internal List<string> countfloors()
        {
            string query = $"SELECT floor_number FROM M3dic.floors where hospital_id = 1 group by floor_number;";
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
                    list.Add(dataReader["floor_number"].ToString());
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

        internal List<string> countrooms(string floor)
        {
            string query = $"SELECT room_number FROM M3dic.floors where floor_number = {floor} and hospital_id = 1;";
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
                    list.Add(dataReader["room_number"].ToString());
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
