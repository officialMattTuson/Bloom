import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { PortfolioComponent } from './pages/portfolio/portfolio.component';
import { TradingComponent } from './pages/trading/trading.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { NewsComponent } from './pages/news/news.component';
import { SettingsComponent } from './pages/settings/settings.component';

export const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'portfolio', component: PortfolioComponent },
  { path: 'trading', component: TradingComponent },
  { path: 'orders', component: OrdersComponent },
  { path: 'news', component: NewsComponent },
  { path: 'settings', component: SettingsComponent },
  { path: '**', redirectTo: '/dashboard' },
];
