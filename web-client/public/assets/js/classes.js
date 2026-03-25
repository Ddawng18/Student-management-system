document.addEventListener("DOMContentLoaded", async () => {
  const createBtn = document.querySelector(".btn.btn-brand");
  const saveShortcut = `${window.getSaveShortcutKeyLabel ? window.getSaveShortcutKeyLabel() : "Ctrl"}+S`;
  const state = {
    editingId: null
  };

  function clearEditingRow() {
    document.querySelectorAll("#classesTable tr").forEach((row) => row.classList.remove("table-row-editing"));
  }

  function markEditingRowById(id) {
    clearEditingRow();
    if (!id) {
      return;
    }

    const activeBtn = document.querySelector(`.js-edit-class[data-id="${id}"]`);
    activeBtn?.closest("tr")?.classList.add("table-row-editing");
  }

  function setCreateMode() {
    const form = document.getElementById("createClassForm");
    const submitBtn = document.getElementById("submitClassBtn");
    const cancelBtn = document.getElementById("cancelClassEditBtn");
    const title = document.getElementById("classQuickFormTitle");
    const badge = document.getElementById("classEditBadge");
    const maLopInput = document.getElementById("maLop");

    if (!form || !submitBtn || !cancelBtn || !title || !badge || !maLopInput) {
      return;
    }

    state.editingId = null;
    form.reset();
    submitBtn.textContent = "Lưu lớp học";
    cancelBtn.classList.add("d-none");
    title.textContent = "Thêm lớp học nhanh";
    badge.classList.add("d-none");
    clearEditingRow();
    maLopInput.focus();
  }

  function setEditMode(item) {
    state.editingId = item.lopHocId;
    document.getElementById("maLop").value = item.maLop ?? "";
    document.getElementById("tenLop").value = item.tenLop ?? "";
    document.getElementById("khoaId").value = item.khoaId ?? "";
    document.getElementById("coVanHocTapId").value = item.coVanHocTapId ?? "";

    const submitBtn = document.getElementById("submitClassBtn");
    const cancelBtn = document.getElementById("cancelClassEditBtn");
    const title = document.getElementById("classQuickFormTitle");
    const badge = document.getElementById("classEditBadge");

    if (submitBtn) {
      submitBtn.textContent = "Cập nhật lớp học";
    }

    if (cancelBtn) {
      cancelBtn.classList.remove("d-none");
    }

    if (title) {
      title.textContent = "Cập nhật lớp học";
    }

    if (badge) {
      badge.classList.remove("d-none");
    }

    document.getElementById("classQuickForm")?.scrollIntoView({ behavior: "smooth", block: "start" });
    document.getElementById("tenLop")?.focus();
    markEditingRowById(item.lopHocId);
  }

  function option(id, label) {
    return `<option value="${id}">${escapeHtml(label)}</option>`;
  }

  async function mountCreateForm() {
    const host = document.querySelector(".container-fluid");
    if (!host || document.getElementById("classQuickForm")) {
      return;
    }

    const departments = await window.apiFetch("/departments");

    const block = document.createElement("div");
    block.id = "classQuickForm";
    block.className = "card surface-card mb-3";
    block.innerHTML = `
      <div id="classQuickFormTitle" class="card-header d-flex justify-content-between align-items-center">Thêm lớp học nhanh<span id="classEditBadge" class="editing-badge d-none">Đang sửa</span></div>
      <div class="card-body">
        <form id="createClassForm" class="quick-form-grid">
          <input id="maLop" class="form-control" placeholder="Mã lớp" required />
          <input id="tenLop" class="form-control" placeholder="Tên lớp" required />
          <select id="khoaId" class="form-control" required>
            <option value="">Chọn khoa</option>
            ${departments.map((x) => option(x.khoaId, `${x.maKhoa} - ${x.tenKhoa}`)).join("")}
          </select>
          <input id="coVanHocTapId" class="form-control" type="number" min="1" placeholder="Cố vấn ID (tùy chọn)" />
          <button id="submitClassBtn" type="submit" class="btn btn-brand">Lưu lớp học</button>
          <button id="cancelClassEditBtn" type="button" class="btn btn-outline-secondary d-none">Hủy sửa</button>
        </form>
        <p class="form-shortcut-hint">Mẹo: nhấn Esc để hủy sửa, nhấn ${saveShortcut} để lưu nhanh khi đang sửa.</p>
      </div>
    `;

    host.insertBefore(block, host.children[1] || null);

    const formEl = document.getElementById("createClassForm");
    const submitBtn = document.getElementById("submitClassBtn");
    window.wireRealtimeValidation?.(formEl);

    formEl.addEventListener("submit", async (event) => {
      event.preventDefault();

      if (!window.validateFormNow?.(formEl)) {
        showPageMessage("Vui lòng kiểm tra lại thông tin lớp học.", "warning");
        return;
      }

      const createPayload = {
        maLop: document.getElementById("maLop").value.trim(),
        tenLop: document.getElementById("tenLop").value.trim(),
        khoaId: Number(document.getElementById("khoaId").value)
      };

      const coVanRaw = document.getElementById("coVanHocTapId").value;
      const updatePayload = {
        maLop: createPayload.maLop,
        tenLop: createPayload.tenLop,
        khoaId: createPayload.khoaId,
        coVanHocTapId: coVanRaw.trim() ? Number(coVanRaw) : null
      };

      if (!createPayload.khoaId) {
        showPageMessage("Vui lòng chọn khoa cho lớp học.", "warning");
        return;
      }

      const stopLoading = window.setButtonLoading?.(submitBtn, state.editingId ? "Đang cập nhật..." : "Đang lưu...");

      try {
        if (state.editingId) {
          await window.apiFetch(`/classes/${state.editingId}`, {
            method: "PUT",
            body: JSON.stringify(updatePayload)
          });
          showPageMessage("Cập nhật lớp học thành công.", "success");
        } else {
          await window.apiFetch("/classes", {
            method: "POST",
            body: JSON.stringify(createPayload)
          });
          showPageMessage("Thêm lớp học thành công.", "success");
        }

        await loadClasses();
        setCreateMode();
      } catch (error) {
        const action = state.editingId ? "Cập nhật" : "Thêm";
        showPageMessage(`${action} lớp học thất bại: ${error.message}`, "danger");
      } finally {
        stopLoading?.();
      }
    });

    document.getElementById("cancelClassEditBtn").addEventListener("click", () => {
      setCreateMode();
    });
  }

  async function deleteClass(id) {
    if (!window.confirm("Bạn chắc chắn muốn xóa lớp học này?")) {
      return;
    }

    try {
      await window.apiFetch(`/classes/${id}`, { method: "DELETE" });
      showPageMessage("Xóa lớp học thành công.", "success");
      await loadClasses();
    } catch (error) {
      showPageMessage(`Xóa lớp học thất bại: ${error.message}`, "danger");
    }
  }

  async function loadClasses() {
    try {
      const classes = await window.apiFetch("/classes");
      const rows = classes.map((x) => {
        const actionCell = `
          <div class="d-flex gap-1">
            <button class="btn btn-sm btn-outline-primary js-edit-class" data-id="${x.lopHocId}">Sửa</button>
            <button class="btn btn-sm btn-outline-danger js-delete-class" data-id="${x.lopHocId}">Xóa</button>
          </div>
        `;
        return [
          escapeHtml(x.maLop),
          escapeHtml(x.tenLop),
          escapeHtml(x.khoaId),
          escapeHtml(x.coVanHocTapId ?? "-"),
          actionCell
        ];
      });

      renderTableRowsHtml("classesTable", rows);
      document.querySelectorAll(".js-edit-class").forEach((button) => {
        button.addEventListener("click", () => {
          const item = classes.find((x) => x.lopHocId === Number(button.dataset.id));
          if (item) {
            setEditMode(item);
          }
        });
      });
      document.querySelectorAll(".js-delete-class").forEach((button) => {
        button.addEventListener("click", () => deleteClass(Number(button.dataset.id)));
      });

      markEditingRowById(state.editingId);
    } catch (error) {
      showPageMessage(`Không tải được danh sách lớp học: ${error.message}`, "danger");
    }
  }

  try {
    await mountCreateForm();
    setCreateMode();
  } catch (error) {
    showPageMessage(`Không tải được form lớp học: ${error.message}`, "danger");
  }

  if (createBtn) {
    createBtn.addEventListener("click", () => {
      setCreateMode();
      const formBlock = document.getElementById("classQuickForm");
      if (formBlock) {
        formBlock.scrollIntoView({ behavior: "smooth", block: "start" });
      }
    });
  }

  document.addEventListener("keydown", (event) => {
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "s" && state.editingId) {
      event.preventDefault();
      document.getElementById("createClassForm")?.requestSubmit();
      return;
    }

    if (event.key === "Escape" && state.editingId) {
      setCreateMode();
      showPageMessage("Đã thoát chế độ sửa lớp học.", "info");
    }
  });

  await loadClasses();
});
