import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { decode } from 'jsonwebtoken';

@Component({
  templateUrl: './welcome.component.html',
  styleUrls: ["./welcome-component.css"],
})
export class WelcomeComponent {
  public pageTitle = 'Welcome';
  public userName: string = "";
  
  constructor(private router: Router, private jwtHelpter : JwtHelperService){}

  isUserAuthenticated(){
    const token = localStorage.getItem("jwt");
    if(token && !this.jwtHelpter.isTokenExpired(token)){
      const decodedToken = this.jwtHelpter.decodeToken(token);
      this.userName = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
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
