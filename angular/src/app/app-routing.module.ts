import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {AppComponent} from '@app/app.component';
import {AuthGuard} from '@shared/auth/auth-guard.service';
import {ProductsComponent} from '@app/products/products.component';
import {CreateOrEditProductComponent} from '@app/products/create-or-edit-product/create-or-edit-product.component';

const routes: Routes = [
  {
    path: '',
    component: AppComponent,
    canActivate: [AuthGuard],
    children: [
      {path: 'products', component: ProductsComponent, canActivate: [AuthGuard]},
      {path: 'products/create', component: CreateOrEditProductComponent, canActivate: [AuthGuard]},
      {path: 'products/edit/:productId', component: CreateOrEditProductComponent, canActivate: [AuthGuard]},
      { path: '', redirectTo: 'products', pathMatch: 'full' },
    ],
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
