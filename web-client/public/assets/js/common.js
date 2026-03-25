function setActiveNav() {
  const path = window.location.pathname.split("/").pop();
  document.querySelectorAll(".nav-link[data-page]").forEach((el) => {
    if (el.getAttribute("data-page") === path) {
      el.classList.add("active");
    }
  });
}

function escapeHtml(value) {
  return String(value)
    .replaceAll("&", "&amp;")
    .replaceAll("<", "&lt;")
    .replaceAll(">", "&gt;")
    .replaceAll('"', "&quot;")
    .replaceAll("'", "&#039;");
}

function renderTableRows(tbodyId, rows) {
  const tbody = document.getElementById(tbodyId);
  if (!tbody) {
    return;
  }

  const columnCount = tbody.closest("table")?.querySelectorAll("thead th").length || 1;

  if (!rows.length) {
    tbody.innerHTML = `<tr class="table-empty-row"><td class="table-empty-cell" colspan="${columnCount}">Chưa có dữ liệu.</td></tr>`;
    return;
  }

  tbody.innerHTML = rows
    .map((row) => `<tr>${row.map((cell) => `<td>${escapeHtml(cell ?? "")}</td>`).join("")}</tr>`)
    .join("");

  decorateTableActionButtons(tbody);
  applySearchFilterForTable(tbodyId);
  reapplyTableSort(tbody.closest("table"));
}

function renderTableRowsHtml(tbodyId, rows) {
  const tbody = document.getElementById(tbodyId);
  if (!tbody) {
    return;
  }

  const columnCount = tbody.closest("table")?.querySelectorAll("thead th").length || 1;

  if (!rows.length) {
    tbody.innerHTML = `<tr class="table-empty-row"><td class="table-empty-cell" colspan="${columnCount}">Chưa có dữ liệu.</td></tr>`;
    return;
  }

  tbody.innerHTML = rows
    .map((row) => `<tr>${row.map((cell) => `<td>${cell ?? ""}</td>`).join("")}</tr>`)
    .join("");

  decorateTableActionButtons(tbody);
  applySearchFilterForTable(tbodyId);
  reapplyTableSort(tbody.closest("table"));
}

function decorateTableActionButtons(tbody) {
  if (!tbody) {
    return;
  }

  const normalizeActionLabel = (value) =>
    String(value || "")
      .normalize("NFD")
      .replace(/[\u0300-\u036f]/g, "")
      .toLowerCase();

  tbody.querySelectorAll(".btn.btn-sm").forEach((button) => {
    if (button.dataset.iconified === "true") {
      return;
    }

    const label = (button.textContent || "").trim();
    const normalizedLabel = normalizeActionLabel(label);
    let iconClass = "fas fa-circle";
    let actionClass = "icon-neutral";

    if (/sua/.test(normalizedLabel)) {
      iconClass = "fas fa-pen";
      actionClass = "icon-edit";
    } else if (/xoa/.test(normalizedLabel)) {
      iconClass = "fas fa-trash";
      actionClass = "icon-delete";
    } else if (/xem|thong tin|chi tiet/.test(normalizedLabel)) {
      iconClass = "fas fa-eye";
      actionClass = "icon-view";
    }

    button.setAttribute("title", label || "Thao tác");
    button.setAttribute("aria-label", label || "Thao tác");
    button.classList.add("icon-btn", actionClass);
    button.innerHTML = `<i class="${iconClass}"></i>`;
    button.dataset.iconified = "true";
  });
}

function normalizeSortValue(text) {
  const value = String(text || "").trim();
  const dateLike = /^\d{4}-\d{2}-\d{2}$/.test(value);
  if (dateLike) {
    return { type: "number", value: new Date(value).getTime() };
  }

  const numberLike = value.replace(/[%\s,.]/g, "").replace(/[−-]/g, "");
  if (numberLike && /^\d+$/.test(numberLike)) {
    const normalizedNumber = Number(value.replace(/\./g, "").replace(",", ".").replace("%", ""));
    return { type: "number", value: Number.isFinite(normalizedNumber) ? normalizedNumber : 0 };
  }

  return { type: "text", value: value.toLowerCase() };
}

