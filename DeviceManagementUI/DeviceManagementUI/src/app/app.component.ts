import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
title = "DeviceManagementUI";
pageTitle = "Device Management System";
token: null | string = ''

logout(): void {
  localStorage.removeItem('jwt');
  window.location.href = '/'
}

ngOnInit(): void{  
  this.token = localStorage.getItem('jwt');
 }
}
