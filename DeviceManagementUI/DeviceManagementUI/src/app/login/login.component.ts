import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  invalidLogin: boolean = false;
  url = "auth/login"

  constructor(private router: Router, private http: HttpClient){}

  login(form: NgForm){
    console.log("submit")
    const credentials ={
      'email': form.value.emailAddress,
      'password': form.value.password 
    }
    console.log(credentials)

    this.http.post(`${environment.apiUrl}/${this.url}`, credentials)
    .subscribe(response => {
      const token = (<any>response).token;
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
       window.location.href = '/'
    }, (err: any) => {
      this.invalidLogin = true;
    })
  }

}
