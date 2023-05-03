using Microsoft.AspNetCore.Mvc;
using webbanhang.Data;

namespace webbanhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaoCaosController : Controller
    {
        private readonly webbanhangContext _context;

        public BaoCaosController(webbanhangContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int? month = 0)
        {
            if (month == 0)
            {
                month = DateTime.Today.Month;
            }
            var firstDayOfMonth = new DateTime(DateTime.Today.Year, (int)month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var listHoaDon = _context.DonDatHang.Where(x => x.NgayLap >= firstDayOfMonth && x.NgayLap <= lastDayOfMonth);
            ViewData["month"] = month;
            var listdoanhthu = new List<int?>();
            for (int i = 1; i <= lastDayOfMonth.Day; i++)
            {
                var doanhthu = listHoaDon.Where(x => x.NgayLap.Day == i && x.TongTien != null);
                var tongtien = 0;

                if (doanhthu != null)
                {
                    foreach (var x in doanhthu)
                    {
                        tongtien += x.TongTien;
                    }
                    listdoanhthu.Add(tongtien);
                }
                else
                {
                    listdoanhthu.Add(0);
                }
            }
            ViewData["doanhthu"] = listdoanhthu;

            var listdoanhthuthang = new List<int?>();
            var dondathang = _context.DonDatHang.Where(x => x.NgayLap.Month >= 1 && x.NgayLap.Month <= 12);
            for (int i = 1; i <= 12; i++)
            {
                var doanhthu = dondathang.Where(x => x.NgayLap.Month == i && x.TongTien != null);
                var tongtien = 0;

                if (doanhthu != null)
                {
                    foreach (var x in doanhthu)
                    {
                        tongtien += x.TongTien;
                    }
                    listdoanhthuthang.Add(tongtien);
                }
                else
                {
                    listdoanhthuthang.Add(0);
                }
            }
            ViewData["doanhthuthang"] = listdoanhthuthang;

            return View();
        }
    }
}
