import { Component, HostListener, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import {
  debounceTime,
  distinctUntilChanged,
  switchMap,
} from 'rxjs/operators';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class Home implements OnInit {
  products: Product[] = [];
  loading = false;
  isLoadingMore = false;
  hasMore = true;

  searchControl = new FormControl('');
  pageSize = 10;
  skip = 0;
  searchQuery = '';

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.setupSearch();
  }

  setupSearch() {
    this.searchControl.valueChanges
      .pipe(
        debounceTime(400),
        distinctUntilChanged(),
        switchMap((query) => {
          this.loading = true;
          this.skip = 0;
          this.searchQuery = query ?? '';
          this.hasMore = true;
          return this.productService.getProducts(this.searchQuery, this.pageSize, this.skip);
        })
      )
      .subscribe({
        next: (res) => {
          this.products = res.products;
          this.skip = this.products.length;
          this.loading = false;
          this.hasMore = this.products.length < res.total;
        },
        error: () => {
          this.loading = false;
          alert('Failed to load products');
        }
      });
  }

  @HostListener('window:scroll', [])
  onScroll(): void {
    const threshold = 100;
    const position = window.innerHeight + window.scrollY;
    const height = document.body.offsetHeight;

    if (position + threshold >= height && !this.isLoadingMore && this.hasMore) {
      this.loadMore();
    }
  }

  loadMore() {
    this.isLoadingMore = true;

    this.productService
      .getProducts(this.searchQuery, this.pageSize, this.skip)
      .subscribe({
        next: (res) => {
          this.products = [...this.products, ...res.products];
          this.skip = this.products.length;
          this.hasMore = this.products.length < res.total;
          this.isLoadingMore = false;
        },
        error: () => {
          this.isLoadingMore = false;
        }
      });
  }
}
