import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Weather } from '../models/weather';
import { WeatherService } from '../services/weather.service';
import { CommonModule } from '@angular/common';

import { CitySearchComponent } from '../city-search/city-search';
import { WeatherCard } from '../weather-card/weather-card';

@Component({
  selector: 'app-weather-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    CitySearchComponent,
    WeatherCard
  ],
  templateUrl: './weather-dashboard.html',
  styleUrls: ['./weather-dashboard.css']
})
export class WeatherDashboardComponent {  
  weather$: Observable<Weather | null>;

  constructor(private weatherService: WeatherService) {
    this.weather$ = this.weatherService.weather$;
  }
}
