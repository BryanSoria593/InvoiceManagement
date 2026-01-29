import { Injectable, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class ToastService {
  private snackBar = inject(MatSnackBar);

  show(message: string, action: string = 'Cerrar', duration: number = 5000, panelClass: string = 'snackbar-error') {
    this.snackBar.open(message, action, { duration, panelClass });
  }
}
