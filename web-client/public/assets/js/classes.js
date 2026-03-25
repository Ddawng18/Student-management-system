document.addEventListener("DOMContentLoaded", async () => {
  try {
    const classes = await window.apiFetch("/classes");
    const rows = classes.map((x) => [x.maLop, x.tenLop, x.khoaId, x.coVanHocTapId ?? "-"]);
    renderTableRows("classesTable", rows);
  } catch (error) {
    showPageMessage(`Khong tai duoc danh sach lop hoc: ${error.message}`, "danger");
  }
});
