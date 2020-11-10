import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from 'src/app/shared/interfaces/response.interface';
import { environment } from 'src/environments/environment';
import { Movie } from '../interfaces/movie.interface';

@Injectable({
  providedIn: 'root'
})
export class MovieService {

  private readonly apiURL: string = `${environment.apiURL}v1/movies`;
  constructor(private http: HttpClient) { }

  public getMovies(): Observable<Response<Movie[]>> {
    return this.http.get<Response<Movie[]>>(`${this.apiURL}`);
  }
}
