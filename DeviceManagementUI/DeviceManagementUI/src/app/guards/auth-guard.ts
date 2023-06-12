import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable() 

export class AuthGuard implements CanActivate {
  constructor(private router: Router, private jwtHelper: JwtHelperService){}

 canActivate(){
    console.log(true)
    return true;
    const token = localStorage.getItem("jwt");
    if(token && !this.jwtHelper.isTokenExpired(token)){
        return true
    }
     
    console.log(false)
    this.router.navigate(["login"]);

      return false;
    }
  
}