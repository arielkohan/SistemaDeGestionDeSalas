import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';

@Injectable()
export class AuthService {
  
  private serverUrl = "http://localhost:51154/";
  private headers = new Headers({'Content-Type': 'application/json', 'Accept': 'application/json'});


  private loggedIn = false;

  constructor( private http: Http ) {
     const auth_token = localStorage.getItem("auth_token");
     if(auth_token){
       const token = JSON.parse(auth_token);
       console.log(token);
       if (token.expire_at <= (new Date()).getTime())
          this.logout();
     }
     
     this.loggedIn = !!localStorage.getItem('auth_token');
  }

  login(username, password) {
    let headers = new Headers();
   headers.append('Content-Type', 'application/x-www-form-urlencoded');

    let grant_type = 'password';

    let data = `username=${username}&password=${password}&grant_type=password`;

    let url = this.serverUrl + 'oauth/token';

    return this.http
      .post(
        url, 
         data,
        // JSON.stringify(data), 
        { headers }
      )
      .map(res => {console.log(res.json()); return res.json()})
      .map((res) => {

        if (!! res.access_token) {
          // localStorage.setItem('auth_token', res.access_token);
          localStorage.setItem('auth_token', JSON.stringify({
            "auth_token":res.access_token,
            "expires_at": res.expires_in * 1000 + (new Date()).getTime()
          }));
          this.loggedIn = true;
        }
        return res.access_token;
      });
  }

   private getAuthenticatedHeaders(): Headers{
    
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    headers.append('Accept', 'application/json');
    
    let authToken: any = localStorage.getItem("auth_token");
    if( ! authToken ){
     return headers;
    }
    
    authToken = JSON.parse(authToken);
    headers.append('Authorization', `Bearer ${authToken.auth_token}`);
    return headers;
  }


  changePassword(username: string, oldPassword: string, newPassword: string, confirmPassword: string){
    const headers = this.getAuthenticatedHeaders();
    const data = {
        username: username,
        oldPassword: oldPassword,
        newPassword: newPassword,
        confirmPassword: confirmPassword
    };
    const url = this.serverUrl + "api/accounts/ChangePassword";

    return this.http
      .post(
        url, 
        data,
        new RequestOptions({headers: headers}) 
      ) 
      .map(res => res);
  }

  registerUser(email, nombre, apellido, legajo, dni, username, newPassword, confirmPassword){

    const data = {
        email: email,
        username: username,
        firstName: nombre,
        lastName: apellido,
        password: newPassword,
        confirmPassword: confirmPassword,
        legajo: legajo,
        dni: dni
    };
    const url = this.serverUrl + "api/accounts/create";

    return this.http
      .post(
        url, 
        data,
        this.headers
      ) 
      .map(res => res);
  }

  logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
  }

  isLoggedIn() {
    return this.loggedIn;
  }


}
