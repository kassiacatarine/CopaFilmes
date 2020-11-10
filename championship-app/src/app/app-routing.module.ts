import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: '', loadChildren: () => import('./movies/movies.module').then(m => m.MoviesModule)},
  { path: 'standings', loadChildren: () => import('./standings/standings.module').then(m => m.StandingsModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
