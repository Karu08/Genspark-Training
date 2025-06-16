import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Product } from '../models/product';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ProductService {
  private baseUrl = 'https://dummyjson.com/products/search';

  constructor(private http: HttpClient) {}

  getProducts(searchTerm: string = '', limit = 10, skip = 0): Observable<{ products: Product[]; total: number }> {
    const url = `${this.baseUrl}?q=${searchTerm}&limit=${limit}&skip=${skip}`;
    return this.http.get<{ products: Product[]; total: number }>(url);
  }
}
