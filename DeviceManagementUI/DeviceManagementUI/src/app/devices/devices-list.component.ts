import { Component, OnChanges, OnDestroy, OnInit } from "@angular/core";
import { Device } from "./device";
import { DeviceService } from "../services/device.service";
import { Subscription } from "rxjs";


@Component({
    selector: "app-devices",
    templateUrl: "./devices-list.component.html",
    styleUrls: ["./devices-list.component.css"],
})
export class DevicesListComponent implements OnInit, OnDestroy{
    pageTitle = 'Device List';
    devices: Device[] = [];
    filteredDevices: Device[] = [];
    subGet! : Subscription;
  
    private _listFilter: string ="";

    get listFilter(): string{
        return this._listFilter;
    }    

    set listFilter(value: string){
        this._listFilter = value;
        this.filteredDevices = this.performFilter(value)
    }     

    constructor(private deviceService: DeviceService){}

    performFilter(filterBy: string): Device[]{
        filterBy = filterBy.toLocaleLowerCase();
        return this.devices.filter((device: Device) =>
        device.name.toLocaleLowerCase().includes(filterBy))
    }

    deleteDevice(id: number): void{
        this.deviceService
        .deleteDevice(id)
        .subscribe((result: Device[]) => {
            this.devices = result;
            this.filteredDevices = result;
        })
    }  

    ngOnInit(): void{    
       this.subGet =  this.deviceService
        .getDevices()
        .subscribe((result: Device[]) => 
            {this.devices = result;
            this.filteredDevices = result
        });    
    }

    ngOnDestroy(): void{
        this.subGet.unsubscribe
    }

}