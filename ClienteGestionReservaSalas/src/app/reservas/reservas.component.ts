import { Component, OnInit,ViewContainerRef } from '@angular/core';
import { Reserva } from '../shared/model/Reserva';
import { ReservaService } from '../shared/services/reserva.service';
import { DetalleReservaComponent } from './detalle-reserva/detalle-reserva.component';


import { TdDialogService } from '@covalent/core';

import { MdDialog, MdDialogRef, MdDialogConfig, MdSnackBar, MdSnackBarRef } from '@angular/material';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reservas',
  templateUrl: './reservas.component.html',
  styleUrls: ['./reservas.component.scss']
})
export class ReservasComponent implements OnInit {

  reservasDelUsuario: Reserva[]  = [];
  reservasDeOtrosUsuarios: Reserva[] = [];
  reservaSeleccionada: Reserva;

  constructor (
    private dialog: MdDialog,
    private viewContainerRef : ViewContainerRef,
    private _reservasService: ReservaService,
    private _router : Router,
     private _dialogService: TdDialogService,
     private _snackBarService: MdSnackBar
      ) { }

  ngOnInit() {
    this._reservasService.getAll()
      .then((response) => {
            this.reservasDelUsuario = response.reservasDelUsuario; 
            this.reservasDeOtrosUsuarios = response.reservasDeOtrosUsuarios;
          })
      .catch((error) => console.error("Error buscando reservas. " , error));
  }

  // openDialog(reserva: Reserva) : void {
  //   console.log(reserva);
  //   this.reservaSeleccionada = reserva;
  // }
  
  editReserva(reserva: Reserva){
    
    this._router.navigate(["reservas",reserva.reservaID,"edit"]);

    // let snackBarRef: MdSnackBarRef<any> = 
    //                   this._snackBarService.open('Editar reserva no implementado.', null, {duration:3000});
  }

  deleteReserva(reserva){
    // console.log("Delete reserva.");
    // console.log(reserva);
    this._dialogService.openConfirm({
      message: 'Seguro desea eliminar la reserva para la sala ' + reserva.salaNombre + '?',
      disableClose: false , 
      viewContainerRef: this.viewContainerRef, 
      title: 'Eliminar', 
      cancelButton: 'CANCELAR', 
      acceptButton: 'ACEPTAR', 
    }).afterClosed().subscribe((accept: boolean) => {
        // console.log(reserva);
        if (accept) {
          this._reservasService.delete(reserva.reservaID);
          let index = this.reservasDelUsuario.indexOf(reserva);
          if( index > -1 ) {
            this.reservasDelUsuario.splice(index,1);
            let snackBarRef: MdSnackBarRef<any> = 
                      this._snackBarService.open('Reserva en la sala ' + reserva.salaNombre + ' ELIMINADA', null, {duration:3000});
          }
        } else {
          console.log("Eliminación Cancelada.");
        }
      },
      (error =>  console.log("Eliminación Cancelada.", error))
    );

  }

  openDialog(reserva : Reserva): void {

    let dialogRef: MdDialogRef<DetalleReservaComponent>;
    let config = new MdDialogConfig();
    config.viewContainerRef = this.viewContainerRef;
    //config.height = "50%";
    config.width = "60%";

    dialogRef = this.dialog.open(DetalleReservaComponent,config); //https://medium.com/@tarik.nzl/making-use-of-dialogs-in-material-2-mddialog-7533d27df41#.3cz5sotng

    dialogRef.componentInstance.reserva = reserva;

    dialogRef.afterClosed().subscribe(result => {
      // this.selectedOption = result;
      // console.log("doSomething");
    });
  }


}
