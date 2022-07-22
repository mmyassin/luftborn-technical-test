import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RootRoutingModule} from './root-routing.module';
import {API_BASE_URL} from '@shared/service-proxies/service-proxies';
import {RootComponent} from './root.component';
import {environment} from './environments/environment';
import {JwtInterceptor} from '@shared/helpers/jwt.interceptor';
import {ErrorInterceptor} from '@shared/helpers/error.interceptor';
import {SharedModule} from '@shared/shared.module';
import {ServiceProxyModule} from '@shared/service-proxies/service-proxy.module';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    RootRoutingModule,
    ServiceProxyModule,
    SharedModule.forRoot(),
  ],
  declarations: [RootComponent],
  providers: [
    {provide: API_BASE_URL, useValue: environment.remoteServiceBaseUrl},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
  ],
  bootstrap: [RootComponent],
})
export class RootModule {
}
