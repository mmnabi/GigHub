import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MyGigsComponent } from './my-gigs/my-gigs.component';
import { GigsRoutingModule } from './gigs-routing.module';



@NgModule({
  declarations: [
    MyGigsComponent
  ],
  imports: [
    CommonModule,
    GigsRoutingModule
  ]
})
export class GigsModule { }
