import {
  Component,
  OnInit,
  ViewEncapsulation,
  Renderer2
} from '@angular/core';

@Component({
  templateUrl: './account.component.html',
  encapsulation: ViewEncapsulation.None
})
export class AccountComponent  implements OnInit {
  constructor(private renderer: Renderer2) {
  }

  ngOnInit(): void {
    this.renderer.addClass(document.body, 'login-page');
  }
}
