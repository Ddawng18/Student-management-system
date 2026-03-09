using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class LopHocService
    {
        private readonly ILopHocRepository _lopHocRepository;

        public LopHocService(ILopHocRepository lopHocRepository)
        {
            _lopHocRepository = lopHocRepository;
        }

        public void ThemLopHoc(string tenLop, int maKhoa = 0)
        {
            var lopHoc = new LopHoc(0, tenLop, maKhoa);
            _lopHocRepository.ThemLopHoc(lopHoc);
        }

        public List<LopHoc> LayTatCaLopHoc()
        {
            return _lopHocRepository.LayTatCaLopHoc();
        }

        public LopHoc LayLopHocTheoId(int id)
        {
            return _lopHocRepository.LayLopHocTheoId(id);
        }

        public void CapNhatLopHoc(int id, string tenLop, int maKhoa = 0)
        {
            var lopHoc = new LopHoc(id, tenLop, maKhoa);
            _lopHocRepository.CapNhatLopHoc(lopHoc);
        }

        public void XoaLopHoc(int id)
        {
            _lopHocRepository.XoaLopHoc(id);
        }
    }
}