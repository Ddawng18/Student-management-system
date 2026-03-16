using System;

namespace StudentManagementSystem.Models
{
    // Abstraction: lớp trừu tượng mô tả thông tin chung của mọi người dùng.
    public abstract class Nguoi
    {
        public string Id { get; protected set; }
        public string HoTen { get; protected set; }
        public string Email { get; protected set; }

        protected Nguoi(string id, string hoTen, string email)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Id không được để trống.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(hoTen))
            {
                throw new ArgumentException("Họ tên không được để trống.", nameof(hoTen));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email không được để trống.", nameof(email));
            }

            Id = id;
            HoTen = hoTen;
            Email = email;
        }

        public abstract string LayVaiTro();

        public virtual string HienThiThongTin()
        {
            return $"ID: {Id}, Họ tên: {HoTen}, Email: {Email}";
        }
    }
}
