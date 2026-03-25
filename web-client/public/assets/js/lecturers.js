document.addEventListener("DOMContentLoaded", async () => {
  try {
    const lecturers = await window.apiFetch("/lecturers");
    const rows = lecturers.map((x) => [x.maGiangVien, x.hoTen, x.email, x.khoaId ?? "-"]);
    renderTableRows("lecturersTable", rows);
  } catch (error) {
    showPageMessage(`Khong tai duoc danh sach giang vien: ${error.message}`, "danger");
  }
});
