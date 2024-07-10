namespace EComerceMVC.ViewModels
{
    public class HangHoaViewModel
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double Dongia { get; set; }
        public string MoTaNgan { get; set; }
        public string TenLoai { get; set; }
    }
    public class ChiTietHangHoaViewModel
    {
        public int MaHH { get; set; }
        public string TenHH { get; set; }
        public string Hinh { get; set; }
        public double Dongia { get; set; }
        public string MoTaNgan { get; set; }
        public string TenLoai { get; set; }
        public string ChiTiet { get; set; }
        public int DiemDanhGia { get; set; }
        public int SoLuongTon { get; set; }
    }
}
