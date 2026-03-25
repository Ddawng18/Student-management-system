function normalizeSession(authData) {
  if (!authData || typeof authData !== "object") {
    return authData;
  }

  const normalized = { ...authData };
  const email = String(normalized.email || "").trim().toLowerCase();
  if (email === "admin123" && normalized.hoTen !== "Admin") {
    normalized.hoTen = "Admin";
  }

  return normalized;
}

function saveSession(authData) {
  localStorage.setItem("sms_auth", JSON.stringify(normalizeSession(authData)));
}

function getSession() {
  const raw = localStorage.getItem("sms_auth");
  if (!raw) {
    return null;
  }

  try {
    const session = normalizeSession(JSON.parse(raw));
    localStorage.setItem("sms_auth", JSON.stringify(session));
    return session;
  } catch {
    return null;
  }
}

function clearSession() {
  localStorage.removeItem("sms_auth");
}

function requireAuth() {
  const session = getSession();
  const page = window.location.pathname.split("/").pop();

  if (!session && page !== "login.html" && page !== "register.html") {
    window.location.href = "/login.html";
    return false;
  }

  return true;
}

window.saveSession = saveSession;
window.getSession = getSession;
window.clearSession = clearSession;
window.requireAuth = requireAuth;
