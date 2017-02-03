import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterReservaByResponsable'
})
export class FilterReservaByResponsablePipe implements PipeTransform {

  transform(reservas: any, args?: any): any {
    let result;
    if( ! reservas || !args)
      return reservas;

    result = reservas.filter((reserva) => reserva.responsableNomYAp.includes(args));
  
    return result;
  }

}
