import { Component, OnChanges, OnDestroy, OnInit } from "@angular/core";
import { Device } from "./device";
import { DeviceService } from "../services/device.service";
import { Subscription } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";
import { UserService } from "../services/user.service";
import { DeviceInsert } from "./deviceInsert";
import jwt from 'jsonwebtoken';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from "../users/user";



@Component({
    templateUrl: "./devices-list.component.html",
    styleUrls: ["./devices-list.component.css"],
})
export class DevicesListComponent implements OnInit, OnDestroy{
    pageTitle = 'Device List';
    devices: Device[] = [];
    filteredDevices: Device[] = [];
    userId: number = 0;
    subGet! : Subscription;
    isCurrentUser = false;
  
    private _listFilter: string ="";

    get listFilter(): string{
        return this._listFilter;
    }    

    set listFilter(value: string){
        this._listFilter = value;
        this.filteredDevices = this.performFilter(value)
    }     

    constructor(private route: ActivatedRoute, private router: Router, private deviceService: DeviceService, private userService:UserService, private jwtHelpter : JwtHelperService){}

    performFilter(filterBy: string): Device[]{
        filterBy = filterBy.toLocaleLowerCase();
        return this.devices.filter((device: Device) =>
        device.name.toLocaleLowerCase().includes(filterBy))
    }

    deleteDevice(id: number, name: string): void{

        if(confirm(`Are you sure you want to delete the device ${name}?`)){
        this.deviceService
        .deleteDevice(id)
        .subscribe((result: Device[]) => {
            console.log(result)            
            this.devices = this.devices.filter(item => item.id !== id);
            this.filteredDevices = this.devices;
            console.log(this.devices);        
            })  
        }           
    }  

    onUpdateDevice(id: number): void{
        this.router.navigate(['devices/edit/',id])
      }

    assignDevice(deviceId: number): void{
        console.log("in component book()")
        this.deviceService.assignDevice(deviceId).subscribe(()=> console.log("assign device"));
        window.location.reload();
    }

    unassignDevice(deviceId: number): void{
        console.log(this.userId)
        this.deviceService.unAssignDevice(deviceId).subscribe(()=> console.log("unassign device"));
        window.location.reload();                
    }

    initNewDevice(): void{
        this.router.navigate(['devices/edit/',0])
    }

    ngOnInit(): void{  
        const token = localStorage.getItem('jwt');
        if(token && !this.jwtHelpter.isTokenExpired(token)){
            const decodedToken = this.jwtHelpter.decodeToken(token);
            const stringId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']; 
            this.userId = +stringId;
            console.log(this.userId);         
          }
      

        console.log("in init");
        // console.log(this.userId);
        // this.subGet =  this.deviceService
        // .getDevices()
        // .subscribe((result: Device[]) => 
        //     {this.devices = result;
        //     this.filteredDevices = result
        //     console.log(result)
        // });   

        this.subGet = this.deviceService.getDevices().subscribe({
            next: (result: Device[]) => {
              this.devices = result;
              this.filteredDevices = result;
              console.log(result);
            },
            error: (err) => {
              if (err.status === 401) {
                this.router.navigateByUrl('/main-url');
              }
              console.error('An error occurred:', err);
            }
          });
         
    }

    ngOnDestroy(): void{
        this.subGet.unsubscribe
    }

}