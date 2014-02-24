using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Database.Repository
{
    public class ConnectionRepository
    {
        Label current = new Label();
        TextBox textBox, textBox2, textBox3, textBox4;

        public void dialog()
        {
            Size size = new Size(600, 280);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Database Connection Setting";

            current.MaximumSize = new Size(500, 30);
            current.AutoSize = true;
            current.Location = new Point(15, 15);
            inputBox.Controls.Add(current);

            Label dataSource = new Label();
            dataSource.Text = "Data Source : ";
            dataSource.Location = new Point(10, 65);
            inputBox.Controls.Add(dataSource);

            textBox = new TextBox();
            textBox.Size = new Size(200, 40);
            textBox.Location = new Point(120, 65);
            inputBox.Controls.Add(textBox);

            Button dataSourceButton = new Button();
            dataSourceButton.DialogResult = DialogResult.OK;
            dataSourceButton.Name = "dataSourceButton";
            dataSourceButton.Size = new Size(75, 23);
            dataSourceButton.Text = "&Change";
            dataSourceButton.Location = new Point(350, 65);
            dataSourceButton.Click += new System.EventHandler(this.dataSourceButton_Click);
            inputBox.Controls.Add(dataSourceButton);

            Label catalog = new Label();
            catalog.Text = "Catalog : ";
            catalog.Location = new Point(10, 105);
            inputBox.Controls.Add(catalog);

            textBox2 = new TextBox();
            textBox2.Size = new Size(200, 40);
            textBox2.Location = new Point(120, 105);
            inputBox.Controls.Add(textBox2);

            Button catalogButton = new Button();
            catalogButton.DialogResult = DialogResult.OK;
            catalogButton.Name = "catalogButton";
            catalogButton.Size = new Size(75, 23);
            catalogButton.Text = "&Change";
            catalogButton.Location = new Point(350, 105);
            catalogButton.Click += new System.EventHandler(this.catalogButton_Click);
            inputBox.Controls.Add(catalogButton);

            Label security = new Label();
            security.Text = "Security : ";
            security.Location = new Point(10, 145);
            inputBox.Controls.Add(security);

            textBox3 = new TextBox();
            textBox3.Size = new Size(200, 40);
            textBox3.Location = new Point(120, 145);
            inputBox.Controls.Add(textBox3);

            Button securityButton = new Button();
            securityButton.DialogResult = DialogResult.OK;
            securityButton.Name = "securityButton";
            securityButton.Size = new Size(75, 23);
            securityButton.Text = "&Change";
            securityButton.Location = new Point(350, 145);
            securityButton.Click += new System.EventHandler(this.securityButton_Click);
            inputBox.Controls.Add(securityButton);

            Label extra = new Label();
            extra.Text = "Extra : ";
            extra.Location = new Point(10, 185);
            inputBox.Controls.Add(extra);

            textBox4 = new TextBox();
            textBox4.Size = new Size(200, 40);
            textBox4.Location = new Point(120, 185);
            inputBox.Controls.Add(textBox4);

            Button extraButton = new Button();
            extraButton.DialogResult = DialogResult.OK;
            extraButton.Name = "extraButton";
            extraButton.Size = new Size(75, 23);
            extraButton.Text = "&Change";
            extraButton.Location = new Point(350, 185);
            extraButton.Click += new System.EventHandler(this.extraButton_Click);
            inputBox.Controls.Add(extraButton);

            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new Point(size.Width - 80 - 80, size.Height - 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new Point(size.Width - 80, size.Height - 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            defaultValue();

            DialogResult result = inputBox.ShowDialog();
        }

        private void defaultValue()
        {
            try
            {
                current.Text = "Current : " + ConnectionManager.getConnection;
                textBox.Text = ConnectionManager.DataSource;
                textBox2.Text = ConnectionManager.Catalog;
                textBox3.Text = ConnectionManager.Security;
                textBox4.Text = ConnectionManager.Extra;
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        private void dataSourceButton_Click(object sender, EventArgs e)
        {
            try{
            ConnectionManager.DataSource = textBox.Text;
            current.Text = "Current : " + ConnectionManager.getConnection;
            current.Refresh();
            }
            catch (Exception e1) { Console.WriteLine(e1); }
        }

        private void catalogButton_Click(object sender, EventArgs e)
        {
            try{
            ConnectionManager.Catalog = textBox2.Text;
            current.Text = "Current : " + ConnectionManager.getConnection;
            current.Refresh();
            }
            catch (Exception e1) { Console.WriteLine(e1); }
        }

        private void securityButton_Click(object sender, EventArgs e)
        {
            try{
            ConnectionManager.Security = textBox3.Text;
            current.Text = "Current : " + ConnectionManager.getConnection;
            current.Refresh();
            }
            catch (Exception e1) { Console.WriteLine(e1); }
        }

        private void extraButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionManager.Extra = textBox4.Text;
                current.Text = "Current : " + ConnectionManager.getConnection;
                current.Refresh();
            }
            catch (Exception e1) { Console.WriteLine(e1); }
        }
    }
}
