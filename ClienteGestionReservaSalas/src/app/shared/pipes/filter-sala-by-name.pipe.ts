import { Pipe, PipeTransform } from '@angular/core';
import { Sala } from '../model/Sala';

@Pipe({
  name: 'filterSalaByName'
})
export class FilterSalaByNamePipe implements PipeTransform {

 transform(value: Sala[], args?: string): any {
    let result: Sala[];
    if( ! value || !args)
      return value;

    result = value.filter((sala) => sala.nombre.includes(args));
  
    return result;
 }
}
