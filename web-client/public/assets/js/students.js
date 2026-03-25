document.addEventListener("DOMContentLoaded", async () => {
  const createBtn = document.querySelector(".btn.btn-brand");

  if (createBtn) {
    createBtn.addEventListener("click", async () => {
      const maSinhVien = prompt("Ma sinh vien:");
      if (!maSinhVien) return;
      const hoTen = prompt("Ho ten:");
      if (!hoTen) return;
      const email = prompt("Email:");
      if (!email) return;
      const ngaySinh = prompt("Ngay sinh (YYYY-MM-DD):", "2004-01-01");
      if (!ngaySinh) return;

      try {
        await window.apiFetch("/students", {
          method: "POST",
          body: JSON.stringify({ maSinhVien, hoTen, email, ngaySinh, lopHocId: null })
        });
        showPageMessage("Them sinh vien thanh cong.", "success");
        window.location.reload();
      } catch (error) {
        showPageMessage(`Them sinh vien that bai: ${error.message}`, "danger");
      }
    });
  }

  try {
    const students = await window.apiFetch("/students");
    const rows = students.map((x) => [x.maSinhVien, x.hoTen, x.email, x.lopHocId ?? "-", formatDate(x.ngaySinh)]);
    renderTableRows("studentsTable", rows);
  } catch (error) {
    showPageMessage(`Khong tai duoc danh sach sinh vien: ${error.message}`, "danger");
  }
});
