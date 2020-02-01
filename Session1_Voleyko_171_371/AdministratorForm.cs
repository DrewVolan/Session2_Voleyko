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
    public partial class AdministratorForm : StandartForm
    {
        public AdministratorForm()
        {
            InitializeComponent();
        }

        private async void AdministratorForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(connectionPath);
            await sqlConnection.OpenAsync();
            SqlDataReader sdr;
            SqlCommand selectUsers = new SqlCommand("SELECT * FROM [Users]", sqlConnection);
            int age;
            DateTime dateBirthDay;
            try
            {
                sdr = await selectUsers.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    dateBirthDay = Convert.ToDateTime(sdr["Birthdate"]);
                    age = DateTime.Now.Year - dateBirthDay.Year;
                    if (DateTime.Now.Month < dateBirthDay.Month || (DateTime.Now.Month == dateBirthDay.Month && DateTime.Now.Day < dateBirthDay.Day)) age--;
                    usersListBox.Items.Add(Convert.ToString(sdr["Firstname"]) + "  " + Convert.ToString(sdr["Lastname"]) + ";  Ages: " + age + "; RoleID: " + Convert.ToString(sdr["RoleID"]) + ";   E-mail: " + Convert.ToString(sdr["Email"]) + ";     OfficeID: " + Convert.ToString(sdr["OfficeID"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            AuthorizationForm auth = new AuthorizationForm();
            auth.Show();
            this.Hide();
        }
    }
}
