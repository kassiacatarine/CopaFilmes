import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Response } from 'src/app/shared/interfaces/response.interface';
import { environment } from 'src/environments/environment';
import { Standing } from '../interfaces/standing.interface';

@Injectable({
  providedIn: 'root'
})
export class StandingService {

  private readonly apiURL: string = `${environment.apiURL}v1/standings`;
  constructor(private http: HttpClient) { }

  public getById(id: string): Observable<Response<Standing>> {
    return this.http.get<Response<Standing>>(`${this.apiURL}/${id}`);
  }
}
