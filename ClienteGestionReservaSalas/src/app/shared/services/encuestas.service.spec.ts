/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { EncuestasService } from './encuestas.service';

describe('EncuestasService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EncuestasService]
    });
  });

  it('should ...', inject([EncuestasService], (service: EncuestasService) => {
    expect(service).toBeTruthy();
  }));
});
