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
    public partial class StandartForm : Form
    {
        public String Login = String.Empty;

        /// <summary>
        /// Путь к базе данных.
        /// </summary>
        public string connectionPath = @"Data Source=localhost;Initial Catalog=Session1_Voleyko;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public SqlConnection sqlConnection;
        public SqlDataReader sdr;
        public StandartForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }
    }
}
