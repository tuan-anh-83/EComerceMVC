using AutoMapper;
using EComerceMVC.Data;
using EComerceMVC.ViewModels;

namespace EComerceMVC.Helpers
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			/*CreateMap<RegisterViewModel, KhachHang>()
				.ForMember(kh => kh.HoTen, option => option.MapFrom(
					RegisterViewModel => RegisterViewModel.HoTen
					)).ReverseMap();*/
		}
	}
}
