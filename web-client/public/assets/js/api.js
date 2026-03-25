window.APP_CONFIG = {
  API_BASE_URL: window.API_BASE_URL || "/api/v1"
};

async function apiFetch(path, options = {}) {
  const response = await fetch(`${window.APP_CONFIG.API_BASE_URL}${path}`, {
    headers: {
      "Content-Type": "application/json",
      ...(options.headers || {})
    },
    ...options
  });

  if (!response.ok) {
    const message = await response.text();
    throw new Error(message || `Request failed: ${response.status}`);
  }

  if (response.status === 204) {
    return null;
  }

  return response.json();
}

window.apiFetch = apiFetch;