function sortTableByColumn(table, columnIndex, direction) {
  if (!table) {
    return;
  }

  const tbody = table.querySelector("tbody");
  if (!tbody) {
    return;
  }

  const rows = [...tbody.querySelectorAll("tr")].filter(
    (row) => !row.classList.contains("table-empty-row") && !row.classList.contains("table-filter-empty-row")
  );

  rows.sort((a, b) => {
    const aText = a.children[columnIndex]?.textContent || "";
    const bText = b.children[columnIndex]?.textContent || "";
    const aValue = normalizeSortValue(aText);
    const bValue = normalizeSortValue(bText);

    let compare = 0;
    if (aValue.type === "number" && bValue.type === "number") {
      compare = aValue.value - bValue.value;
    } else {
      compare = String(aValue.value).localeCompare(String(bValue.value), "vi", { numeric: true, sensitivity: "base" });
    }

    return direction === "asc" ? compare : -compare;
  });

  rows.forEach((row) => tbody.append(row));
  if (tbody.id) {
    applySearchFilterForTable(tbody.id);
  }
}

function updateSortHeaderState(table, columnIndex, direction) {
  if (!table) {
    return;
  }

  table.querySelectorAll("thead th").forEach((th, idx) => {
    th.classList.remove("sort-asc", "sort-desc");
    if (idx === columnIndex) {
      th.classList.add(direction === "asc" ? "sort-asc" : "sort-desc");
    }
  });
}

function reapplyTableSort(table) {
  if (!table || table.dataset.sortColumn === undefined || table.dataset.sortDirection === undefined) {
    return;
  }

  const columnIndex = Number(table.dataset.sortColumn);
  const direction = table.dataset.sortDirection;
  if (Number.isNaN(columnIndex) || !direction) {
    return;
  }

  sortTableByColumn(table, columnIndex, direction);
  updateSortHeaderState(table, columnIndex, direction);
}

function applyTableSort(tbodyId, columnIndex, direction = "asc") {
  const tbody = document.getElementById(tbodyId);
  const table = tbody?.closest("table");
  if (!table) {
    return;
  }

  table.dataset.sortColumn = String(columnIndex);
  table.dataset.sortDirection = direction;
  sortTableByColumn(table, columnIndex, direction);
  updateSortHeaderState(table, columnIndex, direction);
}

function wireDataTableSorting() {
  document.querySelectorAll(".data-table").forEach((table) => {
    const thList = [...table.querySelectorAll("thead th")];
    thList.forEach((th, idx) => {
      const label = th.textContent.trim().toLowerCase();
      const isActionCol = label.includes("thao tác") || label.includes("hành động");
      if (isActionCol) {
        return;
      }

      th.classList.add("table-sortable");
      th.tabIndex = 0;
      th.setAttribute("role", "button");

      const toggleSort = () => {
        const currentColumn = Number(table.dataset.sortColumn);
        const currentDirection = table.dataset.sortDirection || "asc";
        const nextDirection = currentColumn === idx && currentDirection === "asc" ? "desc" : "asc";

        table.dataset.sortColumn = String(idx);
        table.dataset.sortDirection = nextDirection;

        sortTableByColumn(table, idx, nextDirection);
        updateSortHeaderState(table, idx, nextDirection);
      };

      th.addEventListener("click", toggleSort);
      th.addEventListener("keydown", (event) => {
        if (event.key === "Enter" || event.key === " ") {
          event.preventDefault();
          toggleSort();
        }
      });
    });
  });
}

function applySearchFilterForTable(tbodyId) {
  const input = document.querySelector(`.page-search-input[data-table-id="${tbodyId}"]`);
  const tbody = document.getElementById(tbodyId);

  if (!input || !tbody) {
    return;
  }

  const needle = input.value.trim().toLowerCase();
  const rows = [...tbody.querySelectorAll("tr")].filter(
    (row) => !row.classList.contains("table-empty-row") && !row.classList.contains("table-filter-empty-row")
  );

  rows.forEach((row) => {
    const text = row.textContent.toLowerCase();
    row.hidden = needle ? !text.includes(needle) : false;
  });

  const oldFilterEmpty = tbody.querySelector(".table-filter-empty-row");
  if (oldFilterEmpty) {
    oldFilterEmpty.remove();
  }

  if (rows.length && rows.every((row) => row.hidden)) {
    const columnCount = tbody.closest("table")?.querySelectorAll("thead th").length || 1;
    const emptyRow = document.createElement("tr");
    emptyRow.className = "table-filter-empty-row";
    emptyRow.innerHTML = `<td class="table-empty-cell" colspan="${columnCount}">Không tìm thấy kết quả phù hợp.</td>`;
    tbody.append(emptyRow);
  }
}

function wirePageTableSearch() {
  document.querySelectorAll(".page-search-input[data-table-id]").forEach((input) => {
    input.addEventListener("input", () => {
      applySearchFilterForTable(input.dataset.tableId);
    });
  });
}

