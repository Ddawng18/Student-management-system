using System;

namespace StudentManagementSystem.Models
{
    public abstract class Nguoi
    {
        private string _id;
        private string _hoTen;
        private string _email;

        public string Id
        {
            get { return _id; }
        }

        public string HoTen
        {
            get { return _hoTen; }
        }

        public string Email
        {
            get { return _email; }
        }

        protected Nguoi(string id, string hoTen, string email)
        {
            _id = KiemTraChuoiBatBuoc(id, nameof(id), "Id không được để trống.");
            _hoTen = KiemTraChuoiBatBuoc(hoTen, nameof(hoTen), "Họ tên không được để trống.");
            _email = KiemTraChuoiBatBuoc(email, nameof(email), "Email không được để trống.");
        }

        public abstract string LayVaiTro();

        protected void CapNhatHoTen(string hoTenMoi)
        {
            _hoTen = KiemTraChuoiBatBuoc(hoTenMoi, nameof(hoTenMoi), "Họ tên không được để trống.");
        }

        protected void CapNhatEmail(string emailMoi)
        {
            _email = KiemTraChuoiBatBuoc(emailMoi, nameof(emailMoi), "Email không được để trống.");
        }

        public virtual string HienThiThongTin()
        {
            return "ID: " + _id + ", Họ tên: " + _hoTen + ", Email: " + _email;
        }

        private static string KiemTraChuoiBatBuoc(string giaTri, string thamSo, string thongDiep)
        {
            if (string.IsNullOrWhiteSpace(giaTri))
            {
                throw new ArgumentException(thongDiep, thamSo);
            }

            return giaTri.Trim();
        }
    }
}
