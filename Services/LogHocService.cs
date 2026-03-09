using System.Collections.Generic;
using StudentManagementSystem.Models;
using StudentManagementSystem.Repositories;

namespace StudentManagementSystem.Services
{
    public class LogHocService
    {
        private readonly ILogHocRepository _logHocRepository;

        public LogHocService(ILogHocRepository logHocRepository)
        {
            _logHocRepository = logHocRepository;
        }

        public void ThemLogHoc(string tenLop)
        {
            var logHoc = new LogHoc(0, tenLop);
            _logHocRepository.ThemLogHoc(logHoc);
        }

        public List<LogHoc> LayTatCaLogHoc()
        {
            return _logHocRepository.LayTatCaLogHoc();
        }

        public LogHoc LayLogHocTheoId(int id)
        {
            return _logHocRepository.LayLogHocTheoId(id);
        }

        public void CapNhatLogHoc(int id, string tenLop)
        {
            var logHoc = new LogHoc(id, tenLop);
            _logHocRepository.CapNhatLogHoc(logHoc);
        }

        public void XoaLogHoc(int id)
        {
            _logHocRepository.XoaLogHoc(id);
        }
    }
}