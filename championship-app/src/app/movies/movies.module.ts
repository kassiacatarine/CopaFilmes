import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MoviesRoutingModule } from './movies-routing.module';
import { SelectionComponent } from './selection/selection.component';
import { FlexLayoutModule } from '@angular/flex-layout';

import { MatListModule } from '@angular/material/list';


@NgModule({
  declarations: [SelectionComponent],
  imports: [
    CommonModule,
    MatListModule,
    FlexLayoutModule,
    MoviesRoutingModule
  ]
})
export class MoviesModule { }
