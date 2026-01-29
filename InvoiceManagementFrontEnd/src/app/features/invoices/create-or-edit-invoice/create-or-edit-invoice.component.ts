import { InvoicePdfService } from '../services/invoice-pdf.service';
import { Component, inject, OnInit } from '@angular/core';
import { InvoiceStatus } from '../invoice.model';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
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
import { MatDialog } from '@angular/material/dialog';
import { InvoiceProductDialogComponent } from '../invoice-product-dialog/invoice-product-dialog.component';
import { InvoicesService } from '../services/invoices.service';

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
    configuration: any;
    paymentMethodService = inject(AppPaymentMethodsService);
    invoiceStatusOptions = [
        { value: InvoiceStatus.Active, label: 'Pendiente' },
        { value: InvoiceStatus.Inactive, label: 'Pagada' }
    ];
    private route = inject(ActivatedRoute);
    private router = inject(Router);
    private fb = inject(FormBuilder);
    private customersService = inject(AppCustomersService);
    private configurationService = inject(AppConfigurationService);
    private userService = inject(AppUserService);
    private dialog = inject(MatDialog);
    private invoicesService = inject(InvoicesService);
    private invoicePdfService = inject(InvoicePdfService);
    get detailsArray() {
        return this.form.get('details') as import('@angular/forms').FormArray;
    }
    constructor() {
        this.form = this.fb.group({
            id: [0],
            customer: ['', Validators.required],
            customerId: [0, Validators.required],
            customerPhone: [''],
            customerEmail: [''],
            seller: [''],
            userId: [0, Validators.required],
            date: [new Date(), Validators.required],
            paymentMethodId: [0, Validators.required],
            status: [InvoiceStatus.Active],
            total: 0,
            details: this.fb.array([], Validators.required)
        });
    }

    ngOnInit() {
        this.getConfiguration();
        this.getPaymentMethods();
        this.getCustomers();
        this.getUsers();
        this.setupFormListeners();

        const id = this.route.snapshot.paramMap.get('id');
        if (id) {
            this.invoicesService.getById(+id).subscribe(invoice => {
                const dateObj = new Date(invoice.date);
                const formattedDate = this.formatDateForInput(dateObj);
                this.form.patchValue({
                    id: invoice.id,
                    customer: invoice.customerId,
                    customerEmail : invoice.customerEmail,
                    customerPhone : invoice.customerPhone,
                    customerId: invoice.customerId,
                    seller: invoice.userId,
                    userId: invoice.userId,
                    date: formattedDate,
                    paymentMethodId: invoice.paymentMethodId,
                    status: invoice.status,
                    total: invoice.total
                });
                this.detailsArray.clear();
                if (invoice.details && Array.isArray(invoice.details)) {
                    invoice.details.forEach((d: any) => {
                        this.detailsArray.push(this.fb.group({
                            id: [d.id],
                            productId: [d.productId],
                            code: [d.code],
                            productName: [d.productName],
                            quantity: [d.quantity],
                            unitPrice: [d.unitPrice],
                            total: [d.total]
                        }));
                    });
                }
            });
        }
    }

    get detailsSubtotal(): number {
        return this.detailsArray.controls.reduce((sum, d) => sum + (d.value.total || 0), 0);
    }

    get detailsIVA(): number {
        return this.detailsSubtotal * this.vatPercentage / 100;
    }

    get detailsTotal(): number {
        return this.detailsSubtotal + this.detailsIVA;
    }

    onCustomerSelectOpen() {
        this.getCustomers();
    }

    onUserSelectChange() {
        this.getUsers();
    }

    generateInvoicePdf() {
        const invoice = this.getInvoicePdfData();
        this.invoicePdfService.generateInvoicePdf(invoice);
    }

    getInvoicePdfData() {
        const f = this.form.value;
        return {
            number: '',
            customerName: this.customers.find(c => c.id === f.customer)?.name || '',
            customerAddress: '',
            customerPhone: f.customerPhone,
            customerEmail: f.customerEmail,
            sellerName: this.users.find(u => u.id === f.userId)?.firstName || '',
            date: f.date,
            paymentMethodName: this.paymentMethods.find(p => p.id === f.paymentMethodId)?.name || '',
            vatPercentage: this.vatPercentage,
            details: f.details,
            subtotal: this.detailsSubtotal,
            iva: this.detailsIVA,
            total: this.detailsTotal,
            companyName: this.configuration?.companyName || '',
            companyAddress: this.configuration?.address || '',
            companyPhone: this.configuration?.phone || '',
            companyEmail: this.configuration?.email || '',
            companyLogo: this.configuration?.base64LogoImage || ''
        };
    }

    removeDetail(index: number) {
        this.detailsArray.removeAt(index);
    }

    private getConfiguration() {
        this.configurationService.get().subscribe(config => {
            this.vatPercentage = config.vatPercentage || 0;
            this.configuration = config;
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
        if (this.form.invalid) return;
        this.form.patchValue({ total: this.detailsTotal }, { emitEvent: false });
        const payload = this.form.value;
        console.log('Payload a enviar:', payload);
        if (payload.id && payload.id > 0) {
            this.invoicesService.update(payload).subscribe({
                next: (res) => {
                    this.form.reset();
                    this.detailsArray.clear();
                    this.router.navigate(['/invoices']);
                },
                error: (err) => {
                    console.error('Error al actualizar factura', err);
                }
            });
        } else {
            this.invoicesService.create(payload).subscribe({
                next: (res) => {
                    this.form.reset();
                    this.detailsArray.clear();
                    this.router.navigate(['/invoices']);
                },
                error: (err) => {
                    console.error('Error al guardar factura', err);
                }
            });
        }
    }

    onAddProduct() {
        const dialogRef = this.dialog.open(InvoiceProductDialogComponent, {
            width: '1100px'
        });
        dialogRef.afterClosed().subscribe((details: any[]) => {
            if (Array.isArray(details)) {
                details
                    .filter(detail => detail.unitPrice > 0)
                    .forEach(detail => {
                        this.detailsArray.push(this.fb.group({
                            productId: [detail.productId],
                            code: [detail.code],
                            productName: [detail.productName],
                            quantity: [detail.quantity],
                            unitPrice: [detail.unitPrice],
                            total: [detail.total]
                        }));

                    });
            }
        });
    }

    private formatDateForInput(date: Date): string {
        const year = date.getFullYear();
        const month = ('0' + (date.getMonth() + 1)).slice(-2);
        const day = ('0' + date.getDate()).slice(-2);
        return `${year}-${month}-${day}`;
    }
}