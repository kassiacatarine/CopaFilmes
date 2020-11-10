import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Response } from 'src/app/shared/interfaces/response.interface';
import { Standing } from '../shared/interfaces/standing.interface';
import { StandingService } from '../shared/services/standing.service';
import { map, skipWhile, switchMap } from 'rxjs/operators'

@Component({
  selector: 'app-standing-details',
  templateUrl: './standing-details.component.html',
  styleUrls: ['./standing-details.component.scss']
})
export class StandingDetailsComponent implements OnInit {
  id$: Observable<string>
  responseStanding$: Observable<Response<Standing>>;
  constructor(
    private service: StandingService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.id$ = this.route.params.pipe(map(p => p.id));
    this.responseStanding$ = this.id$.pipe(
      skipWhile(id => id === null),
      switchMap(id => this.service.getById(id))
    );
  }

}
