import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ConfigurationService } from './configuration.service';
import { MenuLayoutComponent } from '../../core/components/menu-layout.component';

@Component({
    selector: 'app-configuration',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatProgressSpinnerModule,
        MenuLayoutComponent,
    ],
    templateUrl: './configuration.component.html',
    styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent implements OnInit {
    form!: FormGroup;
    loading = signal(false);
    logoPreview: string | null = null;
    private configService = inject(ConfigurationService);
    private fb = inject(FormBuilder);

    ngOnInit() {
        this.loading.set(true);
        this.getConfiguration();
    }

    getConfiguration() {
        this.configService.get().subscribe({
            next: (config) => {
                this.form = this.fb.group({
                    companyName: [config.companyName, Validators.required],
                    phone: [config.phone],
                    email: [config.email, [Validators.required, Validators.email]],
                    vatPercentage: [config.vatPercentage],
                    currencySymbol: [config.currencySymbol],
                    address: [config.address],
                    city: [config.city],
                    region: [config.region],
                    postalCode: [config.postalCode],
                    logoUrl: [config.logoUrl],
                    base64LogoImage: [config.base64LogoImage ?? null]
                });
                if (config.base64LogoImage) {
                    this.logoPreview = config.base64LogoImage;
                } else {
                    this.logoPreview = config.logoUrl;
                }
                this.loading.set(false);
            },
            error: () => this.loading.set(false)
        });
    }

    onLogoChange(event: Event) {
        const input = event.target as HTMLInputElement;
        if (input.files && input.files[0]) {
            const reader = new FileReader();
            reader.onload = e => {
                const base64 = reader.result as string;
                this.logoPreview = base64;
                this.form.patchValue({ base64LogoImage: base64 });
            };
            reader.readAsDataURL(input.files[0]);
        }
    }

    onSubmit() {
        if (this.form.invalid) return;
        this.loading.set(true);
        const payload = this.form.getRawValue();
        if (!payload.base64LogoImage) {
            delete payload.base64LogoImage;
        }
        this.configService.update(payload).subscribe({
            next: (config) => {
                if (config.base64LogoImage) {
                    this.logoPreview = config.base64LogoImage;
                } else {
                    this.logoPreview = config.logoUrl;
                }
                this.loading.set(false);
            },
            error: () => this.loading.set(false)
        });
    }
}
