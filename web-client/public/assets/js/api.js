function resolveApiBaseUrl() {
  if (window.API_BASE_URL) {
    return window.API_BASE_URL;
  }

  const isLocalHost = window.location.hostname === "localhost" || window.location.hostname === "127.0.0.1";
  if (isLocalHost && window.location.port === "3000") {
    return `${window.location.protocol}//${window.location.hostname}:5019/api/v1`;
  }

  return "/api/v1";
}

window.APP_CONFIG = {
  API_BASE_URL: resolveApiBaseUrl()
};

async function apiFetch(path, options = {}) {
  const sessionRaw = localStorage.getItem("sms_auth");
  let token = "";

  if (sessionRaw) {
    try {
      const session = JSON.parse(sessionRaw);
      token = session?.accessToken || "";
    } catch {
      token = "";
    }
  }

  const response = await fetch(`${window.APP_CONFIG.API_BASE_URL}${path}`, {
    headers: {
      "Content-Type": "application/json",
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...(options.headers || {})
    },
    ...options
  });

  if (!response.ok) {
    const raw = await response.text();
    let message = raw;

    try {
      const parsed = JSON.parse(raw);
      message = parsed?.title || parsed?.message || raw;
    } catch {
      message = raw;
    }

    throw new Error(message || `Request failed: ${response.status}`);
  }

  if (response.status === 204) {
    return null;
  }

  return response.json();
}

window.apiFetch = apiFetch;
