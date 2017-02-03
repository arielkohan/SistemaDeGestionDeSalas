import { Component, OnInit,ViewContainerRef } from '@angular/core';
import { Reserva } from '../shared/model/Reserva';
import { TdDialogService } from '@covalent/core';
import { MdDialog, MdDialogRef, MdDialogConfig, MdSnackBar, MdSnackBarRef } from '@angular/material';
import { Router } from '@angular/router';
import { EncuestasService } from '../shared/services/encuestas.service';


@Component({
  selector: 'app-encuestas',
  templateUrl: './encuestas.component.html',
  styleUrls: ['./encuestas.component.scss']
})
export class EncuestasComponent implements OnInit {

  reservasDeEncuestasRespondidas: Reserva[] = [];
  reservasConEncuestasParaResponder: Reserva[] = [];

  constructor(
    private dialog: MdDialog,
    private viewContainerRef : ViewContainerRef,
    private _router : Router,
    private _dialogService: TdDialogService,
    private _snackBarService: MdSnackBar,
    private _encuestasService : EncuestasService,
     ) { }

  ngOnInit() {
     this._encuestasService.getAll()
      .then((response) => {
            this.reservasConEncuestasParaResponder = response.reservasParaResponder; 
            this.reservasDeEncuestasRespondidas = response.reservasRespondidas;
          })
      .catch((error) => console.error("Error buscando reservas. " , error));
  }

  completarEncuestaDeReserva(item : Reserva){
    this._router.navigate(["encuestas", "add", item.reservaID]);
  }

  isValid(form){
    return form.valid;
  }

}
