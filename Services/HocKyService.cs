using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class HocKyService
    {
        private readonly IHocKyRepository _hocKyRepository;

        public HocKyService(IHocKyRepository hocKyRepository)
        {
            _hocKyRepository = hocKyRepository;
        }

        public void ThemHocKy(string tenHocKy, int namHoc)
        {
            var hocKy = new HocKy(0, tenHocKy, namHoc);
            _hocKyRepository.ThemHocKy(hocKy);
        }

        public List<HocKy> LayTatCaHocKy()
        {
            return _hocKyRepository.LayTatCaHocKy();
        }

        public HocKy LayHocKyTheoId(int id)
        {
            return _hocKyRepository.LayHocKyTheoId(id);
        }

        public void CapNhatHocKy(int id, string tenHocKy, int namHoc)
        {
            var hocKy = new HocKy(id, tenHocKy, namHoc);
            _hocKyRepository.CapNhatHocKy(hocKy);
        }

        public void XoaHocKy(int id)
        {
            _hocKyRepository.XoaHocKy(id);
        }
    }
}