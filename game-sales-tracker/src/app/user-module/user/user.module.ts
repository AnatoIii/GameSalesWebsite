import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ProfileComponent } from '../profile/profile.component';

@NgModule({
  declarations: [ProfileComponent],
  imports: [CommonModule, FormsModule],
})
export class UserModule {}
