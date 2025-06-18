import { Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { Dashboard } from './dashboard/dashboard';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ HttpClientModule, FormsModule, Dashboard],
  template: '<app-dashboard></app-dashboard>',
  styleUrls: ['./app.css']
})
export class App {
  protected title = 'UserApp';
}
