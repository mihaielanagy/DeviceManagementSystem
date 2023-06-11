import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
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
  @Output() deviceUpdatedId = new EventEmitter<number>;
  deviceToAdd : DeviceInsert = new DeviceInsert();  
  deviceTypes: DeviceType[] =[];
  manufacturers: Manufacturer[] = [];
  osVersions: OperatingSystemVersion[] = [];
  processors: Processor[] = [];
  ramAmounts: RamAmount[] = [];
  devices : Device[] = [];

  pageTitle = "Edit device"
  errorMessage = "";
  constructor(private route: ActivatedRoute, private router: Router, private deviceService: DeviceService){}
  
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id!=0){
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
    this.getDevices();
  }
  
  getDevice(id: number): void{
    this.deviceService
    .getDeviceById(id)
    .subscribe((result: Device) => {
        this.device = result;        
    })}

  getDevices(): void{
    this.deviceService
        .getDevices()
        .subscribe((result: Device[]) => 
            this.devices = result)}

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
    })
  }

     updateDevice(device: Device){
      console.log(device);
      this.deviceToAdd.id = device.id;
      this.deviceToAdd.name = device.name;
      this.deviceToAdd.idDeviceType = device.deviceType.id;
      this.deviceToAdd.idManufacturer = device.manufacturer.id;
      this.deviceToAdd.idOsVersion = device.osVersion.id;
      this.deviceToAdd.idProcessor = device.processor.id;
      this.deviceToAdd.idRamamount = device.ramAmount.id;      
      this.deviceToAdd.idUser = device.user?.id;      
      console.log(this.deviceToAdd);
      this.deviceService
      .editDevice(this.deviceToAdd)
      .subscribe((result: number) => {
        this.router.navigate(['/devices']);
        this.deviceUpdatedId.emit(result);
      })
      ;
     }

     createDevice(device: Device){
      const isInDb = this.devices.find((d) => d.name == device.name);
      if(isInDb){
        this.errorMessage = "The device already exists!";
        return;
      }
      console.log(device);
      this.deviceToAdd.name = device.name;
      this.deviceToAdd.idDeviceType = device.deviceType.id;
      this.deviceToAdd.idManufacturer = device.manufacturer.id;
      this.deviceToAdd.idOsVersion = device.osVersion.id;
      this.deviceToAdd.idProcessor = device.processor.id;
      this.deviceToAdd.idRamamount = device.ramAmount.id;
           
      console.log(this.deviceToAdd);
      this.deviceService
      .addDevice(this.deviceToAdd)
      .subscribe((result: number) => {
        this.deviceUpdatedId.emit(result);
        this.router.navigate(['/devices']);})
      ;
      
     }

     onBack(): void{
       this.router.navigate(['/devices']);
     }
  
}

