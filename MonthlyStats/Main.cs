using MonthlyStats.M;
using MonthlyStats.V;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonthlyStats {
    public partial class Main : Form {
        public Main() {
            InitializeComponent();
        }

        

        private void Button1_Click(object sender, EventArgs e) {

            var dbc = MonthlyStats.DB.MonthlyDBConnection.Instance;
            /*
            //dbc.Execute("CREATE TABLE KEYS     (ID AUTOINCREMENT PRIMARY KEY,ELM    TEXT(50) NOT NULL UNIQUE)");
            //dbc.Execute("CREATE TABLE READINGS_MASTER (
                ID AUTOINCREMENT PRIMARY KEY, 
                RD_DATE DATE NOT NULL UNIQUE)");
            //dbc.Execute("DROP TABLE READINGS_DETAIL");
            //dbc.Execute(@"
                CREATE TABLE READINGS_DETAIL (
                    ID AUTOINCREMENT PRIMARY KEY,
                    ELM_ID INTEGER NOT NULL, 
                    RD_VALUE NUMBER, 
                    RD_ID INTEGER NOT NULL, 
                    FOREIGN KEY (RD_ID) REFERENCES READINGS_MASTER(ID), 
                    FOREIGN KEY (ELM_ID) REFERENCES KEYS (ID) 
                )");
            */

        }

        private void Button2_Click(object sender, EventArgs e) {
            var dialog = new Form();
            var l1 = new Label()   { Left=10,Top=10,Width=100,Height=20,Text = "Element"};
            var t1 = new TextBox() { Left=10,Top=30,Width=100,Height=20,Text = "" };
            var b1 = new Button()  { Left=10,Top=50,Width=100,Height=20,Text = "Save" };
            dialog.Controls.AddRange(new Control[] { l1,t1,b1 });
            dialog.AcceptButton = b1;
            b1.Click += (s, ea) => {
                dialog.DialogResult = DialogResult.OK;
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                var dbc = MonthlyStats.DB.MonthlyDBConnection.Instance;
                dbc.Execute("insert into keys (elm) values (?)",t1.Text);
            }
        }

        private void Button3_Click(object sender, EventArgs e) {
            var dialog = new Form();
            var dgv = new DataGridView() { Left = 0,Top = 0, Width=300,Height = 300};
            var b1 = new Button() { Left = 10, Top = 350, Width = 100, Height = 20, Text = "Save" };
            dialog.Controls.AddRange(new Control[] { dgv,b1 });
            var dbc = MonthlyStats.DB.MonthlyDBConnection.Instance;
            dgv.DataSource = dbc.Read("select elm,null from keys order by 1");
            b1.Click += (s, ea) => {

                foreach(DataGridViewRow row in dgv.Rows) {
                    
                }
            };

            if (dialog.ShowDialog() == DialogResult.OK) {

            }
        }

        private void Button4_Click(object sender, EventArgs e) {
            new KeysView().Show();
        }

        private void Button1_Click_1(object sender, EventArgs e) {
            new ReadingMasterView().Show();
        }
    }
}
