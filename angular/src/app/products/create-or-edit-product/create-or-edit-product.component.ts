import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {ProductDto, ProductServiceProxy} from '@shared/service-proxies/service-proxies';
import {finalize} from 'rxjs/operators';

@Component({
  selector: 'app-create-or-edit-product',
  templateUrl: './create-or-edit-product.component.html',
  styles: []
})
export class CreateOrEditProductComponent implements OnInit {
  productId: number;
  product: ProductDto = new ProductDto();
  saving = false;

  constructor(private _router: Router,
              private _activatedRoute: ActivatedRoute,
              private _productServiceProxy: ProductServiceProxy
  ) {
  }

  ngOnInit(): void {
    this._activatedRoute.params.subscribe(async (params: Params) => {
      this.productId = params['productId'];
      if (this.productId) {
        this.getProduct(this.productId);
      }
    });
  }

  getProduct(productId: number) {
    this._productServiceProxy.getProductForEdit(productId).subscribe(result => {
      this.product = result;
    });
  }

  save() {
    this.saving = true;
    this._productServiceProxy.createOrUpdate(this.product)
      .pipe(finalize(() => {
        this.saving = false;
      }))
      .subscribe(result => {
        this._router.navigate(['/app/products']);
      });
  }

}
