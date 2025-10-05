import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../../core/models/product.model';
import { PagedResult } from '../../core/models/paged-result.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private http = inject(HttpClient);
  private apiUrl = '/api/v1/products';

  getProducts(pageNumber: number, pageSize: number, searchTerm?: string): Observable<PagedResult<Product>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (searchTerm) {
      params = params.set('searchTerm', searchTerm);
    }

    return this.http.get<PagedResult<Product>>(this.apiUrl, { params });
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  createProduct(product: Omit<Product, 'id' | 'created_at'>): Observable<number> {
    return this.http.post<number>(this.apiUrl, product);
  }

  updateProduct(id: number, product: Omit<Product, 'id' | 'created_at'>): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, product);
  }

  deleteProduct(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}