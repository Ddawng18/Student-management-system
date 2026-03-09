using System.Collections.Generic;
using System.Linq;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Repositories
{
    public class LogHocRepository : ILogHocRepository
    {
        private readonly List<LogHoc> _logHocs = new List<LogHoc>();
        private int _nextId = 1;

        public void ThemLogHoc(LogHoc logHoc)
        {
            logHoc.MaLop = _nextId++;
            _logHocs.Add(logHoc);
        }

        public List<LogHoc> LayTatCaLogHoc()
        {
            return _logHocs;
        }

        public LogHoc LayLogHocTheoId(int id)
        {
            return _logHocs.FirstOrDefault(l => l.MaLop == id);
        }

        public void CapNhatLogHoc(LogHoc logHoc)
        {
            var existing = LayLogHocTheoId(logHoc.MaLop);
            if (existing != null)
            {
                existing.TenLop = logHoc.TenLop;
            }
        }

        public void XoaLogHoc(int id)
        {
            var logHoc = LayLogHocTheoId(id);
            if (logHoc != null)
            {
                _logHocs.Remove(logHoc);
            }
        }
    }
}