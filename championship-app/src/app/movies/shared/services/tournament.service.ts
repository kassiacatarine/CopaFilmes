import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from 'src/app/shared/interfaces/response.interface';
import { environment } from 'src/environments/environment';
import { CreateTournament } from '../interfaces/create-tournament.interface';

@Injectable({
  providedIn: 'root'
})
export class TournamentService {

  private readonly apiURL: string = `${environment.apiURL}v1/tournaments`;
  constructor(private http: HttpClient) { }

  public create(tournament: CreateTournament): Observable<Response<string>> {
    return this.http.post<Response<string>>(`${this.apiURL}`, tournament);
  }
}
