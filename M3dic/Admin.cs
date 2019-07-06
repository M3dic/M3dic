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
    public partial class Admin : Form
    {
        string name { get; set; }
        string special_id { get; set; }
        string email { get; set; }
        string password { get; set; }
        Timer timer1 = new Timer();
        private string beds;

        public Admin(string n, string spec_index, string email, string pass)
        {
            this.name = n;
            this.special_id = spec_index;
            this.email = email;
            this.password = pass;
            InitializeComponent();
            label1.Text = DateTime.Now.ToString();
            timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Interval = 1000;
            this.timer1.Enabled = true;
            this.label4.Text = this.name;
            this.label3.Text = this.special_id;
            hide_all_panels();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString();
        }

        private void button9_Click(object sender, EventArgs e)//LogOut Button
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

        private void button8_Click(object sender, EventArgs e)
        {
            hidepanels();
            panel2.Visible = true;
            comboBox1.Text = "Select Type";
            restorNewStuffInformation();
            textBox9.Text = DateTime.Today.ToString();
        }//AddNewStuff panel2


        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Admin")
            {
                try
                {
                    AddStuff newAdmin = new AddStuff();
                    newAdmin.AddAdmin(textBox3.Text, textBox5.Text, textBox2.Text, textBox8.Text + textBox16.Text, textBox10.Text, textBox15.Text, int.Parse(textBox7.Text), textBox20.Text);
                    if (checkemail(textBox10.Text))
                        sendmessage(textBox10.Text, textBox2.Text, textBox5.Text);
                    MessageBox.Show($"Successfully registered {textBox2.Text}!", "Register completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1.Text = "Select Type";
                    restorNewStuffInformation();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBox1.Text == "Pharmacist")
            {
                try
                {
                    AddStuff newPH = new AddStuff();
                    string gender = null;
                    if (radioButton1.Checked) gender = radioButton1.Text;
                    else if (radioButton2.Checked) gender = radioButton2.Text;
                    else gender = radioButton3.Text;
                    string timing = textBox13.Text + comboBox4.Text + " " + textBox14.Text + comboBox3.Text;
                    newPH.AddPharmacist(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, gender, int.Parse(textBox7.Text), textBox8.Text + textBox16.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, timing, textBox15.Text, textBox20.Text);
                    if (checkemail(textBox10.Text))
                        sendmessage(textBox10.Text, textBox2.Text, textBox5.Text);
                    MessageBox.Show($"Successfully registered {textBox2.Text}!", "Register completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    restorNewStuffInformation();
                    comboBox1.Text = "Select Type";
                }
                catch (Exception)
                {
                    MessageBox.Show("Please check the values", "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBox1.Text == "Doctor")
            {

                try
                {
                    AddStuff newDoctor = new AddStuff();
                    string gender = null;
                    if (radioButton1.Checked) gender = radioButton1.Text;
                    else if (radioButton2.Checked) gender = radioButton2.Text;
                    else gender = radioButton3.Text;
                    string timing = textBox13.Text + comboBox4.Text + " - " + textBox14.Text + comboBox3.Text;
                    newDoctor.AddDoctor(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, gender, int.Parse(textBox7.Text), textBox8.Text + textBox16.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox1.Text, timing, textBox15.Text, textBox20.Text);
                    if (checkemail(textBox10.Text))
                        sendmessage(textBox10.Text, textBox2.Text, textBox5.Text);
                    MessageBox.Show($"Successfully registered {textBox2.Text}!", "Register completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    restorNewStuffInformation();
                    comboBox1.Text = "Select Type";
                }
                catch (Exception)
                {
                    MessageBox.Show("Please check the values", "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (comboBox1.Text == "Receptionist")
            {

                try
                {
                    AddStuff newreceptionist = new AddStuff();
                    string gender = null;
                    if (radioButton1.Checked) gender = radioButton1.Text;
                    else if (radioButton2.Checked) gender = radioButton2.Text;
                    else gender = radioButton3.Text;
                    string timing = textBox13.Text + comboBox4.Text + " - " + textBox14.Text + comboBox3.Text;
                    newreceptionist.Addreceptionist(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, gender, int.Parse(textBox7.Text), textBox8.Text + textBox16.Text, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text, textBox1.Text, timing, textBox15.Text, textBox20.Text);
                    if (checkemail(textBox10.Text))
                        sendmessage(textBox10.Text, textBox2.Text, textBox5.Text);
                    MessageBox.Show($"Successfully registered {textBox2.Text}!", "Register completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    restorNewStuffInformation();
                    comboBox1.Text = "Select Type";
                }
                catch (Exception)
                {
                    MessageBox.Show("Please check the values", "Wrong input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select Staff Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//storenewstuff

        private void sendmessage(string text, string sequre_number, string password)
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

                MailMessage mm = new MailMessage("donotreply@domain.com", $"{text}", "Successfully registered account", $"\nUser: {sequre_number} \nPassword: {password}");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);

                MessageBox.Show($"Check email for Id and password", "Email sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                restorNewStuffInformation();
                comboBox1.Text = "Select Type";
            }
            catch (Exception)
            {

            }
        }

        private bool checkemail(string text)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(text);
                return addr.Address == text;
            }
            catch
            {
                return false;
            }
        }

        public void hide_all_panels()
        {
            panel2.Visible = false;
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }//nothing

        private void button11_Click(object sender, EventArgs e)
        {
            hide_all_panels();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Select Type")
            {
                restorNewStuffInformation();
            }
            else if (comboBox1.Text == "Admin")
            {
                restorNewStuffInformation();
                textBox1.Enabled = false;
                textBox4.Enabled = false;
                label10.Enabled = false;
                label12.Enabled = false;
                label16.Enabled = false;
                label21.Enabled = false;
                label22.Enabled = false;
                label17.Enabled = false;
                label18.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox14.Enabled = false;
                comboBox4.Enabled = false;
                textBox13.Enabled = false;
                comboBox3.Enabled = false;
                AddStuff adminid = new AddStuff();
                string n = adminid.findLastAdminID().Remove(0, 1);
                textBox2.Text = "A" + (int.Parse(n) + 1);
                textBox5.Text = textBox2.Text;
            }
            else if (comboBox1.Text == "Pharmacist")
            {
                restorNewStuffInformation();
                AddStuff stffid = new AddStuff();
                List<string> vs = stffid.findallPHID();
                Random rand = new Random();
                do
                {
                    int s = rand.Next(100, 999);
                    textBox2.Text = "PH" + s;
                } while (vs.Contains(textBox2.Text));
                textBox5.Text = textBox2.Text;
            }
            else if (comboBox1.Text == "Doctor")
            {
                restorNewStuffInformation();
                AddStuff stffid = new AddStuff();
                List<string> vs = stffid.findall_D_ID();
                Random rand = new Random();
                do
                {
                    int s = rand.Next(100, 999);
                    textBox2.Text = "D" + s;
                } while (vs.Contains(textBox2.Text));
                textBox5.Text = textBox2.Text;

            }
            else if (comboBox1.Text == "Receptionist")
            {
                restorNewStuffInformation();
                AddStuff stffid = new AddStuff();
                List<string> vs = stffid.findall_R_ID();
                Random rand = new Random();
                do
                {
                    int s = rand.Next(100, 999);
                    textBox2.Text = "R" + s;
                } while (vs.Contains(textBox2.Text));
                textBox5.Text = textBox2.Text;

            }
        }

        private void button11_Click_1(object sender, EventArgs e)//backBTN
        {
            restorNewStuffInformation();
            hidepanels();
            comboBox1.Text = "Select Type";
        }
        public void restorNewStuffInformation()
        {

            comboBox1.Enabled = true;
            textBox2.Clear();
            textBox2.Enabled = true;
            textBox3.Clear();
            textBox3.Enabled = true;
            textBox4.Clear();
            textBox4.Enabled = true;
            textBox5.Clear();
            textBox5.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            textBox7.Clear();
            textBox7.Enabled = true;
            textBox8.Clear();
            textBox8.Enabled = true;
            textBox16.Clear();
            textBox16.Enabled = true;
            textBox10.Clear();
            textBox10.Enabled = true;
            textBox11.Clear();
            textBox11.Enabled = true;
            textBox12.Clear();
            textBox1.Clear();
            textBox12.Enabled = true;
            textBox1.Enabled = true;
            textBox14.Clear();
            textBox14.Enabled = true;
            textBox13.Clear();
            textBox13.Enabled = true;
            textBox15.Clear();
            textBox15.Enabled = true;
            label10.Enabled = true;
            label12.Enabled = true;
            label16.Enabled = true;
            label21.Enabled = true;
            label22.Enabled = true;
            label17.Enabled = true;
            label18.Enabled = true;
            label13.Enabled = true;
            textBox7.Enabled = true;
            comboBox3.Enabled = true;
            comboBox4.Enabled = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            textBox20.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            hidepanels();
            panel3.Visible = true;
            AddStuff admininformation = new AddStuff();
            List<string> AdminINFO = admininformation.AdminInformation(this.special_id);
            textBox30.Text = AdminINFO[1];
            if (textBox30.Text != "") textBox30.Enabled = false;
            textBox29.Text = AdminINFO[0];
            textBox26.Text = AdminINFO[5];
            textBox17.Text = AdminINFO[3];
            textBox23.Text = AdminINFO[2];
            if (textBox23.Text != "") textBox23.Enabled = false;
            textBox18.Text = AdminINFO[4];
        }
        public void hidepanels()
        {
            panel8.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel7.Visible = false;
            panel9.Visible = false;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            hidepanels();
        }

        private void button13_Click(object sender, EventArgs e)//update Admin information
        {
            AddStuff updateadmininformation = new AddStuff();
            updateadmininformation.UpdateAdminInformation(textBox29.Text, this.special_id, textBox17.Text, int.Parse(textBox26.Text), textBox18.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            hidepanels();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            hidepanels();
            panel4.Visible = true;

        }

        private void Admin_Load(object sender, EventArgs e)
        {
        }

        private void button14_Click(object sender, EventArgs e)//show Admins
        {
            label53.Text = null;
            label52.Text = null;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from M3dic.admins;", conDataBase);
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

        private void button16_Click(object sender, EventArgs e)//show Doctors
        {
            label53.Text = "doctors";
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from M3dic.doctors;", conDataBase);
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

        private void button17_Click(object sender, EventArgs e)//show pharmacists
        {
            label53.Text = "pharmacist";
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from M3dic.pharmacist;", conDataBase);
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

        private void button18_Click(object sender, EventArgs e)//show receptionist
        {
            label53.Text = "receptionist";
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from M3dic.receptionist;", conDataBase);
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

        private void button23_Click(object sender, EventArgs e)
        {
            //addwardclear();
            hidepanels();
        }

        private void button19_Click(object sender, EventArgs e)//addward
        {
            try
            {
                if (checkRoom(comboBox5.Text.Last().ToString(), comboBox2.Text.Last().ToString()))
                {
                    MessageBox.Show("You have already created this room!", "Room already created", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please select valid input", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int floornumber;
            int roomnumber;
            string roomtype;
            string roomfee;
            try
            {
                floornumber = int.Parse(comboBox2.Text.Last().ToString());
                roomnumber = int.Parse(comboBox5.Text.Last().ToString());
                roomtype = comboBox6.Text;
                roomfee = textBox6.Text;
                try
                {
                    hospitalwards addWard = new hospitalwards();
                    addWard.addWard(roomnumber, roomtype, roomfee, floornumber);
                    MessageBox.Show("Successfully added new ward", "Ward added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please take a look at the input!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please enter correct input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            comboBox9.Items.Clear();
            hospitalwards roomInfo = new hospitalwards();
            List<string> floors = roomInfo.countfloors();
            foreach (var item in floors)
            {
                comboBox9.Items.Add(item);
            }
            comboBox8.Enabled = false;
            comboBox8.Items.Clear();
        }

        private bool checkRoom(string text, string text1)
        {
            hospitalwards checkroom = new hospitalwards();
            return checkroom.checkRom(text, text1);
        }

        private void button5_Click(object sender, EventArgs e)//add new ward button
        {
            addwardclear();
            hidepanels();
            panel5.Visible = true;
            comboBox9.Items.Clear();
            comboBox8.Items.Clear();
            hospitalwards roomInfo = new hospitalwards();
            List<string> floors = roomInfo.countfloors();
            foreach (var item in floors)
            {
                comboBox9.Items.Add(item);
            }

        }
        private void addwardclear()
        {
            textBox6.Enabled = false;
            comboBox6.Enabled = false;
            comboBox5.Enabled = false;
            comboBox8.Enabled = false;
            comboBox9.Text = null;
            comboBox8.Text = null;
            comboBox2.Text = null;
            comboBox5.Text = null;
            comboBox6.Text = null;
            textBox6.Text = null;
            textBox22.Text = "0";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            hidepanels();
            panel7.Visible = true;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox6.Enabled = true;

        }

        private void button20_Click(object sender, EventArgs e)//update
        {
            try
            {
                if (validinput())
                {
                    hospitalwards updateWard = new hospitalwards();
                    updateWard.WardUpdate(int.Parse(comboBox5.Text.Last().ToString()), comboBox6.Text, textBox6.Text, int.Parse(comboBox2.Text.Last().ToString()));
                    MessageBox.Show("Room successfully updated!", "Room updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool validinput()
        {
            return comboBox2.Text != null && comboBox5.Text != null && comboBox6.Text != null && textBox6.Text != null;
        }

        private void button21_Click(object sender, EventArgs e)//addbed
        {
            try
            {
                if (floor(comboBox9.Text) && room(comboBox8.Text))
                {
                    hospitalwards addebd = new hospitalwards();
                    addebd.addBED(int.Parse(comboBox9.Text), int.Parse(comboBox8.Text));
                    textBox22.Text = addebd.countbeds(comboBox8.Text, comboBox9.Text);
                    MessageBox.Show($"Successfully added new bed in room {comboBox8.Text}", "Bed added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Please select valid floor and room", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception)
            {
                MessageBox.Show("Please select valid floor and room", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private bool room(string text)
        {
            hospitalwards room = new hospitalwards();
            return room.searchroom(text);
        }

        private bool floor(string text)
        {
            hospitalwards floor = new hospitalwards();
            return floor.searchfloor(text);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = true;
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.Text != "")
            {

                try
                {
                    hospitalwards hospitalwards1 = new hospitalwards();
                    beds = hospitalwards1.numberofbeds(int.Parse(comboBox9.Text), int.Parse(comboBox8.Text));
                }
                catch (Exception)
                {
                    beds = null;
                }
                if (beds == null)
                    textBox22.Text = "0";
                else
                    textBox22.Text = beds;

            }
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox8.Items.Clear();
            comboBox8.Enabled = true;
            comboBox8.Text = null;
            textBox22.Text = "0";
            hospitalwards roomInfo = new hospitalwards();
            List<string> rooms = roomInfo.countrooms(comboBox9.Text);
            foreach (var item in rooms)
            {
                comboBox8.Items.Add(item);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Enabled = true;
            textBox6.Text = null;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            hidepanels();
            dataGridView3.DataSource = null;
            dataGridView2.DataSource = null;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select room_number as 'Number of rooms',room_type as 'type', room_fee as 'Room fee' from M3dic.floors where floor_number = 1", conDataBase);
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

        private void button2_Click(object sender, EventArgs e)
        {
            hidepanels();
            panel8.Visible = true;
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
                dataGridView4.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            hidepanels();
        }

        private void button29_Click(object sender, EventArgs e)
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
                dataGridView4.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button28_Click(object sender, EventArgs e)
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
                dataGridView4.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            hidepanels();
            panel9.Visible = true;
            textBox21.Text = null;
            textBox25.Text = null;
            textBox28.Text = null;
            textBox32.Text = null;
            textBox24.Text = null;
            textBox27.Text = null;
            textBox31.Text = null;
            textBox33.Text = null;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select billid, patientid as 'Special ID', patientname as 'Name',medicineexpense ,hospitalexpense, discount,total ,amouuntpaid from M3dic.billreceipt order by patientname", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView5.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button35_Click(object sender, EventArgs e)
        {
            hidepanels();
            dataGridView5.DataSource = null;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select billid, patientid as 'Special ID', patientname as 'Name',medicineexpense ,hospitalexpense, discount,total ,amouuntpaid from M3dic.billreceipt where patientid = '{textBox19.Text}'", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bSorce = new BindingSource();
                bSorce.DataSource = dbdataset;
                dataGridView5.DataSource = bSorce;
                sda.Update(dbdataset);

            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (textBox21.Text != null)
            {
                Register_LoginMYSQL changeINFO = new Register_LoginMYSQL();
                changeINFO.updateinformation(textBox21.Text, textBox25.Text, textBox28.Text, textBox32.Text, textBox24.Text, textBox27.Text, textBox31.Text);
                MessageBox.Show("Information updated successfully", "Information Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select from the table down", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                dataGridView4.DataSource = bSorce;
                sda.Update(dbdataset);
            }
            catch (Exception)
            {
                MessageBox.Show("Database error, please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView5.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView5.CurrentRow.Selected = true;
                    textBox21.Text = dataGridView5.Rows[e.RowIndex].Cells["Special ID"].FormattedValue.ToString();
                }
                Register_LoginMYSQL infopatient = new Register_LoginMYSQL();
                List<string> infopatients = infopatient.patientINFO(textBox21.Text);
                textBox25.Text = infopatients[0];
                textBox28.Text = infopatients[1];
                textBox32.Text = infopatients[2];
                textBox24.Text = infopatients[3];
                textBox27.Text = infopatients[4];
                textBox31.Text = infopatients[5];
                textBox33.Text = infopatients[6];
            }
            catch (Exception)
            {

            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (label52.Text == "" || label53.Text == "")
            {
                MessageBox.Show("Please select staff different from Admin", "Error selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            doctorSQL removestaff = new doctorSQL();
            removestaff.removeDoctor(label52.Text, label53.Text);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;
                    label52.Text = dataGridView1.Rows[e.RowIndex].Cells["staffid"].FormattedValue.ToString();
                }
                else
                {
                    label52.Text = null;
                }
            }
            catch (Exception)
            {
                label52.Text = null;
            }

        }
    }
}
