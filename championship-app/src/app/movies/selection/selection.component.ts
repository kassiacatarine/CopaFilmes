import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Response } from 'src/app/shared/interfaces/response.interface';
import { Movie } from '../shared/interfaces/movie.interface';
import { MovieService } from '../shared/services/movie.service';
import { TournamentService } from '../shared/services/tournament.service';

@Component({
  selector: 'app-selection',
  templateUrl: './selection.component.html',
  styleUrls: ['./selection.component.scss']
})
export class SelectionComponent implements OnInit {
  tournamentForm: FormGroup;
  responseMovies$: Observable<Response<Movie[]>>;
  constructor(
    private service: MovieService,
    private tournamentService: TournamentService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.initForm();
    this.responseMovies$ = this.service.getMovies();
  }

  private initForm() {
    this.tournamentForm = this.formBuilder.group({
      moviesIds: [{value: null, disabled: false}, Validators.required]
    })
  }

  public handleSubmitMovies() {
    if (this.tournamentForm.valid && this.tournamentForm.value.moviesIds.length === 8) {
      this.tournamentService.create(this.tournamentForm.value)
        .subscribe(response => this.router.navigate(['/standings', response.data]));
    }
  }

}
