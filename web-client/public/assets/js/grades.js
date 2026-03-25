document.addEventListener("DOMContentLoaded", async () => {
  const gradeBtn = document.querySelector(".btn.btn-brand");

  if (gradeBtn) {
    gradeBtn.addEventListener("click", async () => {
      const dangKyHocId = Number(prompt("DangKyHocId:", "1"));
      const diem = Number(prompt("Diem (0-10):", "8"));

      if (!dangKyHocId || Number.isNaN(diem)) {
        return;
      }

      try {
        await window.apiFetch(`/enrollments/${dangKyHocId}/score`, {
          method: "PUT",
          body: JSON.stringify({ diem })
        });
        showPageMessage("Nhap diem thanh cong.", "success");
        window.location.reload();
      } catch (error) {
        showPageMessage(`Nhap diem that bai: ${error.message}`, "danger");
      }
    });
  }

  try {
    const enrollments = await window.apiFetch("/enrollments");
    const rows = enrollments.map((x) => [x.sinhVienHoTen, x.tenMonHoc, x.diem ?? "-", x.ketQua]);
    renderTableRows("gradesTable", rows);
  } catch (error) {
    showPageMessage(`Khong tai duoc bang diem: ${error.message}`, "danger");
  }
});
