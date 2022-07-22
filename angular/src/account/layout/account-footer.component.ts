import {Component, Injector, ChangeDetectionStrategy} from '@angular/core';

@Component({
  selector: 'account-footer',
  templateUrl: './account-footer.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountFooterComponent {
  currentYear: number;
  versionText: string;

  constructor() {
    this.currentYear = new Date().getFullYear();
    this.versionText = '0.0.0.1';
  }
}
