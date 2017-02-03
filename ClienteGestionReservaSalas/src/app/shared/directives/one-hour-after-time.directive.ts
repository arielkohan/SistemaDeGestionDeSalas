import { Directive, forwardRef, Attribute } from '@angular/core';
import { Validator, AbstractControl, NG_VALIDATORS } from '@angular/forms';

@Directive({
    selector: '[oneHourAfterTime][formControlName],[oneHourAfterTime][formControl],[oneHourAfterTime][ngModel]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => OneHourAfterTimeDirective), multi: true }
    ]
})
export class OneHourAfterTimeDirective implements Validator {
    constructor( 
           @Attribute('oneHourAfterTime') public oneHourAfterTime: string,
           @Attribute('reverse') public reverse: string) {
    }

    private get isReverse() {
        if (!this.reverse) return false;
        return this.reverse === 'true' ? true: false;
    }

    validate(control: AbstractControl): { [key: string]: any } {
      //arbitrary date
        const arbitraryDate = "2000-01-01";

        // self value
        let thisTime = control.value;

        if ( ! thisTime ) return null;

        const thisTimeDate = new Date( arbitraryDate + " " + thisTime);
        // control vlaue
        let otherTime = control.root.get(this.oneHourAfterTime);

        if ( !otherTime ) return null;


        const otherTimeDate = new Date( arbitraryDate + " " + otherTime.value);

        // valor con diferencia de una hora o menos
        if (otherTimeDate && (thisTimeDate.getTime() - otherTimeDate.getTime()) /1000 /3600 < 1 && ! this.isReverse) {
          return {
            oneHourAfterTime: false
          }
        }


        // // value equal and reverse
        if (otherTimeDate && (otherTimeDate.getTime() - thisTimeDate.getTime()) /1000 /3600 >= 1 && this.isReverse) {
            if(otherTime.errors)
              delete otherTime.errors['oneHourAfterTime'];
            if (otherTime.errors && (! Object.keys(otherTime.errors) || ! Object.keys(otherTime.errors).length)) otherTime.setErrors(null);
        }

        // value not equal and reverse
        if (otherTimeDate && (otherTimeDate.getTime() - thisTimeDate.getTime()) /1000 /3600 < 1 && this.isReverse) {
            otherTime.setErrors({
                oneHourAfterTime: false
            })
        }
        
        return null;
    }
}