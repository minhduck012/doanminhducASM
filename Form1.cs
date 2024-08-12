using System.Xml.Linq;

namespace Waterbill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void butlogin_Click(object sender, EventArgs e)
        {
            string name = textname.Text; // Get username
            string pass = textpass.Text; // Get password

             // Check if username is empty
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please Enter Your Name!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textname.Focus(); // Focus on username field
            }

            // Check if password is empty
            else if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please Enter password!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textpass.Focus(); // Focus on password field
            }

            else
            {
                // Validate login credentials
                if (name == "doanminhduc" && pass == "123")
                {
                    Form3 MainForm = new Form3();
                    MainForm.Show(); // Show main form
                    this.Hide(); // Hide current form
                }
                else
                {
                    MessageBox.Show("Incorrect username or password!", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void butexit_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Do you want to exit ?", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (confirm == DialogResult.OK) // Exit if OK is selected
            {
                this.Close();
            }         
        }
    }
}
