import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StandingDetailsComponent } from './standing-details/standing-details.component';

const routes: Routes = [
  { path: ':id', component: StandingDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StandingsRoutingModule { }
