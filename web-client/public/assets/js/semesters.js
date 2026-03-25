document.addEventListener("DOMContentLoaded", async () => {
  try {
    const semesters = await window.apiFetch("/semesters");
    const rows = semesters.map((x) => [x.maHocKy, x.tenHocKy, x.namHoc, formatDate(x.ngayBatDau), formatDate(x.ngayKetThuc)]);
    renderTableRows("semestersTable", rows);
  } catch (error) {
    showPageMessage(`Khong tai duoc danh sach hoc ky: ${error.message}`, "danger");
  }
});
