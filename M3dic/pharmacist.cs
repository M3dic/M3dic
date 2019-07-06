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
    public partial class pharmacist : Form
    {
        private string name;
        private string special_id;
        private string email;
        private string password;
        private Timer timer1 = new Timer();

        public pharmacist(string v1, string v2, string v3, string v4)
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
        }

        private void child_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void pharmacist_Load(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
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

        public void clear()
        {
            panel4.Visible = false;
            panel7.Visible = false;
            dataGridView3.DataSource = null;
            panel2.Visible = false;
            panel3.Visible = false;
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
            textBox4.Text = null;
            textBox5.Text = null;
            textBox6.Text = null;
            textBox7.Text = null;
            textBox14.Text = null;
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            textBox9.Text = null;
            textBox11.Text = null;
            textBox10.Text = null;
            textBox8.Text = "0$";
            label28.Text = "-";
            label29.Text = "-";
            label30.Text = "-";
            textBox11.Enabled = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clear();
            panel7.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkinput())
            {
                medicines add = new medicines();
                add.addmedicine(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
                MessageBox.Show("Successfully added new medicine", "Medicine added", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Please fill all gaps!", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool checkinput()
        {
            if (textBox1.Text != null)
                if (textBox3.Text != "")
                    if (textBox4.Text != "")
                        if (textBox5.Text != "")
                            if (textBox6.Text != "")
                                if (textBox7.Text != "")
                                    return true;
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();

            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select name as `product`, price ,mgf as `MGF`, expiry  , description, manufacturer, quantity from M3dic.medicines", conDataBase);
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
            panel2.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            clear();
        }


        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)//generateBILL
        {
            if (checkmeds())
            {
                //increase quantity
                MedicinesINFOGIVE increasequantit = new MedicinesINFOGIVE();
                int quntit = int.Parse(label30.Text) - int.Parse(textBox11.Text);
                increasequantit.INcreasequantity(comboBox2.Text, quntit);
                //register bill
                increasequantit.registerBILL(textBox14.Text, comboBox1.Text, textBox9.Text, textBox8.Text, appointment_fee, double.Parse(textBox8.Text.Remove(textBox8.Text.Length - 1, 1)) + double.Parse(appointment_fee.Remove(appointment_fee.Length - 1, 1)) + "$");
                MessageBox.Show($"Bill has been generated for Patient {comboBox1.Text}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
            else
            {
                MessageBox.Show("Please enter correct information", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool checkmeds()
        {
            if (textBox9.Text != "")
                if (comboBox2.Text != "")
                    if (textBox11.Text != "")
                        if (textBox10.Text != "")
                            if (int.Parse(label30.Text) - int.Parse(textBox11.Text) >= 0)
                                return true;
            return false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button4_Click(object sender, EventArgs e)//givemedBTN
        {
            clear();
            panel3.Visible = true;
            MedicinesINFOGIVE names = new MedicinesINFOGIVE();
            List<string> namess = names.takeMEDS();
            foreach (var item in namess)
            {
                comboBox2.Items.Add(item);
            }
            List<string> IDS = names.takeIDSNAMES();
            foreach (var item in IDS)
            {
                comboBox1.Items.Add(item);
            }
            string lastbillid = names.generatebillid();
            int n = int.Parse(lastbillid.Remove(0, 1)) + 1;
            textBox14.Text = "B" + n.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != null)
            {
                MedicinesINFOGIVE medinfo = new MedicinesINFOGIVE();
                List<string> info = medinfo.medINFO(comboBox2.Text);
                label28.Text = info[0];
                label29.Text = info[1];
                label30.Text = info[2];
                textBox11.Enabled = true;
            }
            else
            {
                label28.Text = "-";
                label29.Text = "-";
                label30.Text = "-";
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text != null)
            {
                try
                {
                    textBox10.Text = label29.Text;
                    textBox8.Text = (int.Parse(textBox11.Text) * double.Parse(label29.Text.Remove(label29.Text.Length - 1, 1))).ToString() + "$";
                }
                catch (Exception)
                {
                    textBox8.Text = "0$";
                }
            }
        }
        string appointment_fee = null;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != null)
            {
                MedicinesINFOGIVE patientinfo = new MedicinesINFOGIVE();
                List<string> info = patientinfo.patientINFO(comboBox1.Text);
                appointment_fee = info[1];
                textBox9.Text = info[0];
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            AddStuff updatePharmacy = new AddStuff();
            updatePharmacy.UpdatePharmacy(textBox29.Text, this.special_id, textBox17.Text, int.Parse(textBox26.Text), textBox18.Text, textBox23.Text);

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            clear();
            panel4.Visible = true;
            AddStuff pharmacyinfo = new AddStuff();
            List<string> phINFO = pharmacyinfo.PHINFO(this.special_id);
            textBox30.Text = phINFO[1];
            if (textBox30.Text != "") textBox30.Enabled = false;
            textBox29.Text = phINFO[0];
            textBox26.Text = phINFO[5];
            textBox17.Text = phINFO[3];
            textBox23.Text = phINFO[2];
            if (textBox23.Text != "") textBox23.Enabled = false;
            textBox18.Text = phINFO[4];
        }

        private void button10_Click(object sender, EventArgs e)
        {
            medicines updatemed = new medicines();
            updatemed.updatemed(textBox12.Text, textBox15.Text, textBox19.Text, textBox13.Text, textBox16.Text, textBox20.Text);
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select name as `product`, price ,mgf as `MGF`, expiry  , description, manufacturer, quantity from M3dic.medicines", conDataBase);
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
            textBox12.Text = null;
            textBox15.Text = null;
            textBox19.Text = null;
            textBox13.Text = null;
            textBox16.Text = null;
            textBox20.Text = null;
        }
        //MySqlCommand cmdDataBase = new MySqlCommand("select name as `product`, price ,mgf as `MGF`, expiry  , description, manufacturer from M3dic.medicines", conDataBase);
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView3.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dataGridView3.CurrentRow.Selected = true;
                    textBox12.Text = dataGridView3.Rows[e.RowIndex].Cells["product"].FormattedValue.ToString();
                    textBox15.Text = dataGridView3.Rows[e.RowIndex].Cells["price"].FormattedValue.ToString();
                    textBox19.Text = dataGridView3.Rows[e.RowIndex].Cells["quantity"].FormattedValue.ToString();
                    textBox13.Text = dataGridView3.Rows[e.RowIndex].Cells["MGF"].FormattedValue.ToString();
                    textBox16.Text = dataGridView3.Rows[e.RowIndex].Cells["expiry"].FormattedValue.ToString();
                    textBox20.Text = dataGridView3.Rows[e.RowIndex].Cells["manufacturer"].FormattedValue.ToString();
                }
            }
            catch (Exception)
            {

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            medicines deletemed = new medicines();
            deletemed.deleteMED(textBox12.Text);
            string query = "datasource=35.205.24.161;port=3306;username=m3dic;password=\"M3dic\"";
            MySqlConnection conDataBase = new MySqlConnection(query);
            MySqlCommand cmdDataBase = new MySqlCommand("select name as `product`, price ,mgf as `MGF`, expiry  , description, manufacturer, quantity from M3dic.medicines", conDataBase);
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
            textBox12.Text = null;
            textBox15.Text = null;
            textBox19.Text = null;
            textBox13.Text = null;
            textBox16.Text = null;
            textBox20.Text = null;
        }
    }
}
