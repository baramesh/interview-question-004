# Design QA — Profile production copy and placeholder

## Evidence

- Source truth: the original `No4.docx` attachment and `documentation/05-ui-desc/01-profile/01-create-profile/uis-pf-01-create-profile.md`
- Final full view: `documentation/09-qa-and-test/01-profile/screenshots/tc-pf-content-001.png`
- Profile placeholder state: `documentation/09-qa-and-test/01-profile/screenshots/tc-pf-content-001.png`
- Uploaded-photo state: `documentation/09-qa-and-test/01-profile/screenshots/tc-pf-e2e-009.png`
- Datepicker state: `documentation/09-qa-and-test/01-profile/screenshots/tc-pf-e2e-010.png`
- Success Toast state: `documentation/09-qa-and-test/01-profile/screenshots/tc-pf-e2e-005.png`
- Viewport and CSS size: 1440×1100 px at device scale factor 1
- Full-view source and implementation pixels: 1440×1100; no density normalization required
- Compared state: desktop initial Profile form; responsive, uploaded-photo, Datepicker, validation, failure and success states inspected separately

## Findings

No actionable P0, P1, or P2 findings remain.

- Fonts and typography: Google Sans, weights and section hierarchy remain consistent.
- Spacing and layout rhythm: the form preserves its four distinct information sections, field grid, footer actions and responsive behavior.
- Colors and visual tokens: green accents, neutral surfaces, borders, shadows and semantic validation colors remain consistent.
- Image quality and asset fidelity: the text-based `PF` fallback was replaced by the Material Icons `person_outline` glyph loaded locally; uploaded PNG/JPEG previews retain the circular crop.
- Copy and content: assessment identifiers, internal labels, test data, tautological section descriptions and the sample first name are absent. User-facing required, upload constraint, field, action, validation and Toast copy remains.

## Comparison History

1. P2 — the header exposed `Interview Question 004`, `Profile management` and `Personal profile`, which describe the implementation or assessment rather than helping the user.
2. P2 — each section repeated a description already conveyed by its heading, the First-name field exposed sample test data, and the empty avatar used the internal-looking letters `PF`.
3. Fix — retained the product name and Profile heading, removed internal or duplicate copy, removed the sample value and used the local Material Icons profile placeholder.
4. Visual correction — explicitly enabled the icon font ligature so `person_outline` renders as a profile symbol rather than clipped text.
5. Post-fix evidence — `TC-PF-CONTENT-001` passed, the empty and uploaded avatar states render correctly, and the overall information hierarchy and responsive layout are unchanged.

## Implementation Checklist

- [x] Keep only text with a clear user purpose.
- [x] Keep Profile sections visually distinct.
- [x] Use a real icon-library asset for the empty profile image.
- [x] Preserve PNG/JPEG guidance, 2 MB limit and upload behavior.
- [x] Preserve Angular Material Datepicker and Occupation master data behavior.
- [x] Verify content, responsive, validation, success, failure and security flows through Playwright.

## Follow-up Polish

No remaining P3 item is required for this change.

final result: passed
