import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Device } from './device';
import { DeviceService } from '../services/device.service';

@Component({
  templateUrl: './device-details.component.html',
  styleUrls: ['./device-details.component.css']
})
export class DeviceDetailsComponent implements OnInit{
  pageTitle: string = 'Device Specifications';
  device: Device | undefined;

  constructor(private route: ActivatedRoute, private router: Router, private deviceService: DeviceService){}

  getDevice(id: number): void{
    this.deviceService
    .getDeviceById(id)
    .subscribe((result: Device) => {
        this.device = result;        
    })
}  

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.getDevice(id);
  }

  onBack(): void{
    this.router.navigate(['/devices'])
  }

  onUpdateDevice(id: number): void{
    console.log("the test");
    this.router.navigate(['devices/edit/',id])
  }


}
