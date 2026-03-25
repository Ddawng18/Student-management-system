const NAV_ITEMS = [
  { page: "dashboard.html", icon: "fas fa-gauge", label: "Dashboard" },
  { page: "students.html", icon: "fas fa-user-graduate", label: "Sinh viên" },
  { page: "lecturers.html", icon: "fas fa-chalkboard-user", label: "Giảng viên" },
  { page: "departments.html", icon: "fas fa-building", label: "Khoa" },
  { page: "classes.html", icon: "fas fa-users-rectangle", label: "Lớp học" },
  { page: "semesters.html", icon: "fas fa-calendar-days", label: "Học kỳ" },
  { page: "courses.html", icon: "fas fa-book", label: "Môn học" },
  { page: "enrollments.html", icon: "fas fa-clipboard-check", label: "Đăng ký học" },
  { page: "grades.html", icon: "fas fa-pen-to-square", label: "Nhập điểm" },
  { page: "results.html", icon: "fas fa-award", label: "Kết quả học tập" }
];

const PAGE_ICON_BY_ID = {
  studentsTable: "fas fa-user-graduate",
  lecturersTable: "fas fa-chalkboard-user",
  departmentsTable: "fas fa-building",
  classesTable: "fas fa-users-rectangle",
  semestersTable: "fas fa-calendar-days",
  coursesTable: "fas fa-book",
  enrollmentsTable: "fas fa-clipboard-check",
  gradesTable: "fas fa-pen-to-square",
  resultsTable: "fas fa-award"
};

function renderPageShell(config) {
  const session = window.getSession ? window.getSession() : null;
  const userLabel = session?.hoTen || session?.email || "Admin";
  const initials = userLabel
    .split(/\s+/)
    .filter(Boolean)
    .slice(0, 2)
    .map((x) => x[0]?.toUpperCase() || "")
    .join("") || "AD";
  const nav = NAV_ITEMS.map(
    (item) => `<li class="nav-item"><a data-page="${item.page}" href="${item.page}" class="nav-link"><i class="nav-icon ${item.icon}"></i><p>${item.label}</p></a></li>`
  ).join("");

  const searchBox = config.tableId
    ? `<input id="tableSearchInput" data-table-id="${config.tableId}" class="form-control page-search-input" placeholder="Tìm nhanh trong bảng..." />`
    : "";
  const extraTools = config.extraToolsHtml || "";

  const actionButton = config.buttonText
    ? `<button class="btn btn-brand"><i class="fas fa-plus mr-1"></i>${config.buttonText}</button>`
    : "";

  const headActions = searchBox || extraTools || actionButton
    ? `<div class="page-tools">${searchBox}${extraTools}${actionButton}</div>`
    : "";

  const tableHeaders = config.columns.map((c) => `<th>${c}</th>`).join("");
  const pageIcon = PAGE_ICON_BY_ID[config.tableId] || "fas fa-table";
  const pageSubtitle = "Giao diện quản trị trực quan, thao tác nhanh.";

  const userInfo = session
    ? `
      <div class="dropdown user-menu-dropdown">
        <button class="btn user-menu-toggle dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false">
          <span class="user-avatar">${initials}</span>
          <span class="user-chip">${userLabel}</span>
        </button>
        <div class="dropdown-menu dropdown-menu-right">
          <span class="dropdown-item-text text-muted small">Tài khoản quản trị</span>
          <button id="themeToggleBtn" class="dropdown-item" type="button"><i class="fas fa-circle-half-stroke mr-2"></i>Đổi giao diện</button>
          <div class="dropdown-divider"></div>
          <button id="logoutBtn" class="dropdown-item text-danger" type="button"><i class="fas fa-right-from-bracket mr-2"></i>Đăng xuất</button>
        </div>
      </div>`
    : "";

  document.body.innerHTML = `
    <div class="wrapper">
      <nav class="main-header navbar navbar-expand navbar-white navbar-light">
        <ul class="navbar-nav"><li class="nav-item"><a class="nav-link" data-widget="pushmenu" href="#"><i class="fas fa-bars"></i></a></li></ul>
        <a href="index.html" class="brand-title brand-home-link">Hệ thống quản lý sinh viên</a>
        <div class="ml-auto d-flex align-items-center">${userInfo}</div>
      </nav>
      <aside class="main-sidebar sidebar-dark-primary elevation-4">
        <a href="dashboard.html" class="brand-link"><span class="brand-text font-weight-light brand-title">Cổng SMS</span></a>
        <div class="sidebar"><nav><ul class="nav nav-pills nav-sidebar flex-column">${nav}</ul></nav></div>
      </aside>
      <div class="content-wrapper">
        <section class="content pt-3">
          <div class="container-fluid">
            <div class="page-head"><div class="page-title-wrap"><span class="page-title-icon"><i class="${pageIcon}"></i></span><div><h1>${config.title}</h1><p class="page-title-subtitle">${pageSubtitle}</p></div></div>${headActions}</div>
            <div class="card surface-card">
              <div class="card-header card-head-inline"><span class="card-title-main"><i class="${pageIcon}"></i>${config.cardTitle}</span><p class="card-subtitle">Dữ liệu được đồng bộ theo thời gian thực</p></div>
              <div class="card-body table-responsive p-0">
                <table class="table table-hover mb-0 data-table">
                  <thead><tr>${tableHeaders}</tr></thead>
                  <tbody id="${config.tableId}"></tbody>
                </table>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>`;

  const logoutBtn = document.getElementById("logoutBtn");
  if (logoutBtn) {
    logoutBtn.addEventListener("click", () => {
      if (window.clearSession) {
        window.clearSession();
      }

      window.location.href = "/login.html";
    });
  }

  window.wireThemeToggle?.("themeToggleBtn");
}

window.renderPageShell = renderPageShell;
