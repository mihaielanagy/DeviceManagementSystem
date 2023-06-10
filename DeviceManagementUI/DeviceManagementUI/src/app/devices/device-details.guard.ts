import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

// export const deviceDetailsGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean|UrlTree => {
//   const id = Number(route.paramMap.get('id'));
//   if(isNaN(id) || id < 1){
//     alert("Invalid device id");
//     this.router.navigate(["/devices"]);
//     return false
//   }
//   return true;
// };

@Injectable({
  providedIn: 'root'
}) 

export class DeviceDetailsGuard{
  constructor(private router: Router){}

 canActivate(
      route: ActivatedRouteSnapshot,
      state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      const id = Number(route.paramMap.get('id'));
      if (isNaN(id) || id < 1) {
        alert('Invalid product id');
        this.router.navigate(['/products']);
        return false;
      }
      return true;
    }
  
}