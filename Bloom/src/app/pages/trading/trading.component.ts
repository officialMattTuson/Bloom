import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TradingService } from '../../services/trading.service';

@Component({
  selector: 'app-trading',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './trading.component.html',
  styleUrls: ['./trading.component.scss'],
})
export class TradingComponent implements OnInit {
  orderForm = {
    symbol: '',
    type: 'market',
    side: 'buy',
    quantity: 1,
    price: 0,
  };

  watchlist = [
    { symbol: 'AAPL', price: 185.25, change: 2.4, volume: '45.2M' },
    { symbol: 'GOOGL', price: 2820.5, change: -1.2, volume: '28.8M' },
    { symbol: 'MSFT', price: 378.9, change: 1.8, volume: '32.1M' },
    { symbol: 'NVDA', price: 425.8, change: 5.6, volume: '55.7M' },
    { symbol: 'TSLA', price: 248.5, change: -3.2, volume: '67.3M' },
  ];

  marketData = {
    symbol: 'AAPL',
    price: 185.25,
    change: 2.4,
    changePercent: 1.31,
    volume: '45.2M',
    bid: 185.2,
    ask: 185.3,
    high: 187.45,
    low: 182.1,
  };

  constructor(private tradingService: TradingService) {}

  ngOnInit() {
    this.loadMarketData();
  }

  loadMarketData() {
    // Simulate real-time data updates
    setInterval(() => {
      this.updateMarketData();
    }, 5000);
  }

  updateMarketData() {
    this.watchlist = this.watchlist.map((stock) => ({
      ...stock,
      price: stock.price + (Math.random() - 0.5) * 2,
      change: (Math.random() - 0.5) * 10,
    }));
  }

  selectStock(symbol: string) {
    this.orderForm.symbol = symbol;
    const stock = this.watchlist.find((s) => s.symbol === symbol);
    if (stock) {
      this.marketData = {
        symbol: stock.symbol,
        price: stock.price,
        change: stock.change,
        changePercent: (stock.change / stock.price) * 100,
        volume: stock.volume,
        bid: stock.price - 0.05,
        ask: stock.price + 0.05,
        high: stock.price + Math.random() * 5,
        low: stock.price - Math.random() * 5,
      };
    }
  }

  submitOrder() {
    if (!this.orderForm.symbol || this.orderForm.quantity <= 0) {
      alert('Please fill in all required fields');
      return;
    }

    this.tradingService.placeOrder(this.orderForm).subscribe({
      next: (result) => {
        alert(`Order placed successfully! Order ID: ${result.orderId}`);
        this.resetForm();
      },
      error: (error) => {
        alert('Error placing order: ' + error.message);
      },
    });
  }

  resetForm() {
    this.orderForm = {
      symbol: '',
      type: 'market',
      side: 'buy',
      quantity: 1,
      price: 0,
    };
  }

  getChangeClass(change: number): string {
    return change >= 0 ? 'text-success' : 'text-danger';
  }

  getOrderSideClass(): string {
    return this.orderForm.side === 'buy' ? 'btn-success' : 'btn-danger';
  }
}
