using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Session1_Voleyko_171_371
{
    public partial class AuthorizationForm : StandartForm
    {
        public AuthorizationForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
        Timer tmrShow = new Timer();
        void UnlockForm(object sender, EventArgs e)
        {
            tmrShow.Stop();
            tmrShow.Tick -= new System.EventHandler(UnlockForm);
            usernameTextBox.ReadOnly = false;
            passwordTextBox.ReadOnly = false;
            loginButton.Enabled = true;
            warningLabel.Text = tmrShow.ToString();
        }
        int countAttempt = 3;
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            warningLabel.Visible = false;
            sqlConnection = new SqlConnection(connectionPath);
            await sqlConnection.OpenAsync();
            SqlDataReader sdr;
            SqlCommand cmdSelect = new SqlCommand("SELECT * FROM [Users]", sqlConnection);
            SqlCommand checkId = new SqlCommand("SELECT * FROM [Activity]", sqlConnection);
            SqlCommand loginReg = new SqlCommand("INSERT INTO [Activity] (ID, UserID, LoginDate, LoginTime) VALUES (@ID, @UserID, @LoginDate, @LoginTime)", sqlConnection);
            sdr = await checkId.ExecuteReaderAsync();
            int id = 1;
            while (await sdr.ReadAsync())
            {
                id++;
            }
            sdr.Close();
            loginReg.Parameters.AddWithValue("ID", id);
            bool checkLogin = false;
            try
            {
                sdr = await cmdSelect.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    if (usernameTextBox.Text == Convert.ToString(sdr["Email"]))
                    {
                        //checkLogin = true;
                        if (passwordTextBox.Text == Convert.ToString(sdr["Password"]))
                        {
                            MessageBox.Show("Успешно!");
                            int userId = Convert.ToInt32(sdr["ID"]);
                            DateTime dateTime = DateTime.Now;
                            string loginDate = (dateTime.ToString().Split(new char[] { ' ' }))[0];
                            string loginTime = (dateTime.ToString().Split(new char[] { ' ' }))[1];
                            MessageBox.Show(loginDate.ToString() + " " + loginTime);
                            loginReg.Parameters.AddWithValue("UserID", userId);
                            loginReg.Parameters.AddWithValue("LoginDate", loginDate);
                            loginReg.Parameters.AddWithValue("LoginTime", loginTime);
                            checkLogin = true;
                            break;
                        }
                        else
                        {
                            warningLabel.Text = "Пароль неверный!";
                            warningLabel.Visible = true;
                            countAttempt--;
                            break;
                        }
                    }
                    else
                    {
                        warningLabel.Text = "Логин неверный!";
                        warningLabel.Visible = true;
                        countAttempt--;
                        break;
                    }
                }
                sdr.Close();
                if (checkLogin == true)
                {
                    await loginReg.ExecuteNonQueryAsync();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (countAttempt == 0)
            {
                usernameTextBox.ReadOnly = true;
                passwordTextBox.ReadOnly = true;
                loginButton.Enabled = false;
                tmrShow.Interval = 10000;
                tmrShow.Tick += new System.EventHandler(UnlockForm);
                tmrShow.Start();
                warningLabel.Visible = true;
                warningLabel.Text = "Авторизация заблокирована на 10 секунд!";
                countAttempt = 3;
            }
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void AuthorizationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}