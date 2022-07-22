import { Component, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import {
  AccountServiceProxy,
  RegisterInput,
  RegisterOutput
} from '@shared/service-proxies/service-proxies';
import {AuthService} from '@shared/auth/auth.service';

@Component({
  templateUrl: './register.component.html',
})
export class RegisterComponent  {
  model: RegisterInput = new RegisterInput();
  saving = false;

  constructor(
    private _accountService: AccountServiceProxy,
    private _router: Router,
    private authService: AuthService
  ) {
  }

  save(): void {
    this.saving = true;
    this._accountService
      .register(this.model)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe((result: RegisterOutput) => {
        if (result.canLogin) {
          alert('Successfully registered');
          this._router.navigate(['/account/login']);
          return;
        }
      });
  }
}
