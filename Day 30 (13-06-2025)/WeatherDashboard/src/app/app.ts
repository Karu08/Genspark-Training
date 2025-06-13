import { Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { CitySearchComponent } from './city-search/city-search';
import { WeatherDashboardComponent } from './weather-dashboard/weather-dashboard';
import { WeatherCard } from './weather-card/weather-card';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    HttpClientModule,
    FormsModule,
    CitySearchComponent,
    WeatherDashboardComponent,
    WeatherCard
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  protected title = 'WeatherDashboard';
}
