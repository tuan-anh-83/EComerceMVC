using AutoMapper;
using EComerceMVC.Data;
using EComerceMVC.Helpers;
using EComerceMVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        #region Register
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
                    var mess = $"{ex.Message} shh";
                }

            }

            return View();
        }
        #endregion


        #region Login 
        [HttpGet]
        public IActionResult DangNhap(string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginViewModel model, string? ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = _context.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.UserName);
                if (khachHang == null)
                {
                    ModelState.AddModelError("loi", "Không tồn tại khách hàng này");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("loi", "Tài khoản đã bị khóa");

                    }
                    else
                    {
                        if (khachHang.MatKhau == model.Password.ToMd5Hash(khachHang.RandomKey))
                        {
                            ModelState.AddModelError("loi", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                new Claim(ClaimTypes.Name, khachHang.HoTen),
                                new Claim(MySetting.CLAIM_CUSTOMERID, khachHang.MaKh),

								//claim-role động
								new Claim(ClaimTypes.Role, "Customer")
                            };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                            await HttpContext.SignInAsync(claimsPrincipal);

                            if (Url.IsLocalUrl(ReturnUrl))
                                return Redirect(ReturnUrl);
                            else
                                return Redirect("/");
                        }
                    }
                }
            }

            return View();
        }
        #endregion

        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
