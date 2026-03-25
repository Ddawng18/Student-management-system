document.addEventListener("DOMContentLoaded", async () => {
  const createBtn = document.querySelector(".btn.btn-brand");
  const saveShortcut = `${window.getSaveShortcutKeyLabel ? window.getSaveShortcutKeyLabel() : "Ctrl"}+S`;
  const state = {
    editingId: null
  };

  function clearEditingRow() {
    document.querySelectorAll("#departmentsTable tr").forEach((row) => row.classList.remove("table-row-editing"));
  }

  function markEditingRowById(id) {
    clearEditingRow();
    if (!id) {
      return;
    }

    const activeBtn = document.querySelector(`.js-edit-department[data-id="${id}"]`);
    activeBtn?.closest("tr")?.classList.add("table-row-editing");
  }

  function setCreateMode() {
    const form = document.getElementById("createDepartmentForm");
    const submitBtn = document.getElementById("submitDepartmentBtn");
    const cancelBtn = document.getElementById("cancelDepartmentEditBtn");
    const title = document.getElementById("departmentQuickFormTitle");
    const badge = document.getElementById("departmentEditBadge");
    const maKhoaInput = document.getElementById("maKhoa");

    if (!form || !submitBtn || !cancelBtn || !title || !badge || !maKhoaInput) {
      return;
    }

    state.editingId = null;
    form.reset();
    submitBtn.textContent = "Lưu khoa";
    cancelBtn.classList.add("d-none");
    title.textContent = "Thêm khoa nhanh";
    badge.classList.add("d-none");
    clearEditingRow();
    maKhoaInput.focus();
  }

  function setEditMode(department) {
    state.editingId = department.khoaId;
    document.getElementById("maKhoa").value = department.maKhoa ?? "";
    document.getElementById("tenKhoa").value = department.tenKhoa ?? "";

    const submitBtn = document.getElementById("submitDepartmentBtn");
    const cancelBtn = document.getElementById("cancelDepartmentEditBtn");
    const title = document.getElementById("departmentQuickFormTitle");
    const badge = document.getElementById("departmentEditBadge");

    if (submitBtn) {
      submitBtn.textContent = "Cập nhật khoa";
    }

    if (cancelBtn) {
      cancelBtn.classList.remove("d-none");
    }

    if (title) {
      title.textContent = "Cập nhật khoa";
    }

    if (badge) {
      badge.classList.remove("d-none");
    }

    document.getElementById("departmentQuickForm")?.scrollIntoView({ behavior: "smooth", block: "start" });
    document.getElementById("tenKhoa")?.focus();
    markEditingRowById(department.khoaId);
  }

  function mountCreateForm() {
    const host = document.querySelector(".container-fluid");
    if (!host || document.getElementById("departmentQuickForm")) {
      return;
    }

    const block = document.createElement("div");
    block.id = "departmentQuickForm";
    block.className = "card surface-card mb-3";
    block.innerHTML = `
      <div id="departmentQuickFormTitle" class="card-header d-flex justify-content-between align-items-center">Thêm khoa nhanh<span id="departmentEditBadge" class="editing-badge d-none">Đang sửa</span></div>
      <div class="card-body">
        <form id="createDepartmentForm" class="quick-form-grid">
          <input id="maKhoa" class="form-control" placeholder="Mã khoa" required />
          <input id="tenKhoa" class="form-control" placeholder="Tên khoa" required />
          <button id="submitDepartmentBtn" type="submit" class="btn btn-brand">Lưu khoa</button>
          <button id="cancelDepartmentEditBtn" type="button" class="btn btn-outline-secondary d-none">Hủy sửa</button>
        </form>
        <p class="form-shortcut-hint">Mẹo: nhấn Esc để hủy sửa, nhấn ${saveShortcut} để lưu nhanh khi đang sửa.</p>
      </div>
    `;

    host.insertBefore(block, host.children[1] || null);

    const formEl = document.getElementById("createDepartmentForm");
    const submitBtn = document.getElementById("submitDepartmentBtn");
    window.wireRealtimeValidation?.(formEl);

    formEl.addEventListener("submit", async (event) => {
      event.preventDefault();

      if (!window.validateFormNow?.(formEl)) {
        showPageMessage("Vui lòng kiểm tra lại thông tin khoa.", "warning");
        return;
      }

      const payload = {
        maKhoa: document.getElementById("maKhoa").value.trim(),
        tenKhoa: document.getElementById("tenKhoa").value.trim()
      };

      const stopLoading = window.setButtonLoading?.(submitBtn, state.editingId ? "Đang cập nhật..." : "Đang lưu...");

      try {
        if (state.editingId) {
          await window.apiFetch(`/departments/${state.editingId}`, {
            method: "PUT",
            body: JSON.stringify(payload)
          });
          showPageMessage("Cập nhật khoa thành công.", "success");
        } else {
          await window.apiFetch("/departments", {
            method: "POST",
            body: JSON.stringify(payload)
          });
          showPageMessage("Thêm khoa thành công.", "success");
        }

        await loadDepartments();
        setCreateMode();
      } catch (error) {
        const action = state.editingId ? "Cập nhật" : "Thêm";
        showPageMessage(`${action} khoa thất bại: ${error.message}`, "danger");
      } finally {
        stopLoading?.();
      }
    });

    document.getElementById("cancelDepartmentEditBtn").addEventListener("click", () => {
      setCreateMode();
    });
  }

  async function deleteDepartment(id) {
    if (!window.confirm("Bạn chắc chắn muốn xóa khoa này?")) {
      return;
    }

    try {
      await window.apiFetch(`/departments/${id}`, { method: "DELETE" });
      showPageMessage("Xóa khoa thành công.", "success");
      await loadDepartments();
    } catch (error) {
      showPageMessage(`Xóa khoa thất bại: ${error.message}`, "danger");
    }
  }

  async function loadDepartments() {
    try {
      const departments = await window.apiFetch("/departments");
      const rows = departments.map((x) => {
        const actionCell = `
          <div class="d-flex gap-1">
            <button class="btn btn-sm btn-outline-primary js-edit-department" data-id="${x.khoaId}">Sửa</button>
            <button class="btn btn-sm btn-outline-danger js-delete-department" data-id="${x.khoaId}">Xóa</button>
          </div>
        `;
        return [
          escapeHtml(x.maKhoa),
          escapeHtml(x.tenKhoa),
          escapeHtml(x.truongKhoaId ?? "-"),
          actionCell
        ];
      });

      renderTableRowsHtml("departmentsTable", rows);
      document.querySelectorAll(".js-edit-department").forEach((button) => {
        button.addEventListener("click", () => {
          const department = departments.find((x) => x.khoaId === Number(button.dataset.id));
          if (department) {
            setEditMode(department);
          }
        });
      });
      document.querySelectorAll(".js-delete-department").forEach((button) => {
        button.addEventListener("click", () => deleteDepartment(Number(button.dataset.id)));
      });

      markEditingRowById(state.editingId);
    } catch (error) {
      showPageMessage(`Không tải được danh sách khoa: ${error.message}`, "danger");
    }
  }

  mountCreateForm();
  setCreateMode();

  if (createBtn) {
    createBtn.addEventListener("click", () => {
      setCreateMode();
      const formBlock = document.getElementById("departmentQuickForm");
      if (formBlock) {
        formBlock.scrollIntoView({ behavior: "smooth", block: "start" });
      }
    });
  }

  document.addEventListener("keydown", (event) => {
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "s" && state.editingId) {
      event.preventDefault();
      document.getElementById("createDepartmentForm")?.requestSubmit();
      return;
    }

    if (event.key === "Escape" && state.editingId) {
      setCreateMode();
      showPageMessage("Đã thoát chế độ sửa khoa.", "info");
    }
  });

  await loadDepartments();
});
