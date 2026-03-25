document.addEventListener("DOMContentLoaded", async () => {
  if (window.requireAuth && !window.requireAuth()) {
    return;
  }

  const params = new URLSearchParams(window.location.search);
  const studentId = Number(params.get("id"));

  const form = document.getElementById("studentProfileForm");
  const saveBtn = document.getElementById("saveProfileBtn");
  const avatarInput = document.getElementById("avatarInput");

  let student = null;
  let avatarDraft = "";

  const inputIds = [
    "maSinhVien",
    "hoTen",
    "email",
    "ngaySinh",
    "lopHoc",
    "gioiTinh",
    "queQuan",
    "soDienThoai",
    "danToc",
    "tonGiao",
    "cccd",
    "ngayCapCccd",
    "noiCapCccd",
    "noiTamTru",
    "diaChiThuongTru",
    "hoTenPhuHuynh",
    "soDienThoaiPhuHuynh",
    "ngheNghiepPhuHuynh",
    "ghiChu"
  ];

  const elements = inputIds.reduce((acc, id) => {
    acc[id] = document.getElementById(id);
    return acc;
  }, {});

  function showAlert(message, type = "success") {
    const host = document.getElementById("profileAlertHost");
    if (!host) {
      return;
    }

    host.innerHTML = `<div class="alert alert-${type} mb-3">${escapeHtml(message)}</div>`;
  }

  function formatClassValue(lopHocId) {
    if (lopHocId === null || lopHocId === undefined || lopHocId === "") {
      return "Chưa phân lớp";
    }

    return `Lớp #${lopHocId}`;
  }

  function initialsFromName(name) {
    const parts = String(name || "")
      .trim()
      .split(/\s+/)
      .filter(Boolean);

    if (!parts.length) {
      return "SV";
    }

    if (parts.length === 1) {
      return parts[0].slice(0, 2).toUpperCase();
    }

    return `${parts[0][0] || ""}${parts[parts.length - 1][0] || ""}`.toUpperCase();
  }

  function generateAvatarDataUri(name) {
    const initials = initialsFromName(name);
    const svg = `<svg xmlns='http://www.w3.org/2000/svg' width='240' height='240' viewBox='0 0 240 240'><defs><linearGradient id='g' x1='0' y1='0' x2='1' y2='1'><stop stop-color='#0ea5e9'/><stop offset='1' stop-color='#22c55e'/></linearGradient></defs><rect width='240' height='240' rx='36' fill='url(#g)'/><text x='50%' y='54%' dominant-baseline='middle' text-anchor='middle' font-size='88' font-family='Manrope,Segoe UI,sans-serif' font-weight='800' fill='white'>${initials}</text></svg>`;
    return `data:image/svg+xml;charset=UTF-8,${encodeURIComponent(svg)}`;
  }

  function resolveAvatar(studentData) {
    if (avatarDraft) {
      return avatarDraft;
    }

    if (studentData?.avatarUrl) {
      return studentData.avatarUrl;
    }

    return generateAvatarDataUri(studentData?.hoTen || "Sinh viên");
  }

  function syncHero(studentData) {
    document.getElementById("profileAvatar").src = resolveAvatar(studentData);
    document.getElementById("profileName").textContent = studentData.hoTen || "Sinh viên";
    document.getElementById("profileSubline").textContent = `${studentData.maSinhVien || "-"} • ${studentData.email || "-"}`;
    document.getElementById("profileChipClass").textContent = `Lớp: ${formatClassValue(studentData.lopHocId)}`;
    document.getElementById("profileChipBirth").textContent = `Ngày sinh: ${formatDate(studentData.ngaySinh)}`;
    document.getElementById("profileChipHometown").textContent = `Quê quán: ${studentData.queQuan || "Chưa cập nhật"}`;
  }

  function fillForm(studentData) {
    elements.maSinhVien.value = studentData.maSinhVien || "";
    elements.hoTen.value = studentData.hoTen || "";
    elements.email.value = studentData.email || "";
    elements.ngaySinh.value = formatDate(studentData.ngaySinh);
    elements.lopHoc.value = formatClassValue(studentData.lopHocId);

    elements.gioiTinh.value = studentData.gioiTinh || "";
    elements.queQuan.value = studentData.queQuan || "";
    elements.soDienThoai.value = studentData.soDienThoai || "";
    elements.danToc.value = studentData.danToc || "";
    elements.tonGiao.value = studentData.tonGiao || "";
    elements.cccd.value = studentData.cccd || "";
    elements.ngayCapCccd.value = formatDate(studentData.ngayCapCccd);
    elements.noiCapCccd.value = studentData.noiCapCccd || "";
    elements.noiTamTru.value = studentData.noiTamTru || "";
    elements.diaChiThuongTru.value = studentData.diaChiThuongTru || "";
    elements.hoTenPhuHuynh.value = studentData.hoTenPhuHuynh || "";
    elements.soDienThoaiPhuHuynh.value = studentData.soDienThoaiPhuHuynh || "";
    elements.ngheNghiepPhuHuynh.value = studentData.ngheNghiepPhuHuynh || "";
    elements.ghiChu.value = studentData.ghiChu || "";
  }

  function readAsDataUrl(file) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => resolve(String(reader.result || ""));
      reader.onerror = () => reject(new Error("Không đọc được file ảnh."));
      reader.readAsDataURL(file);
    });
  }

  if (!Number.isInteger(studentId) || studentId <= 0) {
    showAlert("Thiếu mã sinh viên hợp lệ để mở hồ sơ.", "danger");
    return;
  }

  try {
    student = await window.apiFetch(`/students/${studentId}`);
  } catch (error) {
    showAlert(`Không tải được hồ sơ sinh viên: ${error.message}`, "danger");
    return;
  }

  fillForm(student);
  syncHero(student);

  avatarInput?.addEventListener("change", async () => {
    const file = avatarInput.files?.[0];
    if (!file) {
      return;
    }

    if (!file.type.startsWith("image/")) {
      showAlert("Vui lòng chọn file ảnh hợp lệ.", "warning");
      return;
    }

    try {
      avatarDraft = await readAsDataUrl(file);
      document.getElementById("profileAvatar").src = avatarDraft;
      showAlert("Đã tải ảnh, nhớ bấm Lưu hồ sơ để lưu lại.", "info");
    } catch (error) {
      showAlert(error.message, "danger");
    }
  });

  form?.addEventListener("input", () => {
    syncHero({
      ...student,
      hoTen: elements.hoTen.value.trim(),
      email: elements.email.value.trim(),
      ngaySinh: elements.ngaySinh.value,
      queQuan: elements.queQuan.value.trim()
    });
  });

  form?.addEventListener("submit", (event) => event.preventDefault());

  saveBtn?.addEventListener("click", async () => {
    if (!window.validateFormNow?.(form)) {
      showAlert("Vui lòng kiểm tra lại thông tin bắt buộc.", "warning");
      return;
    }

    const stopLoading = window.setButtonLoading?.(saveBtn, "Đang lưu hồ sơ...");

    try {
      const updatePayload = {
        hoTen: elements.hoTen.value.trim(),
        email: elements.email.value.trim(),
        ngaySinh: elements.ngaySinh.value,
        lopHocId: student.lopHocId ?? null,
        avatarUrl: avatarDraft || student.avatarUrl || null,
        gioiTinh: elements.gioiTinh.value || null,
        queQuan: elements.queQuan.value.trim() || null,
        soDienThoai: elements.soDienThoai.value.trim() || null,
        diaChiThuongTru: elements.diaChiThuongTru.value.trim() || null,
        noiTamTru: elements.noiTamTru.value.trim() || null,
        danToc: elements.danToc.value.trim() || null,
        tonGiao: elements.tonGiao.value.trim() || null,
        cccd: elements.cccd.value.trim() || null,
        ngayCapCccd: elements.ngayCapCccd.value || null,
        noiCapCccd: elements.noiCapCccd.value.trim() || null,
        hoTenPhuHuynh: elements.hoTenPhuHuynh.value.trim() || null,
        soDienThoaiPhuHuynh: elements.soDienThoaiPhuHuynh.value.trim() || null,
        ngheNghiepPhuHuynh: elements.ngheNghiepPhuHuynh.value.trim() || null,
        ghiChu: elements.ghiChu.value.trim() || null
      };

      await window.apiFetch(`/students/${studentId}`, {
        method: "PUT",
        body: JSON.stringify(updatePayload)
      });

      student = await window.apiFetch(`/students/${studentId}`);
      avatarDraft = "";

      fillForm(student);
      syncHero(student);
      showAlert("Lưu hồ sơ sinh viên thành công.", "success");
    } catch (error) {
      showAlert(`Lưu hồ sơ thất bại: ${error.message}`, "danger");
    } finally {
      stopLoading?.();
    }
  });
});
