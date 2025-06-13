import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, catchError, map, Observable, throwError } from 'rxjs';
import { Weather } from '../models/weather';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  private apiKey = '2555a7a56c4e2ed2819422af4620f9a6'; 
  private baseUrl = 'https://api.openweathermap.org/data/2.5/weather?q=';

  
  private weatherSubject = new BehaviorSubject<Weather | null>(null);
  weather$ = this.weatherSubject.asObservable();

  constructor(private http: HttpClient) { }

  fetchWeather(city: string): void {
    const url = `${this.baseUrl}${city}&units=metric&appid=${this.apiKey}`;
    this.http.get<any>(url)
      .pipe(
        map((data: any) => {
          const weather: Weather = {
            city: data.name,
            temperature: data.main.temp,
            condition: data.weather[0].description,
            icon: `http://openweathermap.org/img/wn/${data.weather[0].icon}@2x.png`,
            humidity: data.main.humidity,
            windSpeed: data.wind.speed
          };
          return weather;
        }),
        catchError(this.handleError)
      )
      .subscribe({
        next: (weather: Weather) => this.weatherSubject.next(weather),
        error: (err: string) => alert(err)  
      });
  }

  private handleError(error: HttpErrorResponse): Observable<never> {
    if (error.status === 404) {
      return throwError(() => 'City not found');
    }
    return throwError(() => 'Something went wrong. Please try again later.');
  }
}
