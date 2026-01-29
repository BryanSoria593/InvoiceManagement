import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Customer, CustomerStatus } from '../customer.model';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';

@Component({
    selector: 'app-create-or-edit-customer',
    standalone: true,
    imports: [
        CommonModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        ReactiveFormsModule,
    MatSelectModule

    ],
    templateUrl: './create-or-edit-customer.component.html',
    styleUrls: ['./create-or-edit-customer.component.css']
})
export class CreateOrEditCustomerComponent {
    form: FormGroup;
    readOnly = false;
    title = '';
    customer: Customer | null = null;
    customerStatus: typeof CustomerStatus;
    constructor(
        private fb: FormBuilder,
        public dialogRef: MatDialogRef<CreateOrEditCustomerComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { customer: Customer, readOnly: boolean, title: string }
    ) {
        this.customer = data.customer;
        this.readOnly = data.readOnly;
        this.title = data.title;
        this.customerStatus = CustomerStatus;
        this.form = this.fb.group({
            name: [{ value: this.customer?.name || '', disabled: this.readOnly }],
            email: [{ value: this.customer?.email || '', disabled: this.readOnly }],
            phone: [{ value: this.customer?.phone || '', disabled: this.readOnly }],
            address: [{ value: this.customer?.address || '', disabled: this.readOnly }],
            status: [{ value: this.customer?.status ?? CustomerStatus.Active, disabled: this.readOnly }]
        });
    }

    onSave() {
        if (this.form.valid) {
            this.dialogRef.close({ ...this.customer, ...this.form.value });
        }
    }
}
