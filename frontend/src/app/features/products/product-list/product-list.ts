import { Component, inject, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
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
import { ProductService } from '../product.service';
import { Product } from '../../../core/models/product.model';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [
    CommonModule, CurrencyPipe, RouterLink, MatFormFieldModule, MatInputModule,
    MatTableModule, MatSortModule, MatPaginatorModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule, MatSnackBarModule
  ],
  templateUrl: './product-list.html',
  styleUrls: ['./product-list.scss']
})
export class ProductListComponent implements AfterViewInit {
  private productService = inject(ProductService);
  private snackBar = inject(MatSnackBar);
  private refreshTrigger = new Subject<void>();

  displayedColumns: string[] = ['id', 'sku', 'name', 'price', 'stock_qty', 'actions'];
  dataSource = new MatTableDataSource<Product>([]);
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
          return this.productService.getProducts(
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

  deleteProduct(id: number): void {
    if (confirm('Tem certeza que deseja deletar este produto?')) {
      this.productService.deleteProduct(id).subscribe({
        next: () => {
          this.snackBar.open('Produto excluido com sucesso!', 'Fechar', { duration: 3000 });
          this.refreshTrigger.next();
        },
        error: (err) => console.error('Erro ao excluir produto:', err)
      });
    }
  }
}