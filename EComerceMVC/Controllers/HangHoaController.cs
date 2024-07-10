using EComerceMVC.Data;
using EComerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EComerceMVC.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context db;
        public HangHoaController(Hshop2023Context context)
        {
            db = context;
        }
        public IActionResult Index(int? loai)
        {
            var hanghoas = db.HangHoas.AsQueryable();

            if (loai.HasValue)
            {
                hanghoas = hanghoas.Where(p => p.MaLoai == loai.Value);
            }

            var result = hanghoas.Select(p => new HangHoaViewModel
            {
                MaHH = p.MaHh,
                TenHH = p.TenHh,
                Dongia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }

        public IActionResult Search(string? query)
        {
            var hanghoas = db.HangHoas.AsQueryable();

            if (query != null)
            {
                hanghoas = hanghoas.Where(p => p.TenHh.Contains(query));
            }

            var result = hanghoas.Select(p => new HangHoaViewModel
            {
                MaHH = p.MaHh,
                TenHH = p.TenHh,
                Dongia = p.DonGia ?? 0,
                Hinh = p.Hinh ?? "",
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var data = db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }

            var result = new ChiTietHangHoaViewModel
            {
                MaHH = data.MaHh,
                TenHH = data.TenHh,
                Dongia = data.DonGia ?? 0,
                Hinh = data.Hinh ?? "",
                ChiTiet = data.MoTa ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10,
                DiemDanhGia = 5,
            };

            return View(result);
        }
    }
}
