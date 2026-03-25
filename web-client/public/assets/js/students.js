document.addEventListener("DOMContentLoaded", async () => {
  const createBtn = document.querySelector(".btn.btn-brand");
  const saveShortcut = `${window.getSaveShortcutKeyLabel ? window.getSaveShortcutKeyLabel() : "Ctrl"}+S`;
  const ITEMS_PER_PAGE = 10;
  const state = {
    editingId: null,
    students: [],
    classes: [],
    departments: [],
    selectedClassId: "all",
    selectedDepartmentId: "all",
    currentPage: 1
  };

  function getClassNameById(lopHocId) {
    if (lopHocId === null || lopHocId === undefined) {
      return "-";
    }

    const found = state.classes.find((x) => Number(x.lopHocId) === Number(lopHocId));
    return found?.tenLop || `Lớp #${lopHocId}`;
  }

  function getDepartmentIdByClassId(lopHocId) {
    const found = state.classes.find((x) => Number(x.lopHocId) === Number(lopHocId));
    return found?.khoaId ?? null;
  }

  function getDepartmentNameById(khoaId) {
    if (khoaId === null || khoaId === undefined) {
      return "-";
    }

    const found = state.departments.find((x) => Number(x.khoaId) === Number(khoaId));
    return found?.tenKhoa || `Khoa #${khoaId}`;
  }

  function clearEditingRow() {
    document.querySelectorAll("#studentsTable tr").forEach((row) => row.classList.remove("table-row-editing"));
  }

  function markEditingRowById(id) {
    clearEditingRow();
    if (!id) {
      return;
    }

    const activeBtn = document.querySelector(`.js-edit-student[data-id="${id}"]`);
    activeBtn?.closest("tr")?.classList.add("table-row-editing");
  }

  function setCreateMode() {
    const form = document.getElementById("createStudentForm");
    const submitBtn = document.getElementById("submitStudentBtn");
    const cancelBtn = document.getElementById("cancelStudentEditBtn");
    const title = document.getElementById("studentQuickFormTitle");
    const badge = document.getElementById("studentEditBadge");
    const maSinhVienInput = document.getElementById("maSinhVien");

    if (!form || !submitBtn || !cancelBtn || !title || !badge || !maSinhVienInput) {
      return;
    }

    state.editingId = null;
    form.reset();
    maSinhVienInput.disabled = false;
    submitBtn.textContent = "Lưu sinh viên";
    cancelBtn.classList.add("d-none");
    title.textContent = "Thêm sinh viên nhanh";
    badge.classList.add("d-none");
    clearEditingRow();
    maSinhVienInput.focus();
  }

  function setEditMode(student) {
    const submitBtn = document.getElementById("submitStudentBtn");
    const cancelBtn = document.getElementById("cancelStudentEditBtn");
    const title = document.getElementById("studentQuickFormTitle");
    const badge = document.getElementById("studentEditBadge");
    const maSinhVienInput = document.getElementById("maSinhVien");

    state.editingId = student.sinhVienId;
    document.getElementById("maSinhVien").value = student.maSinhVien ?? "";
    document.getElementById("hoTen").value = student.hoTen ?? "";
    document.getElementById("email").value = student.email ?? "";
    document.getElementById("ngaySinh").value = formatDate(student.ngaySinh);

    if (maSinhVienInput) {
      maSinhVienInput.disabled = true;
    }

    if (submitBtn) {
      submitBtn.textContent = "Cập nhật sinh viên";
    }

    if (cancelBtn) {
      cancelBtn.classList.remove("d-none");
    }

    if (title) {
      title.textContent = "Cập nhật sinh viên";
    }

    if (badge) {
      badge.classList.remove("d-none");
    }

    document.getElementById("studentQuickForm")?.scrollIntoView({ behavior: "smooth", block: "start" });
    document.getElementById("hoTen")?.focus();
    markEditingRowById(student.sinhVienId);
  }

  function mountCreateForm() {
    const host = document.querySelector(".container-fluid");
    if (!host || document.getElementById("studentQuickForm")) {
      return;
    }

    const block = document.createElement("div");
    block.id = "studentQuickForm";
    block.className = "card surface-card mb-3";
    block.innerHTML = `
      <div id="studentQuickFormHeader" class="form-collapsible-toggle">
        <i class="fas fa-chevron-down"></i>
        <span>Thêm sinh viên nhanh</span>
        <span id="studentEditBadge" class="editing-badge d-none" style="margin-left: auto;">Đang sửa</span>
      </div>
      <div id="studentQuickFormContent" class="form-collapsible-content card-body">
        <form id="createStudentForm" class="quick-form-grid">
          <label class="field-stack">Mã sinh viên
            <input id="maSinhVien" class="form-control" required />
          </label>
          <label class="field-stack">Họ và tên
            <input id="hoTen" class="form-control" required />
          </label>
          <label class="field-stack">Email
            <input id="email" type="email" class="form-control" required />
          </label>
          <label class="field-stack">Ngày sinh
            <input id="ngaySinh" type="date" class="form-control" required />
          </label>
          <div class="form-actions-inline">
            <button id="cancelStudentEditBtn" type="button" class="btn btn-outline-secondary d-none">Hủy sửa</button>
            <button id="submitStudentBtn" type="submit" class="btn btn-brand">Lưu sinh viên</button>
          </div>
        </form>
        <p class="form-shortcut-hint">Mẹo: nhấn Esc để hủy sửa, nhấn ${saveShortcut} để lưu nhanh khi đang sửa.</p>
      </div>
    `;

    host.insertBefore(block, host.children[1] || null);

    // Collapsible toggle
    const header = document.getElementById("studentQuickFormHeader");
    const content = document.getElementById("studentQuickFormContent");
    let isCollapsed = true;

    header.addEventListener("click", () => {
      isCollapsed = !isCollapsed;
      header.classList.toggle("collapsed");
      content.classList.toggle("collapsed");
    });

    const formEl = document.getElementById("createStudentForm");
    const submitBtn = document.getElementById("submitStudentBtn");
    window.wireRealtimeValidation?.(formEl);

    // Auto-expand form when editing
    window.expandStudentForm = () => {
      if (isCollapsed) {
        isCollapsed = false;
        header.classList.remove("collapsed");
        content.classList.remove("collapsed");
      }
    };

    formEl.addEventListener("submit", async (event) => {
      event.preventDefault();

      if (!window.validateFormNow?.(formEl)) {
        showPageMessage("Vui lòng kiểm tra lại thông tin sinh viên.", "warning");
        window.showToast?.("Vui lòng kiểm tra lại thông tin sinh viên.", "warning");
        return;
      }

      const createPayload = {
        maSinhVien: document.getElementById("maSinhVien").value.trim(),
        hoTen: document.getElementById("hoTen").value.trim(),
        email: document.getElementById("email").value.trim(),
        ngaySinh: document.getElementById("ngaySinh").value,
        lopHocId: null
      };

      const updatePayload = {
        hoTen: createPayload.hoTen,
        email: createPayload.email,
        ngaySinh: createPayload.ngaySinh,
        lopHocId: null
      };

      const stopLoading = window.setButtonLoading?.(submitBtn, state.editingId ? "Đang cập nhật..." : "Đang lưu...");

      try {
        if (state.editingId) {
          await window.apiFetch(`/students/${state.editingId}`, {
            method: "PUT",
            body: JSON.stringify(updatePayload)
          });
          showPageMessage("Cập nhật sinh viên thành công.", "success");
          window.showToast?.("Cập nhật sinh viên thành công.", "success");
        } else {
          await window.apiFetch("/students", {
            method: "POST",
            body: JSON.stringify(createPayload)
          });
          showPageMessage("Thêm sinh viên thành công.", "success");
          window.showToast?.("Thêm sinh viên thành công.", "success");
        }

        await loadStudents();
        setCreateMode();
      } catch (error) {
        const action = state.editingId ? "Cập nhật" : "Thêm";
        showPageMessage(`${action} sinh viên thất bại: ${error.message}`, "danger");
        window.showToast?.(`${action} sinh viên thất bại.`, "danger");
      } finally {
        stopLoading?.();
      }
    });

    document.getElementById("cancelStudentEditBtn").addEventListener("click", () => {
      setCreateMode();
    });
  }

  function buildHoverSummary(student) {
    return [
      `Mã SV: ${student.maSinhVien || "-"}`,
      `Email: ${student.email || "-"}`,
      `Ngày sinh: ${formatDate(student.ngaySinh)}`,
      `Quê quán: ${student.queQuan || "Chưa cập nhật"}`
    ].join(" | ");
  }

  function renderNameCell(student) {
    const hoverSummary = escapeHtml(buildHoverSummary(student));
    return `<a class="student-name-link" href="student-profile.html?id=${student.sinhVienId}" title="${hoverSummary}">${escapeHtml(student.hoTen)}</a>`;
  }

  async function deleteStudent(studentId) {
    if (!window.confirm("Bạn chắc chắn muốn xóa sinh viên này?")) {
      return;
    }

    try {
      await window.apiFetch(`/students/${studentId}`, {
        method: "DELETE"
      });
      showPageMessage("Xóa sinh viên thành công.", "success");
      window.showToast?.("Xóa sinh viên thành công.", "success");
      await loadStudents();
    } catch (error) {
      showPageMessage(`Xóa sinh viên thất bại: ${error.message}`, "danger");
      window.showToast?.("Xóa sinh viên thất bại.", "danger");
    }
  }

  function getFilteredStudents() {
    return state.students.filter((student) => {
      const classId = student.lopHocId ? String(student.lopHocId) : "none";
      const departmentId = getDepartmentIdByClassId(student.lopHocId);
      const departmentKey = departmentId ? String(departmentId) : "none";

      const matchClass = state.selectedClassId === "all" || classId === state.selectedClassId;
      const matchDepartment = state.selectedDepartmentId === "all" || departmentKey === state.selectedDepartmentId;
      return matchClass && matchDepartment;
    });
  }

  function renderStudentsTable(students) {
    const totalItems = students.length;
    const totalPages = Math.ceil(totalItems / ITEMS_PER_PAGE);
    
    // Validate current page
    if (state.currentPage < 1) state.currentPage = 1;
    if (state.currentPage > totalPages && totalPages > 0) state.currentPage = totalPages;

    const startIdx = (state.currentPage - 1) * ITEMS_PER_PAGE;
    const endIdx = startIdx + ITEMS_PER_PAGE;
    const pageStudents = students.slice(startIdx, endIdx);

    const rows = pageStudents.map((x) => {
      const actionCell = `
        <div class="d-flex gap-1">
          <button class="btn btn-sm btn-outline-primary icon-btn js-edit-student" data-id="${x.sinhVienId}" data-tooltip="Sửa thông tin"><i class="fas fa-pen"></i></button>
          <button class="btn btn-sm btn-outline-danger icon-btn js-delete-student" data-id="${x.sinhVienId}" data-tooltip="Xóa sinh viên"><i class="fas fa-trash"></i></button>
        </div>
      `;

      return [
        escapeHtml(x.maSinhVien),
        renderNameCell(x),
        escapeHtml(x.email),
        escapeHtml(getClassNameById(x.lopHocId)),
        escapeHtml(formatDate(x.ngaySinh)),
        actionCell
      ];
    });

    renderTableRowsHtml("studentsTable", rows);

    document.querySelectorAll(".js-edit-student").forEach((button) => {
      button.addEventListener("click", () => {
        const student = state.students.find((x) => x.sinhVienId === Number(button.dataset.id));
        if (student) {
          window.expandStudentForm?.();
          setEditMode(student);
        }
      });
    });
    document.querySelectorAll(".js-delete-student").forEach((button) => {
      button.addEventListener("click", () => deleteStudent(Number(button.dataset.id)));
    });

    markEditingRowById(state.editingId);

    // Render pagination
    renderPagination(totalPages, totalItems);
  }

  function renderPagination(totalPages, totalItems) {
    const tableContainer = document.getElementById("studentsTable")?.closest(".table-responsive");
    const existingPagination = document.getElementById("studentsPagination");
    
    if (existingPagination) {
      existingPagination.remove();
    }

    if (totalPages <= 1) {
      return;
    }

    const paginationHtml = `
      <div id="studentsPagination" class="pagination">
        <a class="page-link ${state.currentPage === 1 ? "disabled" : ""}" ${state.currentPage === 1 ? "disabled" : "data-page='prev'"}>
          <i class="fas fa-chevron-left"></i> Trước
        </a>
        ${Array.from({ length: totalPages }, (_, i) => i + 1)
          .map(
            (page) =>
              `<a class="page-link ${page === state.currentPage ? "active" : ""}" data-page="${page}">${page}</a>`
          )
          .join("")}
        <a class="page-link ${state.currentPage === totalPages ? "disabled" : ""}" ${state.currentPage === totalPages ? "disabled" : "data-page='next'"}>
          Sau <i class="fas fa-chevron-right"></i>
        </a>
      </div>
      <div class="pagination-info">
        Hiển thị từ ${Math.min((state.currentPage - 1) * ITEMS_PER_PAGE + 1, totalItems)} đến ${Math.min(state.currentPage * ITEMS_PER_PAGE, totalItems)} của ${totalItems} sinh viên
      </div>
    `;

    if (tableContainer) {
      tableContainer.insertAdjacentHTML("afterend", paginationHtml);

      document.querySelectorAll("#studentsPagination .page-link[data-page]").forEach((link) => {
        link.addEventListener("click", (e) => {
          e.preventDefault();
          const page = link.dataset.page;
          
          if (page === "next") {
            state.currentPage = Math.min(state.currentPage + 1, totalPages);
          } else if (page === "prev") {
            state.currentPage = Math.max(state.currentPage - 1, 1);
          } else {
            state.currentPage = Number(page);
          }

          renderStudentsTable(getFilteredStudents());
          tableContainer.scrollIntoView({ behavior: "smooth", block: "start" });
        });
      });
    }
  }

  function populateFilterOptions() {
    const classSelect = document.getElementById("studentClassFilter");
    const departmentSelect = document.getElementById("studentDepartmentFilter");
    if (!classSelect || !departmentSelect) {
      return;
    }

    classSelect.innerHTML = `<option value="all">Tất cả lớp</option>${state.classes
      .map((x) => `<option value="${x.lopHocId}">${escapeHtml(x.tenLop || `Lớp #${x.lopHocId}`)}</option>`)
      .join("")}`;

    departmentSelect.innerHTML = `<option value="all">Tất cả khoa</option>${state.departments
      .map((x) => `<option value="${x.khoaId}">${escapeHtml(x.tenKhoa || `Khoa #${x.khoaId}`)}</option>`)
      .join("")}`;

    classSelect.addEventListener("change", () => {
      state.selectedClassId = classSelect.value;
      state.currentPage = 1;
      renderStudentsTable(getFilteredStudents());
    });

    departmentSelect.addEventListener("change", () => {
      state.selectedDepartmentId = departmentSelect.value;
      state.currentPage = 1;
      renderStudentsTable(getFilteredStudents());
    });
  }

  async function loadFilterData() {
    try {
      const [classes, departments] = await Promise.all([
        window.apiFetch("/classes"),
        window.apiFetch("/departments")
      ]);

      state.classes = classes || [];
      state.departments = departments || [];
      populateFilterOptions();
    } catch {
      state.classes = [];
      state.departments = [];
    }
  }

  async function loadStudents() {
    try {
      state.students = await window.apiFetch("/students");
      state.currentPage = 1;
      renderStudentsTable(getFilteredStudents());
    } catch (error) {
      showPageMessage(`Không tải được danh sách sinh viên: ${error.message}`, "danger");
      window.showToast?.("Không tải được danh sách sinh viên.", "danger");
    }
  }

  mountCreateForm();
  setCreateMode();
  await loadFilterData();

  if (createBtn) {
    createBtn.addEventListener("click", () => {
      setCreateMode();
      const formBlock = document.getElementById("studentQuickForm");
      if (formBlock) {
        formBlock.scrollIntoView({ behavior: "smooth", block: "start" });
      }
    });
  }

  document.addEventListener("keydown", (event) => {
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "s" && state.editingId) {
      event.preventDefault();
      document.getElementById("createStudentForm")?.requestSubmit();
      return;
    }

    if (event.key === "Escape" && state.editingId) {
      setCreateMode();
      showPageMessage("Đã thoát chế độ sửa sinh viên.", "info");
    }
  });

  await loadStudents();
});
