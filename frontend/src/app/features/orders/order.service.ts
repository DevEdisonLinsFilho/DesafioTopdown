import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { v4 as uuidv4 } from 'uuid';

export interface CreateOrderPayload {
  customerId: number;
  items: {
    productId: number;
    quantity: number;
  }[];
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private http = inject(HttpClient);
  private apiUrl = '/api/v1/orders';

  createOrder(orderPayload: CreateOrderPayload): Observable<number> {
    const idempotencyKey = uuidv4();

    const headers = new HttpHeaders({
      'Idempotency-Key': idempotencyKey
    });

    return this.http.post<number>(this.apiUrl, orderPayload, { headers });
  }
}