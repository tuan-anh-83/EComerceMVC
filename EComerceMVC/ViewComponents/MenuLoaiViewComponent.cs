using EComerceMVC.Data;
using EComerceMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EComerceMVC.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;

        public MenuLoaiViewComponent(Hshop2023Context context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Loais.Select(loai => new MenuLoaiViewModels
            {
                Maloai =  loai.MaLoai,
                TenLoai =  loai.TenLoai,
                SoLuong = loai.HangHoas.Count
            }).OrderBy(p => p.TenLoai);

            return View(data);
        }
    }
}
