import { expect, test, type Page } from '@playwright/test';

const image = {
  name: 'profile.png',
  mimeType: 'image/png',
  buffer: Buffer.from(
    'iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mNk+A8AAQUBAScY42YAAAAASUVORK5CYII=',
    'base64',
  ),
};

test.beforeEach(async ({ page }) => {
  await page.goto('/');
});

test('TC-PF-E2E-001 แสดงหมวดข้อมูล ฟิลด์ และปุ่มตามข้อกำหนด', async ({ page }) => {
  await expect(page.locator('link[rel="stylesheet"]')).toHaveAttribute('media', 'all');
  await page.evaluate(() => document.fonts.ready);
  const fontFamily = await page.evaluate(() => getComputedStyle(document.body).fontFamily);
  expect(fontFamily).toContain('Google Sans');
  await expect(page.getByTestId('profile-page')).toBeVisible();
  await expect(page.getByTestId('profile-form')).toBeVisible();
  await expect(page.getByTestId('profile-photo-section')).toBeVisible();
  await expect(page.getByTestId('personal-details-section')).toBeVisible();
  await expect(page.getByTestId('contact-details-section')).toBeVisible();
  await expect(page.getByTestId('professional-details-section')).toBeVisible();
  await expect(page.getByTestId('required-fields-note')).toHaveText('Fields marked * are required');
  await expect(page.locator('.mat-mdc-form-field-required-marker')).toHaveCount(6);
  await expect(page.locator('.required-marker')).toHaveCount(3);
  await expect(page.getByTestId('first-name-input')).toBeVisible();
  await expect(page.getByTestId('last-name-input')).toBeVisible();
  await expect(page.getByTestId('email-input')).toBeVisible();
  await expect(page.getByTestId('phone-input')).toBeVisible();
  await expect(page.getByTestId('birth-date-input')).toBeVisible();
  await expect(page.getByTestId('birth-date-toggle')).toBeVisible();
  await expect(page.getByTestId('birth-date-input')).not.toHaveAttribute('type', 'date');
  await expect(page.getByTestId('occupation-select')).toBeVisible();
  await expect(page.getByTestId('sex-radio-group')).toBeVisible();
  await expect(page.getByTestId('choose-image-button')).toBeVisible();
  await expect(page.getByTestId('profile-image-input')).toHaveAttribute(
    'accept',
    'image/png,image/jpeg',
  );
  await expect(page.getByTestId('clear-button')).toBeVisible();
  await expect(page.getByTestId('save-button')).toBeVisible();
});

test('TC-PF-E2E-002 ปฏิเสธการส่งแบบฟอร์มว่างโดยไม่เรียก API', async ({ page }) => {
  let createRequests = 0;
  page.on('request', (request) => {
    if (request.method() === 'POST' && request.url().endsWith('/api/profiles')) {
      createRequests += 1;
    }
  });

  await page.getByTestId('save-button').click();

  await expect(page.getByTestId('first-name-error')).toHaveText('This field is required.');
  await expect(page.getByTestId('last-name-error')).toHaveText('This field is required.');
  await expect(page.getByTestId('email-error')).toHaveText('This field is required.');
  await expect(page.getByTestId('phone-error')).toHaveText('This field is required.');
  await expect(page.getByTestId('birth-date-error')).toHaveText('This field is required.');
  await expect(page.getByTestId('occupation-error')).toHaveText('This field is required.');
  await expect(page.getByTestId('sex-error')).toHaveText('This field is required.');
  await expect(page.getByTestId('profile-image-error')).toHaveText('This field is required.');
  expect(createRequests).toBe(0);
});

test('TC-PF-E2E-003 แสดงข้อผิดพลาดของอีเมล โทรศัพท์ และวันเกิด', async ({ page }) => {
  await page.getByTestId('email-input').fill('not-an-email');
  await page.getByTestId('phone-input').fill('12345');
  await page.getByTestId('birth-date-input').fill('2099-01-01');
  await page.getByTestId('save-button').click();

  await expect(page.getByTestId('email-error')).toHaveText('Please provide a valid email.');
  await expect(page.getByTestId('phone-error')).toHaveText('Please provide a valid phone number.');
  await expect(page.getByTestId('birth-date-error')).toHaveText(
    'Use a past date in DD/MM/YYYY format.',
  );
});

test('TC-PF-E2E-004 ปุ่ม Clear ล้างข้อมูลและรูปตัวอย่าง', async ({ page }) => {
  await fillValidForm(page, 'clear');
  await expect(page.getByAltText('Selected profile preview')).toBeVisible();
  await expect(page.getByTestId('remove-image-button')).toBeVisible();

  await page.getByTestId('clear-button').click();

  await expect(page.getByTestId('first-name-input')).toHaveValue('');
  await expect(page.getByTestId('last-name-input')).toHaveValue('');
  await expect(page.getByTestId('email-input')).toHaveValue('');
  await expect(page.getByTestId('phone-input')).toHaveValue('');
  await expect(page.getByTestId('birth-date-input')).toHaveValue('');
  await expect(page.getByText('No file selected')).toBeVisible();
  await expect(page.getByAltText('Selected profile preview')).toHaveCount(0);
  await expect(page.getByTestId('remove-image-button')).toHaveCount(0);
  await expect(page.getByRole('radio', { checked: true })).toHaveCount(0);
});

