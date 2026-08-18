import { expect, test } from '@playwright/test';

const validPayload = {
  firstName: 'Security',
  lastName: 'Test',
  email: 'security.test@example.com',
  phone: '+66 81 234 5678',
  birthDate: '18/08/1990',
  occupationCode: 'software-engineer',
  sex: 'Female',
};

test.beforeEach(async ({ page }) => {
  await page.goto('/');
});

test('SEC-CP-001 ปฏิเสธรูปที่ถอดรหัสแล้วเกิน 2 MiB', async ({ request }) => {
  const oversizedPng = Buffer.concat([
    Buffer.from('89504e470d0a1a0a', 'hex'),
    Buffer.alloc(2 * 1024 * 1024),
  ]).toString('base64');
  const response = await request.post('/api/candidate-profiles', {
    data: { ...validPayload, profileBase64: `data:image/png;base64,${oversizedPng}` },
  });

  expect(response.status()).toBe(400);
  const body = (await response.json()) as { errors: Record<string, string[]> };
  expect(body.errors['ProfileBase64']).toBeTruthy();
});

test('SEC-CP-002 ปฏิเสธ MIME ที่ไม่ตรงกับ byte signature', async ({ request }) => {
  const jpegBytes = Buffer.from('ffd8ff00', 'hex').toString('base64');
  const response = await request.post('/api/candidate-profiles', {
    data: { ...validPayload, profileBase64: `data:image/png;base64,${jpegBytes}` },
  });

  expect(response.status()).toBe(400);
  const body = (await response.json()) as { errors: Record<string, string[]> };
  expect(body.errors['ProfileBase64']).toContain(
    'Profile image content does not match its declared MIME type.',
  );
});

test('SEC-CP-003 ปฏิเสธ request body เกิน 3 MiB', async ({ request }) => {
  const response = await request.post('/api/candidate-profiles', {
    data: {
      ...validPayload,
      profileBase64: `data:image/png;base64,${'A'.repeat(3 * 1024 * 1024)}`,
    },
  });

  expect(response.status()).toBe(413);
});

test('SEC-CP-008 ส่ง security headers ผ่าน Nginx', async ({ request }) => {
  for (const path of ['/', '/api/occupations']) {
    const response = await request.get(path);

    expect(response.headers()['content-security-policy']).toBeTruthy();
    expect(response.headers()['x-content-type-options']).toBe('nosniff');
    expect(response.headers()['x-frame-options']).toBe('DENY');
    expect(response.headers()['referrer-policy']).toBe('no-referrer');
    expect(response.headers()['permissions-policy']).toBeTruthy();
  }
});

test('SEC-CP-004 จำกัดอัตราคำขอสร้างข้อมูล', async ({ request }) => {
  const invalidPayload = { ...validPayload, profileBase64: 'invalid' };
  const statuses: number[] = [];

  for (let index = 0; index < 21; index += 1) {
    const response = await request.post('/api/candidate-profiles', { data: invalidPayload });
    statuses.push(response.status());
    if (response.status() === 429) {
      break;
    }
  }

  expect(statuses).toContain(429);
});
