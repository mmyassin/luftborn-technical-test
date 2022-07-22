import {Injectable, Injector} from '@angular/core';
import {BehaviorSubject, map, Observable} from 'rxjs';
import {AuthenticateModel, AuthenticateResultModel, TokenAuthServiceProxy} from '@shared/service-proxies/service-proxies';
import {finalize} from 'rxjs/operators';
import {Router} from '@angular/router';

@Injectable({providedIn: 'root'})
export class AuthService {
  authenticateModel: AuthenticateModel = new AuthenticateModel();
  authenticateResult: AuthenticateResultModel = new AuthenticateResultModel();
  rememberMe: boolean;

  constructor(
    private injector: Injector,
    private router: Router,
    private _tokenAuthService: TokenAuthServiceProxy) {
  }

  public authenticate(finallyCallback?: () => void) {
    debugger
    this._tokenAuthService.authenticate(this.authenticateModel).pipe(
      finalize(() => {
        if (finallyCallback) {
          finallyCallback();
        }
      })).subscribe((result: AuthenticateResultModel) => {
        if (result.accessToken) {
          localStorage.setItem('authenticateInfo', JSON.stringify(result));
          this.authenticateResult = result;
          this.router.navigate(['/app'])
        } else {
          alert('Unpected authentexicateResult!');
        }
      });
  }

  logout() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('authenticateInfo');
    this.router.navigate(['/account/login']);
  }
}
