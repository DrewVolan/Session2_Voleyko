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
    public partial class ManagerForm : StandartForm
    {
        public ManagerForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private async void ManagerForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "session2_VoleykoDataSet.Schedules". При необходимости она может быть перемещена или удалена.
            this.schedulesTableAdapter.Fill(this.session2_VoleykoDataSet.Schedules);
            loginText.Text += $"{Login}.";
            sqlConnection = new SqlConnection(connectionPath);
            await sqlConnection.OpenAsync();
            SqlDataReader sdr;
            SqlCommand selectSchedules = new SqlCommand("SELECT * FROM [Schedules]", sqlConnection);
            try
            {
                sdr = await selectSchedules.ExecuteReaderAsync();
                while (await sdr.ReadAsync())
                {
                    //schedulesListBox.Items.Add("ID: " + Convert.ToString(sdr["ID"]) + "; Date: " + Convert.ToString(sdr["Lastname"]) + ";  Ages: " + age + "; RoleID: " + Convert.ToString(sdr["RoleID"]) + ";   E-mail: " + Convert.ToString(sdr["Email"]) + ";     OfficeID: " + Convert.ToString(sdr["OfficeID"]));
                }
                sdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sqlConnection.Close();
            //this.routesTableAdapter.Fill(this.session2_VoleykoDataSet.Routes);
            //this.airportsTableAdapter.Fill(this.session2_VoleykoDataSet.Airports);
            this.schedulesTableAdapter.Fill(this.session2_VoleykoDataSet.Schedules);
        }
    }
}
