using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database.Repository
{
    public class AdminRepository
    {
        private String adminPass = "dips123";

        public Boolean verified()
        {
            String pass = ShowInputDialog();
            if (pass.Equals(adminPass)) return true;
            else return false;
        }

        private String ShowInputDialog()
        {
            System.Drawing.Size size = new System.Drawing.Size(220, 80);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Enter Password";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            inputBox.Controls.Add(textBox);

            Button inputButton = new Button();
            inputButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            inputButton.Name = "inputButton";
            inputButton.Size = new System.Drawing.Size(75, 23);
            inputButton.Text = "&Enter";
            inputButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(inputButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = inputButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            return textBox.Text;
        }
    }
}
