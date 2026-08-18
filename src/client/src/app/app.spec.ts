import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { App } from './app';

interface TestableApp {
  occupations: () => Array<{ code: string; name: string }>;
  form: {
    controls: { occupationCode: { setValue: (value: string) => void; value: string } };
    setValue: (value: Record<string, string>) => void;
  };
  save: () => void;
}

describe('App', () => {
  let httpTesting: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => httpTesting.verify());

  it('renders the candidate form', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    flushOccupations();
    fixture.detectChanges();
    const element = fixture.nativeElement as HTMLElement;
    expect(element.querySelector('h1')?.textContent).toContain('Create your profile');
    expect(element.querySelector('[data-testid="candidate-profile-form"]')).not.toBeNull();
    expect(element.querySelectorAll('input').length).toBeGreaterThanOrEqual(6);
  });

  it('does not submit an empty form', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    flushOccupations();
    fixture.detectChanges();
    const button = fixture.nativeElement.querySelector(
      'button[type="submit"]',
    ) as HTMLButtonElement;
    button.click();
    fixture.detectChanges();
    expect(fixture.nativeElement.querySelector('[data-testid="first-name-error"]')).not.toBeNull();
  });

  it('clears the submitted error state', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    flushOccupations();
    fixture.detectChanges();
    const element = fixture.nativeElement as HTMLElement;
    (element.querySelector('[data-testid="save-button"]') as HTMLButtonElement).click();
    fixture.detectChanges();
    (element.querySelector('[data-testid="clear-button"]') as HTMLButtonElement).click();
    fixture.detectChanges();
    expect(element.querySelector('.mat-form-field-invalid')).toBeNull();
  });

  it('loads occupation master data from the API', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    flushOccupations();
    fixture.detectChanges();

    const component = fixture.componentInstance as unknown as TestableApp;
    const form = component.form;
    form.controls.occupationCode.setValue('software-engineer');

    expect(component.occupations()).toEqual([
      { code: 'software-engineer', name: 'Software Engineer' },
    ]);
    expect(form.controls.occupationCode.value).toBe('software-engineer');
  });

  it('posts the selected occupation code', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    flushOccupations();
    const component = fixture.componentInstance as unknown as TestableApp;
    component.form.setValue({
      firstName: 'Ada',
      lastName: 'Lovelace',
      email: 'ada@example.com',
      phone: '+66 81 234 5678',
      profileBase64: 'data:image/png;base64,iVBORw0KGgo=',
      birthDate: '18/08/1990',
      occupationCode: 'software-engineer',
      sex: 'Female',
    });

    component.save();

    const request = httpTesting.expectOne('/api/candidate-profiles');
    expect(request.request.method).toBe('POST');
    expect(request.request.body.occupationCode).toBe('software-engineer');
    request.flush({ id: 1, message: 'save data success' });
  });

  function flushOccupations(): void {
    const request = httpTesting.expectOne('/api/occupations');
    expect(request.request.method).toBe('GET');
    request.flush([{ code: 'software-engineer', name: 'Software Engineer' }]);
  }
});
