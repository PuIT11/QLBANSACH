using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBANSACH.Models
{
    public class Giohang
    {
        DbQuanLyBanSachDataContext db = new DbQuanLyBanSachDataContext();
        public int iMasach { set; get; }
        public string sTensach { set; get; }
        public string   sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public  Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        public Giohang(int Masach)
        {
            iMasach = Masach;
            SACH sach = db.SACHes.Single(n => n.MaSach == iMasach);
            sTensach = sach.TenSach;
            sAnhbia = sach.AnhBia;
            dDongia = double.Parse(sach.GiaBan.ToString());
            iSoluong = 1;
        }
    }
}