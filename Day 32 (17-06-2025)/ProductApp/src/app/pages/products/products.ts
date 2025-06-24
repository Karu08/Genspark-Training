import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product';
import { debounceTime, distinctUntilChanged, Subject, switchMap, tap } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './products.html',
  styleUrls: ['./products.css'],
})
export class Products implements OnInit {
  products: Product[] = [];
  searchTerm = '';
  private searchSubject = new Subject<string>();

  isLoading = false;
  noResults = false;
  limit = 10;
  skip = 0;
  isEndReached = false;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.listenToSearch();
    this.loadProducts();
  }


  onSearch(term: string) {
    this.searchTerm = term;
    this.skip = 0;
    this.products = [];
    this.isEndReached = false;
    this.searchSubject.next(term);
  }

  
  private listenToSearch() {
    this.searchSubject.pipe(
      debounceTime(500),
      distinctUntilChanged(),
      switchMap(term => this.productService.getProducts(term, this.limit, this.skip)),
      tap(() => this.isLoading = true)
    ).subscribe({
      next: (res) => {
        this.products = res.products;
        this.noResults = res.products.length === 0;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
        this.noResults = true;
      }
    });
  }


  loadMore() {
    if (this.isLoading || this.isEndReached) return;

    this.isLoading = true;
    this.skip += this.limit;

    this.productService.getProducts(this.searchTerm, this.limit, this.skip)
      .subscribe({
        next: res => {
          if (res.products.length === 0) {
            this.isEndReached = true;
          } else {
            this.products = [...this.products, ...res.products];
          }
          this.isLoading = false;
        },
        error: () => {
          this.isLoading = false;
        }
      });
  }

 
  loadProducts() {
    this.isLoading = true;
    this.productService.getProducts(this.searchTerm, this.limit, this.skip)
      .subscribe({
        next: res => {
          this.products = res.products;
          this.noResults = res.products.length === 0;
          this.isLoading = false;
        },
        error: () => {
          this.noResults = true;
          this.isLoading = false;
        }
      });
  }
}
