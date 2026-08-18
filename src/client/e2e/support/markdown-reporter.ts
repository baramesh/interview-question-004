import { mkdirSync, writeFileSync } from 'node:fs';
import { dirname, resolve } from 'node:path';
import { format } from 'prettier';
import type {
  FullConfig,
  FullResult,
  Reporter,
  Suite,
  TestCase,
  TestResult,
} from '@playwright/test/reporter';

interface MarkdownReporterOptions {
  outputFile: string;
  baseURL: string;
}

interface RecordedResult {
  id: string;
  title: string;
  project: string;
  status: string;
  durationMs: number;
  error?: string;
}

export default class MarkdownReporter implements Reporter {
  private readonly results: RecordedResult[] = [];
  private totalTests = 0;

  constructor(private readonly options: MarkdownReporterOptions) {}

  onBegin(_config: FullConfig, suite: Suite): void {
    this.totalTests = suite.allTests().length;
  }

  onTestEnd(test: TestCase, result: TestResult): void {
    const match = /^(TC-[A-Z0-9-]+)\s+(.+)$/.exec(test.title);
    const passed = result.status === test.expectedStatus;
    this.results.push({
      id: match?.[1] ?? 'UNMAPPED',
      title: match?.[2] ?? test.title,
      project: test.parent.project()?.name ?? '-',
      status: passed ? 'PASS' : result.status.toUpperCase(),
      durationMs: result.duration,
      error: passed ? undefined : result.error?.message,
    });
  }

  async onEnd(result: FullResult): Promise<void> {
    const passed = this.results.filter((item) => item.status === 'PASS').length;
    const failed = this.results.length - passed;
    const lines = [
      '---',
      'doc_id: DNEW-QAT-CP-04',
      'module: CANDIDATE_PROFILE',
      'type: playwright-test-result',
      `generated_at: ${new Date().toISOString()}`,
      '---',
      '',
      '# QAT-CP-04 — ผลทดสอบ Playwright',
      '',
      '> ไฟล์นี้สร้างอัตโนมัติจาก `npm run test:e2e` ห้ามแก้ผลด้วยมือ',
      '',
      '## สภาพแวดล้อม',
      '',
      '| รายการ | ค่า |',
      '|---|---|',
      `| Base URL | \`${this.options.baseURL}\` |`,
      '| Browser | Chromium |',
      '| ระบบที่ทดสอบ | Angular → Nginx → ASP.NET Core API → PostgreSQL บน OrbStack |',
      '',
      '## สรุปผล',
      '',
      '| ทั้งหมด | ผ่าน | ไม่ผ่าน | สถานะชุดทดสอบ |',
      '|---:|---:|---:|---|',
      `| ${this.totalTests} | ${passed} | ${failed} | ${result.status === 'passed' ? 'PASS' : result.status.toUpperCase()} |`,
      '',
      '## ผลรายกรณี',
      '',
      '| Test Case ID | ชื่อกรณีทดสอบ | Project | ผล | เวลา (ms) |',
      '|---|---|---|---|---:|',
      ...this.results.map(
        (item) =>
          `| ${item.id} | ${escapeCell(item.title)} | ${escapeCell(item.project)} | ${item.status} | ${item.durationMs} |`,
      ),
    ];

    const failures = this.results.filter((item) => item.error);
    if (failures.length > 0) {
      lines.push('', '## รายละเอียดข้อผิดพลาด', '');
      for (const failure of failures) {
        lines.push(`### ${failure.id}`, '', '```text', failure.error ?? '', '```', '');
      }
    }

    lines.push(
      '',
      '## การสืบย้อน',
      '',
      '- รายละเอียดขั้นตอนและผลที่คาดหวัง: `playwright-test-cases.md`',
      '- รหัสทดสอบในรายงานตรงกับชื่อ `test()` ใน `src/client/e2e/candidate-profile.spec.ts`',
      '',
    );

    const outputPath = resolve(process.cwd(), this.options.outputFile);
    mkdirSync(dirname(outputPath), { recursive: true });
    const markdown = await format(`${lines.join('\n')}\n`, { parser: 'markdown' });
    writeFileSync(outputPath, markdown, 'utf8');
  }

  printsToStdio(): boolean {
    return false;
  }
}

function escapeCell(value: string): string {
  return value.replaceAll('|', '\\|').replaceAll('\n', '<br>');
}
