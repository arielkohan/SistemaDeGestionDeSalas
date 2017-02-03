import { Injectable } from '@angular/core';
import { Headers, Http, URLSearchParams, RequestOptions } from '@angular/http'

import { Sala } from '../model/Sala';
import { Reserva } from '../model/Reserva';


@Injectable()
export class ReservaService {

  private apiUrl = "http://localhost:51154/api/";
  private reservasUrl = this.apiUrl + "Reservas";
  private salasReservaUrl = this.apiUrl + "reservas/salas-disponibles";
  private Headers = new Headers({'Content-Type': 'application/json', 'Accept': 'application/json'});

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

    getReserva(reservaID : number): Promise<Reserva>{
      const headers = this.getHeaders();

      return this.http.get(this.reservasUrl + `/${reservaID}` , new RequestOptions({ headers: headers}))
                .toPromise()
                .then((response) => 
                    {
                      let responseJSON = response.json();
                      this.removeTimeZoneOffset(responseJSON);
                      return responseJSON;
                    }
                  )
                .catch(this.handleError);
    }

  getAll():Promise<any>{
     const headers = this.getHeaders();

    return this.http.get(this.reservasUrl, new RequestOptions({ headers: headers}))
               .toPromise()
               .then((response) => 
                  {
                    const responseJSON = response.json();
                    this.removeTimeZoneOffsetFromArray(responseJSON.reservasDelUsuario);
                    this.removeTimeZoneOffsetFromArray(responseJSON.reservasDeOtrosUsuarios);
                    return responseJSON;
                  }
                )
               .catch(this.handleError);
  }

  removeTimeZoneOffset(reserva: any): void{//METODO PARA ELIMINAR EL TIMEZONEOFFSET DE LAS RESERVAS QUE SE GENERA POR LA LOCALIZACION
      let fechaInicio = new Date(reserva.fechaInicio);
      const _userDateTimeOffset = fechaInicio.getTimezoneOffset()*60000; // [min*60000 = ms]
      fechaInicio = new Date(fechaInicio.getTime() + _userDateTimeOffset);
      let fechaFin = new Date(reserva.fechaFin);
      fechaFin = new Date(fechaFin.getTime() + _userDateTimeOffset);
      reserva.fechaInicio = fechaInicio.toISOString();
      reserva.fechaFin = fechaFin.toISOString();
  }
  removeTimeZoneOffsetFromArray(reservas: any[]): void{//METODO PARA ELIMINAR EL TIMEZONEOFFSET DE LAS RESERVAS QUE SE GENERA POR LA LOCALIZACION
    for(let reserva of reservas){
      this.removeTimeZoneOffset(reserva);
    }
  }

  getSalasParaReservar(_ingresoSala: Date, _egresoSala: Date, _tipoSalaID: number, _cantidadPersonas: number): Promise<Sala []>{
    const headers = this.getHeaders();

    let params: URLSearchParams = new URLSearchParams();
    const _userDateTimeOffset = _ingresoSala.getTimezoneOffset()*60000; // [min*60000 = ms]
    // _ingresoSala = new Date(_ingresoSala.getTime() - _userDateTimeOffset);
    // _egresoSala = new Date(_egresoSala.getTime() - _userDateTimeOffset);

    params.set("ingresoSala", _ingresoSala.toISOString());
    params.set("egresoSala", _egresoSala.toISOString());
    params.set("tipoSalaID", _tipoSalaID.toString());
    params.set("cantidadPersonas", _cantidadPersonas.toString());

     return this.http.get(this.salasReservaUrl, {search: params, headers: headers})
               .toPromise()
               .then((response) => 
                  {
                    return response.json() as Sala[];
                  }
                )
               .catch(this.handleError);
  }

  reservar(reserva: Reserva): Promise<void>{
    const headers = this.getHeaders();
    
     
    const _userDateTimeOffset = reserva.fechaInicio.getTimezoneOffset()*60000; // [min*60000 = ms]
    const fechaInicio = new Date(reserva.fechaInicio.getTime() - _userDateTimeOffset);
    const fechaFin = new Date(reserva.fechaFin.getTime() - _userDateTimeOffset);

    return this.http.post(
        this.reservasUrl, { //  SE CREA EL JSON A PATA PARA PREVENIR LA CONVERSION DE LOS DATES CON UN OFFSET POR EL TIMEZONE
          almuerzo: reserva.almuerzo,
          cantidadPersonas: reserva.cantidadPersonas,
          encuestaID: null,
          fechaFin: fechaFin.toISOString(),
          fechaInicio: fechaInicio.toISOString(),
          motivo: reserva.motivo,
          proyector: reserva.proyector,
          salaID: reserva.salaID,
          servicio: reserva.servicio
        }, 
        new RequestOptions({'headers': headers})
      )
    
    
    // return this.http.post(this.reservasUrl, reserva, new RequestOptions({'headers': headers}))
      .toPromise()
      .then(() => null)
      .catch((error) => this.handleError(error));
  }


  editReserva(reserva: Reserva): Promise<void>{
    const headers = this.getHeaders();
     const _userDateTimeOffset = reserva.fechaInicio.getTimezoneOffset()*60000; // [min*60000 = ms]
    const fechaInicio = new Date(reserva.fechaInicio.getTime() - _userDateTimeOffset);
    const fechaFin = new Date(reserva.fechaFin.getTime() - _userDateTimeOffset);

    return this.http.put(this.reservasUrl + `/${reserva.reservaID}`, { //  SE CREA EL JSON A PATA PARA PREVENIR LA CONVERSION DE LOS DATES CON UN OFFSET POR EL TIMEZONE
          almuerzo: reserva.almuerzo,
          cantidadPersonas: reserva.cantidadPersonas,
          encuestaID: reserva.encuestaID,
          reservaID: reserva.reservaID,
          fechaFin: fechaFin.toISOString(),
          fechaInicio: fechaInicio.toISOString(),
          motivo: reserva.motivo,
          proyector: reserva.proyector,
          salaID: reserva.salaID,
          servicio: reserva.servicio
        },  new RequestOptions({'headers': headers}))
      .toPromise()
      .then(() => null)
      .catch((error) => this.handleError(error));
  }

  delete(reservaID){
    let url  = this.reservasUrl + '/' + reservaID;
    const headers = this.getHeaders();
    return this.http.delete(url, new RequestOptions({'headers': headers}))
            .toPromise()
            .then(() => null)
            .catch((error) => this.handleError(error));
  }


  private handleError(error: any): Promise<any> {
      console.error('An error occurred', error); // for demo purposes only
      return Promise.reject(error.message || error);
    }

}
