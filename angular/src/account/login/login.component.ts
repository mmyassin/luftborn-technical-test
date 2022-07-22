import { Component } from '@angular/core';
import { AuthService } from '@shared/auth/auth.service';

@Component({
  templateUrl: './login.component.html',
})
export class LoginComponent {
  submitting = false;

  constructor(
    public authService: AuthService) {
  }

  login(): void {
    this.submitting = true;
    this.authService.authenticate(() => (this.submitting = false));
  }
}
