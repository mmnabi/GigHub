import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyGigsComponent } from './my-gigs/my-gigs.component';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'mine', component: MyGigsComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class GigsRoutingModule { }
