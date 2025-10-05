import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { CustomerService } from '../customer.service';

@Component({
  selector: 'app-customer-form',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule, RouterLink,
    MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule, MatSnackBarModule
  ],
  templateUrl: './customer-form.html',
  styleUrl: './customer-form.scss'
})
export class CustomerFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private customerService = inject(CustomerService);
  private snackBar = inject(MatSnackBar);

  form: FormGroup;
  customerId: string | null = null;
  isEditMode = false;

  constructor() {
    this.form = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      document: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.customerId = this.route.snapshot.paramMap.get('id');
    this.isEditMode = !!this.customerId;

    if (this.isEditMode) {
      this.customerService.getCustomerById(Number(this.customerId)).subscribe(customer => {
        this.form.patchValue(customer);
      });
    }
  }

  onSubmit() {
    if (this.form.invalid) {
      return;
    }

    const customerData = this.form.value;
    let operation$: Observable<number | void>;
    let successMessage: string;

    if (this.isEditMode && this.customerId) {
      operation$ = this.customerService.updateCustomer(Number(this.customerId), customerData);
      successMessage = 'Cliente atualizado com sucesso!';
    } else {
      operation$ = this.customerService.createCustomer(customerData);
      successMessage = 'Cliente criado com sucesso!';
    }

    operation$.subscribe({
      next: () => {
        this.snackBar.open(successMessage, 'Fechar', { duration: 3000 });
        this.router.navigate(['/customers']);
      },
      error: (err: any) => {
        console.error('Erro ao salvar cliente:', err);
      }
    });
  }
}