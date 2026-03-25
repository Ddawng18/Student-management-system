document.addEventListener("DOMContentLoaded", async () => {
  const createBtn = document.querySelector(".btn.btn-brand");
  const saveShortcut = `${window.getSaveShortcutKeyLabel ? window.getSaveShortcutKeyLabel() : "Ctrl"}+S`;
  const state = {
    editingId: null
  };

  function clearEditingRow() {
    document.querySelectorAll("#lecturersTable tr").forEach((row) => row.classList.remove("table-row-editing"));
  }

  function markEditingRowById(id) {
    clearEditingRow();
    if (!id) {
      return;
    }

    const activeBtn = document.querySelector(`.js-edit-lecturer[data-id="${id}"]`);
    activeBtn?.closest("tr")?.classList.add("table-row-editing");
  }

  function setCreateMode() {
    const form = document.getElementById("createLecturerForm");
    const submitBtn = document.getElementById("submitLecturerBtn");
    const cancelBtn = document.getElementById("cancelLecturerEditBtn");
    const title = document.getElementById("lecturerQuickFormTitle");
    const badge = document.getElementById("lecturerEditBadge");
    const maGiangVienInput = document.getElementById("maGiangVien");

    if (!form || !submitBtn || !cancelBtn || !title || !badge || !maGiangVienInput) {
      return;
    }

    state.editingId = null;
    form.reset();
    maGiangVienInput.disabled = false;
    submitBtn.textContent = "Lưu giảng viên";
    cancelBtn.classList.add("d-none");
    title.textContent = "Thêm giảng viên nhanh";
    badge.classList.add("d-none");
    clearEditingRow();
    maGiangVienInput.focus();
  }

  function setEditMode(item) {
    state.editingId = item.giangVienId;
    document.getElementById("maGiangVien").value = item.maGiangVien ?? "";
    document.getElementById("hoTen").value = item.hoTen ?? "";
    document.getElementById("email").value = item.email ?? "";
    document.getElementById("khoaId").value = item.khoaId ?? "";

    const submitBtn = document.getElementById("submitLecturerBtn");
    const cancelBtn = document.getElementById("cancelLecturerEditBtn");
    const title = document.getElementById("lecturerQuickFormTitle");
    const badge = document.getElementById("lecturerEditBadge");
    const maGiangVienInput = document.getElementById("maGiangVien");

    if (submitBtn) {
      submitBtn.textContent = "Cập nhật giảng viên";
    }

    if (cancelBtn) {
      cancelBtn.classList.remove("d-none");
    }

    if (title) {
      title.textContent = "Cập nhật giảng viên";
    }

    if (badge) {
      badge.classList.remove("d-none");
    }

    if (maGiangVienInput) {
      maGiangVienInput.disabled = true;
    }

    document.getElementById("lecturerQuickForm")?.scrollIntoView({ behavior: "smooth", block: "start" });
    document.getElementById("hoTen")?.focus();
    markEditingRowById(item.giangVienId);
  }

  function option(id, label) {
    return `<option value="${id}">${escapeHtml(label)}</option>`;
  }

  async function mountCreateForm() {
    const host = document.querySelector(".container-fluid");
    if (!host || document.getElementById("lecturerQuickForm")) {
      return;
    }

    const departments = await window.apiFetch("/departments");

    const block = document.createElement("div");
    block.id = "lecturerQuickForm";
    block.className = "card surface-card mb-3";
    block.innerHTML = `
      <div id="lecturerQuickFormTitle" class="card-header d-flex justify-content-between align-items-center">Thêm giảng viên nhanh<span id="lecturerEditBadge" class="editing-badge d-none">Đang sửa</span></div>
      <div class="card-body">
        <form id="createLecturerForm" class="quick-form-grid">
          <input id="maGiangVien" class="form-control" placeholder="Mã giảng viên" required />
          <input id="hoTen" class="form-control" placeholder="Họ và tên" required />
          <input id="email" type="email" class="form-control" placeholder="Email" required />
          <select id="khoaId" class="form-control">
            <option value="">Không chọn khoa</option>
            ${departments.map((x) => option(x.khoaId, `${x.maKhoa} - ${x.tenKhoa}`)).join("")}
          </select>
          <button id="submitLecturerBtn" type="submit" class="btn btn-brand">Lưu giảng viên</button>
          <button id="cancelLecturerEditBtn" type="button" class="btn btn-outline-secondary d-none">Hủy sửa</button>
        </form>
        <p class="form-shortcut-hint">Mẹo: nhấn Esc để hủy sửa, nhấn ${saveShortcut} để lưu nhanh khi đang sửa.</p>
      </div>
    `;

    host.insertBefore(block, host.children[1] || null);

    const formEl = document.getElementById("createLecturerForm");
    const submitBtn = document.getElementById("submitLecturerBtn");
    window.wireRealtimeValidation?.(formEl);

    formEl.addEventListener("submit", async (event) => {
      event.preventDefault();

      if (!window.validateFormNow?.(formEl)) {
        showPageMessage("Vui lòng kiểm tra lại thông tin giảng viên.", "warning");
        return;
      }

      const khoaRaw = document.getElementById("khoaId").value;
      const createPayload = {
        maGiangVien: document.getElementById("maGiangVien").value.trim(),
        hoTen: document.getElementById("hoTen").value.trim(),
        email: document.getElementById("email").value.trim(),
        khoaId: khoaRaw ? Number(khoaRaw) : null
      };

      const updatePayload = {
        hoTen: createPayload.hoTen,
        email: createPayload.email,
        khoaId: createPayload.khoaId
      };

      const stopLoading = window.setButtonLoading?.(submitBtn, state.editingId ? "Đang cập nhật..." : "Đang lưu...");

      try {
        if (state.editingId) {
          await window.apiFetch(`/lecturers/${state.editingId}`, {
            method: "PUT",
            body: JSON.stringify(updatePayload)
          });
          showPageMessage("Cập nhật giảng viên thành công.", "success");
        } else {
          await window.apiFetch("/lecturers", {
            method: "POST",
            body: JSON.stringify(createPayload)
          });
          showPageMessage("Thêm giảng viên thành công.", "success");
        }

        await loadLecturers();
        setCreateMode();
      } catch (error) {
        const action = state.editingId ? "Cập nhật" : "Thêm";
        showPageMessage(`${action} giảng viên thất bại: ${error.message}`, "danger");
      } finally {
        stopLoading?.();
      }
    });

    document.getElementById("cancelLecturerEditBtn").addEventListener("click", () => {
      setCreateMode();
    });
  }

  async function deleteLecturer(id) {
    if (!window.confirm("Bạn chắc chắn muốn xóa giảng viên này?")) {
      return;
    }

    try {
      await window.apiFetch(`/lecturers/${id}`, { method: "DELETE" });
      showPageMessage("Xóa giảng viên thành công.", "success");
      await loadLecturers();
    } catch (error) {
      showPageMessage(`Xóa giảng viên thất bại: ${error.message}`, "danger");
    }
  }

  async function loadLecturers() {
    try {
      const lecturers = await window.apiFetch("/lecturers");
      const rows = lecturers.map((x) => {
        const actionCell = `
          <div class="d-flex gap-1">
            <button class="btn btn-sm btn-outline-primary js-edit-lecturer" data-id="${x.giangVienId}">Sửa</button>
            <button class="btn btn-sm btn-outline-danger js-delete-lecturer" data-id="${x.giangVienId}">Xóa</button>
          </div>
        `;
        return [
          escapeHtml(x.maGiangVien),
          escapeHtml(x.hoTen),
          escapeHtml(x.email),
          escapeHtml(x.khoaId ?? "-"),
          actionCell
        ];
      });

      renderTableRowsHtml("lecturersTable", rows);
      document.querySelectorAll(".js-edit-lecturer").forEach((button) => {
        button.addEventListener("click", () => {
          const item = lecturers.find((x) => x.giangVienId === Number(button.dataset.id));
          if (item) {
            setEditMode(item);
          }
        });
      });
      document.querySelectorAll(".js-delete-lecturer").forEach((button) => {
        button.addEventListener("click", () => deleteLecturer(Number(button.dataset.id)));
      });

      markEditingRowById(state.editingId);
    } catch (error) {
      showPageMessage(`Không tải được danh sách giảng viên: ${error.message}`, "danger");
    }
  }

  try {
    await mountCreateForm();
    setCreateMode();
  } catch (error) {
    showPageMessage(`Không tải được form giảng viên: ${error.message}`, "danger");
  }

  if (createBtn) {
    createBtn.addEventListener("click", () => {
      setCreateMode();
      const formBlock = document.getElementById("lecturerQuickForm");
      if (formBlock) {
        formBlock.scrollIntoView({ behavior: "smooth", block: "start" });
      }
    });
  }

  document.addEventListener("keydown", (event) => {
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "s" && state.editingId) {
      event.preventDefault();
      document.getElementById("createLecturerForm")?.requestSubmit();
      return;
    }

    if (event.key === "Escape" && state.editingId) {
      setCreateMode();
      showPageMessage("Đã thoát chế độ sửa giảng viên.", "info");
    }
  });

  await loadLecturers();
});
