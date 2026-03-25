const NAV_ITEMS = [
  { page: "dashboard.html", icon: "fas fa-gauge", label: "Dashboard" },
  { page: "students.html", icon: "fas fa-user-graduate", label: "Sinh vien" },
  { page: "lecturers.html", icon: "fas fa-chalkboard-user", label: "Giang vien" },
  { page: "departments.html", icon: "fas fa-building", label: "Khoa" },
  { page: "classes.html", icon: "fas fa-users-rectangle", label: "Lop hoc" },
  { page: "semesters.html", icon: "fas fa-calendar-days", label: "Hoc ky" },
  { page: "courses.html", icon: "fas fa-book", label: "Mon hoc" },
  { page: "enrollments.html", icon: "fas fa-clipboard-check", label: "Dang ky hoc" },
  { page: "grades.html", icon: "fas fa-pen-to-square", label: "Nhap diem" },
  { page: "results.html", icon: "fas fa-award", label: "Ket qua hoc tap" }
];

function renderPageShell(config) {
  const nav = NAV_ITEMS.map(
    (item) => `<li class="nav-item"><a data-page="${item.page}" href="${item.page}" class="nav-link"><i class="nav-icon ${item.icon}"></i><p>${item.label}</p></a></li>`
  ).join("");

  const headActions = config.buttonText
    ? `<button class="btn btn-brand">${config.buttonText}</button>`
    : "";

  const tableHeaders = config.columns.map((c) => `<th>${c}</th>`).join("");

  document.body.innerHTML = `
    <div class="wrapper">
      <nav class="main-header navbar navbar-expand navbar-white navbar-light">
        <ul class="navbar-nav"><li class="nav-item"><a class="nav-link" data-widget="pushmenu" href="#"><i class="fas fa-bars"></i></a></li></ul>
        <span class="brand-title">Student Management System</span>
      </nav>
      <aside class="main-sidebar sidebar-dark-primary elevation-4">
        <a href="dashboard.html" class="brand-link"><span class="brand-text font-weight-light brand-title">SMS Portal</span></a>
        <div class="sidebar"><nav><ul class="nav nav-pills nav-sidebar flex-column">${nav}</ul></nav></div>
      </aside>
      <div class="content-wrapper">
        <section class="content pt-3">
          <div class="container-fluid">
            <div class="page-head"><h1>${config.title}</h1>${headActions}</div>
            <div class="card surface-card">
              <div class="card-header">${config.cardTitle}</div>
              <div class="card-body table-responsive p-0">
                <table class="table table-hover mb-0">
                  <thead><tr>${tableHeaders}</tr></thead>
                  <tbody id="${config.tableId}"></tbody>
                </table>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>`;
}

window.renderPageShell = renderPageShell;
