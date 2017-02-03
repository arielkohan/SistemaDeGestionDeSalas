import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { EqualValidator } from '../shared/directives/validate-equal.directive';
import { Router } from '@angular/router';

import { TdLoadingService, TdDialogService } from '@covalent/core';

import { AuthService } from '../shared/services/auth.service';


@Component({
  selector: 'app-registrar-usuario',
  templateUrl: './registrar-usuario.component.html',
  styleUrls: ['./registrar-usuario.component.scss']
})
export class RegistrarUsuarioComponent implements OnInit {

  nombre: string;
  email: string;
  apellido: string;
  legajo: string;
  dni: string;
  username: string;
  newPassword: string; 
  confirmPassword: string;


  constructor(private _router: Router,
              private _loadingService: TdLoadingService,
              private _dialogService: TdDialogService,
              private viewContainerRef: ViewContainerRef,
              private _authService : AuthService) { }

  ngOnInit() {
  }
  login(){
    this._router.navigate(["login"]);
  }
   registerUser(): void {
     this._loadingService.register('registerUser');
    this._authService.registerUser(this.email, this.nombre, this.apellido, this.legajo, this.dni, this.username, this.newPassword, this.confirmPassword)
            .subscribe((result) => {
              this._loadingService.resolve('registerUser');
              this._router.navigate(["login"]);
              
            },
            (error) =>{
                this._loadingService.resolve('registerUser');
                this._dialogService.openAlert({
                  message: 'Hubo un problema al intentar cambiar la contraseña. Asegurese que los datos sean correctos e ntente nuevamente.',
                  disableClose: false, // defaults to false
                  viewContainerRef: this.viewContainerRef, //OPTIONAL
                  title: 'Error', //OPTIONAL, hides if not provided
                  closeButton: 'CERRAR', //OPTIONAL, defaults to 'CLOSE'
                });
            });
  }


}
