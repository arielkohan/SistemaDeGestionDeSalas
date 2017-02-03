import { Directive, Input, OnChanges, SimpleChanges, OnInit } from '@angular/core';
import { AbstractControl, NG_VALIDATORS, Validator, ValidatorFn, Validators } from '@angular/forms';

@Directive({
  selector: '[min-hour-now]',
     providers: [{provide: NG_VALIDATORS, useExisting: MinHourNowDirective, multi: true}]

})
export class MinHourNowDirective implements Validator, OnInit{

  private valFn = Validators.nullValidator;

  constructor() { }

  ngOnInit(){
      this.valFn = minHourNowValidator();
  }

   validate(control: AbstractControl): {[key: string]: any} {
    return this.valFn(control);
  }

}

export function minHourNowValidator() : ValidatorFn {

  return (control: AbstractControl): {[key: string]: any} => {
    const arbitraryDate = "2000-01-01";
    const controlValue = new Date(arbitraryDate + " " + control.value);
    const now = new Date();
    let timeNow = new Date(arbitraryDate + " 00:00");
    timeNow.setHours(now.getHours(),now.getMinutes());
    const valid = controlValue >= timeNow; 
    return valid ? null : {'minHourNow': {controlValue}} ;
  };

}