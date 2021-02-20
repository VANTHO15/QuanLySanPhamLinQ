using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySanPhamLinQ
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        List<SanPham> dsSPGoc = new List<SanPham>();
        private void btnLuuSP_Click(object sender, EventArgs e)
        {
            SanPham sp = new SanPham();
            sp.Ma = int.Parse(txtMa.Text);
            sp.Ten = txtTen.Text;
            sp.SoLuong = int.Parse(txtSoLuong.Text);
            sp.DonGia = long.Parse(txtDonGia.Text);
            sp.XuatXu = txtXuatXu.Text;
            sp.HanDung = dtpHanDung.Value;
            dsSPGoc.Add(sp);
            XoaDuLieuNhapCu();
            HienThiDSSanPhamLenListView(dsSPGoc, lvDanhSachSP);

        }
        void XoaDuLieuNhapCu()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtSoLuong.Text = "";
            txtDonGia.Text = "";
            txtXuatXu.Text = "";
            txtMa.Focus();
        }
        void HienThiDSSanPhamLenListView(List<SanPham>ds,ListView lv)
        {
            lv.Items.Clear();
            ds.ForEach(sp =>
            {
                ListViewItem lvi = new ListViewItem(sp.Ma.ToString());
                lvi.SubItems.Add(sp.Ten);
                lvi.SubItems.Add(sp.SoLuong + "");
                lvi.SubItems.Add(sp.DonGia + "");
                lvi.SubItems.Add(sp.XuatXu + "");
                lvi.SubItems.Add(sp.HanDung.ToString("dd/MM/yyyy"));
                lv.Items.Add(lvi);
            });
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if(lvDanhSachSP.SelectedIndices.Count==0)
            {
                MessageBox.Show("Bạn phải chọn mưới xóa được chớ ", "Lỗi Rồi",MessageBoxButtons.OK);
                return;
            }
            int index = lvDanhSachSP.SelectedIndices[0];
            dsSPGoc.RemoveAt(index);
            HienThiDSSanPhamLenListView(dsSPGoc, lvDanhSachSP);
        }

        private void btnXoaXuatXu_Click(object sender, EventArgs e)
        {
            for(int i=dsSPGoc.Count-1;i>=0;i--)
            {
                if(string.Compare(dsSPGoc[i].XuatXu,txtXoaXuatXu.Text,true)==0)
                {
                    dsSPGoc.RemoveAt(i);
                }
            }
            HienThiDSSanPhamLenListView(dsSPGoc, lvDanhSachSP);
        }

        private void btnKiemTRaSPQuaHan_Click(object sender, EventArgs e)
        {
            bool kq = dsSPGoc.Any(sp =>
             {
                 return sp.HanDung.Date < DateTime.Now;
             });
            if(kq==true)
            {
                MessageBox.Show("Có sản phẩm quá hạn ", "Quá hạn", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Kho OK", "OK", MessageBoxButtons.OK);
            }
        }

        private void btn1SpGiaCaoNhat_Click(object sender, EventArgs e)
        {
            SanPham sp = dsSPGoc[0];
            for(int i=1;i<dsSPGoc.Count;i++)
            {
                if(dsSPGoc[i].DonGia>sp.DonGia)
                {
                    sp = dsSPGoc[i];
                }
            }
            List<SanPham> dsTim = new List<SanPham>() { sp};
            HienThiDSSanPhamLenListView(dsTim, lvTimKiem);

        }
    }
}
