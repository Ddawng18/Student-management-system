document.addEventListener("DOMContentLoaded", async () => {
  const registerBtn = document.querySelector(".btn.btn-brand");

  if (registerBtn) {
    registerBtn.addEventListener("click", async () => {
      const sinhVienId = Number(prompt("SinhVienId:", "1"));
      const monHocId = Number(prompt("MonHocId:", "1"));
      const hocKyId = Number(prompt("HocKyId:", "1"));

      if (!sinhVienId || !monHocId || !hocKyId) {
        return;
      }

      try {
        await window.apiFetch("/enrollments", {
          method: "POST",
          body: JSON.stringify({ sinhVienId, monHocId, hocKyId })
        });
        showPageMessage("Dang ky mon hoc thanh cong.", "success");
        window.location.reload();
      } catch (error) {
        showPageMessage(`Dang ky that bai: ${error.message}`, "danger");
      }
    });
  }

  try {
    const enrollments = await window.apiFetch("/enrollments");
    const rows = enrollments.map((x) => [x.sinhVienHoTen, x.tenMonHoc, x.tenHocKy, formatDate(x.ngayDangKy)]);
    renderTableRows("enrollmentsTable", rows);
  } catch (error) {
    showPageMessage(`Khong tai duoc dang ky hoc: ${error.message}`, "danger");
  }
});
