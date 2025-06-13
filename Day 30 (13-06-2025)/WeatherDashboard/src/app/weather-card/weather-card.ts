import { Component, Input } from '@angular/core';
import { Weather } from '../models/weather';

@Component({
  selector: 'app-weather-card',
  imports: [],
  templateUrl: './weather-card.html',
  styleUrl: './weather-card.css'
})
export class WeatherCard {

    @Input() weather!: Weather;

}
