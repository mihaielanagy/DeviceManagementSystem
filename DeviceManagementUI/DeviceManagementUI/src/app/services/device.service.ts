import { Injectable } from '@angular/core';
import { Device, DeviceType, Manufacturer, OperatingSystemVersion, Processor, RamAmount } from '../devices/device';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DeviceInsert } from '../devices/deviceInsert';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  
  constructor(private http: HttpClient) { }
  private url = "Devices";

  getDevices(): Observable<Device[]>{
    return this.http.get<Device[]>(`${environment.apiUrl}/${this.url}`);
  }

  getDeviceById(id: number): Observable<Device>{
    return this.http.get<Device>(`${environment.apiUrl}/${this.url}/${id}`);
  }

  // addDevice(device: DeviceInsert)Observable<number>{
  //   return this.http.post<num(`${environment.apiUrl}/${this.url}`);
  // }

  deleteDevice(id: number): Observable<Device[]>{
    return this.http.delete<Device[]>(`${environment.apiUrl}/${this.url}/${id}`);
  } 

  editDevice(id: number): Observable<Device[]>{
    return this.http.delete<Device[]>(`${environment.apiUrl}/${this.url}/${id}`);
  }

  getDeviceTypes(): Observable<DeviceType[]>{
    return this.http.get<DeviceType[]>(`${environment.apiUrl}/DeviceTypes`);
  }
  getManufacturers(): Observable<Manufacturer[]>{
    return this.http.get<Manufacturer[]>(`${environment.apiUrl}/Manufacturers`);
  }
  getOperatingSystemVersions(): Observable<OperatingSystemVersion[]>{
    return this.http.get<OperatingSystemVersion[]>(`${environment.apiUrl}/OperatingSystemVersions`);
  }
  getProcessors(): Observable<Processor[]>{
    return this.http.get<Processor[]>(`${environment.apiUrl}/Processors`);
  }
  getRamAmounts(): Observable<RamAmount[]>{
    return this.http.get<RamAmount[]>(`${environment.apiUrl}/RamAmounts`);
  }

}
