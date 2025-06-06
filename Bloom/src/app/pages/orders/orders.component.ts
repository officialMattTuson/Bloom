import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TradingService } from '../../services/trading.service';

@Component({
  selector: 'app-orders',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss'],
})
export class OrdersComponent implements OnInit {
  orders: any[] = [];
  filteredOrders: any[] = [];
  filterStatus = 'all';

  constructor(private tradingService: TradingService) {}

  ngOnInit() {
    this.loadOrders();
  }

  loadOrders() {
    this.tradingService.getOrders().subscribe((orders) => {
      this.orders = orders.sort(
        (a, b) =>
          new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime()
      );
      this.applyFilter();
    });
  }

  applyFilter() {
    if (this.filterStatus === 'all') {
      this.filteredOrders = this.orders;
    } else {
      this.filteredOrders = this.orders.filter(
        (order) => order.status === this.filterStatus
      );
    }
  }

  onFilterChange(status: string) {
    this.filterStatus = status;
    this.applyFilter();
  }

  getSideClass(side: string): string {
    return side === 'buy' ? 'text-success' : 'text-danger';
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'filled':
        return 'status-filled';
      case 'pending':
        return 'status-pending';
      case 'cancelled':
        return 'status-cancelled';
      default:
        return '';
    }
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleString();
  }
}
