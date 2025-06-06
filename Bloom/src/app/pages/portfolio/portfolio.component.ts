import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortfolioService } from '../../services/portfolio.service';

@Component({
  selector: 'app-portfolio',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.scss'],
})
export class PortfolioComponent implements OnInit {
  positions: any[] = [];
  portfolioStats: any = {};

  constructor(private portfolioService: PortfolioService) {}

  ngOnInit() {
    this.loadPortfolioData();
  }

  loadPortfolioData() {
    this.portfolioService.getPositions().subscribe((positions) => {
      this.positions = positions.map((pos) => ({
        ...pos,
        unrealizedPnL: (pos.currentPrice - pos.avgPrice) * pos.quantity,
        unrealizedPnLPercent:
          ((pos.currentPrice - pos.avgPrice) / pos.avgPrice) * 100,
      }));
    });

    this.portfolioService.getPortfolioStats().subscribe((stats) => {
      this.portfolioStats = stats;
    });
  }

  getPnLClass(pnl: number): string {
    return pnl >= 0 ? 'text-success' : 'text-danger';
  }
}
