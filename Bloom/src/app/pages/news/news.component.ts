import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-news',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.scss'],
})
export class NewsComponent implements OnInit {
  newsArticles = [
    {
      title: 'Fed Signals Potential Rate Cut in Q2 2024',
      summary:
        'Federal Reserve officials hint at possible interest rate reductions following stronger than expected economic data.',
      source: 'Financial Times',
      time: '2 hours ago',
      category: 'Economics',
      impact: 'bullish',
      relevantSymbols: ['SPY', 'QQQ', 'AAPL', 'MSFT'],
    },
    {
      title: 'NVIDIA Reports Record Q4 Earnings',
      summary:
        'NVIDIA surpasses analyst expectations with AI chip demand driving unprecedented revenue growth.',
      source: 'Reuters',
      time: '4 hours ago',
      category: 'Earnings',
      impact: 'bullish',
      relevantSymbols: ['NVDA', 'AMD', 'TSM'],
    },
    {
      title: 'Tesla Recalls 2.2M Vehicles Over Safety Concerns',
      summary:
        'Tesla issues massive recall affecting multiple models due to autopilot software issues.',
      source: 'Bloomberg',
      time: '6 hours ago',
      category: 'Corporate',
      impact: 'bearish',
      relevantSymbols: ['TSLA'],
    },
    {
      title: 'Oil Prices Surge on Middle East Tensions',
      summary:
        'Crude oil jumps 5% as geopolitical tensions escalate in key oil-producing regions.',
      source: 'CNBC',
      time: '8 hours ago',
      category: 'Commodities',
      impact: 'bullish',
      relevantSymbols: ['XOM', 'CVX', 'USO'],
    },
    {
      title: 'Apple Announces New AI Features for iPhone',
      summary:
        'Apple unveils revolutionary AI capabilities set to launch with iOS 18, boosting developer confidence.',
      source: 'TechCrunch',
      time: '10 hours ago',
      category: 'Technology',
      impact: 'bullish',
      relevantSymbols: ['AAPL', 'GOOGL', 'MSFT'],
    },
  ];

  selectedCategory = 'all';
  categories = [
    'all',
    'Economics',
    'Earnings',
    'Technology',
    'Corporate',
    'Commodities',
  ];

  ngOnInit() {}

  get filteredNews() {
    if (this.selectedCategory === 'all') {
      return this.newsArticles;
    }
    return this.newsArticles.filter(
      (article) => article.category === this.selectedCategory
    );
  }

  getImpactClass(impact: string): string {
    return impact === 'bullish' ? 'impact-bullish' : 'impact-bearish';
  }

  getCategoryClass(category: string): string {
    const classes: { [key: string]: string } = {
      Economics: 'category-economics',
      Earnings: 'category-earnings',
      Technology: 'category-technology',
      Corporate: 'category-corporate',
      Commodities: 'category-commodities',
    };
    return classes[category] || 'category-default';
  }
}
