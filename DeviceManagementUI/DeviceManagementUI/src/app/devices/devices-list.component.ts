import { Component, OnChanges, OnDestroy, OnInit } from "@angular/core";
import { Device } from "./device";
import { DeviceService } from "../services/device.service";
import { Subscription } from "rxjs";
import { ActivatedRoute, Router } from "@angular/router";
import { UserService } from "../services/user.service";
import { DeviceInsert } from "./deviceInsert";
// import jwt from 'jsonwebtoken';


@Component({
    templateUrl: "./devices-list.component.html",
    styleUrls: ["./devices-list.component.css"],
})
export class DevicesListComponent implements OnInit, OnDestroy{
    pageTitle = 'Device List';
    devices: Device[] = [];
    filteredDevices: Device[] = [];
    userEmail: string = "";
    subGet! : Subscription;
  
    private _listFilter: string ="";

    get listFilter(): string{
        return this._listFilter;
    }    

    set listFilter(value: string){
        this._listFilter = value;
        this.filteredDevices = this.performFilter(value)
    }     

    constructor(private route: ActivatedRoute, private router: Router, private deviceService: DeviceService, private userService:UserService){}

    performFilter(filterBy: string): Device[]{
        filterBy = filterBy.toLocaleLowerCase();
        return this.devices.filter((device: Device) =>
        device.name.toLocaleLowerCase().includes(filterBy))
    }

    deleteDevice(id: number): void{
        this.deviceService
        .deleteDevice(id)
        .subscribe((result: Device[]) => {
            console.log(result)            
            this.devices = this.devices.filter(item => item.id !== id);
            this.filteredDevices = this.devices;
            console.log(this.devices);        
        })   
           
    }  

    onUpdateDevice(id: number): void{
        this.router.navigate(['devices/edit/',id])
      }

    bookDevice(id: number): void{
        console.log("book")
        this.deviceService.assignDevice(id).subscribe(()=> console.log("assign device"));
    }

    initNewDevice(): void{
        this.router.navigate(['devices/edit/',0])
    }

    ngOnInit(): void{  
        // const token = localStorage.getItem('jwt');
        // const decodedToken = jwt.decode(token) as {email: string}

        // if(decodedToken && decodedToken.email){
        //     this.userEmail = decodedToken.email;
        // } else{
        //     console.error("Failed to decode the token or retrieve the email address.")
        // }

        console.log("in init");
        console.log(this.userEmail);
        this.subGet =  this.deviceService
        .getDevices()
        .subscribe((result: Device[]) => 
            {this.devices = result;
            this.filteredDevices = result
            console.log(result)
        });   
         
    }

    ngOnDestroy(): void{
        this.subGet.unsubscribe
    }

}