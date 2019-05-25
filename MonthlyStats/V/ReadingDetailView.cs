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
    public partial class ReadingDetailView : Form , IView {

        private IController ReadingDetailController, ReadingMasterController,KeysController;
        private ReadingDetailModel m;

        public ReadingDetailModel Model {
            get {
                m.ID = int.Parse($"0{txtID.Text}");
                m.RD_ID = int.Parse($"0{cmbDATE.SelectedValue}");
                m.ELM_ID = int.Parse($"0{cmbELM.SelectedValue}");
                m.RD_VALUE = Convert.ToDouble( $"0{txtVAL.Text}" );
                return m;
            }
            set {
                m = value;
                txtID.Text = m.ID.ToString();
                cmbELM.SelectedValue = m.ELM_ID;
                cmbDATE.SelectedValue = m.RD_ID;
                txtVAL.Text = m.RD_VALUE.ToString("000.000");
            }
        }

        public ReadingDetailView() {
            InitializeComponent(); if (DesignMode) return;
            this.ReadingDetailController = ControllersFactory.GetController(Controllers.READING_DETAIL);
            this.ReadingMasterController = ControllersFactory.GetController(Controllers.READING_MASTER);
            this.KeysController = ControllersFactory.GetController(Controllers.KEYS);

            this.Model = new ReadingDetailModel();

            this.cmbDATE.DataSource = ReadingMasterController.Read(new ReadingMasterModel());
            this.cmbDATE.DisplayMember = "RD_DATE";
            this.cmbDATE.ValueMember = "ID";

            this.cmbELM.DataSource = ReadingMasterController.Read(new KeysModel());
            this.cmbELM.DisplayMember = "ELM";
            this.cmbELM.ValueMember = "ID";

        }

        private void KeysView_Load(object sender, EventArgs e) {
            this.RefreshList();
        }

        void RefreshList() {
            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange((from DataRow row in ReadingDetailController.Read(new ReadingDetailModel()).Rows select string.Format("{0,-3} {1}", row[0],row[1])).ToArray());
        }


        private void Button3_Click(object sender, EventArgs e) {
            this.Model = new ReadingDetailModel();
        }

        private void Button2_Click(object sender, EventArgs e) {
            this.ReadingDetailController.Save(this.Model);
            this.RefreshList();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.ReadingDetailController.Delete(this.Model);
            this.RefreshList();
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.listBox1.SelectedIndex < 0) return;
            var table = ReadingDetailController.Read(new KeysModel() {
                ID = int.Parse( this.listBox1.Text.Substring(0,3).Trim() )
            },"ID");
            if (table.Rows.Count == 0) return; 
            this.Model = new ReadingDetailModel() {
                ID    = int.Parse(table.Rows[0]["ID"].ToString()),
                RD_ID = int.Parse(table.Rows[0]["RD_ID"].ToString()),
                ELM_ID = int.Parse(table.Rows[0]["ELM_ID"].ToString()),
                RD_VALUE = double.Parse(table.Rows[0]["RD_VALUE"].ToString()),
            };
        }
    }
}
