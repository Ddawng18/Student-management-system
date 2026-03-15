using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class LopHocService
    {
        // Encapsulation: Repository được đóng gói trong Service
        private readonly ILopHocRepository _lopHocRepository;

        public LopHocService(ILopHocRepository lopHocRepository)
        {
            _lopHocRepository = lopHocRepository;
        }

        // Thêm lớp học
        public void ThemLopHoc(LopHoc lopHoc)
        {
            _lopHocRepository.ThemLopHoc(lopHoc);
        }

        // Lấy tất cả lớp học
        public List<LopHoc> LayTatCaLopHoc()
        {
            return _lopHocRepository.LayTatCaLopHoc();
        }

        // Lấy lớp học theo ID
        public LopHoc LayLopHocTheoId(int id)
        {
            return _lopHocRepository.LayLopHocTheoId(id);
        }

        // Cập nhật lớp học
        public void CapNhatLopHoc(LopHoc lopHoc)
        {
            _lopHocRepository.CapNhatLopHoc(lopHoc);
        }

        // Xóa lớp học
        public void XoaLopHoc(int id)
        {
            _lopHocRepository.XoaLopHoc(id);
        }
    }
}