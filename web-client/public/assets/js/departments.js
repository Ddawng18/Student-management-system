document.addEventListener("DOMContentLoaded", async () => {
  try {
    const departments = await window.apiFetch("/departments");
    const rows = departments.map((x) => [x.maKhoa, x.tenKhoa, x.truongKhoaId ?? "-"]);
    renderTableRows("departmentsTable", rows);
  } catch (error) {
    showPageMessage(`Khong tai duoc danh sach khoa: ${error.message}`, "danger");
  }
});
