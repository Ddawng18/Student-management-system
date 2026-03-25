function setFieldError(input, errorEl, isInvalid) {
  input.classList.toggle("is-invalid", isInvalid);
  if (errorEl) {
    errorEl.hidden = !isInvalid;
  }
}

function evaluatePasswordStrength(value) {
  let score = 0;
  if (value.length >= 8) score += 1;
  if (/[a-z]/.test(value) && /[A-Z]/.test(value)) score += 1;
  if (/\d/.test(value)) score += 1;
  if (/[^A-Za-z0-9]/.test(value)) score += 1;

  if (!value) return { label: "Chưa nhập", level: "none", percent: 0 };
  if (score <= 1) return { label: "Yếu", level: "weak", percent: 25 };
  if (score === 2) return { label: "Trung bình", level: "medium", percent: 55 };
  if (score === 3) return { label: "Khá", level: "good", percent: 78 };
  return { label: "Mạnh", level: "strong", percent: 100 };
}

function renderPasswordStrength(input, barEl, textEl) {
  if (!input || !barEl || !textEl) {
    return;
  }

  const result = evaluatePasswordStrength(input.value);
  barEl.style.width = `${result.percent}%`;
  barEl.dataset.level = result.level;
  textEl.textContent = `Mức độ: ${result.label}`;
}

function renderPasswordChecklist(input, rules) {
  if (!input || !rules) {
    return;
  }

  const value = input.value;
  rules.ruleLen?.classList.toggle("ok", value.length >= 8);
  rules.ruleCase?.classList.toggle("ok", /[a-z]/.test(value) && /[A-Z]/.test(value));
  rules.ruleNum?.classList.toggle("ok", /\d/.test(value));
  rules.ruleSpec?.classList.toggle("ok", /[^A-Za-z0-9]/.test(value));
}

function wirePasswordToggle(toggleBtn, passwordInputs) {
  if (!toggleBtn || !Array.isArray(passwordInputs) || passwordInputs.length === 0) {
    return;
  }

  toggleBtn.addEventListener("click", () => {
    const isHidden = passwordInputs[0].type === "password";
    passwordInputs.forEach((input) => {
      input.type = isHidden ? "text" : "password";
    });

    toggleBtn.textContent = isHidden ? "Ẩn" : "Hiện";
    toggleBtn.setAttribute("aria-label", isHidden ? "Ẩn mật khẩu" : "Hiện mật khẩu");
  });
}

function setSubmitting(button, isSubmitting, normalText, loadingText) {
  if (!button) {
    return;
  }

  button.disabled = isSubmitting;
  button.classList.toggle("is-loading", isSubmitting);
  button.textContent = isSubmitting ? loadingText : normalText;
}

function isValidEmail(value) {
  return /.+@.+\..+/.test(value);
}

window.AuthUi = {
  setFieldError,
  evaluatePasswordStrength,
  renderPasswordStrength,
  renderPasswordChecklist,
  wirePasswordToggle,
  setSubmitting,
  isValidEmail
};
