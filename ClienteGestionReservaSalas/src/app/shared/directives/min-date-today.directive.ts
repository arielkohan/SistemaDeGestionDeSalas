import { Directive, Input, SimpleChanges, OnInit } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, Validator, ValidatorFn, Validators } from '@angular/forms';

 @Directive({
  selector: '[minDateToday]',
   providers: [{provide: NG_VALIDATORS, useExisting: MinDateTodayValidatorDirective, multi: true}]
 })

export class MinDateTodayValidatorDirective implements Validator,  OnInit { 

  //@Input() minDateToday: string;
   private valFn = Validators.nullValidator;

   ngOnInit(){
      this.valFn = minDateTodayValidator();
   }

  validate(control: AbstractControl): {[key: string]: any} {
    return this.valFn(control);
  }


}

export function minDateTodayValidator() : ValidatorFn {

  return (control: AbstractControl): {[key: string]: any} => {
    const date = new Date(control.value + " 00:00:00");
    let dateToday = new Date();
    dateToday.setHours(0,0,0,0);
    const temp = date >= dateToday;
    return temp ? null : {'minDateToday': {date}} ;
  };

}