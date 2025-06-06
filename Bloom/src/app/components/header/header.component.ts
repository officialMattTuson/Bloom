import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  totalBalance = 50000;
  dailyPnL = 1250.5;
  dailyPnLPercent = 2.56;

  get dailyPnLClass() {
    return this.dailyPnL >= 0 ? 'text-success' : 'text-danger';
  }
}
