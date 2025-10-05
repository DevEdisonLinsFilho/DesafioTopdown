import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../../core/models/customer.model';
import { PagedResult } from '../../core/models/paged-result.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private http = inject(HttpClient);
  private apiUrl = '/api/v1/customers';

  getCustomers(pageNumber: number, pageSize: number, searchTerm?: string): Observable<PagedResult<Customer>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchTerm) {
      params = params.set('searchTerm', searchTerm);
    }

    return this.http.get<PagedResult<Customer>>(this.apiUrl, { params });
  }

  getAllCustomers(): Observable<Customer[]> {
    return this.http.get<PagedResult<Customer>>(this.apiUrl, { params: { pageNumber: 1, pageSize: 1000 } })
      .pipe(map(pagedResult => pagedResult.items));
  }

  getCustomerById(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/${id}`);
  }

  createCustomer(customer: Omit<Customer, 'id' | 'createdAt'>): Observable<number> {
    return this.http.post<number>(this.apiUrl, customer);
  }

  updateCustomer(id: number, customer: Omit<Customer, 'id' | 'createdAt'>): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, customer);
  }

  deleteCustomer(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}