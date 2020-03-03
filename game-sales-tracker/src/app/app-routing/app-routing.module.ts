import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LandingPageComponent } from '../landing-page/landing-page.component';
import { GalleryComponent } from '../authorization-module/gallery/gallery.component';

const routes: Routes = [
  { path: '', component: LandingPageComponent },
  { path: 'authorization', component: GalleryComponent },
  { path: '**', redirectTo: '/'}
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
  declarations: [],
})
export class AppRoutingModule { }
