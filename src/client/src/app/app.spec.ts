import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { App } from './app';

describe('App', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [App],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
  });

  it('renders the candidate form', () => {
    const fixture = TestBed.createComponent(App);
    fixture.detectChanges();
    const element = fixture.nativeElement as HTMLElement;
    expect(element.querySelector('h1')?.textContent).toContain('Create your profile');
    expect(element.querySelector('[data-testid="candidate-profile-form"]')).not.toBeNull();
    expect(element.querySelectorAll('input').length).toBeGreaterThanOrEqual(6);
  });

  it('does not submit an empty form', () => {
    const fixture = TestBed.createComponent(App);
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
    const element = fixture.nativeElement as HTMLElement;
    (element.querySelector('[data-testid="save-button"]') as HTMLButtonElement).click();
    fixture.detectChanges();
    (element.querySelector('[data-testid="clear-button"]') as HTMLButtonElement).click();
    fixture.detectChanges();
    expect(element.querySelector('.mat-form-field-invalid')).toBeNull();
  });
});
