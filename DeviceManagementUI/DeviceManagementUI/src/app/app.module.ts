import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import { JwtModule } from '@auth0/angular-jwt';

import { AuthInterceptor } from './services/auth.interceptor';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DevicesListComponent } from './devices/devices-list.component';
import { DeviceDetailsComponent } from './devices/device-details.component';
import { RouterModule } from '@angular/router';
import { WelcomeComponent } from './home/welcome-component';
import { DeviceDetailsGuard } from './guards/device-details.guard';
import { EditDeviceComponent } from './devices/edit-device.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './guards/auth-guard';
import { RegisterComponent } from './register/register.component';

export function tokenGetter(){
  console.log("in token getter")
  return localStorage.getItem("jwt");

}

@NgModule({
  declarations: [
    AppComponent,
    DevicesListComponent,
    DeviceDetailsComponent,
    WelcomeComponent,
    EditDeviceComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: "", component: WelcomeComponent},
      {path: "login", component: LoginComponent},
      {path: "register", component: RegisterComponent},
      {path: "devices", component: DevicesListComponent, canActivate : [AuthGuard]},
      {
        path: "devices/:id", 
        canActivate: [DeviceDetailsGuard, AuthGuard],
        component: DeviceDetailsComponent,
      },
      {
        path: "devices/edit/:id", 
        component: EditDeviceComponent,
      },
      {path: "welcome", component: WelcomeComponent},
      {path: "**", redirectTo: "welcome", pathMatch: "full"},
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["https://localhost:7250"],
        disallowedRoutes: []
      }
    })
  ],
  providers: [AuthGuard,   {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
