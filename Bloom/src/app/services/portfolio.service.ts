import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PortfolioService {
  private portfolioData = {
    totalValue: 50000,
    dailyChange: 1250.5,
    dailyChangePercent: 2.56,
    totalReturn: 8750.25,
    totalReturnPercent: 21.25,
    positions: 12,
    avgReturn: 15.8,
  };

  private positions = [
    {
      symbol: 'AAPL',
      quantity: 100,
      avgPrice: 175.5,
      currentPrice: 185.25,
      value: 18525,
    },
    {
      symbol: 'GOOGL',
      quantity: 15,
      avgPrice: 2750.0,
      currentPrice: 2820.5,
      value: 42307.5,
    },
    {
      symbol: 'MSFT',
      quantity: 50,
      avgPrice: 365.75,
      currentPrice: 378.9,
      value: 18945,
    },
    {
      symbol: 'NVDA',
      quantity: 25,
      avgPrice: 380.25,
      currentPrice: 425.8,
      value: 10645,
    },
    {
      symbol: 'TSLA',
      quantity: 30,
      avgPrice: 265.5,
      currentPrice: 248.5,
      value: 7455,
    },
  ];

  constructor() {}

  getPortfolioStats(): Observable<any> {
    return of(this.portfolioData);
  }

  getPositions(): Observable<any[]> {
    return of(this.positions);
  }

  getPosition(symbol: string): Observable<any> {
    const position = this.positions.find((p) => p.symbol === symbol);
    return of(position);
  }
}
