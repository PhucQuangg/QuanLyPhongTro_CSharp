﻿using QuanLyPhongTro.models;
using QuanLyPhongTro.services;
using System;
using System.Data;
using System.Net.NetworkInformation;

namespace QuanLyPhongTro.views.Pages
{
    public partial class UC_Phong : UserControl
    {
        XuLyPhong xuLy;
        public UC_Phong()
        {
            InitializeComponent();
            xuLy = new XuLyPhong();
        }

        private void UC_Phong_Load(object sender, EventArgs e)
        {
            xuLy = new XuLyPhong();
            dgvHienThi.AutoGenerateColumns = false;
            xuLy.DisplayOnDataGridView(dgvHienThi);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmAddRoom frm = new frmAddRoom();
            frm.FormClosed += frmClosed;
            frm.ShowDialog();
        }

        private void frmClosed(object? sender, FormClosedEventArgs e)
        {
            xuLy.DisplayOnDataGridView(dgvHienThi);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            frmEditPhong frm = new frmEditPhong();
            frm.FormClosed += frmClosed;
            frm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvHienThi.SelectedRows.Count > 0)
            {
                Phong p = xuLy.getAll().Find(ph => ph.Maphong == dgvHienThi.SelectedRows[0].Cells[0].Value.ToString());
                xuLy.delete(p);
                MessageBox.Show(@"Xóa phòng thành công", @"Notice");
                xuLy.DisplayOnDataGridView(dgvHienThi);
            }
        }

        private void cbbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbFilter.SelectedIndex != -1)
            {
                string ds = cbbFilter.Items[cbbFilter.SelectedIndex].ToString();
                BindingSource bs = new BindingSource();
                bs.DataSource = xuLy.getAll().Where(p => p.Trangthai == ds);
                dgvHienThi.DataSource = bs;
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = xuLy.getAll().Where(p => p.Maphong.Contains(txtSearch.Text));
                dgvHienThi.DataSource = bs;
            }
        }

        private void dgvHienThi_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvHienThi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvHienThi.SelectedRows.Count > 0)
            {
                Phong P = xuLy.getAll().Find(ph => ph.Maphong == dgvHienThi.SelectedRows[0].Cells[0].Value.ToString());
                if (P.Trangthai == "Phòng trống")
                {
                    frmThongTinKhacThue frm = new frmThongTinKhacThue(P);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(@"Phòng đã được thuê", @"Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                dgvHienThi.ClearSelection();
            }
        }
    }
}
