import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {SharedModule} from '@shared/shared.module';
import {ServiceProxyModule} from '@shared/service-proxies/service-proxy.module';
import { ProductsComponent } from './products/products.component';
import {BrowserModule} from '@angular/platform-browser';
import {CommonModule} from '@angular/common';
import { CreateOrEditProductComponent } from './products/create-or-edit-product/create-or-edit-product.component';
import {FormsModule} from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    CreateOrEditProductComponent
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    SharedModule,
    ServiceProxyModule,
    FormsModule
  ],
  providers: [],
})
export class AppModule { }
