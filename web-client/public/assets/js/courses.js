document.addEventListener("DOMContentLoaded", async () => {
  const createBtn = document.querySelector(".btn.btn-brand");
  const saveShortcut = `${window.getSaveShortcutKeyLabel ? window.getSaveShortcutKeyLabel() : "Ctrl"}+S`;
  const state = {
    editingId: null
  };

  function clearEditingRow() {
    document.querySelectorAll("#coursesTable tr").forEach((row) => row.classList.remove("table-row-editing"));
  }

  function markEditingRowById(id) {
    clearEditingRow();
    if (!id) {
      return;
    }

    const activeBtn = document.querySelector(`.js-edit-course[data-id="${id}"]`);
    activeBtn?.closest("tr")?.classList.add("table-row-editing");
  }

  function setCreateMode() {
    const form = document.getElementById("createCourseForm");
    const submitBtn = document.getElementById("submitCourseBtn");
    const cancelBtn = document.getElementById("cancelCourseEditBtn");
    const title = document.getElementById("courseQuickFormTitle");
    const badge = document.getElementById("courseEditBadge");
    const maMonInput = document.getElementById("maMonHoc");

    if (!form || !submitBtn || !cancelBtn || !title || !badge || !maMonInput) {
      return;
    }

    state.editingId = null;
    form.reset();
    maMonInput.disabled = false;
    submitBtn.textContent = "Lưu môn học";
    cancelBtn.classList.add("d-none");
    title.textContent = "Thêm môn học nhanh";
    badge.classList.add("d-none");
    clearEditingRow();
    maMonInput.focus();
  }

  function setEditMode(item) {
    state.editingId = item.monHocId;

    document.getElementById("maMonHoc").value = item.maMonHoc ?? "";
    document.getElementById("tenMon").value = item.tenMon ?? "";
    document.getElementById("soTinChi").value = item.soTinChi ?? "";
    document.getElementById("hocKyId").value = item.hocKyId ?? "";
    document.getElementById("giangVienGiangDayId").value = item.giangVienGiangDayId ?? "";
    document.getElementById("khoaId").value = item.khoaId ?? "";

    const submitBtn = document.getElementById("submitCourseBtn");
    const cancelBtn = document.getElementById("cancelCourseEditBtn");
    const title = document.getElementById("courseQuickFormTitle");
    const badge = document.getElementById("courseEditBadge");
    const maMonInput = document.getElementById("maMonHoc");

    if (submitBtn) {
      submitBtn.textContent = "Cập nhật môn học";
    }

    if (cancelBtn) {
      cancelBtn.classList.remove("d-none");
    }

    if (title) {
      title.textContent = "Cập nhật môn học";
    }

    if (badge) {
      badge.classList.remove("d-none");
    }

    if (maMonInput) {
      maMonInput.disabled = true;
    }

    document.getElementById("courseQuickForm")?.scrollIntoView({ behavior: "smooth", block: "start" });
    document.getElementById("tenMon")?.focus();
    markEditingRowById(item.monHocId);
  }

  function option(id, label) {
    return `<option value="${id}">${escapeHtml(label)}</option>`;
  }

  async function mountCreateForm() {
    const host = document.querySelector(".container-fluid");
    if (!host || document.getElementById("courseQuickForm")) {
      return;
    }

    const [lecturers, semesters, departments] = await Promise.all([
      window.apiFetch("/lecturers"),
      window.apiFetch("/semesters"),
      window.apiFetch("/departments")
    ]);

    const block = document.createElement("div");
    block.id = "courseQuickForm";
    block.className = "card surface-card mb-3";
    block.innerHTML = `
      <div id="courseQuickFormTitle" class="card-header d-flex justify-content-between align-items-center">Thêm môn học nhanh<span id="courseEditBadge" class="editing-badge d-none">Đang sửa</span></div>
      <div class="card-body">
        <form id="createCourseForm" class="quick-form-grid">
          <input id="maMonHoc" class="form-control" placeholder="Mã môn học" required />
          <input id="tenMon" class="form-control" placeholder="Tên môn học" required />
          <input id="soTinChi" class="form-control" type="number" min="1" placeholder="Số tín chỉ" required />
          <select id="khoaId" class="form-control">
            <option value="">Không chọn khoa</option>
            ${departments.map((x) => option(x.khoaId, `${x.maKhoa} - ${x.tenKhoa}`)).join("")}
          </select>
          <select id="hocKyId" class="form-control" required>
            <option value="">Chọn học kỳ</option>
            ${semesters.map((x) => option(x.hocKyId, x.tenHocKy ?? `Học kỳ ${x.hocKyId}`)).join("")}
          </select>
          <select id="giangVienGiangDayId" class="form-control">
            <option value="">Không chọn giảng viên</option>
            ${lecturers.map((x) => option(x.giangVienId, `${x.maGiangVien} - ${x.hoTen}`)).join("")}
          </select>
          <button id="submitCourseBtn" type="submit" class="btn btn-brand">Lưu môn học</button>
          <button id="cancelCourseEditBtn" type="button" class="btn btn-outline-secondary d-none">Hủy sửa</button>
        </form>
        <p class="form-shortcut-hint">Mẹo: nhấn Esc để hủy sửa, nhấn ${saveShortcut} để lưu nhanh khi đang sửa.</p>
      </div>
    `;

    host.insertBefore(block, host.children[1] || null);

    const formEl = document.getElementById("createCourseForm");
    const submitBtn = document.getElementById("submitCourseBtn");
    window.wireRealtimeValidation?.(formEl);

    formEl.addEventListener("submit", async (event) => {
      event.preventDefault();

      if (!window.validateFormNow?.(formEl)) {
        showPageMessage("Vui lòng kiểm tra lại thông tin môn học.", "warning");
        return;
      }

      const hocKyId = Number(document.getElementById("hocKyId").value);
      if (!hocKyId) {
        showPageMessage("Vui lòng chọn học kỳ.", "warning");
        return;
      }

      const khoaRaw = document.getElementById("khoaId").value;
      const lecturerRaw = document.getElementById("giangVienGiangDayId").value;
      const createPayload = {
        maMonHoc: document.getElementById("maMonHoc").value.trim(),
        tenMon: document.getElementById("tenMon").value.trim(),
        soTinChi: Number(document.getElementById("soTinChi").value),
        khoaId: khoaRaw ? Number(khoaRaw) : null,
        hocKyId,
        giangVienGiangDayId: lecturerRaw ? Number(lecturerRaw) : null
      };

      const updatePayload = {
        tenMon: createPayload.tenMon,
        soTinChi: createPayload.soTinChi,
        khoaId: createPayload.khoaId,
        giangVienGiangDayId: createPayload.giangVienGiangDayId,
        hocKyId: createPayload.hocKyId
      };

      const stopLoading = window.setButtonLoading?.(submitBtn, state.editingId ? "Đang cập nhật..." : "Đang lưu...");

      try {
        if (state.editingId) {
          await window.apiFetch(`/courses/${state.editingId}`, {
            method: "PUT",
            body: JSON.stringify(updatePayload)
          });
          showPageMessage("Cập nhật môn học thành công.", "success");
        } else {
          await window.apiFetch("/courses", {
            method: "POST",
            body: JSON.stringify(createPayload)
          });
          showPageMessage("Thêm môn học thành công.", "success");
        }

        await loadCourses();
        setCreateMode();
      } catch (error) {
        const action = state.editingId ? "Cập nhật" : "Thêm";
        showPageMessage(`${action} môn học thất bại: ${error.message}`, "danger");
      } finally {
        stopLoading?.();
      }
    });

    document.getElementById("cancelCourseEditBtn").addEventListener("click", () => {
      setCreateMode();
    });
  }

  async function deleteCourse(id) {
    if (!window.confirm("Bạn chắc chắn muốn xóa môn học này?")) {
      return;
    }

    try {
      await window.apiFetch(`/courses/${id}`, { method: "DELETE" });
      showPageMessage("Xóa môn học thành công.", "success");
      await loadCourses();
    } catch (error) {
      showPageMessage(`Xóa môn học thất bại: ${error.message}`, "danger");
    }
  }

  async function loadCourses() {
    try {
      const courses = await window.apiFetch("/courses");
      const rows = courses.map((x) => {
        const actionCell = `
          <div class="d-flex gap-1">
            <button class="btn btn-sm btn-outline-primary js-edit-course" data-id="${x.monHocId}">Sửa</button>
            <button class="btn btn-sm btn-outline-danger js-delete-course" data-id="${x.monHocId}">Xóa</button>
          </div>
        `;
        return [
          escapeHtml(x.maMonHoc),
          escapeHtml(x.tenMon),
          escapeHtml(x.soTinChi),
          escapeHtml(x.giangVienGiangDayId ?? "-"),
          escapeHtml(x.hocKyId ?? "-"),
          actionCell
        ];
      });

      renderTableRowsHtml("coursesTable", rows);
      document.querySelectorAll(".js-edit-course").forEach((button) => {
        button.addEventListener("click", () => {
          const item = courses.find((x) => x.monHocId === Number(button.dataset.id));
          if (item) {
            setEditMode(item);
          }
        });
      });
      document.querySelectorAll(".js-delete-course").forEach((button) => {
        button.addEventListener("click", () => deleteCourse(Number(button.dataset.id)));
      });

      markEditingRowById(state.editingId);
    } catch (error) {
      showPageMessage(`Không tải được danh sách môn học: ${error.message}`, "danger");
    }
  }

  try {
    await mountCreateForm();
    setCreateMode();
  } catch (error) {
    showPageMessage(`Không tải được form môn học: ${error.message}`, "danger");
  }

  if (createBtn) {
    createBtn.addEventListener("click", () => {
      setCreateMode();
      const formBlock = document.getElementById("courseQuickForm");
      if (formBlock) {
        formBlock.scrollIntoView({ behavior: "smooth", block: "start" });
      }
    });
  }

  document.addEventListener("keydown", (event) => {
    if ((event.ctrlKey || event.metaKey) && event.key.toLowerCase() === "s" && state.editingId) {
      event.preventDefault();
      document.getElementById("createCourseForm")?.requestSubmit();
      return;
    }

    if (event.key === "Escape" && state.editingId) {
      setCreateMode();
      showPageMessage("Đã thoát chế độ sửa môn học.", "info");
    }
  });

  await loadCourses();
});