function formatDate(dateValue) {
  if (!dateValue) {
    return "-";
  }

  const date = new Date(dateValue);
  if (Number.isNaN(date.getTime())) {
    return String(dateValue);
  }

  return date.toISOString().slice(0, 10);
}

function showPageMessage(message, type = "warning") {
  const container = document.querySelector(".container-fluid");
  if (!container) {
    return;
  }

  const old = document.getElementById("page-alert");
  if (old) {
    old.remove();
  }

  const alert = document.createElement("div");
  alert.id = "page-alert";
  alert.className = `alert alert-${type} mt-2`;
  alert.textContent = message;
  container.prepend(alert);
}

function getThemeMode() {
  return localStorage.getItem("sms_theme") || "light";
}

function applyThemeMode(mode) {
  const nextMode = mode === "dark" ? "dark" : "light";
  document.body.classList.toggle("dark-mode", nextMode === "dark");
  localStorage.setItem("sms_theme", nextMode);
}

function toggleThemeMode() {
  const current = getThemeMode();
  const next = current === "dark" ? "light" : "dark";
  applyThemeMode(next);
  showToast(next === "dark" ? "Đã bật chế độ tối" : "Đã chuyển về chế độ sáng", "info");
}

function wireThemeToggle(buttonId) {
  if (!buttonId) {
    return;
  }

  const button = document.getElementById(buttonId);
  if (!button) {
    return;
  }

  button.addEventListener("click", () => {
    toggleThemeMode();
  });
}

function getToastContainer() {
  let container = document.getElementById("toastContainer");
  if (container) {
    return container;
  }

  container = document.createElement("div");
  container.id = "toastContainer";
  container.className = "toast-container-fixed";
  document.body.append(container);
  return container;
}

function showToast(message, type = "info") {
  if (!message) {
    return;
  }

  const tone = ["success", "danger", "warning", "info"].includes(type) ? type : "info";
  const container = getToastContainer();
  const toast = document.createElement("div");
  toast.className = `toast-lite toast-${tone}`;
  toast.textContent = message;
  container.append(toast);

  requestAnimationFrame(() => {
    toast.classList.add("show");
  });

  setTimeout(() => {
    toast.classList.remove("show");
    setTimeout(() => {
      toast.remove();
    }, 260);
  }, 2400);
}

function getSaveShortcutKeyLabel() {
  const platform = navigator.userAgentData?.platform || navigator.platform || "";
  return /mac|iphone|ipad|ipod/i.test(platform) ? "Cmd" : "Ctrl";
}

function markFieldValidationState(field) {
  if (!field || field.disabled || field.type === "hidden") {
    return;
  }

  if (field.checkValidity()) {
    field.classList.remove("is-invalid");
  } else {
    field.classList.add("is-invalid");
  }
}

function wireRealtimeValidation(form) {
  if (!form) {
    return;
  }

  const fields = form.querySelectorAll("input, select, textarea");
  fields.forEach((field) => {
    field.addEventListener("input", () => markFieldValidationState(field));
    field.addEventListener("blur", () => markFieldValidationState(field));
  });
}

function validateFormNow(form) {
  if (!form) {
    return false;
  }

  const fields = form.querySelectorAll("input, select, textarea");
  fields.forEach((field) => markFieldValidationState(field));

  if (!form.checkValidity()) {
    form.reportValidity();
    return false;
  }

  return true;
}

function setButtonLoading(button, loadingText = "Đang lưu...") {
  if (!button) {
    return () => {};
  }

  const previousText = button.textContent;
  button.disabled = true;
  button.classList.add("is-loading");
  button.textContent = loadingText;

  return () => {
    button.disabled = false;
    button.classList.remove("is-loading");
    button.textContent = previousText;
  };
}

document.addEventListener("DOMContentLoaded", () => {
  applyThemeMode(getThemeMode());

  if (window.requireAuth && !window.requireAuth()) {
    return;
  }

  setActiveNav();
  wirePageTableSearch();
  wireDataTableSorting();
});

window.renderTableRows = renderTableRows;
window.renderTableRowsHtml = renderTableRowsHtml;
window.formatDate = formatDate;
window.showPageMessage = showPageMessage;
window.getSaveShortcutKeyLabel = getSaveShortcutKeyLabel;
window.wireRealtimeValidation = wireRealtimeValidation;
window.validateFormNow = validateFormNow;
window.setButtonLoading = setButtonLoading;
window.applyTableSort = applyTableSort;
window.showToast = showToast;
window.wireThemeToggle = wireThemeToggle;
