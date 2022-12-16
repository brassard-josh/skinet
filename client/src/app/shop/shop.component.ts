import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  selectedBrandId: number = 0;
  selectedTypeId: number = 0;
  selectedSort = 'name';
  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getBrands();
    this.getTypes();
    this.getProducts();
  }

  getProducts() {
    this.shopService.getProducts(this.selectedBrandId, this.selectedTypeId, this.selectedSort)
      .subscribe({
        next: r => this.products = r.data,
        error: e => console.log(e)
      });
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: r => this.brands = [{ id: 0, name: 'All' }, ...r],
      error: e => console.log(e)
    });
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: r => this.types = [{ id: 0, name: 'All' }, ...r],
      error: e => console.log(e)
    });
  }

  onBrandSelected(brandId: number) {
    this.selectedBrandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.selectedTypeId = typeId;
    this.getProducts();
  }

  onSortSelected(sort: string) {
    this.selectedSort = sort;
    this.getProducts();
  }
}
