import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = 'https://dummyjson.com/products';

  constructor(private http: HttpClient) {}

  getProducts(search: string, limit = 10, skip = 0): Observable<any> {
    const query = search ? `/search?q=${search}&limit=${limit}&skip=${skip}` : `?limit=${limit}&skip=${skip}`;
    return this.http.get<any>(`${this.baseUrl}${query}`);
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/${id}`);
  }
}
