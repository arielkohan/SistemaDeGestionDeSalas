import { Injectable } from '@angular/core';
import { Headers, Http, URLSearchParams, RequestOptions } from '@angular/http';
import { Pregunta } from '../model/Pregunta';
import { Encuesta } from '../model/Encuesta';

@Injectable()
export class EncuestasService {

  private apiUrl = "http://localhost:51154/api/";
  private encuestasUrl = this.apiUrl + "Encuestas";
  private reservasDeMisEncuestasUrl = this.encuestasUrl + "/reservas";
  private obtenerPreguntasUrl = this.encuestasUrl + "/obtener-preguntas"

  constructor(private http : Http) { }
 
 
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

  getAll():Promise<any>{
   const headers = this.getHeaders();

    return this.http.get(this.reservasDeMisEncuestasUrl, new RequestOptions({ headers: headers}))
               .toPromise()
               .then((response) => 
                  {
                    return response.json();
                  }
                )
               .catch((error) => this.handleError(error));
  }


  getPreguntas():Promise<Pregunta[]>{
    const headers = this.getHeaders();
    return this.http.get(this.obtenerPreguntasUrl, new RequestOptions({ headers: headers}))
               .toPromise()
               .then((response) => 
                  {
                    return response.json() as Pregunta[];
                  }
                )
               .catch((error) => this.handleError(error));
  }


  responderEncuesta(reservaID: number, encuesta: Encuesta): Promise<void>{
    const headers = this.getHeaders();

    return this.http.post(this.encuestasUrl + "/" + reservaID, encuesta, new RequestOptions({ headers: headers}))
      .toPromise()
      .then(() => null)
      .catch(error => this.handleError(error));
  }









  private handleError(error: any): Promise<any> {
      console.error('An error occurred', error); // for demo purposes only
      return Promise.reject(error.message || error);
    }

}
