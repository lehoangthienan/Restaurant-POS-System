﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;

namespace GUI
{
    public partial class TableControl : UserControl
    {
        public delegate void OnDeleteHandler(Table table);
        public event OnDeleteHandler OnDelete;

        private Table table;

        public Table Table {
            get => table;
            set
            {
                table = value;
                this.UpdatGUI();
            }
        }

        public TableControl()
        {
            InitializeComponent();
            this.UpdatGUI();
        }

        public TableControl(Table table)
        {
            InitializeComponent();
            this.Table = table;
        }

        private void UpdatGUI()
        {
            this.txtName.Text = this.Table.Name;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure to delete table \"" + this.Table.Name + "\"", "Confirm", MessageBoxButtons.YesNo);
            if(dr==DialogResult.Yes)
            {
                TableBLL tableBLL = new TableBLL();
                tableBLL.Delete(this.Table);

                // fire event
                this.OnDelete(this.Table);
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Edit();
        }

        private void TableControl_DoubleClick(object sender, EventArgs e)
        {
            this.Edit();
        }

        private void txtName_DoubleClick(object sender, EventArgs e)
        {
            this.Edit();
        }

        private void Edit()
        {
            TableEditDialog tableEditDialog = new TableEditDialog(this.Table);
            DialogResult dr = tableEditDialog.ShowDialog();
            if(dr == DialogResult.OK)
            {
                this.Table = tableEditDialog.table;
            }
        }
    }
}