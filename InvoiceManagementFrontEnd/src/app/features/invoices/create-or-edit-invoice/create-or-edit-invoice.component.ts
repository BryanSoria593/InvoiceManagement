import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MenuLayoutComponent } from '../../../core/components/menu-layout.component';
import { AppConfigurationService } from '../../../core/services/app-configuration.service';
import { AppPaymentMethodsService } from '../../../core/services/app-payment-methods.service';
import { PaymentMethod } from '../../../core/models/payment-method.model';

@Component({
  selector: 'app-create-or-edit-invoice',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule,
    MenuLayoutComponent
  ],
  templateUrl: './create-or-edit-invoice.component.html',
  styleUrls: ['./create-or-edit-invoice.component.css']
})
export class CreateOrEditInvoiceComponent {
  form: FormGroup;
  paymentMethods: PaymentMethod[] = [];
  private fb = inject(FormBuilder);
  vatPercentage = 0;
  paymentMethodService = inject(AppPaymentMethodsService);
  private configurationService = inject(AppConfigurationService);
  constructor() {
    this.form = this.fb.group({
      customer: [''],
      customerPhone: [''],
      customerEmail: [''],
      seller: [''],
      date: [''],
      paymentMethod: [''],
      status: ['41']
    });
  }

  ngOnInit() {
    this.configurationService.get().subscribe(config => {
      this.vatPercentage = config.vatPercentage || 0;
    });
    this.paymentMethodService.get().subscribe(methods => {
      this.paymentMethods = methods;
    });
  }
}