import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TradingService } from '../../services/trading.service';
import { PortfolioService } from '../../services/portfolio.service';
import { ChartComponent } from '../../components/chart/chart.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, ChartComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  portfolioStats = {
    totalValue: 50000,
    dailyChange: 1250.5,
    dailyChangePercent: 2.56,
    totalReturn: 8750.25,
    totalReturnPercent: 21.25,
    positions: 12,
    avgReturn: 15.8,
  };

  topPerformers = [
    { symbol: 'AAPL', change: 5.2, price: 185.25 },
    { symbol: 'NVDA', change: 8.7, price: 425.8 },
    { symbol: 'TSLA', change: -2.4, price: 248.5 },
    { symbol: 'MSFT', change: 3.1, price: 378.9 },
  ];

  recentTrades = [
    {
      symbol: 'AAPL',
      type: 'BUY',
      quantity: 50,
      price: 180.25,
      time: '2 hours ago',
    },
    {
      symbol: 'GOOGL',
      type: 'SELL',
      quantity: 25,
      price: 2820.5,
      time: '4 hours ago',
    },
    {
      symbol: 'NVDA',
      type: 'BUY',
      quantity: 30,
      price: 420.75,
      time: '6 hours ago',
    },
  ];

  constructor(
    private readonly tradingService: TradingService,
    private readonly portfolioService: PortfolioService
  ) {}

  ngOnInit() {
    this.loadDashboardData();
  }

  loadDashboardData() {
    // Load portfolio data
    this.portfolioService.getPortfolioStats().subscribe((stats) => {
      this.portfolioStats = stats;
    });
  }

  getChangeClass(change: number): string {
    return change >= 0 ? 'text-success' : 'text-danger';
  }

  getTradeTypeClass(type: string): string {
    return type === 'BUY' ? 'text-success' : 'text-danger';
  }
}
