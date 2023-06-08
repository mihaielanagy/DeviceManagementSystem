import { Component } from '@angular/core';
import { DeviceService } from './services/device.service';
import { Device } from './models/device';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'DeviceManagementUI';
  devices: Device[] = [];

  constructor(private deviceService: DeviceService){}

  ngOnInit(): void{
    this.deviceService
    .getDevices()
    .subscribe((result: Device[]) => (this.devices = result));
  }
}
