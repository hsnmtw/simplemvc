using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonthlyStats.C;
using MonthlyStats.M;

namespace MonthlyStats.V {
    public partial class KeysView : Form , IView {

        private IController Controller;
        private KeysModel m;

        public KeysModel Model {
            get {
                m.ID = int.Parse($"0{txtID.Text}");
                m.ELM = txtELM.Text.Trim();
                return m;
            }
            set {
                m = value;
                txtID.Text = m.ID.ToString();
                txtELM.Text = m.ELM;
            }
        }

        public KeysView() {
            InitializeComponent(); if (DesignMode) return;
            this.Controller = ControllersFactory.GetController(Controllers.KEYS);
            this.Model = new KeysModel();
        }

        private void KeysView_Load(object sender, EventArgs e) {
            this.RefreshList();
        }

        void RefreshList() {
            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange((from DataRow row in Controller.Read(new KeysModel()).Rows select string.Format("{0,-3} {1}", row[0],row[1])).ToArray());
        }


        private void Button3_Click(object sender, EventArgs e) {
            this.Model = new KeysModel(); 
        }

        private void Button2_Click(object sender, EventArgs e) {
            this.Controller.Save(this.Model);
            this.RefreshList();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Controller.Delete(this.Model);
            this.RefreshList();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBox1.SelectedIndex < 0) return;
            var table = Controller.Read(new KeysModel() {
                ID = int.Parse( this.listBox1.Text.Substring(0,3).Trim() )
            },"ID");
            if (table.Rows.Count == 0) return; 
            this.Model = new KeysModel() {
                ID = int.Parse(table.Rows[0]["ID"].ToString()),
                ELM = table.Rows[0]["ELM"].ToString()
            };
        }
    }
}
