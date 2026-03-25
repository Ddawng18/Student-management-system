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

  tbody.innerHTML = rows
    .map((row) => `<tr>${row.map((cell) => `<td>${escapeHtml(cell ?? "")}</td>`).join("")}</tr>`)
    .join("");
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

document.addEventListener("DOMContentLoaded", () => {
  setActiveNav();
});

window.renderTableRows = renderTableRows;
window.formatDate = formatDate;
window.showPageMessage = showPageMessage;
