import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  templateUrl: './welcome.component.html',
  styleUrls: ["./welcome-component.css"],
})
export class WelcomeComponent {
  public pageTitle = 'Welcome';
  
  constructor(private router: Router, private jwtHelpter : JwtHelperService){}

  isUserAuthenticated(){
    const token = localStorage.getItem("jwt");
    if(token && !this.jwtHelpter.isTokenExpired(token)){
      return true;
    }
    else{
      return false;
    }
  }

  logOut(){
    localStorage.removeItem("jwt");
  }
}
