using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3dic
{
    class PlaceAppointmentmysql
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public PlaceAppointmentmysql()
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

        public List<string> FindDoctorsNAMEbyqualification(string qualification)
        {
            string query = $"SELECT first_name,last_name FROM doctors where qualification = '{qualification}' and hospital_id = 1";
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
                    string NAME = dataReader["first_name"].ToString() + " " + dataReader["last_name"].ToString();
                    list.Add(NAME);
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
        public List<string> FindDoctorsQualifications()
        {
            string query = "SELECT qualification FROM doctors where hospital_id = 1";
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
                    string NAME = dataReader["qualification"].ToString();
                    list.Add(NAME);
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

        public List<string> FindAllPatintsID()
        {
            string query = "SELECT sequre_number FROM register where placed = 'false' and hospital_id = 1";
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
                    string NAME = dataReader["sequre_number"].ToString();
                    list.Add(NAME);
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

        public string findpatientsinformation(string id)
        {
            string query = $"SELECT first_name FROM register where sequre_number = '{id}' and hospital_id = 1";
            string name = null;
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    name = dataReader["first_name"].ToString();
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return name;
            }
            else
            {
                return name;
            }
        }

        public List<string> findDoctorInformation(string first_name)
        {
            string query = $"SELECT staffid,phone_number,timings FROM doctors where concat(first_name,' ',last_name) = '{first_name}' and hospital_id = 1";
            List<string> doctor = new List<string>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    doctor.Add(dataReader["staffid"].ToString());
                    doctor.Add(dataReader["phone_number"].ToString());
                    doctor.Add(dataReader["timings"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return doctor;
            }
            else
            {
                return doctor;
            }
        }

        public List<string> FindFloors()
        {
            string query = $"SELECT floor_number as floor FROM M3dic.floors where hospital_id = 1 group by floor_number";
            List<string> floors = new List<string>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    floors.Add(dataReader["floor"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return floors;
            }
            else
            {
                return floors;
            }
        }

        public List<string> FindRooms(int floor)
        {
            string query = $"SELECT room_number as room FROM M3dic.floors where floor_number = {floor} and hospital_id = 1;";
            List<string> floors = new List<string>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    floors.Add(dataReader["room"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return floors;
            }
            else
            {
                return floors;
            }
        }

        public string FindRoomFee(int floor, int room)
        {
            string query = $"SELECT concat(room_type, ' - ', room_fee) as roomfee FROM M3dic.floors where room_number = {room} and floor_number = {floor} and hospital_id = 1;";
            string fee = null;
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    fee = dataReader["roomfee"].ToString();
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return fee;
            }
            else
            {
                return fee;
            }
        }

        public List<string> freeBeds(int floor, int room)
        {
            string query = $"SELECT bed_number FROM M3dic.beds where isfree = 'true' and floor_n = {floor} and room_n = {room} and hospital_id = 1;";
            List<string> beds = new List<string>();
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    beds.Add(dataReader["bed_number"].ToString());
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return beds;
            }
            else
            {
                return beds;
            }
        }

        public void AppointPacient(string id, string patientname, string doctorname, int floor, int room, string bed, string date, string time, string fee)
        {
            string query = $"INSERT INTO patients_appointment (patient_id, patient_name, doctor_name, floor_placed, room_placed, bed_number, appointment_data, appointment_time, appontment_fee, hospital_id)" +
               $" VALUES('{id}', '{patientname}', '{doctorname}','{floor}', '{room}', '{bed}', '{date}', '{time}', '{fee}', 1)";

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

        public void changeBedfree(string bed, int floor, int room)
        {
            string query = $"UPDATE `beds` SET `isfree` = 'false' WHERE `room_n` = {room} and `floor_n` = {floor} and `bed_number` = '{bed}' and hospital_id = 1; ";
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

        public void changepatientPLaced(string id)
        {
            string query = $"UPDATE register SET placed = 'true' WHERE `sequre_number` = '{id}' and hospital_id = 1; ";
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
