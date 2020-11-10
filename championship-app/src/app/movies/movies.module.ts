import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { MoviesRoutingModule } from './movies-routing.module';
import { SelectionComponent } from './selection/selection.component';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MovieService } from './shared/services/movie.service';
import { ReactiveFormsModule } from '@angular/forms';
import { TournamentService } from './shared/services/tournament.service';
import { CoreModule } from '../core/core.module';



@NgModule({
  declarations: [SelectionComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatListModule,
    MatButtonModule,
    FlexLayoutModule,
    CoreModule,
    MoviesRoutingModule
  ],
  providers: [
    MovieService,
    TournamentService
  ]
})
export class MoviesModule { }
