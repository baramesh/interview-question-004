import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild, computed, signal } from '@angular/core';
import { FormBuilder, FormGroupDirective, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';

interface SaveResponse {
  id: number;
  message: string;
}

interface OccupationOption {
  code: string;
  name: string;
}

@Component({
  selector: 'app-root',
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatSelectModule,
  ],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  @ViewChild('profileInput') private profileInput?: ElementRef<HTMLInputElement>;
  @ViewChild(FormGroupDirective) private formDirective?: FormGroupDirective;

  protected readonly occupations = signal<OccupationOption[]>([]);
  protected readonly isLoadingOccupations = signal(true);
  protected readonly occupationLoadError = signal<string | null>(null);
  protected readonly isSaving = signal(false);
  protected readonly notification = signal<{ type: 'success' | 'error'; text: string } | null>(
    null,
  );
  protected readonly selectedFileName = signal('No file selected');
  protected readonly imagePreview = signal<string | null>(null);
  protected readonly currentYear = new Date().getFullYear();
  protected readonly hasPreview = computed(() => this.imagePreview() !== null);
  protected readonly form;

  constructor(
    formBuilder: FormBuilder,
    private readonly http: HttpClient,
  ) {
    this.form = formBuilder.nonNullable.group({
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(254)]],
      phone: ['', [Validators.required, Validators.pattern(/^\+?[0-9](?:[0-9 .()-]{7,18}[0-9])$/)]],
      profileBase64: ['', Validators.required],
      birthDate: ['', [Validators.required, this.birthDateValidator]],
      occupationCode: ['', Validators.required],
      sex: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.http.get<OccupationOption[]>('/api/occupations').subscribe({
      next: (occupations) => {
        this.occupations.set(occupations);
        this.isLoadingOccupations.set(false);
      },
      error: () => {
        this.occupationLoadError.set('Unable to load occupations. Please refresh the page.');
        this.isLoadingOccupations.set(false);
      },
    });
  }

  protected showError(fieldName: keyof typeof this.form.controls): boolean {
    const control = this.form.controls[fieldName];
    return control.invalid && (control.touched || control.dirty);
  }

  protected errorMessage(fieldName: keyof typeof this.form.controls): string {
    const control = this.form.controls[fieldName];
    if (control.hasError('required')) return 'This field is required.';
    if (control.hasError('email')) return 'Please provide a valid email.';
    if (control.hasError('pattern')) return 'Please provide a valid phone number.';
    if (control.hasError('birthDate')) return 'Use a past date in DD/MM/YYYY format.';
    if (control.hasError('file')) return control.getError('file');
    return 'Please check this value.';
  }

  protected selectProfile(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    const control = this.form.controls.profileBase64;

    if (!file) {
      this.clearProfile();
      return;
    }

    if (!['image/png', 'image/jpeg', 'image/gif', 'image/webp'].includes(file.type)) {
      control.setValue('');
      control.setErrors({ file: 'Select a PNG, JPEG, GIF or WebP image.' });
      this.selectedFileName.set('Unsupported file');
      return;
    }

    if (file.size > 2 * 1024 * 1024) {
      control.setValue('');
      control.setErrors({ file: 'Profile image must be no larger than 2 MB.' });
      this.selectedFileName.set('File is too large');
      return;
    }

    const reader = new FileReader();
    reader.onload = () => {
      const dataUrl = String(reader.result);
      control.setValue(dataUrl);
      control.markAsTouched();
      this.imagePreview.set(dataUrl);
      this.selectedFileName.set(file.name);
    };
    reader.onerror = () => control.setErrors({ file: 'The selected image could not be read.' });
    reader.readAsDataURL(file);
  }

  protected save(): void {
    this.notification.set(null);
    if (this.form.invalid || this.isSaving()) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSaving.set(true);
    this.http.post<SaveResponse>('/api/candidate-profiles', this.form.getRawValue()).subscribe({
      next: ({ id, message }) => {
        this.notification.set({ type: 'success', text: `${message} · ID: ${id}` });
        this.resetForm(false);
        this.isSaving.set(false);
      },
      error: (error: HttpErrorResponse) => {
        const details = error.error?.title ?? 'Unable to save the data. Please try again.';
        this.notification.set({ type: 'error', text: details });
        this.isSaving.set(false);
      },
    });
  }

  protected clear(): void {
    this.resetForm(true);
  }

  protected removeProfile(): void {
    this.clearProfile();
  }

  private resetForm(clearNotification: boolean): void {
    if (this.formDirective) {
      this.formDirective.resetForm();
    } else {
      this.form.reset();
    }
    this.clearProfile();
    if (clearNotification) this.notification.set(null);
  }

  private clearProfile(): void {
    this.form.controls.profileBase64.setValue('');
    this.form.controls.profileBase64.markAsUntouched();
    this.selectedFileName.set('No file selected');
    this.imagePreview.set(null);
    if (this.profileInput) this.profileInput.nativeElement.value = '';
  }

  private readonly birthDateValidator = (control: { value: string }) => {
    if (!control.value) return null;
    const match = /^(\d{2})\/(\d{2})\/(\d{4})$/.exec(control.value);
    if (!match) return { birthDate: true };

    const [, dayText, monthText, yearText] = match;
    const day = Number(dayText);
    const month = Number(monthText);
    const year = Number(yearText);
    const date = new Date(Date.UTC(year, month - 1, day));
    const today = new Date();
    const valid =
      date.getUTCFullYear() === year &&
      date.getUTCMonth() === month - 1 &&
      date.getUTCDate() === day &&
      date < today;
    return valid ? null : { birthDate: true };
  };
}
