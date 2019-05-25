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
    public partial class ReadingMasterView : Form , IView {

        private IController MasterController, DetailController;
        private ReadingMasterModel m;

        public ReadingMasterModel Model {
            get {
                m.ID = int.Parse($"0{txtID.Text}");
                m.RD_DATE = DateTime.Parse(txtDATE.Text.Trim()); //DateTime.Parse(txtDATE.Text.Trim());
                return m;
            }
            set {
                m = value;
                txtID.Text = m.ID.ToString();
                txtDATE.Text = m.RD_DATE==null ? "" : Convert.ToString( m.RD_DATE );
                this.dataGridView1.DataSource = ((ReadingDetailController)this.DetailController).ReadView(
                    new ReadingDetailModel() {
                        RD_ID = m.ID
                    },
                    "RD_ID"
                );
            }
        }

        public ReadingMasterView() {
            InitializeComponent(); if (DesignMode) return;
            this.MasterController = ControllersFactory.GetController(Controllers.READING_MASTER);
            this.DetailController = ControllersFactory.GetController(Controllers.READING_DETAIL);
            this.Model = new ReadingMasterModel();
        }

        private void KeysView_Load(object sender, EventArgs e) {
            this.RefreshList();
        }

        void RefreshList() {
            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange((from DataRow row in MasterController.Read(new ReadingMasterModel()).Rows select string.Format("{0,-3} {1}", row[0],row[1])).ToArray());
        }


        private void Button3_Click(object sender, EventArgs e) {
            this.Model = new ReadingMasterModel();
        }

        private void Button2_Click(object sender, EventArgs e) {
            this.MasterController.Save(this.Model);
            this.RefreshList();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.MasterController.Delete(this.Model);
            this.RefreshList();
        }

        private void Button4_Click(object sender, EventArgs e) {
            new ReadingDetailView() {
                Model = new ReadingDetailModel() {
                    RD_ID = this.Model.ID
                }
            }.ShowDialog();
            RefreshList();
        }

        private void Button5_Click(object sender, EventArgs e) {
            if (this.dataGridView1.SelectedRows.Count < 1) return;
            var dialog = new ReadingDetailView();
            dialog.Model = new ReadingDetailModel() {
                ID = int.Parse(this.dataGridView1.SelectedRows[0].Cells["ID"].ToString()),
                RD_VALUE = double.Parse(this.dataGridView1.SelectedRows[0].Cells["RD_VALUE"].ToString()),
                ELM_ID = int.Parse(this.dataGridView1.SelectedRows[0].Cells["ELM_ID"].ToString()),
                RD_ID = int.Parse(this.dataGridView1.SelectedRows[0].Cells["RD_ID"].ToString()),
            };
            dialog.ShowDialog();
            RefreshList();
        }

        private void Button6_Click(object sender, EventArgs e) {
            
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBox1.SelectedIndex < 0) return;
            var table = MasterController.Read(new ReadingMasterModel() {
                ID = int.Parse( this.listBox1.Text.Substring(0,3).Trim() )
            },"ID");
            if (table.Rows.Count == 0) return; 
            this.Model = new ReadingMasterModel() {
                ID = int.Parse(table.Rows[0]["ID"].ToString()),
                RD_DATE = Convert.ToDateTime(table.Rows[0]["RD_DATE"].ToString())
            };
        }
    }
}
