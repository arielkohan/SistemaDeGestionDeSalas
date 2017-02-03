import { Injectable } from '@angular/core';
import { Headers, Http } from '@angular/http'

import { Sala } from '../model/Sala';

@Injectable()
export class SalasService {

  private apiUrl = "http://localhost:51154/api/";
  private salasUrl = this.apiUrl + 'salas';  // URL to web api
  private tiposUrl = this.apiUrl + 'tipos';
  private headers = new Headers({'Content-Type': 'application/json', 'Accept': 'application/json'});

  constructor(
    private http: Http
  ) { }

   private getHeaders(): Headers{ 
       
      let headers = new Headers();
      headers.append('Content-Type', 'application/json');
      headers.append('Accept', 'application/json');
      
      let authToken: any = localStorage.getItem("auth_token");
      if( ! authToken ){
        throw "NO HAY TOKEN DE AUTORIZACION";
      }
      
      authToken = JSON.parse(authToken);
      headers.append('Authorization', `Bearer ${authToken.auth_token}`);
      return headers;
    }


  getTipos(): Promise<any>{
    
    return this.http.get(this.tiposUrl, this.headers)
                .toPromise()
                .then(response => { 
                  return response.json();
                })
                .catch(this.handleError);
  };

  

  getSala(salaID: number): Promise<Sala> {
    return this.http.get(this.salasUrl + `/${salaID}`, this.headers)
               .toPromise()
               .then((response) => 
                  {
                    return response.json() as Sala;
                  }
                )
               .catch((error) => this.handleError(error));
  }


  getSalas(): Promise<Sala[]> {
    return this.http.get(this.salasUrl, this.headers)
               .toPromise()
               .then((response) => 
                  {
                    return response.json() as Sala[];
                  }
                )
               .catch((error) => this.handleError(error));
  }

  createSala(sala: Sala): Promise<void>{
     let headers = this.getHeaders();
     return this.http.post(this.salasUrl, sala, headers)
               .toPromise()
               .then((response) => null)
               .catch(this.handleError);
  }
  
  editSala(sala: Sala): Promise<void>{
     let headers = this.getHeaders();
     return this.http.put(this.salasUrl + `/${sala.salaID}`, sala, headers)
               .toPromise()
               .then((response) => null)
               .catch(this.handleError);
  }

  delete(id: number) : Promise<void>{
    let headers = new Headers(this.headers.values());
    let authToken = localStorage.getItem("auth_token");
    headers.append('Authorization', `Bearer ${authToken}`);

    let url  = this.salasUrl + '/' + id;
    return this.http.delete(url, headers)
            .toPromise()
            .then(() => null)
            .catch(this.handleError);
  }


    private handleError(error: any): Promise<any> {
      console.error('An error occurred', error); // for demo purposes only
      return Promise.reject(error.message || error);
    }

}
