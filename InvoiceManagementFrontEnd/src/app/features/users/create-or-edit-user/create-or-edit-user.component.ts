import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { User, UserStatus } from '../user.model';
import { CommonModule } from '@angular/common';
import { MatSelectModule } from '@angular/material/select';

@Component({
    selector: 'app-create-or-edit-user',
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
    templateUrl: './create-or-edit-user.component.html',
    styleUrls: ['./create-or-edit-user.component.css']
})
export class CreateOrEditUserComponent {
    form: FormGroup;
    readOnly = false;
    title = '';
    user: User | null = null;
    userStatus: typeof UserStatus;
    constructor(
        private fb: FormBuilder,
        public dialogRef: MatDialogRef<CreateOrEditUserComponent>,
        @Inject(MAT_DIALOG_DATA) public data: { user: User, readOnly: boolean, title: string }
    ) {
        this.user = data.user;
        this.readOnly = data.readOnly;
        this.title = data.title;
        this.userStatus = UserStatus;
        this.form = this.fb.group({
            firstName: [{ value: this.user?.firstName || '', disabled: this.readOnly }],
            lastName: [{ value: this.user?.lastName || '', disabled: this.readOnly }],
            email: [{ value: this.user?.email || '', disabled: this.readOnly }],
            password: [{ value: '', disabled: this.readOnly }],
            status: [{ value: this.user?.status ?? UserStatus.Active, disabled: this.readOnly }]
        });
    }

    onSave() {
        if (this.form.valid) {
            const value = { ...this.user, ...this.form.value };
            // Si está editando y el campo password está vacío, no lo envía
            if (this.user && !this.form.value.password) {
                delete value.password;
            }
            this.dialogRef.close(value);
        }
    }
}
