using Clinik.WinUI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clinik.WinUI
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            dynamic payload = new ExpandoObject();
            payload.UID = txtUserName.Text;
            payload.PASS = txtPassword.Text;


            dynamic result = ApiService.DoRequest("POST", "Auth/Login/", payload);

            if (result.Status.ToString() == "Success")
            {
                Program.Token = result.UserToken;
                MessageBox.Show("OK");
                this.Hide();
                new Main().Show();
            }
            else
            {
                MessageBox.Show("Not OK");
            }

        }
    }
}
