(() => {
  const { setFieldError, wirePasswordToggle, setSubmitting } = window.AuthUi;

  if (window.getSession()) {
    window.location.href = "/dashboard.html";
    return;
  }

  const loginForm = document.getElementById("loginForm");
  const emailInput = document.getElementById("email");
  const passwordInput = document.getElementById("password");
  const rememberUserInput = document.getElementById("rememberUser");
  const message = document.getElementById("message");
  const submitBtn = document.getElementById("loginSubmitBtn");
  const emailError = document.getElementById("emailError");
  const passwordError = document.getElementById("passwordError");
  const togglePasswordBtn = document.getElementById("togglePasswordBtn");

  if (!loginForm || !emailInput || !passwordInput || !submitBtn) {
    return;
  }

  const remembered = localStorage.getItem("sms_remember_user");
  if (remembered) {
    emailInput.value = remembered;
    if (rememberUserInput) {
      rememberUserInput.checked = true;
    }
  }

  function validateFields() {
    const email = emailInput.value.trim();
    const password = passwordInput.value;
    const emailOk = Boolean(email);
    const passwordOk = password.length >= 3;

    setFieldError(emailInput, emailError, !emailOk);
    setFieldError(passwordInput, passwordError, !passwordOk);
    return emailOk && passwordOk;
  }

  emailInput.addEventListener("input", () => {
    setFieldError(emailInput, emailError, !emailInput.value.trim());
  });

  passwordInput.addEventListener("input", () => {
    setFieldError(passwordInput, passwordError, passwordInput.value.length < 3);
  });

  wirePasswordToggle(togglePasswordBtn, [passwordInput]);

  loginForm.addEventListener("submit", async (event) => {
    event.preventDefault();

    if (!validateFields()) {
      message.textContent = "Vui lòng kiểm tra lại thông tin đăng nhập.";
      message.className = "auth-message error";
      const invalid = loginForm.querySelector(".is-invalid");
      invalid?.focus();
      return;
    }

    const email = emailInput.value.trim().toLowerCase();
    const matKhau = passwordInput.value;

    if (rememberUserInput?.checked) {
      localStorage.setItem("sms_remember_user", emailInput.value.trim());
    } else {
      localStorage.removeItem("sms_remember_user");
    }

    setSubmitting(submitBtn, true, "Đăng nhập", "Đang đăng nhập...");

    try {
      const result = await window.apiFetch("/auth/login", {
        method: "POST",
        body: JSON.stringify({ email, matKhau })
      });
      window.saveSession(result);
      message.textContent = "Đăng nhập thành công. Đang chuyển trang...";
      message.className = "auth-message ok";
      setTimeout(() => {
        window.location.href = "/dashboard.html";
      }, 600);
    } catch (error) {
      message.textContent = `Đăng nhập thất bại: ${error.message}`;
      message.className = "auth-message error";
    } finally {
      setSubmitting(submitBtn, false, "Đăng nhập", "Đang đăng nhập...");
    }
  });
})();
