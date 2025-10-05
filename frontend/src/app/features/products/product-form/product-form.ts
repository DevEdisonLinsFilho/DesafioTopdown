import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, RouterLink, MatFormFieldModule,
    MatInputModule, MatButtonModule, MatCardModule, MatSlideToggleModule, MatSnackBarModule
  ],
  templateUrl: './product-form.html',
  styleUrls: ['./product-form.scss']
})
export class ProductFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private snackBar = inject(MatSnackBar);

  form: FormGroup;
  productId: string | null = null;
  isEditMode = false;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      sku: ['', Validators.required],
      price: [null, [Validators.required, Validators.min(0.01)]],
      stockQty: [null, [Validators.required, Validators.min(0)]],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.productId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.productId;

    if (this.isEditMode) {
      this.productService.getProductById(Number(this.productId)).subscribe(product => {
        this.form.patchValue(product);
      });
    }
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    const productData = this.form.value;
    let operation$: Observable<number | void>;
    let successMessage: string;

    if (this.isEditMode && this.productId) {
      operation$ = this.productService.updateProduct(Number(this.productId), productData);
      successMessage = 'Produto atualizado com sucesso!';
    } else {
      operation$ = this.productService.createProduct(productData);
      successMessage = 'Produto criado com sucesso!';
    }

    operation$.subscribe({
      next: () => {
        this.snackBar.open(successMessage, 'Fechar', { duration: 3000 });
        this.router.navigate(['/products']);
      },
      error: (err: any) => {
        console.error('Erro ao salvar produto:', err);
      }
    });
  }
}