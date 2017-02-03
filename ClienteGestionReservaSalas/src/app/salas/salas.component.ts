import { Component, OnInit , ViewContainerRef} from '@angular/core';
import { MdSnackBar, MdSnackBarRef } from '@angular/material';
import { SalasService } from '../shared/services/salas.service';
import { Sala } from '../shared/model/sala';
import { TdDialogService } from '@covalent/core';
import {FilterSalaByNamePipe} from '../shared/pipes/filter-sala-by-name.pipe';

import { Router } from '@angular/router';

// import { TdLoadingService, ILoadingOptions, LoadingType, LoadingMode } from '@covalent/core';

@Component({
  selector: 'app-salas',
  templateUrl: './salas.component.html',
  styleUrls: ['./salas.component.scss']
})
export class SalasComponent implements OnInit {

  salas : Sala[];
  loading: boolean = true;
  buscarInput: string;

  constructor ( 
    private salasService : SalasService,
     private viewContainerRef: ViewContainerRef,
     private _dialogService: TdDialogService,
     private _snackBarService: MdSnackBar,
     private _router: Router,
  ) { }

  ngOnInit() {

    //this.MOCKSALAS();

    this.salasService.getSalas()
      .then((response) => {
        this.salas = response;
        this.loading = false;
      })
      .catch((errorMessage) => console.error(errorMessage));
  }

  deleteSala(sala ):void{
    this._dialogService.openConfirm({
      message: 'Seguro desea eliminar la sala ' + sala.nombre + '?',
      disableClose: false , 
      viewContainerRef: this.viewContainerRef, 
      title: 'Eliminar', 
      cancelButton: 'CANCELAR', 
      acceptButton: 'ACEPTAR', 
    }).afterClosed().subscribe((accept: boolean) => {
      if (accept) {
        this.salasService.delete(sala.salaID);
        let index = this.salas.indexOf(sala);
        if( index > -1 ) {
          this.salas.splice(index,1);
          let snackBarRef: MdSnackBarRef<any> = 
                    this._snackBarService.open('SALA ' + sala.nombre + ' ELIMINADA', null, {duration:3000});
        }
      } else {
        console.log("Eliminación Cancelada.");
      }
    });
  }

  editSala(sala) : void{
    // this._dialogService.openAlert({
    //   message: 'Editar sala será implementado para la próxima actualización del sistema.',
    //   disableClose: false, // defaults to false
    //   viewContainerRef: this.viewContainerRef, //OPTIONAL
    //   title: 'Editar', //OPTIONAL, hides if not provided
    //   closeButton: 'CERRAR', //OPTIONAL, defaults to 'CLOSE'
    // });

    this._router.navigate(["salas", sala.salaID, "edit"]);
  }


  // MOCKSALAS(): void{
  //   this.salas = [
  //     new Sala(1,"Nombre 01", "Pasillo 01 Norte", 1, "Reunion") ,
  //     new Sala(2,"Nombre 02", "Pasillo 03 ESTE", 2, "Auditorio") ,
  //     new Sala(3,"Nombre 03", "Pasillo 01 SUR", 1, "Reunion") ,
  //     new Sala(4,"Nombre 04", "Pasillo 01 Norte", 1, "Reunion") ,
  //   ];
  // }

}
