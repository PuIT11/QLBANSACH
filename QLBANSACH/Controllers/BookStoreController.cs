using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLBANSACH.Models;
using PagedList;
using QLBANSACH.VNpays;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBANSACH.Controllers
{
    public class BookStoreController : Controller
    {
        DbQuanLyBanSachDataContext db = new DbQuanLyBanSachDataContext();

        private List<SACH> LaySachMoi(int count)
        {
            return db.SACHes.OrderByDescending(s => s.NgayCapNhat).Take(count).ToList();
        }

        //
        // GET: /BookStore/


//=========================

        public ActionResult Index(int? page, String sortOrder, string searchString, double? to, double? from, string TenSach)
        {

            ViewBag.MaSortParm = String.IsNullOrEmpty(sortOrder) ? "ma_desc" : "";
            ViewBag.TenSortParm = sortOrder == "ten" ? "ten_desc" : "ten";
            var sachmoi = from ss in db.SACHes
                          select ss;

            //------ search --------------------> tim kiem: kieu du lieu maloai: loi Err103

            if (!String.IsNullOrEmpty(searchString))
                sachmoi = sachmoi.Where(s => s.TenSach.Contains(searchString));


            switch (sortOrder)
            {
                case "ma_desc": //kiểm tra giá trị của biến sort = tên biến (MaSortParm) 
                    sachmoi = sachmoi.OrderByDescending(s => s.MaSach);
                    break;
                case "ten":
                    sachmoi = sachmoi.OrderBy(s => s.TenSach);
                    break;
                case "ten_desc":
                    sachmoi = sachmoi.OrderByDescending(s => s.TenSach);
                    break;
                default:
                    sachmoi = sachmoi.OrderBy(s => s.MaSach);
                    break;
            }
            //================================================

            //Sai sap min gia
           /* if (!String.IsNullOrEmpty(tensach))
            {
                if (to != null && from != null)
                {
                    sachmoi = sachmoi.Where(s => s.TenSach.Contains(TenSach) && s.GiaBan >= to && s.GiaBan <= from);
                }
                else
                {
                    sachmoi = sachmoi.Where(s => s.TenSach.Contains(TenSach));
                }
            }
            else
            {
                if (to != null && from != null)
                {
                    sachmoi = sachmoi.Where(s => s.TenSach.Contains(TenSach) && s.GiaBan >= to && s.GiaBan <= from);
                }
            }*/


            //===========================================


            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var sach2 = this.LaySachMoi(15);  //fix

            return View(sachmoi.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ChuDe()
        {
            var chude = from cd in db.CHUDEs select cd;
            return PartialView(chude);
        }
        public ActionResult NhaXuatBan()
        {
            var nhaxuatban = from nxb in db.NHAXUATBANs select nxb;
            return PartialView(nhaxuatban);
        }
        public ActionResult SPTheoChuDe(int id, int ? page)
        {
            var sanpham = from sp in db.SACHes where sp.MaCD == id select sp;
            ViewBag.MaChuDe = id;
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(sanpham.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult SPTheoNXB(int id, int ? page)
        {
            var sanpham = from sp in db.SACHes where sp.MaNXB == id select sp;
            ViewBag.MaNXB = id;
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(sanpham.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int id)
        {
            var sanpham = (from sp in db.SACHes where sp.MaSach == id select sp).Single();
            return View(sanpham);
        }
    }
}
