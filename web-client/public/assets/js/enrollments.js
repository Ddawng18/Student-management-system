document.addEventListener("DOMContentLoaded", async () => {
  const registerBtn = document.querySelector(".btn.btn-brand");
  const query = new URLSearchParams(window.location.search).get("q") || "";
  const state = {
    allEnrollments: [],
    activeFilter: "all"
  };

  const FILTERS = [
    { key: "all", label: "Tất cả" },
    { key: "graded", label: "Có điểm" },
    { key: "pending", label: "Chưa có điểm" },
    { key: "pass", label: "Đạt" },
    { key: "fail", label: "Chưa đạt" }
  ];

  function createOption(id, label) {
    return `<option value="${id}">${escapeHtml(label)}</option>`;
  }

  async function mountRegisterForm() {
    const host = document.querySelector(".container-fluid");
    if (!host || document.getElementById("enrollmentQuickForm")) {
      return;
    }

    const [students, courses, semesters] = await Promise.all([
      window.apiFetch("/students"),
      window.apiFetch("/courses"),
      window.apiFetch("/semesters")
    ]);

    const block = document.createElement("div");
    block.id = "enrollmentQuickForm";
    block.className = "card surface-card mb-3";
    block.innerHTML = `
      <div class="card-header">Đăng ký nhanh</div>
      <div class="card-body">
        <form id="createEnrollmentForm" class="quick-form-grid">
          <select id="sinhVienId" class="form-control" required>
            <option value="">Chọn sinh viên</option>
            ${students.map((x) => createOption(x.sinhVienId, `${x.maSinhVien} - ${x.hoTen}`)).join("")}
          </select>
          <select id="monHocId" class="form-control" required>
            <option value="">Chọn môn học</option>
            ${courses.map((x) => createOption(x.monHocId, `${x.maMonHoc} - ${x.tenMon}`)).join("")}
          </select>
          <select id="hocKyId" class="form-control" required>
            <option value="">Chọn học kỳ</option>
            ${semesters.map((x) => createOption(x.hocKyId, `${x.maHocKy} - ${x.tenHocKy}`)).join("")}
          </select>
          <button type="submit" class="btn btn-brand">Lưu đăng ký</button>
        </form>
      </div>
    `;

    host.insertBefore(block, host.children[1] || null);

    const formEl = document.getElementById("createEnrollmentForm");
    const submitBtn = formEl.querySelector("button[type=\"submit\"]");
    window.wireRealtimeValidation?.(formEl);

    formEl.addEventListener("submit", async (event) => {
      event.preventDefault();

      if (!window.validateFormNow?.(formEl)) {
        showPageMessage("Vui lòng chọn đầy đủ thông tin đăng ký.", "warning");
        return;
      }

      const payload = {
        sinhVienId: Number(document.getElementById("sinhVienId").value),
        monHocId: Number(document.getElementById("monHocId").value),
        hocKyId: Number(document.getElementById("hocKyId").value)
      };

      if (!payload.sinhVienId || !payload.monHocId || !payload.hocKyId) {
        showPageMessage("Vui lòng chọn đầy đủ thông tin đăng ký.", "warning");
        return;
      }

      const stopLoading = window.setButtonLoading?.(submitBtn, "Đang lưu...");

      try {
        await window.apiFetch("/enrollments", {
          method: "POST",
          body: JSON.stringify(payload)
        });
        showPageMessage("Đăng ký môn học thành công.", "success");
        await loadEnrollments();
      } catch (error) {
        showPageMessage(`Đăng ký thất bại: ${error.message}`, "danger");
      } finally {
        stopLoading?.();
      }
    });
  }

  function mountFilterBar() {
    const host = document.querySelector(".container-fluid");
    if (!host || document.getElementById("enrollmentFilterBar")) {
      return;
    }

    const tableCard = document.getElementById("enrollmentsTable")?.closest(".card");
    if (!tableCard) {
      return;
    }

    const block = document.createElement("div");
    block.id = "enrollmentFilterBar";
    block.className = "card surface-card mb-3";
    block.innerHTML = `
      <div class="card-body py-2">
        <div class="enrollment-filter-wrap">
          ${FILTERS.map((x) => `<button type="button" class="enrollment-chip ${x.key === "all" ? "active" : ""}" data-filter="${x.key}">${x.label}</button>`).join("")}
        </div>
      </div>
    `;

    host.insertBefore(block, tableCard);

    block.querySelectorAll(".enrollment-chip").forEach((chip) => {
      chip.addEventListener("click", () => {
        state.activeFilter = chip.dataset.filter || "all";
        block.querySelectorAll(".enrollment-chip").forEach((x) => x.classList.remove("active"));
        chip.classList.add("active");
        renderEnrollmentRows();
      });
    });
  }

  function getFilteredEnrollments() {
    switch (state.activeFilter) {
      case "graded":
        return state.allEnrollments.filter((x) => x.diem !== null && x.diem !== undefined);
      case "pending":
        return state.allEnrollments.filter((x) => x.diem === null || x.diem === undefined);
      case "pass":
        return state.allEnrollments.filter((x) => x.diem !== null && x.diem !== undefined && Number(x.diem) >= 5);
      case "fail":
        return state.allEnrollments.filter((x) => x.diem !== null && x.diem !== undefined && Number(x.diem) < 5);
      default:
        return state.allEnrollments;
    }
  }

  function renderEnrollmentRows() {
    const filtered = getFilteredEnrollments();
    const rows = filtered.map((x) => {
      const actionCell = `<button class="btn btn-sm btn-outline-danger js-delete-enrollment" data-id="${x.dangKyHocId}">Xóa</button>`;
      return [
        escapeHtml(x.sinhVienHoTen),
        escapeHtml(x.tenMonHoc),
        escapeHtml(x.tenHocKy),
        escapeHtml(formatDate(x.ngayDangKy)),
        actionCell
      ];
    });

    renderTableRowsHtml("enrollmentsTable", rows);
    document.querySelectorAll(".js-delete-enrollment").forEach((button) => {
      button.addEventListener("click", () => deleteEnrollment(Number(button.dataset.id)));
    });

    const table = document.getElementById("enrollmentsTable")?.closest("table");
    if (table && table.dataset.sortColumn === undefined) {
      window.applyTableSort?.("enrollmentsTable", 3, "desc");
    }

    if (query) {
      const searchInput = document.getElementById("tableSearchInput");
      if (searchInput) {
        searchInput.value = query;
        applySearchFilterForTable("enrollmentsTable");
      }
    }
  }

  async function deleteEnrollment(id) {
    if (!window.confirm("Bạn chắc chắn muốn xóa đăng ký này?")) {
      return;
    }

    try {
      await window.apiFetch(`/enrollments/${id}`, {
        method: "DELETE"
      });
      showPageMessage("Xóa đăng ký thành công.", "success");
      await loadEnrollments();
    } catch (error) {
      showPageMessage(`Xóa đăng ký thất bại: ${error.message}`, "danger");
    }
  }

  async function loadEnrollments() {
    try {
      state.allEnrollments = await window.apiFetch("/enrollments");
      renderEnrollmentRows();

      if (query) {
        showPageMessage(`Đang lọc theo: ${query}`, "info");
      }
    } catch (error) {
      showPageMessage(`Không tải được đăng ký học: ${error.message}`, "danger");
    }
  }

  try {
    await mountRegisterForm();
    mountFilterBar();
  } catch (error) {
    showPageMessage(`Không tải được dữ liệu form đăng ký: ${error.message}`, "danger");
  }

  if (registerBtn) {
    registerBtn.addEventListener("click", () => {
      const formBlock = document.getElementById("enrollmentQuickForm");
      if (formBlock) {
        formBlock.scrollIntoView({ behavior: "smooth", block: "start" });
      }
    });
  }

  await loadEnrollments();
});
