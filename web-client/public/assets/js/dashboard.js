document.addEventListener("DOMContentLoaded", async () => {
  const state = {
    allEnrollments: [],
    selectedSemesterId: "all",
    semesterLabelById: new Map()
  };

  function setText(id, value) {
    const el = document.getElementById(id);
    if (el) {
      el.textContent = value;
    }
  }

  function setKpiBadge(id, value, tone) {
    const el = document.getElementById(id);
    if (!el) {
      return;
    }

    el.innerHTML = `<span class="kpi-value-badge kpi-value-${tone}">${escapeHtml(value)}</span>`;
  }

  function setPendingBadge(value) {
    const el = document.getElementById("kpi-pending-grades");
    if (!el) {
      return;
    }

    const tone = value > 0 ? "warning" : "success";
    const note = value > 0 ? "Cần nhập điểm" : "Tất cả đã hoàn tất";
    el.innerHTML = `<span class="kpi-value-badge kpi-value-${tone}">${value}</span><small class="kpi-inline-note">${note}</small>`;
  }

  function getSelectedSemesterLabel() {
    if (state.selectedSemesterId === "all") {
      return "";
    }

    return state.semesterLabelById.get(Number(state.selectedSemesterId)) || "";
  }

  function goToEnrollmentList(extraQuery = "") {
    const parts = [getSelectedSemesterLabel(), extraQuery].filter(Boolean);
    const query = parts.join(" ").trim();
    window.location.href = query ? `enrollments.html?q=${encodeURIComponent(query)}` : "enrollments.html";
  }

  function getFilteredEnrollments() {
    if (state.selectedSemesterId === "all") {
      return state.allEnrollments;
    }

    const selected = Number(state.selectedSemesterId);
    return state.allEnrollments.filter((x) => Number(x.hocKyId) === selected);
  }

  function renderTopCourses(enrollments) {
    const host = document.getElementById("topCourses");
    if (!host) {
      return;
    }

    const map = new Map();
    enrollments.forEach((x) => {
      const key = x.tenMonHoc || "(Chưa rõ)";
      map.set(key, (map.get(key) || 0) + 1);
    });

    const top = [...map.entries()]
      .sort((a, b) => b[1] - a[1])
      .slice(0, 6);

    if (top.length === 0) {
      host.innerHTML = '<li class="text-muted">Chưa có dữ liệu đăng ký.</li>';
      return;
    }

    const max = Math.max(1, ...top.map(([, count]) => count));

    host.innerHTML = top
      .map(([name, count]) => `
        <li class="top-course-item">
          <div class="top-course-meta">
            <span class="top-course-name">${escapeHtml(name)}</span>
            <div class="top-course-progress"><span style="width:${Math.max(10, Math.round((count / max) * 100))}%"></span></div>
          </div>
          <span class="top-course-count">${count} lượt</span>
        </li>
      `)
      .join("");
  }

  function renderEnrollmentTrend(enrollments) {
    const host = document.getElementById("enrollmentTrendChart");
    if (!host) {
      return;
    }

    const today = new Date();
    const days = [];
    for (let i = 6; i >= 0; i -= 1) {
      const d = new Date(today);
      d.setDate(today.getDate() - i);
      const iso = d.toISOString().slice(0, 10);
      days.push({
        iso,
        label: `${String(d.getDate()).padStart(2, "0")}/${String(d.getMonth() + 1).padStart(2, "0")}`,
        count: 0
      });
    }

    const dayMap = new Map(days.map((x) => [x.iso, x]));
    enrollments.forEach((x) => {
      const iso = String(x.ngayDangKy || "").slice(0, 10);
      const item = dayMap.get(iso);
      if (item) {
        item.count += 1;
      }
    });

    const max = Math.max(1, ...days.map((x) => x.count));
    const total = days.reduce((sum, x) => sum + x.count, 0);

    if (total === 0) {
      host.innerHTML = `
        <div class="trend-empty-state">
          <i class="fas fa-chart-line"></i>
          <p>Chưa có đăng ký mới trong 7 ngày gần nhất</p>
          <small>Dữ liệu sẽ hiển thị ngay khi có phát sinh đăng ký.</small>
        </div>
      `;
      return;
    }

    host.innerHTML = days
      .map((x) => {
        const height = Math.max(8, Math.round((x.count / max) * 140));
        return `
          <button type="button" class="trend-col js-trend-drill" data-date="${x.iso}">
            <span class="trend-count">${x.count}</span>
            <div class="trend-bar-wrap">
              <div class="trend-bar" style="height:${height}px"></div>
            </div>
            <span class="trend-label">${x.label}</span>
          </button>
        `;
      })
      .join("");

    host.querySelectorAll(".js-trend-drill").forEach((button) => {
      button.addEventListener("click", () => {
        goToEnrollmentList(button.dataset.date || "");
      });
    });
  }

  function renderRecentEnrollments(enrollments) {
    const scoreCell = (value) => {
      if (value === null || value === undefined) {
        return '<span class="score-pill score-pending">Chưa có điểm</span>';
      }

      const num = Number(value);
      const cls = Number.isFinite(num) && num >= 5 ? "score-pass" : "score-fail";
      return `<span class="score-pill ${cls}">${escapeHtml(num.toFixed(1))}</span>`;
    };

    const resultCell = (value) => {
      const text = String(value || "-");
      const key = text.toLowerCase();
      let cls = "result-neutral";
      if (key.includes("dat") || key.includes("pass")) {
        cls = "result-pass";
      } else if (key.includes("truot") || key.includes("rot") || key.includes("fail")) {
        cls = "result-fail";
      }

      return `<span class="result-pill ${cls}">${escapeHtml(text)}</span>`;
    };

    const latest = [...enrollments]
      .sort((a, b) => new Date(b.ngayDangKy) - new Date(a.ngayDangKy))
      .slice(0, 8)
      .map((x) => [
        escapeHtml(x.sinhVienHoTen),
        escapeHtml(x.tenMonHoc),
        escapeHtml(x.tenHocKy),
        scoreCell(x.diem),
        resultCell(x.ketQua)
      ]);

    renderTableRowsHtml("recentEnrollments", latest);
  }

  function refreshDashboardByFilter() {
    const filtered = getFilteredEnrollments();
    const scored = filtered.filter((x) => x.diem !== null && x.diem !== undefined);
    const passCount = scored.filter((x) => Number(x.diem) >= 5).length;
    const passRate = scored.length ? Math.round((passCount / scored.length) * 100) : 0;
    const avgScore = scored.length
      ? (scored.reduce((sum, x) => sum + Number(x.diem), 0) / scored.length).toFixed(2)
      : "-";
    const pendingGrades = filtered.length - scored.length;

    setKpiBadge("kpi-pass-rate", `${passRate}%`, passRate >= 70 ? "success" : "danger");

    const avgScoreNumber = Number(avgScore);
    const avgTone = avgScore === "-" ? "neutral" : avgScoreNumber >= 7 ? "success" : avgScoreNumber >= 5 ? "warning" : "danger";
    setKpiBadge("kpi-avg-score", String(avgScore), avgTone);

    setPendingBadge(pendingGrades);
    renderTopCourses(filtered);
    renderEnrollmentTrend(filtered);
    renderRecentEnrollments(filtered);
  }

  function wireSemesterFilter(semesters) {
    const select = document.getElementById("dashboardSemesterFilter");
    if (!select) {
      return;
    }

    semesters.forEach((x) => {
      state.semesterLabelById.set(Number(x.hocKyId), x.tenHocKy || `Học kỳ ${x.hocKyId}`);
    });

    select.innerHTML = `
      <option value="all">Tất cả học kỳ</option>
      ${semesters
        .map((x) => `<option value="${x.hocKyId}">${escapeHtml(x.maHocKy)} - ${escapeHtml(x.tenHocKy || `Học kỳ ${x.hocKyId}`)}</option>`)
        .join("")}
    `;

    select.addEventListener("change", () => {
      state.selectedSemesterId = select.value;
      refreshDashboardByFilter();
    });
  }

  function wireKpiDrilldown() {
    document.querySelectorAll(".kpi-drill").forEach((card) => {
      const handleGo = () => {
        const target = card.dataset.target;
        if (!target) {
          return;
        }

        if (target === "enrollments") {
          goToEnrollmentList();
          return;
        }

        window.location.href = target;
      };

      card.tabIndex = 0;
      card.setAttribute("role", "button");
      card.addEventListener("click", handleGo);
      card.addEventListener("keydown", (event) => {
        if (event.key === "Enter" || event.key === " ") {
          event.preventDefault();
          handleGo();
        }
      });
    });
  }

  function wireUserActions() {
    const host = document.getElementById("dashboardUserActions");
    const session = window.getSession ? window.getSession() : null;
    if (!host || !session) {
      return;
    }

    const userLabel = session.hoTen || session.email || "Admin";
    const initials = userLabel
      .split(/\s+/)
      .filter(Boolean)
      .slice(0, 2)
      .map((x) => x[0]?.toUpperCase() || "")
      .join("") || "AD";

    host.innerHTML = `
      <div class="dropdown user-menu-dropdown">
        <button class="btn user-menu-toggle dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false">
          <span class="user-avatar">${escapeHtml(initials)}</span>
          <span class="user-chip">${escapeHtml(userLabel)}</span>
        </button>
        <div class="dropdown-menu dropdown-menu-right">
          <span class="dropdown-item-text text-muted small">Quản trị viên</span>
          <button id="dashboardThemeToggleBtn" class="dropdown-item" type="button"><i class="fas fa-circle-half-stroke mr-2"></i>Đổi giao diện</button>
          <div class="dropdown-divider"></div>
          <button id="dashboardLogoutBtn" class="dropdown-item text-danger" type="button"><i class="fas fa-right-from-bracket mr-2"></i>Đăng xuất</button>
        </div>
      </div>
    `;

    window.wireThemeToggle?.("dashboardThemeToggleBtn");

    document.getElementById("dashboardLogoutBtn")?.addEventListener("click", () => {
      if (window.clearSession) {
        window.clearSession();
      }

      window.location.href = "/login.html";
    });
  }

  wireUserActions();

  try {
    const [overview, enrollments, semesters] = await Promise.all([
      window.apiFetch("/dashboard/overview"),
      window.apiFetch("/enrollments"),
      window.apiFetch("/semesters")
    ]);

    state.allEnrollments = enrollments;

    const stats = {
      departments: overview.totalDepartments,
      classes: overview.totalClasses,
      students: overview.totalStudents,
      lecturers: overview.totalLecturers,
      courses: overview.totalCourses,
      enrollments: overview.totalEnrollments
    };

    Object.entries(stats).forEach(([key, value]) => {
      setText(`kpi-${key}`, value);
    });

    wireSemesterFilter(semesters);
    wireKpiDrilldown();
    refreshDashboardByFilter();
  } catch (error) {
    showPageMessage(`Không tải được bảng điều khiển: ${error.message}`, "danger");
  }
});
