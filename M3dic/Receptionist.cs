using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3dic
{
    public partial class Receptionist : Form
    {
        private string name;
        private string special_id;
        private string email;
        private string password;
        private Timer timer1 = new Timer();

        public Receptionist(string v1, string v2, string v3, string v4)
        {
            this.name = v1;
            this.special_id = v2;
            this.email = v3;
            this.password = v4;
            InitializeComponent();
            label1.Text = DateTime.Now.ToString();
            timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Interval = 1000;
            this.timer1.Enabled = true;
            this.label4.Text = this.name;
            this.label3.Text = this.special_id;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString();
            textBox7.Text = DateTime.Today.ToString();
        }

        private void button9_Click_1(object sender, EventArgs e)//LogOut
        {
            DialogResult dialog = MessageBox.Show($"{this.name} do you want to logout", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                Form1 child = new Form1(); //create new isntance of form
                child.FormClosed += new FormClosedEventHandler(child_FormClosed); //add handler to catch when child form is closed
                child.Show(); //show child
                this.Hide(); //hide parent            
            }
        }
        private void child_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clera();
            Random rand = new Random();
            Register_LoginMYSQL newRegister = new Register_LoginMYSQL();
            int num = rand.Next(1000, 9999);
            while (newRegister.FindID().Contains(num))
                num = rand.Next(1000, 9999);
            textBox3.Text = "P" + num;
            textBox6.Text = textBox3.Text;
            panel3.Visible = true;
        }
        private void Clera()
        {
            panel7.Visible = false;
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            panel6.Visible = false;
            panel5.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            textBox8.Clear();
            textBox9.Clear();
            textBox13.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox22.Clear();
            textBox20.Clear();
            textBox14.Clear();
            textBox2.Clear();
            textBox17.Clear();
            textBox18.Clear();
            textBox16.Clear();
            textBox15.Clear();
            comboBox3.Items.Clear();
            comboBox3.Items.Add("Select Doctor");
            comboBox3.Text = "Select Doctor";
            comboBox3.Enabled = false;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Doctor Qualification");
            comboBox2.Text = "Doctor Qualification";
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Patient ID");
            comboBox1.Text = "Patient ID";
            comboBox6.Text = "AM";
            comboBox5.Text = "AM";
            comboBox4.Items.Clear();
            comboBox4.Items.Add("Select Floor");
            comboBox4.Text = "Select Floor";
            comboBox7.Items.Clear();
            comboBox7.Items.Add("Select Room");
            comboBox7.Text = "Select Room";
            comboBox8.Items.Clear();
            comboBox8.Items.Add("Select Bed");
            comboBox8.Text = "Select Bed";
            comboBox7.Enabled = false;
            comboBox8.Enabled = false;
            checkBox1.Checked = false;
            comboBox4.Enabled = false;
            comboBox9.Items.Clear();
            textBox36.Text = null;
            textBox33.Text = null;
            textBox31.Text = null;
            textBox21.Text = null;
            textBox34.Text = null;
            textBox32.Text = null;
            textBox25.Text = "- %";
        }
        private bool isValidInput()
        {
            try
            {
                if (textBox4.Text != null)
                    if (textBox5.Text != null)
                        if (textBox6.Text != null)
                            if (textBox1.Text != null)
                                if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
                                    if (int.Parse(textBox8.Text) > 0)
                                        return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!isValidInput())
            {
                MessageBox.Show("Please enter correct input", "Incorrect input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Register_LoginMYSQL newRegister = new Register_LoginMYSQL();
                string gender = null;
                if (textBox10.Text != "")
                    if (checkemail(textBox10.Text))
                        send_messaga(textBox10.Text, textBox3.Text, textBox9.Text);

                if (radioButton1.Checked) gender = radioButton1.Text;
                else if (radioButton2.Checked) gender = radioButton2.Text;
                else if (radioButton3.Checked) gender = radioButton3.Text;

                newRegister.Insert(textBox4.Text, textBox5.Text, textBox6.Text, gender, int.Parse(textBox8.Text), textBox9.Text + " " + textBox13.Text,
                    textBox7.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox3.Text, textBox1.Text);

                MessageBox.Show("Registration as patient, ready!", "Registration ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panel3.Visible = false;
                Clera();
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter correct input", "Incorrect input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }//ok

        private bool checkemail(string text)
        {
            Register_LoginMYSQL newRegister = new Register_LoginMYSQL();
            if (!IsValidEmail(text))
            {
                MessageBox.Show("Please enter valid email and check the notes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            if (newRegister.SearchEmails().Contains(text))
            {
                List<string> ll = newRegister.FindPassword().Find(x => x[2] == text);
                string sequre_number = ll[0];
                string password = ll[1];
                resend_email(text, sequre_number, password);
                return true;
            }
            return false;
        }//ok

        private void resend_email(string text, string sequre_number, string password)
        {
            try
            {

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("hospitalm3dic@gmail.com", "08857490200");

                MailMessage mm = new MailMessage("donotreply@domain.com", $"{text}", "You have already account", $"\nUser: {sequre_number} \nPassword: {password}");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);

                MessageBox.Show("Already have account check email for Id and password", "Email sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                //MessageBox.Show("Please correct email or contact support!", "Email error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void send_messaga(string email, string username, string password)
        {
            try
            {

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("hospitalm3dic@gmail.com", "08857490200");

                MailMessage mm = new MailMessage("donotreply@domain.com", $"{email}", "Register competed!", $"You have been registered successfully! \nUser: {username} \nPassword: {password}");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);

                MessageBox.Show("Check email for Id and password", "Email sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Please correct email or contact support!", "Email error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            Clera();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clera();
            panel2.Visible = true;
            dateTimePicker1.MinDate = DateTime.Today;
            PlaceAppointmentmysql doctorsqualifications = new PlaceAppointmentmysql();
            List<string> qualifications = doctorsqualifications.FindDoctorsQualifications();
            List<string> patientsID = doctorsqualifications.FindAllPatintsID();
            List<string> floors = doctorsqualifications.FindFloors();//???
            foreach (var item in qualifications)
            {
                comboBox2.Items.Add(item);
            }
            foreach (var item in patientsID)
            {
                comboBox1.Items.Add(item);
            }
            foreach (var item in floors)
            {
                comboBox4.Items.Add(item);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Clera();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            textBox20.Clear();
            textBox14.Clear();
            textBox2.Clear();
            comboBox3.Items.Add("Select Doctor");
            comboBox3.Text = "Select Doctor";
            if (comboBox2.Text != "Doctor Qualification")
            {
                PlaceAppointmentmysql DoctorsNames = new PlaceAppointmentmysql();
                List<string> names = DoctorsNames.FindDoctorsNAMEbyqualification(comboBox2.Text);
                foreach (var item in names)
                {
                    comboBox3.Items.Add(item.ToString());
                    comboBox3.Enabled = true;
                }
            }
            else
                comboBox3.Enabled = false;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox20.Clear();
            textBox14.Clear();
            textBox2.Clear();
            if (comboBox3.Text != "Select Doctor")
            {
                PlaceAppointmentmysql doctorinfo = new PlaceAppointmentmysql();
                List<string> doctor = doctorinfo.findDoctorInformation(comboBox3.Text);
                try
                {
                    textBox20.Text = doctor[0];
                    textBox14.Text = doctor[1];
                    textBox2.Text = doctor[2];
                }
                catch (Exception)
                {
                    textBox20.Clear();
                    textBox14.Clear();
                    textBox2.Clear();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox22.Clear();
            if (comboBox1.Text != "Patient ID")
            {
                PlaceAppointmentmysql name = new PlaceAppointmentmysql();
                textBox22.Text = name.findpatientsinformation(comboBox1.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clera();
            panel5.Visible = true;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select sequre_number as 'Special ID', first_name as 'First Name', last_name as 'Last Name',CNIC,Gender , phone_number as 'Phone', date,placed,address,city  from M3dic.register", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView1.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text != "Select Floor")
            {
                comboBox7.Items.Clear();
                comboBox7.Items.Add("Select Room");
                comboBox7.Text = "Select Room";
                comboBox7.Enabled = true;
                PlaceAppointmentmysql findroom = new PlaceAppointmentmysql();
                List<string> rooms = findroom.FindRooms(int.Parse(comboBox4.Text));
                foreach (var item in rooms)
                {
                    comboBox7.Items.Add(item);
                }
            }
            else
            {
                comboBox7.Items.Clear();
                comboBox7.Items.Add("Select Room");
                comboBox7.Text = "Select Room";
                comboBox8.Items.Clear();
                comboBox8.Items.Add("Select Bed");
                comboBox8.Text = "Select Bed";
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.Text != "Select Room")
            {
                comboBox8.Items.Clear();
                comboBox8.Items.Add("Select Bed");
                comboBox8.Text = "Select Bed";
                comboBox8.Enabled = true;
                PlaceAppointmentmysql roomfee = new PlaceAppointmentmysql();
                textBox15.Text = roomfee.FindRoomFee(int.Parse(comboBox4.Text), int.Parse(comboBox7.Text));
                List<string> beds = roomfee.freeBeds(int.Parse(comboBox4.Text), int.Parse(comboBox7.Text));
                if (beds == null)
                {
                    comboBox8.Text = "No free beds";
                    comboBox8.Enabled = false;
                }
                else
                {
                    foreach (var item in beds)
                    {
                        comboBox8.Items.Add(item);
                    }
                }
            }
            else
            {
                comboBox8.Items.Clear();
                comboBox8.Items.Add("Select Bed");
                comboBox8.Text = "Select Bed";
                comboBox8.Enabled = false;
                textBox15.Text = null;
            }
        }

        private void button12_Click(object sender, EventArgs e)//appointBTN
        {
            if (checkinput())
            {
                if (checkBox1.Checked)
                {
                    try
                    {
                        PlaceAppointmentmysql appointPatient = new PlaceAppointmentmysql();
                        appointPatient.AppointPacient(comboBox1.Text, textBox22.Text, comboBox3.Text, int.Parse(comboBox4.Text), int.Parse(comboBox7.Text), comboBox8.Text, dateTimePicker1.Text, textBox17.Text + comboBox6.Text + " - " + textBox18.Text + comboBox5.Text, textBox16.Text);
                        MessageBox.Show($"Successfully added patient - {comboBox1.Text} : {textBox22.Text} to appointment list", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        appointPatient.changeBedfree(comboBox8.Text, int.Parse(comboBox4.Text), int.Parse(comboBox7.Text));
                        appointPatient.changepatientPLaced(comboBox1.Text);
                        Clera();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Please enter valid details", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    PlaceAppointmentmysql appointPatient = new PlaceAppointmentmysql();
                    appointPatient.AppointPacient(comboBox1.Text, textBox22.Text, comboBox3.Text, 0, 0, null, dateTimePicker1.Text, textBox17.Text + comboBox6.Text + " - " + textBox18.Text + comboBox5.Text, textBox16.Text);
                    MessageBox.Show($"Successfully added patient - {comboBox1.Text} : {textBox22.Text} to appointment list", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    appointPatient.changepatientPLaced(comboBox1.Text);
                    Clera();
                }
            }
            else
                MessageBox.Show("Please enter valid input", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool checkinput()
        {
            if (comboBox1.Text != "Patient ID")
                if (comboBox2.Text != "Doctor Qualification")
                    if (comboBox3.Text != "Select Doctor")
                        if (textBox17.Text != null)
                            if (textBox18.Text != null)
                                return true;
            return false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Clera();
            panel4.Visible = true;
            AddStuff receptionistInformaion = new AddStuff();
            List<string> ReceptINFO = receptionistInformaion.ReceptionistInformation(this.special_id);
            textBox30.Text = ReceptINFO[1];
            if (textBox30.Text != "") textBox30.Enabled = false;
            textBox29.Text = ReceptINFO[0];
            textBox26.Text = ReceptINFO[5];
            textBox19.Text = ReceptINFO[3];
            textBox23.Text = ReceptINFO[2];
            if (textBox23.Text != "") textBox23.Enabled = false;
            textBox18.Text = ReceptINFO[4];
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AddStuff updateReceptionistInformation = new AddStuff();
            updateReceptionistInformation.UpdateReceptionistInformation(this.special_id, textBox19.Text, textBox23.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Clera();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Clera();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select sequre_number as 'Special ID', first_name as 'First Name', last_name as 'Last Name',CNIC,Gender , phone_number as 'Phone', date,placed,address,city  from M3dic.register", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView1.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select patient_id as 'Special ID', patient_name as 'Name',doctor_name as 'Doctor',floor_placed as 'floor',room_placed as 'room',bed_number as 'Bed',appointment_data as 'data',appointment_time as 'time',appontment_fee as 'fee' from M3dic.patients_appointment order by patient_id", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView1.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clera();
            panel6.Visible = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Clera();
        }
        int floor = 0;
        private void button17_Click(object sender, EventArgs e)
        {
            floor = 1;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select room_number as 'Room Number',room_type as 'type', room_fee as 'Room fee' from M3dic.floors where floor_number = 1", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView3.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string query1 = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase1 = new MySqlConnection(query1);
            MySqlCommand cmdDataBase1 = new MySqlCommand("select room_n as 'Room number', bed_number as 'Bed number', isfree as 'free' from M3dic.beds as b where b.floor_n = 1", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase1;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView2.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//floor1

        private void button24_Click(object sender, EventArgs e)
        {
            floor = 2;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select room_number as 'Number of rooms',room_type as 'type', room_fee as 'Room fee' from M3dic.floors where floor_number = 2", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView3.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string query1 = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase1 = new MySqlConnection(query1);
            MySqlCommand cmdDataBase1 = new MySqlCommand("select room_n as 'Room number', bed_number as 'Bed number', isfree as 'free' from M3dic.beds as b where b.floor_n = 2", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase1;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView2.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//floor2

        private void button25_Click(object sender, EventArgs e)
        {
            floor = 3;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select room_number as 'Number of rooms',room_type as 'type', room_fee as 'Room fee' from M3dic.floors where floor_number = 3", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView3.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string query1 = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase1 = new MySqlConnection(query1);
            MySqlCommand cmdDataBase1 = new MySqlCommand("select room_n as 'Room number', bed_number as 'Bed number', isfree as 'free' from M3dic.beds as b where b.floor_n = 3", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase1;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView2.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//floor3

        private void button27_Click(object sender, EventArgs e)
        {
            floor = 4;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select room_number as 'Number of rooms',room_type as 'type', room_fee as 'Room fee' from M3dic.floors where floor_number = 4", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView3.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string query1 = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase1 = new MySqlConnection(query1);
            MySqlCommand cmdDataBase1 = new MySqlCommand("select room_n as 'Room number', bed_number as 'Bed number', isfree as 'free' from M3dic.beds as b where b.floor_n = 4", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase1;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView2.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//floor4

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int room = 0;
            foreach (DataGridViewRow row in dataGridView3.SelectedRows)
            {
                room = int.Parse(row.Cells[0].Value.ToString());
            }
            string query1 = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase1 = new MySqlConnection(query1);
            MySqlCommand cmdDataBase1 = new MySqlCommand($"select bed_number as 'Bed number', isfree as 'free' from M3dic.beds as b where b.floor_n = {floor} and b.room_n = {room}", conDataBase1);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase1;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView2.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Clera();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clera();
            panel7.Visible = true;
            MedicinesINFOGIVE meds = new MedicinesINFOGIVE();
            List<string> ids = meds.medIDs();
            foreach (var item in ids)
            {
                comboBox9.Items.Add(item);
            }

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (checkbill())
            {
                MedicinesINFOGIVE deletebill = new MedicinesINFOGIVE();
                deletebill.payBill(comboBox9.Text, textBox32.Text);
                MessageBox.Show($"Successfully paid Bill {comboBox9.Text}", "Successfully paid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clera();
            }
            else
                MessageBox.Show("Please select bill and write amount paid", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private bool checkbill()
        {
            if (comboBox9.Text != "")
                if (textBox32.Text != "")
                    return true;
            return false;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                comboBox4.Enabled = true;
            }
            else
            {
                comboBox4.Text = "Select Floor";
                comboBox7.Items.Clear();
                comboBox7.Items.Add("Select Room");
                comboBox7.Text = "Select Room";
                comboBox8.Items.Clear();
                comboBox8.Items.Add("Select Bed");
                comboBox8.Text = "Select Bed";
                comboBox4.Enabled = false;
                comboBox7.Enabled = false;
                textBox15.Clear();
                comboBox8.Enabled = false;
            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox9.Text != null)
            {
                MedicinesINFOGIVE allInfo = new MedicinesINFOGIVE();
                List<string> info = allInfo.GETINFOID(comboBox9.Text);
                textBox36.Text = info[0];
                textBox33.Text = info[1];
                textBox31.Text = info[2];
                textBox28.Text = info[3];
                textBox21.Text = info[4];
                textBox25.Text = info[5];
                if (textBox25.Text == "")
                    textBox25.Text = "- %";
                textBox34.Text = textBox28.Text;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
