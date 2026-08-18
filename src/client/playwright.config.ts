import { defineConfig } from '@playwright/test';

const baseURL = process.env['PLAYWRIGHT_BASE_URL'] ?? 'http://127.0.0.1:4204';

export default defineConfig({
  testDir: './e2e',
  fullyParallel: false,
  forbidOnly: !!process.env['CI'],
  retries: process.env['CI'] ? 2 : 0,
  workers: 1,
  timeout: 30_000,
  expect: {
    timeout: 5_000,
  },
  reporter: [
    ['list'],
    [
      './e2e/support/markdown-reporter.ts',
      {
        outputFile:
          '../../documentation/09-qa-and-test/01-candidate-profile/playwright-test-result.md',
        screenshotDir: '../../documentation/09-qa-and-test/01-candidate-profile/screenshots',
        baseURL,
      },
    ],
    ['html', { outputFolder: 'playwright-report', open: 'never' }],
  ],
  outputDir: 'test-results',
  use: {
    baseURL,
    trace: 'retain-on-failure',
    screenshot: { mode: 'on', fullPage: true },
    video: 'retain-on-failure',
  },
  projects: [
    {
      name: 'chromium',
      use: {
        browserName: 'chromium',
        viewport: { width: 1440, height: 1100 },
      },
    },
  ],
});
