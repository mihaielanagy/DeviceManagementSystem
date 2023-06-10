import { Component, Input, OnInit } from '@angular/core';
import { Device, DeviceType, Manufacturer, OperatingSystemVersion, Processor, RamAmount } from './device';
import { ActivatedRoute, Router } from '@angular/router';
import { DeviceService } from '../services/device.service';

@Component({
  selector: 'app-edit-device',
  templateUrl: './edit-device.component.html',
  styleUrls: ['./edit-device.component.css']
})
export class EditDeviceComponent implements OnInit{

  @Input() device?: Device;
  
  deviceTypes: DeviceType[] =[];
  manufacturers: Manufacturer[] = [];
  osVersions: OperatingSystemVersion[] = [];
  processors: Processor[] = [];
  ramAmounts: RamAmount[] = [];

  pageTitle = "Edit device"
  constructor(private route: ActivatedRoute, private router: Router, private deviceService: DeviceService){}
  
  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.getDevice(id);
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
    }) }
  
  getOsVersions(): void {
    this.deviceService
    .getOperatingSystemVersions()
    .subscribe((result: OperatingSystemVersion[]) => {
        this.osVersions = result;        
    })}

  getProcessors(): void {
    this.deviceService
    .getProcessors()
    .subscribe((result: Processor[]) => {
        this.processors = result;        
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
    
    onBack(): void{
      this.router.navigate(['/devices'])
    }
  
}

