using System.Text;

namespace EComerceMVC.Helpers
{
	public class MyUtil
	{
		public static string GenerateRandomKey(int length = 5)
		{
			var pattern = @"qazwsxedcrfvtgbyhnujmikolpQAZWSXEDCRFVTGBYHNUJMIKOLP!";
			var sb = new StringBuilder();
			var rb = new Random();
			for (int i = 0; i < length; i++)
			{
				sb.Append(pattern[rb.Next(0, pattern.Length)]);

			}
			return sb.ToString();
		}

		//Load Hình
		public static string UpLoadImage(IFormFile Hinh, string folder)
		{
			try
			{
				var fullPath = Path.Combine(Directory.GetCurrentDirectory(),
								"wwwroot", "Hinh", folder, Hinh.FileName);
				using (var myFile = new FileStream(fullPath, FileMode.CreateNew))
				{
					Hinh.CopyTo(myFile);
				}
				return Hinh.FileName;
			}
			catch (Exception ex)
			{
				return string.Empty;
			}
		}
	}
}
