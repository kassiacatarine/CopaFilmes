import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StandingsRoutingModule } from './standings-routing.module';
import { StandingDetailsComponent } from './standing-details/standing-details.component';

import {MatIconModule} from '@angular/material/icon';
import {MatListModule} from '@angular/material/list';


import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { StandingService } from './shared/services/standing.service';
import { CoreModule } from '../core/core.module';



@NgModule({
  declarations: [StandingDetailsComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    FlexLayoutModule,
    MatListModule,
    MatIconModule,
    CoreModule,
    StandingsRoutingModule
  ],
  providers: [
    StandingService
  ]
})
export class StandingsModule { }
