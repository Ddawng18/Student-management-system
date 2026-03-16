using System;

namespace StudentManagementSystem.Models
{
    public class Nguoi
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }

        public Nguoi(int id, string hoTen, string email)
        {
            Id = id;
            HoTen = hoTen;
            Email = email;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Họ tên: {HoTen}, Email: {Email}";
        }
    }
}