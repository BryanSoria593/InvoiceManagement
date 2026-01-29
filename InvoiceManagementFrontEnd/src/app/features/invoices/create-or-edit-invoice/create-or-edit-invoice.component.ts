import { Component, inject, OnInit } from '@angular/core';
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
import { Customer } from '../../../core/models/customer.model';
import { AppCustomersService } from '../../../core/services/app-customers.service';
import { User } from '../../../core/models/user.model';
import { AppUserService } from '../../../core/services/app-user.service';
import { Invoice } from '../invoice.model';
import { MatDialog } from '@angular/material/dialog';
import { InvoiceProductDialogComponent } from '../invoice-product-dialog/invoice-product-dialog.component';
import { ProductsService } from '../../products/products.service';

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
export class CreateOrEditInvoiceComponent implements OnInit {
    form: FormGroup;
    paymentMethods: PaymentMethod[] = [];
    customers: Customer[] = [];
    users: User[] = [];
    vatPercentage = 0;
    invoiceDetails: any[] = [];
    paymentMethodService = inject(AppPaymentMethodsService);
    private fb = inject(FormBuilder);
    private customersService = inject(AppCustomersService);
    private configurationService = inject(AppConfigurationService);
    private userService = inject(AppUserService);
    private dialog = inject(MatDialog);
    private productsService = inject(ProductsService);
    constructor() {
        this.form = this.fb.group({
            customer: [''],
            customerId: 0,
            customerPhone: [''],
            customerEmail: [''],
            seller: [''],
            userId: 0,
            date: [''],
            paymentMethodId: 0,
            status: ['41']
        });
    }

    ngOnInit() {
        this.getConfiguration();
        this.getPaymentMethods();
        this.getCustomers();
        this.getUsers();
        this.setupFormListeners();
    }

    onCustomerSelectOpen() {
        this.getCustomers();
    }

    onUserSelectChange() {
        this.getUsers();
    }

    private getConfiguration() {
        this.configurationService.get().subscribe(config => {
            this.vatPercentage = config.vatPercentage || 0;
        });
    }

    private getPaymentMethods() {
        this.paymentMethodService.get().subscribe(methods => {
            this.paymentMethods = methods;
        });
    }

    private getCustomers() {
        this.customersService.get().subscribe(customers => {
            this.customers = customers;
        });
    }
    private getUsers() {
        this.userService.get().subscribe(users => {
            this.users = users;
        });
    }

    private setupFormListeners() {
        this.form.get('customer')?.valueChanges.subscribe((customerId: number) => {
            const customer = this.customers.find(c => c.id === customerId);
            if (customer) {
                this.form.patchValue({
                    customerPhone: customer.phone || '',
                    customerEmail: customer.email || '',
                    customerId: customer.id
                });
            }

        });

        this.form.get('seller')?.valueChanges.subscribe((userId: number) => {
            const user = this.users.find(u => u.id === userId);
            if (user) {
                this.form.patchValue({
                    seller: `${user.firstName} ${user.lastName}`,
                    userId: user.id
                });
            }
        });
    }

    onSubmit() {
        console.log(this.form.value);
    }

    onAddProduct() {
            const dialogRef = this.dialog.open(InvoiceProductDialogComponent, {
                width: '1100px'
            });
            dialogRef.afterClosed().subscribe((detail: any) => {
                if (detail) {
                    this.invoiceDetails.push(detail);
                }
            });
    }
}