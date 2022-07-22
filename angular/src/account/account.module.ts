import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientJsonpModule} from '@angular/common/http';
import {HttpClientModule} from '@angular/common/http';
import {AccountRoutingModule} from './account-routing.module';
import {ServiceProxyModule} from '@shared/service-proxies/service-proxy.module';
import {AccountComponent} from './account.component';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {AccountHeaderComponent} from './layout/account-header.component';
import {AccountFooterComponent} from './layout/account-footer.component';
import {SharedModule} from '@shared/shared.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    ServiceProxyModule,
    AccountRoutingModule,
    SharedModule
  ],
  declarations: [
    AccountComponent,
    LoginComponent,
    RegisterComponent,
    AccountHeaderComponent,
    AccountFooterComponent,

  ],
  entryComponents: []
})
export class AccountModule {

}
