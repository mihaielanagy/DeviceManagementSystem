import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UserService } from '../services/user.service';
import { Role, Location, User } from '../users/user';
import { NgForm } from '@angular/forms';
import { UserInsert } from '../users/userInsert';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit{ 
  @Output() userRegisteredId = new EventEmitter<number>;
  locations: Location[] = [];
  users: User[] = []
  roles: Role[] =[];
  errorMessage ="";
  constructor(private userService: UserService, private router: Router){}
  
  getUsers(): void {
    this.userService
    .getUsers()
    .subscribe((result: User[]) => {
        this.users = result;  
    }) }

  getLocations(): void {
    this.userService
    .getLocations()
    .subscribe((result: Location[]) => {
        this.locations = result;  
    }) }

  getRoles(): void {
    this.userService
    .getRoles()
    .subscribe((result: Role[]) => {
        this.roles = result;  
    }) }
  
  ngOnInit(): void {
      this.getLocations();
      this.getRoles();
      this.getUsers();
    }
  
  register(form: NgForm){
    const password : string = form.value.password;
    const rePassword = form.value.rePassword; 
    const email = form.value.email;

    const isInDb = this.users.find(u=> u.email == email);
    if(isInDb){
      this.errorMessage = "This email address is already registered.";
      return;
    }

    const emailPattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
    if(!emailPattern.test(email)) {
      this.errorMessage = 'Email is invalid';
      return;
    }

    if(password.length < 8 ){
      this.errorMessage = 'Password must have at least 8 characters';
      return;
    }

    if(password !== rePassword) {
      this.errorMessage = 'Passwords do not match';
      return;
    }

    this.errorMessage = '';
    console.log("submit");
    const newUser : UserInsert = {
      firstName: form.value.firstName,
      lastName: form.value.lastName,      
      idRole: form.value.role,
      idLocation: form.value.location,
      email: email,
      password: password
    };
    console.log("after creating new user");
    console.log(newUser);

    this.userService
    .registerUser(newUser)
    .subscribe((result: number) => {
      this.userRegisteredId.emit(result);
      this.router.navigate(["/login"]);
    })
       
  }
  
}
