import { Component, inject, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { fromEvent, merge, of, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, startWith, switchMap, map, catchError, tap } from 'rxjs/operators';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { CustomerService } from '../customer.service';
import { Customer } from '../../../core/models/customer.model';

@Component({
  selector: 'app-customer-list',
  standalone: true,
  imports: [
    CommonModule, RouterLink, MatFormFieldModule, MatInputModule,
    MatTableModule, MatSortModule, MatPaginatorModule, MatButtonModule,
    MatIconModule, MatProgressSpinnerModule, MatSnackBarModule
  ],
  templateUrl: './customer-list.html',
  styleUrls: ['./customer-list.scss']
})
export class CustomerListComponent implements AfterViewInit {
  private customerService = inject(CustomerService);
  private snackBar = inject(MatSnackBar);
  private refreshTrigger = new Subject<void>();

  displayedColumns: string[] = ['id', 'name', 'email', 'document', 'actions'];
  dataSource = new MatTableDataSource<Customer>([]);
  isLoadingResults = true;
  resultsLength = 0;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild('input') input!: ElementRef;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;

    const filterChanges$ = fromEvent(this.input.nativeElement, 'keyup').pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.paginator.pageIndex = 0)
    );

    merge(this.paginator.page, this.refreshTrigger, filterChanges$)
      .pipe(
        startWith({}),
        switchMap(() => {
          this.isLoadingResults = true;
          return this.customerService.getCustomers(
            this.paginator.pageIndex + 1,
            this.paginator.pageSize,
            this.input.nativeElement.value
          ).pipe(catchError(() => of(null)));
        }),
        map(data => {
          this.isLoadingResults = false;
          if (data === null) {
            return [];
          }
          this.resultsLength = data.totalCount;
          return data.items;
        })
      )
      .subscribe(data => (this.dataSource.data = data));
  }

  deleteCustomer(id: number): void {
    if (confirm('Tem certeza que deseja deletar este cliente?')) {
      this.customerService.deleteCustomer(id).subscribe({
        next: () => {
          this.snackBar.open('Cliente deletado com sucesso!', 'Fechar', { duration: 3000 });
          this.refreshTrigger.next();
        },
        error: (err) => {
          console.error('Erro ao deletar cliente:', err);
        }
      });
    }
  }
}