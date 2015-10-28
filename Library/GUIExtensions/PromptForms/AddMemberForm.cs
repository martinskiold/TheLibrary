//Library
//Martin Skiöld
//Version 1.0 2015-11-02
using Library.Models;
using Library.Repositories;
using Library.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library.GUIExtensions.PromptForms
{
    public partial class AddMemberForm : Form
    {
        public AddMemberForm()
        {
            InitializeComponent();
        }

        private void btnCancelAddBook_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            string memberName = tbAddMemberName.Text;
            string memberId = tbAddMemberId.Text;

            if (memberName != "" && memberId != "")
            {
                int parsedId;
                if (Int32.TryParse(memberId, out parsedId))
                {
                    if (LibraryForm.memberService.AddMember(parsedId, memberName))
                    {
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("AddMemberError: TheLibraryApplication has encountered an error while trying to register the new member. The Member has not been registered. Please try again, or call support.", "AddMemberError" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please insert only numbers, '0-9', as the members Personal Id.");
                    tbAddMemberId.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please enter Member's Name and Member's Personal Id before adding a new member.");
            }
        }

        private void AddMemberForm_Load(object sender, EventArgs e)
        {

        }


    }
}
