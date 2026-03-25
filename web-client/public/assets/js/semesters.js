document.addEventListener("DOMContentLoaded", async () => {
  const createBtn = document.querySelector(".btn.btn-brand");
  const saveShortcut = `${window.getSaveShortcutKeyLabel ? window.getSaveShortcutKeyLabel() : "Ctrl"}+S`;
  const state = {
    editingId: null
  };

  function clearEditingRow() {
    document.querySelectorAll("#semestersTable tr").forEach((row) => row.classList.remove("table-row-editing"));
  }

  function markEditingRowById(id) {
    clearEditingRow();
    if (!id) {
      return;
    }

    const activeBtn = document.querySelector(`.js-edit-semester[data-id="${id}"]`);
    activeBtn?.closest("tr")?.classList.add("table-row-editing");
  }

  function todayIso() {
    return new Date().toISOString().slice(0, 10);
  }

  function setCreateMode() {
    const form = document.getElementById("createSemesterForm");
    const submitBtn = document.getElementById("submitSemesterBtn");
    const cancelBtn = document.getElementById("cancelSemesterEditBtn");
    const title = document.getElementById("semesterQuickFormTitle");
    const badge = document.getElementById("semesterEditBadge");
    const maHocKyInput = document.getElementById("maHocKy");
    const namHocInput = document.getElementById("namHoc");
    const ngayBatDauInput = document.getElementById("ngayBatDau");
    const ngayKetThucInput = document.getElementById("ngayKetThuc");

    if (!form || !submitBtn || !cancelBtn || !title || !badge || !maHocKyInput) {
      return;
    }

    const currentYear = new Date().getFullYear();

    state.editingId = null;
    form.reset();
    maHocKyInput.disabled = false;
    submitBtn.textContent = "Lưu học kỳ";
    cancelBtn.classList.add("d-none");
    title.textContent = "Thêm học kỳ nhanh";
    badge.classList.add("d-none");
    clearEditingRow();

    if (namHocInput) {
      namHocInput.value = String(currentYear);
    }

    if (ngayBatDauInput) {
      ngayBatDauInput.value = todayIso();
    }

    if (ngayKetThucInput) {
      ngayKetThucInput.value = todayIso();
    }

    maHocKyInput.focus();
  }

  function setEditMode(item) {
    state.editingId = item.hocKyId;
    document.getElementById("maHocKy").value = item.maHocKy ?? "";
    document.getElementById("tenHocKy").value = item.tenHocKy ?? "";
    document.getElementById("namHoc").value = item.namHoc ?? "";
    document.getElementById("ngayBatDau").value = item.ngayBatDau ? formatDate(item.ngayBatDau) : "";
    document.getElementById("ngayKetThuc").value = item.ngayKetThuc ? formatDate(item.ngayKetThuc) : "";

    const submitBtn = document.getElementById("submitSemesterBtn");
    const cancelBtn = document.getElementById("cancelSemesterEditBtn");
    const title = document.getElementById("semesterQuickFormTitle");
    const badge = document.getElementById("semesterEditBadge");
    const maHocKyInput = document.getElementById("maHocKy");

    if (submitBtn) {
      submitBtn.textContent = "Cập nhật học kỳ";
    }

    if (cancelBtn) {
      cancelBtn.classList.remove("d-none");
    }

    if (title) {
      title.textContent = "Cập nhật học kỳ";
    }

    if (badge) {
      badge.classList.remove("d-none");
    }

    if (maHocKyInput) {
      maHocKyInput.disabled = true;
    }

    document.getElementById("semesterQuickForm")?.scrollIntoView({ behavior: "smooth", block: "start" });
    document.getElementById("tenHocKy")?.focus();
    markEditingRowById(item.hocKyId);
  }

  function mountCreateForm() {
    const host = document.querySelector(".container-fluid");
    if (!host || document.getElementById("semesterQuickForm")) {
      return;
    }

    const now = new Date();
    const defaultYear = now.getFullYear();

    const block = document.createElement("div");
    block.id = "semesterQuickForm";
    block.className = "card surface-card mb-3";
    block.innerHTML = `
      <div id="semesterQuickFormTitle" class="card-header d-flex justify-content-between align-items-center">Thêm học kỳ nhanh<span id="semesterEditBadge" class="editing-badge d-none">Đang sửa</span></div>
      <div class="card-body">
        <form id="createSemesterForm" class="quick-form-grid">
          <input id="maHocKy" class="form-control" placeholder="Mã học kỳ" required />
          <input id="tenHocKy" class="form-control" placeholder="Tên học kỳ" required />
          <input id="namHoc" class="form-control" type="number" min="2000" max="2100" value="${defaultYear}" required />
          <input id="ngayBatDau" class="form-control" type="date" value="${todayIso()}" />
          <input id="ngayKetThuc" class="form-control" type="date" value="${todayIso()}" />
          <button id="submitSemesterBtn" type="submit" class="btn btn-brand">Lưu học kỳ</button>
          <button id="cancelSemesterEditBtn" type="button" class="btn btn-outline-secondary d-none">Hủy sửa</button>
        </form>
        <p class="form-shortcut-hint">Mẹo: nhấn Esc để hủy sửa, nhấn ${saveShortcut} để lưu nhanh khi đang sửa.</p>
      </div>
    `;

    host.insertBefore(block, host.children[1] || null);

    const formEl = document.getElementById("createSemesterForm");
    const submitBtn = document.getElementById("submitSemesterBtn");
    window.wireRealtimeValidation?.(formEl);

    formEl.addEventListener("submit", async (event) => {
      event.preventDefault();

      if (!window.validateFormNow?.(formEl)) {
        showPageMessage("Vui lòng kiểm tra lại thông tin học kỳ.", "warning");
        return;
      }

      const createPayload = {
        maHocKy: document.getElementById("maHocKy").value.trim(),
        tenHocKy: document.getElementById("tenHocKy").value.trim(),
        namHoc: Number(document.getElementById("namHoc").value),
        ngayBatDau: document.getElementById("ngayBatDau").value || null,
        ngayKetThuc: document.getElementById("ngayKetThuc").value || null
      };

      const updatePayload = {
        tenHocKy: createPayload.tenHocKy,
        namHoc: createPayload.namHoc,
        ngayBatDau: createPayload.ngayBatDau,
        ngayKetThuc: createPayload.ngayKetThuc
      };

      const stopLoading = window.setButtonLoading?.(submitBtn, state.editingId ? "Đang cập nhật..." : "Đang lưu...");

      try {
        if (state.editingId) {
          await window.apiFetch(`/semesters/${state.editingId}`, {
            method: "PUT",
            body: JSON.stringify(updatePayload)
          });
          showPageMessage("Cập nhật học kỳ thành công.", "success");
        } else {
          await window.apiFetch("/semesters", {
            method: "POST",
            body: JSON.stringify(createPayload)
          });
          showPageMessage("Thêm học kỳ thành công.", "success");
        }

        await loadSemesters();
        setCreateMode();
      } catch (error) {
        const action = state.editingId ? "Cập nhật" : "Thêm";
        showPageMessage(`${action} học kỳ thất bại: ${error.message}`, "danger");
      } finally {
        stopLoading?.();
      }
    });

    document.getElementById("cancelSemesterEditBtn").addEventListener("click", () => {
      setCreateMode();
    });
  }

  async function deleteSemester(id) {
    if (!window.confirm("Bạn chắc chắn muốn xóa học kỳ này?")) {
      return;
    }

    try {
      await window.apiFetch(`/semesters/${id}`, { method: "DELETE" });
      showPageMessage("Xóa học kỳ thành công.", "success");
      await loadSemesters();
    } catch (error) {
      showPageMessage(`Xóa học kỳ thất bại: ${error.message}`, "danger");
    }
  }

  async function loadSemesters() {
    try {
      const semesters = await window.apiFetch("/semesters");
      const rows = semesters.map((x) => {
        const actionCell = `
          <div class="d-flex gap-1">
            <button class="btn btn-sm btn-outline-primary js-edit-semester" data-id="${x.hocKyId}">Sửa</button>
            <button class="btn btn-sm btn-outline-danger js-delete-semester" data-id="${x.hocKyId}">Xóa</button>
          </div>
        `;
        return [
          escapeHtml(x.maHocKy),
          escapeHtml(x.tenHocKy),
          escapeHtml(x.namHoc),
          escapeHtml(formatDate(x.ngayBatDau)),
          escapeHtml(formatDate(x.ngayKetThuc)),
          actionCell
        ];
      });

      renderTableRowsHtml("semestersTable", rows);
      document.querySelectorAll(".js-edit-semester").forEach((button) => {
        button.addEventListener("click", () => {
          const item = semesters.find((x) => x.hocKyId === Number(button.dataset.id));
          if (item) {
            setEditMode(item);
          }
        });
      });
      document.querySelectorAll(".js-delete-semester").forEach((button) => {
        button.addEventListener("click", () => deleteSemester(Number(button.dataset.id)));
      });

      markEditingRowById(state.editingId);
    } catch (error) {
      showPageMessage(`Không tải được danh sách học kỳ: ${error.message}`, "danger");
    }
  }

  mountCreateForm();
  setCreateMode();

  if (createBtn) {
    createBtn.addEventListener("click", () => {
      setCreateMode();
      const formBlock = document.getElementById("semesterQuickForm");
      if (formBlock) {
        formBlock.scrollIntoView({ behavior: "smooth", block: "start" });
      }
    });
  }

  document.addEventListener("keydown", (event) => {
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "s" && state.editingId) {
      event.preventDefault();
      document.getElementById("createSemesterForm")?.requestSubmit();
      return;
    }

    if (event.key === "Escape" && state.editingId) {
      setCreateMode();
      showPageMessage("Đã thoát chế độ sửa học kỳ.", "info");
    }
  });

  await loadSemesters();
});
