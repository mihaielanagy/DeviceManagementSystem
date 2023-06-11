import { Component, Input, OnInit } from '@angular/core';
import { Device, DeviceType, Manufacturer, OperatingSystemVersion, Processor, RamAmount } from './device';
import { ActivatedRoute, Router } from '@angular/router';
import { DeviceService } from '../services/device.service';
import { DeviceInsert } from './deviceInsert';

@Component({
  selector: 'app-edit-device',
  templateUrl: './edit-device.component.html',
  styleUrls: ['./edit-device.component.css']
})
export class EditDeviceComponent implements OnInit{

  @Input() device?: Device;
  deviceToAdd : DeviceInsert = new DeviceInsert();
  
  deviceTypes: DeviceType[] =[];
  manufacturers: Manufacturer[] = [];
  osVersions: OperatingSystemVersion[] = [];
  processors: Processor[] = [];
  ramAmounts: RamAmount[] = [];

  pageTitle = "Edit device"
  constructor(private route: ActivatedRoute, private router: Router, private deviceService: DeviceService){}
  
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    console.log(id)
    if (id!=0){
      console.log("hello motto")
      this.getDevice(id);
    }else{
      this.device = {} as Device;
      this.device.deviceType = {} as DeviceType;
      this.device.manufacturer = {} as Manufacturer;
      this.device.osVersion = {} as OperatingSystemVersion;
      this.device.processor = {} as Processor;
      this.device.ramAmount = {} as RamAmount;
    }
    this.getDeviceTypes();
    this.getManufacturers();
    this.getOsVersions();
    this.getProcessors();
    this.getRamAmounts();
  }
  
  getDevice(id: number): void{
    this.deviceService
    .getDeviceById(id)
    .subscribe((result: Device) => {
        this.device = result;        
    })}

  getDeviceTypes(): void {
    this.deviceService
    .getDeviceTypes()
    .subscribe((result: DeviceType[]) => {
        this.deviceTypes = result;   
    })}

  getManufacturers(): void {
    this.deviceService
    .getManufacturers()
    .subscribe((result: Manufacturer[]) => {
        this.manufacturers = result;  
        this.manufacturers.sort((a,b) => a.name.localeCompare(b.name))      
    }) }
  
  getOsVersions(): void {
    this.deviceService
    .getOperatingSystemVersions()
    .subscribe((result: OperatingSystemVersion[]) => {
        this.osVersions = result;
        this.osVersions.sort((a,b) => a.name.localeCompare(b.name))       
    })}

  getProcessors(): void {
    this.deviceService
    .getProcessors()
    .subscribe((result: Processor[]) => {
        this.processors = result;        
        this.processors.sort((a,b) => a.name.localeCompare(b.name));       
    })}
    
  getRamAmounts(): void {
    this.deviceService
    .getRamAmounts()
    .subscribe((result: RamAmount[]) => {
        this.ramAmounts = result;
        this.ramAmounts.sort((a,b) => a.amount - b.amount);        
    })}

     updateDevice(device: Device){

     }

     createDevice(device: Device){

     }

     onBack(): void{
       this.router.navigate(['/devices']);
     }
  
}

