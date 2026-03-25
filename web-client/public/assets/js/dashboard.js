document.addEventListener("DOMContentLoaded", async () => {
  try {
    const [overview, enrollments] = await Promise.all([
      window.apiFetch("/dashboard/overview"),
      window.apiFetch("/enrollments")
    ]);

    const stats = {
      departments: overview.totalDepartments,
      classes: overview.totalClasses,
      students: overview.totalStudents,
      lecturers: overview.totalLecturers,
      courses: overview.totalCourses,
      enrollments: overview.totalEnrollments
    };

    Object.entries(stats).forEach(([key, value]) => {
      const el = document.getElementById(`kpi-${key}`);
      if (el) {
        el.textContent = value;
      }
    });

    const latest = [...enrollments]
      .sort((a, b) => new Date(b.ngayDangKy) - new Date(a.ngayDangKy))
      .slice(0, 8)
      .map((x) => [x.sinhVienHoTen, x.tenMonHoc, x.tenHocKy, x.diem ?? "-", x.ketQua]);

    renderTableRows("recentEnrollments", latest);
  } catch (error) {
    showPageMessage(`Khong tai duoc dashboard: ${error.message}`, "danger");
  }
});
