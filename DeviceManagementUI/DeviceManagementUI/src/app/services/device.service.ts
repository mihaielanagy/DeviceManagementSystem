import { Injectable } from '@angular/core';
import { Device } from '../devices/device';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, tap } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  
  constructor(private http: HttpClient) { }
  private url = "Devices";

  getDevices(): Observable<Device[]>{
    return this.http.get<Device[]>(`${environment.apiUrl}/${this.url}`);
  }

  deleteDevice(id: number): Observable<Device[]>{
    return this.http.delete<Device[]>(`${environment.apiUrl}/${this.url}/${id}`);
  }

}
