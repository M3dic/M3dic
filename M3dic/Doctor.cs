using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M3dic
{
    public partial class Doctor : Form
    {
        private string name;
        private string special_id;
        private string email;
        private string password;
        private string qualification;
        private Timer timer1 = new Timer();

        public Doctor(string v1, string v2, string v3, string v4, string v5)
        {
            this.name = v1;
            this.special_id = v2;
            this.email = v3;
            this.password = v4;
            this.qualification = v5;
            InitializeComponent();
            label1.Text = DateTime.Now.ToString();
            timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Interval = 1000;
            this.timer1.Enabled = true;
            this.label4.Text = this.name;
            this.label3.Text = this.special_id;
            this.label6.Text = this.qualification;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label1.Text = DateTime.Now.ToString();
        }

        private void button9_Click(object sender, EventArgs e)//logOUT
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

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            clear();
            panel6.Visible = true;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select prescription_ID, patinet_id, patient_name, prescription_date from M3dic.prescription where doctor_name = '{this.name}' order by patient_name", conDataBase);
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

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
            panel9.Visible = true;
            textBox21.Text = null;
            textBox25.Text = null;
            textBox28.Text = null;
            textBox32.Text = null;
            textBox24.Text = null;
            textBox27.Text = null;
            textBox31.Text = null;
            textBox33.Text = null;
            textBox19.Text = null;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select placed_appointment, patient_id as 'Special ID', patient_name as 'Name',dignose ,floor_placed, room_placed,bed_number ,appointment_data,appointment_time from M3dic.patients_appointment where doctor_name = '{this.name}' order by patient_name", conDataBase);
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
            clear();
        }

        private void clear()
        {
            panel9.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
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
            MySqlCommand cmdDataBase = new MySqlCommand($"select placed_appointment, patient_id as 'Special ID', patient_name as 'Name',dignose ,floor_placed, room_placed,bed_number ,appointment_data,appointment_time from M3dic.patients_appointment where doctor_name = '{this.name}' order by patient_name", conDataBase);
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

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void button30_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select placed_appointment, patient_id as 'Special ID', patient_name as 'Name',dignose ,floor_placed, room_placed,bed_number ,appointment_data,appointment_time from M3dic.patients_appointment where doctor_name = '{this.name}' and patient_id = '{textBox19.Text}'", conDataBase);
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

        private void button11_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clear();
            panel2.Visible = true;
            textBox8.Text = null;
            textBox6.Text = null;
            textBox4.Text = null;
            textBox2.Text = null;
            textBox7.Text = null;
            textBox5.Text = null;
            textBox3.Text = null;
            textBox1.Text = null;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select  patient_id as 'Special ID', patient_name as 'Name',floor_placed, room_placed,bed_number from M3dic.patients_appointment where doctor_name = '{this.name}' and floor_placed > 0 and curdate() < str_to_date(appointment_data, '%d/%m/%Y') order by patient_name", conDataBase);
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

        private void button10_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select patient_id as 'Special ID', patient_name as 'Name',dignose ,floor_placed, room_placed,bed_number ,appointment_data,appointment_time from M3dic.patients_appointment where doctor_name = '{this.name}' and patient_id = '{textBox9.Text}'", conDataBase);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView1.CurrentRow.Selected = true;
                    textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells["Special ID"].FormattedValue.ToString();
                }
                Register_LoginMYSQL infopatient = new Register_LoginMYSQL();
                List<string> infopatients = infopatient.patientINFO(textBox8.Text);
                textBox6.Text = infopatients[0];
                textBox4.Text = infopatients[1];
                textBox7.Text = infopatients[3];
                textBox5.Text = infopatients[4];
                textBox1.Text = infopatients[6];
            }
            catch (Exception)
            {

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            panel3.Visible = true;
            textBox17.Text = null;
            textBox15.Text = null;
            textBox13.Text = null;
            textBox11.Text = null;
            textBox16.Text = null;
            textBox14.Text = null;
            textBox12.Text = null;
            textBox10.Text = null;
            textBox20.Text = null;
            label27.Text = "*";
            label59.Text = this.special_id;
            label60.Text = this.name;
            label65.Text = "ID";
            label66.Text = "DATE";
            button13.Text = "GENERATE PRESCRIPTION";
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select patient_id as 'Special ID', patient_name as 'Name', dignose from M3dic.patients_appointment where doctor_name = '{this.name}' ", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView2.CurrentRow.Selected = true;
                    textBox17.Text = dataGridView2.Rows[e.RowIndex].Cells["Special ID"].FormattedValue.ToString();
                    textBox20.Text = dataGridView2.Rows[e.RowIndex].Cells["dignose"].FormattedValue.ToString();
                }
                label27.Text = textBox17.Text + " - Diagnose";
                Register_LoginMYSQL infopatient = new Register_LoginMYSQL();
                List<string> infopatients = infopatient.patientINFO(textBox17.Text);
                textBox15.Text = infopatients[0];
                textBox13.Text = infopatients[1];
                textBox11.Text = infopatients[2];
                textBox16.Text = infopatients[3];
                textBox14.Text = infopatients[4];
                textBox12.Text = infopatients[5];
                textBox10.Text = infopatients[6];
                doctorSQL checkforPRID = new doctorSQL();
                List<string> ID = checkforPRID.checkPRID(textBox17.Text);
                if (ID.Count == 0)//no such prescription
                {
                    label65.Text = checkforPRID.lastprid();
                    label66.Text = DateTime.Today.ToString();
                    button13.Text = "GENERATE PRESCRIPTION";

                }
                else
                {
                    label65.Text = ID[0];
                    label66.Text = ID[1];
                    button13.Text = "UPDATE";
                }
            }
            catch (Exception)
            {

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                doctorSQL updatediagnose = new doctorSQL();
                updatediagnose.UPdateDIAGNOSE(textBox17.Text, textBox20.Text);
                MessageBox.Show("Successfully updated diagnose user: " + textBox17.Text, textBox17.Text + " - diagnose updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Please contact admin", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select patient_id as 'Special ID', patient_name as 'Name', dignose from M3dic.patients_appointment where doctor_name = '{this.name}' and patient_id = '{textBox18.Text}'", conDataBase);
            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
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

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox21.Text != null)
            {
                Register_LoginMYSQL changeINFO = new Register_LoginMYSQL();
                changeINFO.DeletePerson(textBox21.Text);
                MessageBox.Show("Information updated successfully", "Information Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select from the table down", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select placed_appointment, patient_id as 'Special ID', patient_name as 'Name',dignose ,floor_placed, room_placed,bed_number ,appointment_data,appointment_time from M3dic.patients_appointment where doctor_name = '{this.name}' order by patient_name", conDataBase);
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

        private void button7_Click(object sender, EventArgs e)
        {
            clear();
            panel4.Visible = true;
            AddStuff doctorINFOrmation = new AddStuff();
            List<string> DoctorInfo = doctorINFOrmation.doctorINFO(this.special_id);
            textBox34.Text = DoctorInfo[0];
            textBox30.Text = DoctorInfo[1];
            textBox35.Text = DoctorInfo[2];
            textBox29.Text = DoctorInfo[3];
            textBox22.Text = DoctorInfo[4];
            textBox26.Text = DoctorInfo[5];
            textBox23.Text = DoctorInfo[6];
            textBox36.Text = DoctorInfo[7];
            textBox37.Text = DoctorInfo[8];
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AddStuff UPDATEDOCTOR = new AddStuff();
            UPDATEDOCTOR.updatedoctorinformation(this.special_id, textBox30.Text, textBox35.Text, textBox29.Text, textBox22.Text, textBox26.Text, textBox23.Text, textBox36.Text, textBox37.Text);

        }

        private void button18_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clear();
            panel5.Visible = true;
            textBox45.Text = null;
            textBox43.Text = null;
            textBox41.Text = null;
            textBox39.Text = null;
            textBox44.Text = null;
            textBox42.Text = null;
            textBox40.Text = null;
            textBox38.Text = null;
            textBox46.Text = null;
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"SELECT patient_id,appointment_data,appointment_time FROM M3dic.patients_appointment where curdate() <= str_to_date(appointment_data, '%d/%m/%Y') and doctor_name = '{this.name}';", conDataBase);
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
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (textBox46.Text != null)
            {
                string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
                MySqlConnection conDataBase = new MySqlConnection(query);
                MySqlCommand cmdDataBase = new MySqlCommand($"SELECT patient_id,appointment_data,appointment_time FROM M3dic.patients_appointment where curdate() <= str_to_date(appointment_data, '%d/%m/%Y') and doctor_name = '{this.name}' and  patient_id = '{textBox46.Text}'", conDataBase);
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
            }
            else
            {
                string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
                MySqlConnection conDataBase = new MySqlConnection(query);
                MySqlCommand cmdDataBase = new MySqlCommand($"SELECT patient_id,appointment_data,appointment_time FROM M3dic.patients_appointment where curdate() <= str_to_date(appointment_data, '%d/%m/%Y') and doctor_name = '{this.name}';", conDataBase);
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
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            doctorSQL update_appointment_time = new doctorSQL();
            update_appointment_time.UpdateTIME(textBox45.Text, textBox39.Text, textBox38.Text);
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"SELECT patient_id,appointment_data,appointment_time FROM M3dic.patients_appointment where curdate() <= str_to_date(appointment_data, '%d/%m/%Y') and doctor_name = '{this.name}';", conDataBase);
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
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView3.CurrentRow.Selected = true;
                    textBox45.Text = dataGridView3.Rows[e.RowIndex].Cells["patient_id"].FormattedValue.ToString();
                    textBox39.Text = dataGridView3.Rows[e.RowIndex].Cells["appointment_data"].FormattedValue.ToString();
                    textBox38.Text = dataGridView3.Rows[e.RowIndex].Cells["appointment_time"].FormattedValue.ToString();
                }
                Register_LoginMYSQL infopatient = new Register_LoginMYSQL();
                List<string> infopatients = infopatient.patientINFOtt(textBox45.Text);
                textBox43.Text = infopatients[0];
                textBox41.Text = infopatients[1];
                textBox44.Text = infopatients[2];
                textBox42.Text = infopatients[3];
                textBox40.Text = infopatients[4];
            }
            catch (Exception)
            {

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"SELECT patient_id,appointment_data,appointment_time FROM M3dic.patients_appointment where curdate() <= str_to_date(appointment_data, '%d/%m/%Y') and doctor_name = '{this.name}';", conDataBase);
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
        }

        private void button21_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"SELECT patient_id,appointment_data,appointment_time FROM M3dic.patients_appointment where doctor_name = '{this.name}';", conDataBase);
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
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            doctorSQL checkforPRID = new doctorSQL();
            if (button13.Text == "GENERATE PRESCRIPTION")
            {
                try
                {
                    checkforPRID.generateprescription(label65.Text, textBox17.Text, textBox15.Text + " " + textBox13.Text, this.special_id, this.name, label66.Text);
                    checkforPRID.UPdateDIAGNOSE(textBox17.Text, textBox20.Text);
                    MessageBox.Show("Prescription generated successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please contact support", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
                MySqlConnection conDataBase = new MySqlConnection(query);
                MySqlCommand cmdDataBase = new MySqlCommand($"select patient_id as 'Special ID', patient_name as 'Name', dignose from M3dic.patients_appointment where doctor_name = '{this.name}' ", conDataBase);
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
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
            else
            {
                checkforPRID.UPdateDIAGNOSE(textBox17.Text, textBox20.Text);
                MessageBox.Show("Prescription updated successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
                MySqlConnection conDataBase = new MySqlConnection(query);
                MySqlCommand cmdDataBase = new MySqlCommand($"select patient_id as 'Special ID', patient_name as 'Name', dignose from M3dic.patients_appointment where doctor_name = '{this.name}' ", conDataBase);
                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
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
        }

        private void button24_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            doctorSQL removePR = new doctorSQL();
            removePR.removePrescription(label69.Text);

            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select prescription_ID, patinet_id, patient_name, prescription_date from M3dic.prescription where doctor_name = '{this.name}' order by patient_name", conDataBase);
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand($"select prescription_ID, patinet_id, patient_name, prescription_date from M3dic.prescription where doctor_name = '{this.name}' and patinet_id = '{textBox47.Text}' order by patient_name", conDataBase);
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

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView4.CurrentRow.Selected = true;
                label69.Text = dataGridView4.Rows[e.RowIndex].Cells["prescription_ID"].FormattedValue.ToString();
            }
        }
    }
}
