import {AfterViewInit, Component, OnInit} from '@angular/core';
import {ProductDto, ProductServiceProxy} from '@shared/service-proxies/service-proxies';
import {finalize} from 'rxjs/operators';

@Component({
  templateUrl: './products.component.html',
})
export class ProductsComponent implements OnInit, AfterViewInit {

  products: ProductDto[] = [];
  constructor(private _productServiceProxy: ProductServiceProxy) { }

  ngOnInit(): void {

  }
  ngAfterViewInit(): void {
    this.getProducts();
  }

  getProducts(){
    this._productServiceProxy.getAll().subscribe(result => {
      this.products = result;
    })
  }

  deleteProduct(productId: any) {
    const confirmation =  confirm("Are you sure!\nEither OK or Cancel.");
    if (confirmation) {
      this._productServiceProxy.delete(productId)
        .pipe(finalize(()=>{
          this.getProducts();
        })).subscribe();
    }
  }

}
