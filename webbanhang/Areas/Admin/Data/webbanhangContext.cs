using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webbanhang.Models;

namespace webbanhang.Data
{
    public class webbanhangContext : DbContext
    {
        public webbanhangContext (DbContextOptions<webbanhangContext> options)
            : base(options)
        {
        }

        public DbSet<webbanhang.Models.QuyenHan> QuyenHan { get; set; } = default!;

        public DbSet<webbanhang.Models.TaiKhoan>? TaiKhoan { get; set; }

        public DbSet<webbanhang.Models.Banner>? Banner { get; set; }

        public DbSet<webbanhang.Models.DanhMuc>? DanhMuc { get; set; }

        public DbSet<webbanhang.Models.SanPham>? SanPham { get; set; }

        public DbSet<webbanhang.Models.HinhAnh>? HinhAnh { get; set; }

        public DbSet<webbanhang.Models.DonDatHang>? DonDatHang { get; set; }

        public DbSet<webbanhang.Models.ChiTietDH>? ChiTietDH { get; set; }

        public DbSet<webbanhang.Models.ThuongHieu>? ThuongHieu { get; set; }

        public DbSet<webbanhang.Models.NhapHang>? NhapHang { get; set; }

        public DbSet<webbanhang.Models.ChiTietNH>? ChiTietNH { get; set; }

        public DbSet<webbanhang.Models.DonViTinh>? DonViTinh { get; set; }

        public DbSet<webbanhang.Models.NhaCungCap>? NhaCungCap { get; set; }

        public DbSet<webbanhang.Models.ThongSo>? ThongSo { get; set; }

        public DbSet<webbanhang.Models.MoTa>? MoTa { get; set; }
    }
}
