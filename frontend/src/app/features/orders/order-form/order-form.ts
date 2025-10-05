import { Component, OnInit, inject } from '@angular/core';
import { CommonModule, CurrencyPipe } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, map, startWith, catchError } from 'rxjs/operators';

import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';

import { Customer } from '../../../core/models/customer.model';
import { Product } from '../../../core/models/product.model';
import { CustomerService } from '../../customers/customer.service';
import { ProductService } from '../../products/product.service';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-order-form',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, RouterLink, CurrencyPipe,
    MatCardModule, MatFormFieldModule, MatInputModule, MatButtonModule,
    MatAutocompleteModule, MatIconModule, MatTableModule, MatSnackBarModule
  ],
  templateUrl: './order-form.html',
  styleUrl: './order-form.scss'
})
export class OrderFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private customerService = inject(CustomerService);
  private productService = inject(ProductService);
  private orderService = inject(OrderService);
  private snackBar = inject(MatSnackBar);

  orderForm: FormGroup;

  customerCtrl = new FormControl<string | Customer>('');
  filteredCustomers$!: Observable<Customer[]>;

  productCtrl = new FormControl<string | Product>('');
  filteredProducts$!: Observable<Product[]>;
  selectedProduct: Product | null = null;
  quantityCtrl = new FormControl(1, [Validators.required, Validators.min(1)]);

  itemDisplayedColumns: string[] = ['productName', 'quantity', 'unitPrice', 'lineTotal', 'actions'];
  itemsDataSource = new MatTableDataSource<AbstractControl>();

  constructor() {
    this.orderForm = this.fb.group({
      customerId: [null, Validators.required],
      items: this.fb.array([], Validators.required)
    });
  }

  ngOnInit(): void {
    this.itemsDataSource.data = this.items.controls;

    this.filteredCustomers$ = this.customerCtrl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(value => {
        const searchTerm = typeof value === 'string' ? value : value?.name;
        return this.customerService.getCustomers(1, 10, searchTerm || '');
      }),
      map(pagedResult => pagedResult.items)
    );

    this.filteredProducts$ = this.productCtrl.valueChanges.pipe(
      startWith(''),
      debounceTime(300),
      distinctUntilChanged(),
      switchMap(value => {
        const searchTerm = typeof value === 'string' ? value : value?.name;
        return this.productService.getProducts(1, 10, searchTerm || '');
      }),
      map(pagedResult => pagedResult.items)
    );
  }

  addProduct(): void {
    if (!this.selectedProduct || !this.quantityCtrl.valid) return;

    const quantityToAdd = this.quantityCtrl.value!;
    const existingItemIndex = this.items.controls.findIndex(
      control => control.get('productId')?.value === this.selectedProduct!.id
    );

    if (existingItemIndex !== -1) {
      const existingItem = this.items.at(existingItemIndex);
      const currentQuantity = existingItem.get('quantity')?.value;
      const newTotalQuantity = currentQuantity + quantityToAdd;
      
      if (newTotalQuantity > this.selectedProduct.stockQty) {
        this.snackBar.open(`Quantidade total excede o estoque para ${this.selectedProduct.name}. Disponível: ${this.selectedProduct.stockQty}`, 'Fechar', {
          duration: 3000
        });
        return;
      }

      existingItem.get('quantity')?.setValue(newTotalQuantity);
    } else {
      if (quantityToAdd > this.selectedProduct.stockQty) {
        this.snackBar.open(`Estoque insuficiente para ${this.selectedProduct.name}. Disponível: ${this.selectedProduct.stockQty}`, 'Fechar', {
          duration: 3000
        });
        return;
      }
      
      const newItem = this.fb.group({
        productId: [this.selectedProduct.id, Validators.required],
        productName: [this.selectedProduct.name],
        quantity: [quantityToAdd, [Validators.required, Validators.min(1)]],
        unitPrice: [this.selectedProduct.price]
      });
      this.items.push(newItem);
    }

    this.itemsDataSource.data = this.items.controls;
    this.productCtrl.setValue('');
    this.selectedProduct = null;
    this.quantityCtrl.setValue(1);
  }

  displayCustomer(customer: Customer): string {
    return customer && customer.name ? customer.name : '';
  }

  onCustomerSelected(event: MatAutocompleteSelectedEvent): void {
    const selectedCustomer: Customer = event.option.value;
    this.orderForm.get('customerId')?.setValue(selectedCustomer.id);
  }
  
  displayProduct(product: Product): string {
    return product && product.name ? product.name : '';
  }
  
  onProductSelected(event: MatAutocompleteSelectedEvent): void {
    this.selectedProduct = event.option.value;
  }
  
  removeItem(index: number): void {
    this.items.removeAt(index);
    this.itemsDataSource.data = this.items.controls;
  }
  
  getTotalCost(): number {
    if (!this.items.controls || this.items.controls.length === 0) {
      return 0;
    }
    return this.items.controls
      .map(control => (control.get('quantity')?.value || 0) * (control.get('unitPrice')?.value || 0))
      .reduce((acc, value) => acc + value, 0);
  }
  
  get items(): FormArray {
    return this.orderForm.get('items') as FormArray;
  }

  getItemFormGroup(control: AbstractControl): FormGroup {
    return control as FormGroup;
  }

  onSubmit() {
    if (this.orderForm.invalid) {
      this.snackBar.open('Por favor, preencha todos os campos obrigatórios.', 'Fechar', { duration: 3000 });
      return;
    }
    
    this.orderService.createOrder(this.orderForm.value).subscribe({
      next: (newOrderId) => {
        this.snackBar.open(`Pedido #${newOrderId} criado com sucesso!`, 'Fechar', { duration: 5000 });
        this.orderForm.reset();
        this.customerCtrl.reset();
        this.productCtrl.reset();
        this.items.clear();
        this.itemsDataSource.data = [];
      },
      error: (err) => {
        console.error('Erro ao criar pedido:', err);
      }
    });
  }
}