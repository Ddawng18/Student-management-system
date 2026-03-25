# Phase 5 - Frontend template integration and page build

## Implemented stack

- Node.js + Express static server
- HTML/CSS/JavaScript (no React/Vue/Angular)
- AdminLTE 3 free dashboard template via CDN

## Frontend project

- web-client/package.json
- web-client/server.js
- web-client/public

## Pages completed

- dashboard.html
- students.html
- lecturers.html
- departments.html
- classes.html
- semesters.html
- courses.html
- enrollments.html
- grades.html
- results.html

## UI and assets

- Shared style: public/assets/css/styles.css
- Shared helpers: public/assets/js/common.js
- Configurable shell layout: public/assets/js/shell.js
- Per-page scripts with sample data:
  - dashboard.js
  - students.js
  - lecturers.js
  - departments.js
  - classes.js
  - semesters.js
  - courses.js
  - enrollments.js
  - grades.js
  - results.js

## Commands

1. Install dependencies:

   cd web-client
   npm install

2. Run frontend:

   npm run dev

3. Open in browser:

   http://localhost:3000/dashboard.html

## Verification

- Checked pages return HTTP 200:
  - /dashboard.html
  - /students.html
  - /results.html

## Notes for phase 6

- `assets/js/api.js` already provides `apiFetch()` with base URL to backend API.
- Next phase will replace sample data scripts with real fetch calls to ASP.NET endpoints.
