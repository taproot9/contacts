using System;
using System.Windows.Forms;
using TooSharp.Models;

namespace TestConnection
{
    public partial class FrmAddEditContact : Form
    {
        Contact _Contact = null;
        public FrmAddEditContact()
        {
            InitializeComponent();
            lblTitle.Text = @"Add Contact";
        }

        public FrmAddEditContact(Contact contact)
        {
            InitializeComponent();
            btnSave.Text = @"Update";
            btnDelete.Visible = true;
            _Contact = contact;
            lblTitle.Text = @"Update Contact";

            //validation
            _Contact.onValidationError += Contact_onValidationError;

            txtFirstName.Text = _Contact.FirstName;
            txtLastName.Text = _Contact.LastName;
            txtEmail.Text = _Contact.Email;
            txtMobile.Text = _Contact.Mobile;
        }

        public void ShowError(string text)
        {
            lblError.Text = text;
            panelError.Visible = true;
            timerError.Start();
        }

        //Create and Update
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //check if Contact not set
            if (_Contact == null)
            {
                //new contact
                Contact contact = new Contact()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Mobile = txtMobile.Text
                };
                //Validation
                contact.onValidationError += Contact_onValidationError;

                //save
                if (contact.Save() > 0)
                {
                    MessageBox.Show(@"Contact Saved");
                    this.Close();
                }

            }
            else
            {
                //update contact
                _Contact.FirstName = txtFirstName.Text;
                _Contact.LastName = txtLastName.Text;
                _Contact.Email = txtEmail.Text;
                _Contact.Mobile = txtMobile.Text;
                //save
                if (_Contact.Save() > 0) MessageBox.Show(@"Contact Updated");
                MessageBox.Show(@"Contact Updated");
            }
        }
    
        private void Contact_onValidationError(object sender, TooSharp.Core.TsExeptionArgs e)
        {
            ShowError(e.exception.Message);
        }
     
        private void TimerError_Tick(object sender, EventArgs e)
        {
            timerError.Stop();
            panelError.Visible = false;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
