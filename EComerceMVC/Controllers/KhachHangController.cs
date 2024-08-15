using AutoMapper;
using EComerceMVC.Data;
using EComerceMVC.Helpers;
using EComerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EComerceMVC.Controllers
{
	public class KhachHangController : Controller
	{
		private readonly Hshop2023Context _context;
		private readonly IMapper _mapper;

		public KhachHangController(Hshop2023Context context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult DangKy()
		{
			return View();
		}

		[HttpPost]
		public IActionResult DangKy(RegisterViewModel model, IFormFile Hinh)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var khachHang = _mapper.Map<KhachHang>(model);
					khachHang.RandomKey = MyUtil.GenerateRandomKey();
					khachHang.MatKhau = model.MatKhau.ToMd5Hash(khachHang.RandomKey);
					khachHang.HieuLuc = true;
					khachHang.VaiTro = 0;

					if (Hinh != null)
					{
						khachHang.Hinh = MyUtil.UpLoadImage(Hinh, "khachHang");
					}

					//add vao database
					_context.Add(khachHang);
					_context.SaveChanges();
					return RedirectToAction("Index", "HangHoa");
				}
				catch (Exception ex)
				{
				}

			}

			return View();
		}
	}
}