test('TC-PF-E2E-005 บันทึกโปรไฟล์ผ่าน API และล้างสถานะแบบฟอร์ม', async ({ page }) => {
  await fillValidForm(page, `${Date.now()}`);

  const responsePromise = page.waitForResponse(
    (response) =>
      response.request().method() === 'POST' && response.url().endsWith('/api/profiles'),
  );
  await page.getByTestId('save-button').click();
  const response = await responsePromise;
  const body = (await response.json()) as { id: number; message: string };
  const requestBody = response.request().postDataJSON() as { occupationCode: string };

  expect(response.status()).toBe(201);
  expect(body.id).toBeGreaterThan(0);
  expect(body.message).toBe('save data success');
  expect(requestBody.occupationCode).toBe('software-engineer');
  await expect(page.getByTestId('save-notification')).toContainText(
    `save data success · ID: ${body.id}`,
  );
  await expect(page.getByTestId('first-name-input')).toHaveValue('');
  await expect(page.getByRole('radio', { checked: true })).toHaveCount(0);
  await expect(page.locator('mat-radio-button.mat-mdc-radio-checked')).toHaveCount(0);
  await expect(page.locator('.mat-form-field-invalid')).toHaveCount(0);
  await page.getByTestId('save-button').focus();
});

test('TC-PF-E2E-006 API ตอบ ValidationProblemDetails เมื่อ payload ไม่ถูกต้อง', async ({
  request,
}) => {
  const response = await request.post('/api/profiles', {
    data: {
      firstName: '',
      lastName: '',
      email: 'invalid',
      phone: '12345',
      profileBase64: 'invalid',
      birthDate: '2099-01-01',
      occupationCode: 'unknown',
      sex: 'Other',
    },
  });
  const body = (await response.json()) as {
    title: string;
    status: number;
    errors: Record<string, string[]>;
    traceId: string;
  };

  expect(response.status()).toBe(400);
  expect(body.title).toBe('One or more validation errors occurred.');
  expect(body.status).toBe(400);
  expect(Object.keys(body.errors).length).toBeGreaterThan(0);
  expect(body.traceId).toBeTruthy();
});

test('TC-PF-E2E-007 หน้าจอมือถือไม่มีการล้นแนวนอน', async ({ page }) => {
  await page.setViewportSize({ width: 390, height: 844 });
  await page.reload();

  const dimensions = await page.evaluate(() => ({
    viewport: document.documentElement.clientWidth,
    documentWidth: document.documentElement.scrollWidth,
    bodyWidth: document.body.scrollWidth,
  }));

  expect(dimensions.documentWidth).toBe(dimensions.viewport);
  expect(dimensions.bodyWidth).toBe(dimensions.viewport);
  await expect(page.getByTestId('profile-form')).toBeVisible();
});

test('TC-PF-E2E-008 แสดงข้อมูลหลักอาชีพจาก API ตามลำดับ', async ({ page, request }) => {
  const response = await request.get('/api/occupations');
  const occupations = (await response.json()) as Array<{ code: string; name: string }>;

  expect(response.status()).toBe(200);
  expect(occupations).toEqual([
    { code: 'software-engineer', name: 'Software Engineer' },
    { code: 'business-analyst', name: 'Business Analyst' },
    { code: 'quality-assurance', name: 'Quality Assurance' },
    { code: 'ux-ui-designer', name: 'UX/UI Designer' },
    { code: 'project-manager', name: 'Project Manager' },
  ]);

  await page.getByTestId('occupation-select').click();
  await expect(page.getByRole('option')).toHaveText(occupations.map((item) => item.name));
});

test('TC-PF-E2E-009 แสดงรูปโปรไฟล์ใน avatar แบบองค์กร', async ({ page }) => {
  await fillValidForm(page, 'avatar');

  const preview = page.getByAltText('Selected profile preview');
  await expect(preview).toBeVisible();
  await expect(page.getByText('profile.png')).toBeVisible();
  await expect(page.getByTestId('remove-image-button')).toBeVisible();
  const presentation = await preview.evaluate((element) => {
    const avatar = element.parentElement;
    const style = avatar ? getComputedStyle(avatar) : null;
    return {
      width: avatar?.getBoundingClientRect().width,
      height: avatar?.getBoundingClientRect().height,
      borderRadius: style?.borderRadius,
      objectFit: getComputedStyle(element).objectFit,
    };
  });

  expect(presentation.width).toBe(96);
  expect(presentation.height).toBe(96);
  expect(Number.parseFloat(presentation.borderRadius ?? '0')).toBeGreaterThanOrEqual(48);
  expect(presentation.objectFit).toBe('cover');
});

test('TC-PF-E2E-010 เลือกวันเกิดผ่าน Angular Material Datepicker', async ({ page }) => {
  await page.getByTestId('birth-date-toggle').click();

  await expect(page.locator('mat-datepicker-content')).toBeVisible();
  await expect(page.locator('mat-calendar')).toBeVisible();
  await expect(page.getByRole('grid')).toBeVisible();
});

async function fillValidForm(page: Page, suffix: string): Promise<void> {
  await page.getByTestId('first-name-input').fill('Grace');
  await page.getByTestId('last-name-input').fill('Hopper');
  await page.getByTestId('email-input').fill(`grace.hopper.${suffix}@example.com`);
  await page.getByTestId('phone-input').fill('+66 89 456 7890');
  await page.getByTestId('birth-date-input').fill('09/12/1906');
  await page.getByTestId('occupation-select').click();
  await page.getByRole('option', { name: 'Software Engineer' }).click();
  await page.getByRole('radio', { name: 'Female' }).check();
  await page.getByTestId('profile-image-input').setInputFiles(image);
}
