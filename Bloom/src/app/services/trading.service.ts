import { Injectable } from '@angular/core';
import { Observable, of, delay } from 'rxjs';

export interface Order {
  symbol: string;
  type: string;
  side: string;
  quantity: number;
  price?: number;
}

export interface OrderResult {
  orderId: string;
  status: string;
  executedPrice?: number;
}

@Injectable({
  providedIn: 'root',
})
export class TradingService {
  private orders: any[] = [];

  constructor() {}

  placeOrder(order: Order): Observable<OrderResult> {
    // Simulate order processing
    const result: OrderResult = {
      orderId: this.generateOrderId(),
      status: 'filled',
      executedPrice:
        order.type === 'market'
          ? this.getMarketPrice(order.symbol)
          : order.price,
    };

    // Store order for history
    this.orders.push({
      ...order,
      ...result,
      timestamp: new Date(),
      total: (result.executedPrice || 0) * order.quantity,
    });

    return of(result).pipe(delay(500));
  }

  getOrders(): Observable<any[]> {
    return of(this.orders);
  }

  private generateOrderId(): string {
    return 'ORD-' + Date.now() + '-' + Math.random().toString(36).substr(2, 9);
  }

  private getMarketPrice(symbol: string): number {
    // Simulate market prices
    const prices: { [key: string]: number } = {
      AAPL: 185.25,
      GOOGL: 2820.5,
      MSFT: 378.9,
      NVDA: 425.8,
      TSLA: 248.5,
    };

    return prices[symbol] || 100 + Math.random() * 100;
  }
}
