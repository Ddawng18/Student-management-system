document.addEventListener("DOMContentLoaded", async () => {
  try {
    const courses = await window.apiFetch("/courses");
    const rows = courses.map((x) => [x.maMonHoc, x.tenMon, x.soTinChi, x.giangVienGiangDayId ?? "-", x.hocKyId ?? "-"]);
    renderTableRows("coursesTable", rows);
  } catch (error) {
    showPageMessage(`Khong tai duoc danh sach mon hoc: ${error.message}`, "danger");
  }
});
