using System;
using System.Collections.Generic;

namespace StudentManagementSystem.Services
{
    public abstract class DichVuQuanLyCoSo<T>
    {
        private readonly List<T> _duLieu;

        protected DichVuQuanLyCoSo()
        {
            _duLieu = new List<T>();
        }

        protected List<T> DuLieuNoiBo
        {
            get { return _duLieu; }
        }

        protected abstract string LayKhoa(T doiTuong);

        public virtual void Them(T doiTuong)
        {
            if (doiTuong == null)
            {
                throw new ArgumentNullException(nameof(doiTuong));
            }

            string khoa = LayKhoa(doiTuong);
            T? daTonTai = TimNoiBoTheoMa(khoa);

            if (daTonTai != null)
            {
                throw new InvalidOperationException("Đối tượng với mã " + khoa + " đã tồn tại.");
            }

            _duLieu.Add(doiTuong);
        }

        public virtual void CapNhat(T doiTuong)
        {
            if (doiTuong == null)
            {
                throw new ArgumentNullException(nameof(doiTuong));
            }

            string khoa = LayKhoa(doiTuong);
            int index = TimChiSoNoiBo(khoa);

            if (index < 0)
            {
                throw new InvalidOperationException("Không tìm thấy đối tượng với mã " + khoa + " để cập nhật.");
            }

            _duLieu[index] = doiTuong;
        }

        public virtual void XoaTheoMa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
            {
                throw new ArgumentException("Mã không được để trống.", nameof(ma));
            }

            int index = TimChiSoNoiBo(ma.Trim());

            if (index < 0)
            {
                return;
            }

            _duLieu.RemoveAt(index);
        }

        public IReadOnlyList<T> LayTatCa()
        {
            List<T> banSao = new List<T>();
            int index = 0;

            while (index < _duLieu.Count)
            {
                banSao.Add(_duLieu[index]);
                index = index + 1;
            }

            return banSao.AsReadOnly();
        }

        public T? TimTheoMa(string ma)
        {
            if (string.IsNullOrWhiteSpace(ma))
            {
                return default;
            }

            return TimNoiBoTheoMa(ma.Trim());
        }

        protected int TimChiSoNoiBo(string ma)
        {
            int index = 0;

            while (index < _duLieu.Count)
            {
                T doiTuong = _duLieu[index];

                if (string.Equals(LayKhoa(doiTuong), ma, StringComparison.OrdinalIgnoreCase))
                {
                    return index;
                }

                index = index + 1;
            }

            return -1;
        }

        protected T? TimNoiBoTheoMa(string ma)
        {
            int index = TimChiSoNoiBo(ma);

            if (index < 0)
            {
                return default;
            }

            return _duLieu[index];
        }
    }
}
