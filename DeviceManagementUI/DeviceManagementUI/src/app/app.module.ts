import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule} from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DevicesListComponent } from './devices/devices-list.component';
import { DeviceDetailsComponent } from './devices/device-details.component';
import { RouterModule } from '@angular/router';
import { WelcomeComponent } from './home/welcome-component';
import { DeviceDetailsGuard } from './devices/device-details.guard';
import { EditDeviceComponent } from './devices/edit-device.component';

@NgModule({
  declarations: [
    AppComponent,
    DevicesListComponent,
    DeviceDetailsComponent,
    WelcomeComponent,
    EditDeviceComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: "devices", component: DevicesListComponent},
      {
        path: "devices/:id", 
        canActivate: [DeviceDetailsGuard],
        component: DeviceDetailsComponent,
      },
      {
        path: "devices/edit/:id", 
        component: EditDeviceComponent,
      },
      {path: "welcome", component: WelcomeComponent},
      {path: "", redirectTo: "welcome", pathMatch: "full"},
      {path: "**", redirectTo: "welcome", pathMatch: "full"},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
