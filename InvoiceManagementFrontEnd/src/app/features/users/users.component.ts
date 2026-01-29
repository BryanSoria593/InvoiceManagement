import { Component, ViewChild, signal, inject } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe } from '@angular/common';
import { Subject, debounceTime } from 'rxjs';
import { UsersService } from './users.service';
import { User, UserStatus } from './user.model';
import { MenuLayoutComponent } from '../../core/components/menu-layout.component';
import { SearchFilterComponent } from '../../shared/components/search-filter/search-filter.component';
import { CreateOrEditUserComponent } from './create-or-edit-user/create-or-edit-user.component';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatIconModule,
    MatButtonModule,
    DatePipe,
    MenuLayoutComponent,
    SearchFilterComponent,
    MatDialogModule
  ],
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  private usersService = inject(UsersService);
  private dialog = inject(MatDialog);

  displayedColumns = ['firstName', 'lastName', 'email', 'status', 'createdAt', 'actions'];
  dataSource = new MatTableDataSource<User>([]);
  total = signal(0);
  pageIndex = signal(0);
  pageSize = signal(10);
  filter = signal('');
  pageSizeOptions = signal<number[]>([5, 10, 20, 50]);
  loading = signal(false);
  private filter$ = new Subject<string>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor() {
    this.filter$
      .pipe(debounceTime(300))
      .subscribe(value => {
        this.filter.set(value);
        this.pageIndex.set(0);
        this.loadUsers();
      });
    this.loadUsers();
  }

  viewUser(user: User) {
    this.dialog.open(CreateOrEditUserComponent, {
      data: {
        user,
        readOnly: true,
        title: 'Ver Usuario'
      },
      width: '400px'
    });
  }

  applyFilter(value: string) {
    this.filter$.next(value);
  }

  onPage(event: PageEvent) {
    this.pageIndex.set(event.pageIndex);
    this.pageSize.set(event.pageSize);
    this.loadUsers();
  }

  loadUsers() {
    this.loading.set(true);
    this.usersService.getPaged(this.pageIndex() + 1, this.pageSize(), this.filter())
      .subscribe(result => {
        this.dataSource.data = result.items;
        this.total.set(result.totalCount);
        this.loading.set(false);
      });
  }

  getStatusLabel(status: UserStatus) {
    switch (status) {
      case UserStatus.Active: return 'Activo';
      case UserStatus.Inactive: return 'Inactivo';
      default: return '';
    }
  }

  newUser() {
    const dialogRef = this.dialog.open(CreateOrEditUserComponent, {
      data: {
        user: null,
        readOnly: false,
        title: 'Nuevo Usuario'
      },
      width: '400px'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.usersService.create(result).subscribe(() => this.loadUsers());
      }
    });
  }

  editUser(user: User) {
    const dialogRef = this.dialog.open(CreateOrEditUserComponent, {
      data: {
        user,
        readOnly: false,
        title: 'Editar Usuario'
      },
      width: '400px'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.usersService.update(result).subscribe(() => this.loadUsers());
      }
    });
  }
}
