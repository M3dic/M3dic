using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using Infralution.Localization;
using Microsoft.VisualBasic;

namespace M3dic
{
    public partial class Form1 : Form
    {
        Timer timer1 = new Timer();
        public Form1()
        {
            InitializeComponent();
            label5.Text = DateTime.Now.ToString();
            timer1.Tick += new EventHandler(timer1_Tick);
            this.timer1.Interval = 1000;
            this.timer1.Enabled = true;
            textBox2.UseSystemPasswordChar = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label5.Text = DateTime.Now.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel1.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Admin")
            {
                sendAdminPassword(textBox14.Text);
                panel4.Visible = false;
                panel1.Enabled = true;
            }
            else if (forgottenpassword(textBox14.Text))
            {
                textBox14.Clear();
                panel4.Visible = false;
                panel1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Wrong or incorrect email", "Check email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox14.Clear();
                panel4.Visible = false;
                panel1.Enabled = true;
            }
        }

        private void sendAdminPassword(string adminemail)
        {
            Register_LoginMYSQL newRegister = new Register_LoginMYSQL();

            List<string> ll = newRegister.checkAdminLogin().Find(x => x[3] == adminemail);
            if (ll == null)
            {
                MessageBox.Show("Please check the admin email", "Incorrect admin email", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            string sequre_number = ll[0];
            string password = ll[1];
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

                MailMessage mm = new MailMessage("donotreply@domain.com", $"{adminemail}", "You have already account", $"\nUser: {sequre_number} \nPassword: {password}");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);

                MessageBox.Show("Check admin email for special ID and password", "Email sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Please correct Admin email or contact support!", "Email error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool forgottenpassword(string text)
        {
            Register_LoginMYSQL newRegister = new Register_LoginMYSQL();

            if (newRegister.SearchEmails().Contains(text))
            {
                List<string> ll = newRegister.FindPassword().Find(x => x[2] == text);
                string sequre_number = ll[0];
                string password = ll[1];
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

                    MessageBox.Show("Check email for Id and password", "Email sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    //MessageBox.Show("Please correct email or contact support!", "Email error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return true;
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select service from the box", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            if (comboBox1.Text == "Admin")
            {
                adminLogin();
            }
            else if (comboBox1.Text == "Doctor")
            {
                doctorlogin();
            }
            else if (comboBox1.Text == "Receptionist")
            {
                receptionistLogin();
            }
            else if (comboBox1.Text == "Pharmacist")
            {
                PharmacistLogin();
            }
        }

        private void PharmacistLogin()
        {
            Register_LoginMYSQL _LoginMYSQL = new Register_LoginMYSQL();
            List<List<string>> pharmacyLogin = _LoginMYSQL.chackPharmacyLogin();
            List<string> currentreceptionist = new List<string>();
            try
            {
                currentreceptionist = pharmacyLogin.Find(x => x[0] == textBox1.Text && x[1] == textBox2.Text);
            }
            catch (Exception)
            {

            }
            if (currentreceptionist == null)
            {
                MessageBox.Show("Invalid Pharmacist details, please contact Admin!", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Welcome Pharmacist {currentreceptionist[2]} ", "Welcome Pharmacist", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            pharmacist child = new pharmacist(currentreceptionist[2], currentreceptionist[0], currentreceptionist[3], currentreceptionist[1]); //create new isntance of form
            child.FormClosed += new FormClosedEventHandler(child_FormClosed); //add handler to catch when child form is closed
            child.Show(); //show child
            this.Hide(); //hide parent
        }

        private void receptionistLogin()
        {
            Register_LoginMYSQL _LoginMYSQL = new Register_LoginMYSQL();
            List<List<string>> receptionistLogin = _LoginMYSQL.checkReceptionistLogin();
            List<string> currentreceptionist = new List<string>();
            try
            {
                currentreceptionist = receptionistLogin.Find(x => x[0] == textBox1.Text && x[1] == textBox2.Text);
            }
            catch (Exception)
            {

            }
            if (currentreceptionist == null)
            {
                MessageBox.Show("Invalid Receptionist details, please contact Admin!", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Welcome Receptionist {currentreceptionist[2]} ", "Welcome Receptionist", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Receptionist child = new Receptionist(currentreceptionist[2], currentreceptionist[0], currentreceptionist[3], currentreceptionist[1]); //create new isntance of form
            child.FormClosed += new FormClosedEventHandler(child_FormClosed); //add handler to catch when child form is closed
            child.Show(); //show child
            this.Hide(); //hide parent
        }


        private void doctorlogin()
        {
            Register_LoginMYSQL _LoginMYSQL = new Register_LoginMYSQL();
            List<List<string>> doctorregistration = _LoginMYSQL.checkDoctorLogin();
            List<string> currentdoctor = new List<string>();
            try
            {
                currentdoctor = doctorregistration.Find(x => x[0] == textBox1.Text && x[1] == textBox2.Text);
            }
            catch (Exception)
            {

            }
            if (currentdoctor == null)
            {
                MessageBox.Show("Invalid Doctor details, please contact Admin!", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Welcome Doctor {currentdoctor[2]} ", "Welcome doctor", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Doctor child = new Doctor(currentdoctor[2], currentdoctor[0], currentdoctor[3], currentdoctor[1], currentdoctor[4]); //create new isntance of form
            child.FormClosed += new FormClosedEventHandler(child_FormClosed); //add handler to catch when child form is closed
            child.Show(); //show child
            this.Hide(); //hide parent
        }



        private void adminLogin()
        {
            Register_LoginMYSQL _LoginMYSQL = new Register_LoginMYSQL();
            List<List<string>> AdminsRegister = _LoginMYSQL.checkAdminLogin();
            List<string> CurrentAdmin = new List<string>();
            try
            {
                CurrentAdmin = AdminsRegister.Find(x => x[0] == textBox1.Text && x[1] == textBox2.Text);
            }
            catch (Exception)
            {

            }
            if (CurrentAdmin == null)
            {
                MessageBox.Show("Invalid Admins details, please contact support!", "Invalid details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Welcome Admin {CurrentAdmin[2]} ", "Welcome admin", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Admin child = new Admin(CurrentAdmin[2], CurrentAdmin[0], CurrentAdmin[3], CurrentAdmin[1]); //create new isntance of form
            child.FormClosed += new FormClosedEventHandler(child_FormClosed); //add handler to catch when child form is closed
            child.Show(); //show child
            this.Hide(); //hide parent
        }

        private void child_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
