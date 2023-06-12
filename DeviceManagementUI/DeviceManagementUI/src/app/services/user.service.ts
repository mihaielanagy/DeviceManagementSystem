import { Injectable } from '@angular/core';
import { Device } from '../devices/device';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Role, Location, User} from '../users/user';
import { UserInsert } from '../users/userInsert';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient){}

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiUrl}/Users`);
  }

  getLocations(): Observable<Location[]>{
    return this.http.get<Location[]>(`${environment.apiUrl}/Locations`);
  }

  getRoles(): Observable<Role[]>{
    return this.http.get<Role[]>(`${environment.apiUrl}/Roles`);
  }

  registerUser(user: UserInsert): Observable<number>{
    return this.http.post<number>(`${environment.apiUrl}/Users`, user);
  }
}