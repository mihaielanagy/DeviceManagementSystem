import { Injectable } from '@angular/core';
import { Device, DeviceType, Manufacturer, OperatingSystemVersion, Processor, RamAmount } from '../devices/device';
import { HttpClient, HttpHeaders } from '@angular/common/http';
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
    // const token = localStorage.getItem('jwt');
    // const headers = new HttpHeaders({
    //   Authorization: `Bearer ${token}`
    // });
    return this.http.get<Device[]>(`${environment.apiUrl}/${this.url}`);
  }

  getDeviceById(id: number): Observable<Device>{
    return this.http.get<Device>(`${environment.apiUrl}/${this.url}/${id}`);
  }

  addDevice(device: DeviceInsert): Observable<number>{
    console.log("add device")
    console.log(`${environment.apiUrl}/${this.url}`)
    return this.http.post<number>(`${environment.apiUrl}/${this.url}`,device);
  }

  deleteDevice(id: number): Observable<Device[]>{
    return this.http.delete<Device[]>(`${environment.apiUrl}/${this.url}/${id}`);
  } 

  editDevice(device: DeviceInsert): Observable<number>{
    return this.http.put<number>(`${environment.apiUrl}/${this.url}`,device);
  }

  assignDevice(deviceId: number): Observable<number>{
    console.log("in device service assign")
    return this.http.put<number>(`${environment.apiUrl}/${this.url}/assign/${deviceId}`,deviceId);
  }

  unAssignDevice(deviceId: number): Observable<number>{
    console.log("in device service unassign")
    return this.http.put<number>(`${environment.apiUrl}/${this.url}/unassign/${deviceId}`,deviceId);
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
