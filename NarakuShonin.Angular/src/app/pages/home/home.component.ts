import { Component } from '@angular/core';
import {
  NzColDirective,
  NzRowDirective
} from 'ng-zorro-antd/grid';
import { NzCardComponent } from 'ng-zorro-antd/card';

@Component({
  selector: 'app-home',
  imports: [
    NzRowDirective,
    NzColDirective,
    NzCardComponent
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
