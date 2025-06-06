import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
})
export class SettingsComponent {
  settings = {
    trading: {
      maxPositionSize: 10000,
      stopLossPercent: 5,
      takeProfitPercent: 15,
      riskPerTrade: 2,
      autoTrade: true,
      maxDailyTrades: 10,
    },
    ai: {
      confidence: 75,
      useNews: true,
      useTechnicalAnalysis: true,
      useVolumeAnalysis: true,
      sentiment: true,
    },
    notifications: {
      email: true,
      push: false,
      orderFills: true,
      priceAlerts: true,
      newsAlerts: true,
    },
    display: {
      theme: 'dark',
      currency: 'USD',
      timezone: 'UTC',
      compactView: false,
    },
  };

  saveSettings() {
    // Save settings logic
    console.log('Settings saved:', this.settings);
    alert('Settings saved successfully!');
  }

  resetSettings() {
    if (confirm('Are you sure you want to reset all settings to default?')) {
      // Reset to default values
      this.settings = {
        trading: {
          maxPositionSize: 10000,
          stopLossPercent: 5,
          takeProfitPercent: 15,
          riskPerTrade: 2,
          autoTrade: true,
          maxDailyTrades: 10,
        },
        ai: {
          confidence: 75,
          useNews: true,
          useTechnicalAnalysis: true,
          useVolumeAnalysis: true,
          sentiment: true,
        },
        notifications: {
          email: true,
          push: false,
          orderFills: true,
          priceAlerts: true,
          newsAlerts: true,
        },
        display: {
          theme: 'dark',
          currency: 'USD',
          timezone: 'UTC',
          compactView: false,
        },
      };
      alert('Settings reset to default values!');
    }
  }
}
