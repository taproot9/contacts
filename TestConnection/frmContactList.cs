using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TooSharp.Models;

namespace TestConnection
{
    public partial class frmContactList : Form
    {
        public frmContactList()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ReloadData();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new FrmAddEditContact().ShowDialog();
            ReloadData();
        }
        void ReloadData()
        {
            //check textbox search 
            if (txtSearch.Text.Trim().Length > 0)
            {
                //search specific data
                PopulateData(
                    Contacts.Records()
                    .Where(Contacts.COLUMNS.Email, "like", $@"%{txtSearch.Text}%")
                    .Get()
                );
            }
            else
            {
                //fetch all data
                PopulateData(Contacts.Records().Get());
            }
        }

        void PopulateData(IEnumerable<Contact> contacts)
        {
            //Clear the gridview
            dataGridViewContacts.Rows.Clear();

            //populate to gridview
            foreach (var contact in contacts)
            {
                dataGridViewContacts.Rows.Add(new object[]{
                        contact.Id,
                        contact.FirstName,
                        contact.LastName,
                        contact.Email,
                        contact.Mobile,
                        "Edit",
                        "Delete"
                });

                dataGridViewContacts.Rows[dataGridViewContacts.RowCount - 1].Tag = contact;
            }
        }

        //shown if the form load
        private void FrmContactList_Shown(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            ReloadData();
        }

        //for delete and edit
        private void DataGridViewContacts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5) //edit
            {
                //pass Contact object to FrmAddEditContact
                new FrmAddEditContact((Contact) dataGridViewContacts.CurrentRow.Tag).ShowDialog();
                ReloadData();
            }
            else if (e.ColumnIndex == 6) //delete
            {
                //get the current rows access with contact object
                Contact contact = (Contact) dataGridViewContacts.CurrentRow.Tag;

                //show deletion or not
                if (MessageBox.Show($@"Delete {contact.FirstName}?", @"Confirm", MessageBoxButtons.YesNoCancel)==DialogResult.Yes)
                {
                    contact.Delete();
                    ReloadData();
                }
            }
        }
    }
}
