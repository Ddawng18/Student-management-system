(() => {
  const {
    setFieldError,
    renderPasswordStrength,
    renderPasswordChecklist,
    wirePasswordToggle,
    setSubmitting,
    isValidEmail
  } = window.AuthUi;

  if (window.getSession()) {
    window.location.href = "/dashboard.html";
    return;
  }

  const registerForm = document.getElementById("registerForm");
  const fullNameInput = document.getElementById("fullName");
  const emailInput = document.getElementById("email");
  const passwordInput = document.getElementById("password");
  const confirmPasswordInput = document.getElementById("confirmPassword");
  const submitBtn = document.getElementById("registerSubmitBtn");
  const message = document.getElementById("message");

  const fullNameError = document.getElementById("fullNameError");
  const emailError = document.getElementById("emailError");
  const passwordError = document.getElementById("passwordError");
  const confirmPasswordError = document.getElementById("confirmPasswordError");
  const togglePasswordBtn = document.getElementById("togglePasswordBtn");
  const passwordStrengthBar = document.getElementById("passwordStrengthBar");
  const passwordStrengthText = document.getElementById("passwordStrengthText");
  const ruleLen = document.getElementById("ruleLen");
  const ruleCase = document.getElementById("ruleCase");
  const ruleNum = document.getElementById("ruleNum");
  const ruleSpec = document.getElementById("ruleSpec");

  if (!registerForm || !fullNameInput || !emailInput || !passwordInput || !confirmPasswordInput || !submitBtn) {
    return;
  }

  function validateFields() {
    const hoTen = fullNameInput.value.trim();
    const email = emailInput.value.trim();
    const matKhau = passwordInput.value;
    const matKhauNhapLai = confirmPasswordInput.value;

    const nameOk = hoTen.length >= 2;
    const emailOk = isValidEmail(email);
    const passwordOk = matKhau.length >= 3;
    const confirmOk = matKhauNhapLai.length >= 3 && matKhauNhapLai === matKhau;

    setFieldError(fullNameInput, fullNameError, !nameOk);
    setFieldError(emailInput, emailError, !emailOk);
    setFieldError(passwordInput, passwordError, !passwordOk);
    setFieldError(confirmPasswordInput, confirmPasswordError, !confirmOk);

    return nameOk && emailOk && passwordOk && confirmOk;
  }

  fullNameInput.addEventListener("input", () => {
    setFieldError(fullNameInput, fullNameError, fullNameInput.value.trim().length < 2);
  });

  emailInput.addEventListener("input", () => {
    setFieldError(emailInput, emailError, !isValidEmail(emailInput.value.trim()));
  });

  passwordInput.addEventListener("input", () => {
    setFieldError(passwordInput, passwordError, passwordInput.value.length < 3);
    setFieldError(confirmPasswordInput, confirmPasswordError, confirmPasswordInput.value !== passwordInput.value);
    renderPasswordStrength(passwordInput, passwordStrengthBar, passwordStrengthText);
    renderPasswordChecklist(passwordInput, { ruleLen, ruleCase, ruleNum, ruleSpec });
  });

  confirmPasswordInput.addEventListener("input", () => {
    setFieldError(confirmPasswordInput, confirmPasswordError, confirmPasswordInput.value !== passwordInput.value);
  });

  wirePasswordToggle(togglePasswordBtn, [passwordInput, confirmPasswordInput]);
  renderPasswordStrength(passwordInput, passwordStrengthBar, passwordStrengthText);
  renderPasswordChecklist(passwordInput, { ruleLen, ruleCase, ruleNum, ruleSpec });

  registerForm.addEventListener("submit", async (event) => {
    event.preventDefault();

    if (!validateFields()) {
      message.textContent = "Vui lòng kiểm tra lại thông tin đăng ký.";
      message.className = "auth-message error";
      const invalid = registerForm.querySelector(".is-invalid");
      invalid?.focus();
      return;
    }

    const hoTen = fullNameInput.value.trim();
    const email = emailInput.value.trim();
    const matKhau = passwordInput.value;

    setSubmitting(submitBtn, true, "Đăng ký", "Đang đăng ký...");

    try {
      const result = await window.apiFetch("/auth/register", {
        method: "POST",
        body: JSON.stringify({ hoTen, email, matKhau })
      });
      window.saveSession(result);
      message.textContent = "Đăng ký thành công. Đang chuyển trang...";
      message.className = "auth-message ok";
      setTimeout(() => {
        window.location.href = "/dashboard.html";
      }, 600);
    } catch (error) {
      message.textContent = `Đăng ký thất bại: ${error.message}`;
      message.className = "auth-message error";
    } finally {
      setSubmitting(submitBtn, false, "Đăng ký", "Đang đăng ký...");
    }
  });
})();
