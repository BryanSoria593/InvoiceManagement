import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, FormControl, FormGroup, Validators } from '@angular/forms';

import { Product } from '../product.model';

@Component({
  selector: 'app-edit-product',
  standalone: true,
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule
  ],
  templateUrl: './create-or-edit-product.component.html',
  styleUrls: ['./create-or-edit-product.component.css']
})
export class EditProductComponent {

  private dialogRef = inject(MatDialogRef<EditProductComponent>);
  private data = inject<{ product: Product; readOnly: boolean; title: string }>(MAT_DIALOG_DATA);

  readonly title = this.data.title;
  readonly readOnly = this.data.readOnly;
  readonly product = this.data.product;

  form = new FormGroup({
    code: new FormControl(this.product?.code ?? '', { nonNullable: true }),
    name: new FormControl(this.product?.name ?? '', { nonNullable: true, validators: Validators.required }),
    description: new FormControl(this.product?.description ?? '', { nonNullable: true }),
    salePrice: new FormControl(this.product?.salePrice ?? 0, { nonNullable: true })
  });

  constructor() {
    if (this.readOnly) {
      this.form.disable();
    }
  }

  onSave() {
    if (this.form.invalid) return;

    this.dialogRef.close({
      ...this.product,
      ...this.form.getRawValue()
    });
  }

  onCancel() {
    this.dialogRef.close();
  }
}
