import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import {AuthService } from './auth.service';

@Injectable()
export class LoggedInGuard implements CanActivate {

  constructor(
    private _authService: AuthService,
    private _router: Router  
  ) {}

  canActivate() {
    if(! this._authService.isLoggedIn)
      return false;


    const token = localStorage.getItem('auth_token') ;
    // console.log("TOKEN: ", token);

    if(token){
    //  console.log("PARSE TOKEN: ", JSON.parse(token));
      
      const parseToken = JSON.parse(token);
      
      // console.log("parseToken && (new Date()).getTime() < parseToken.expires_at: ", parseToken && (new Date()).getTime() < parseToken.expires_at);
      if( parseToken && (new Date()).getTime() < parseToken.expires_at)
        return true;
  }
  console.log("RUTA PROHIBIDA SIN HABER INICIADO SESION");
   localStorage.removeItem('auth_token');
        // not logged in so redirect to login page
   this._router.navigate(['/login']);
   return false;
  }

}
