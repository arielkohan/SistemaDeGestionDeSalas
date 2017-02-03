import { Component, ViewContainerRef } from '@angular/core';
import { Router } from '@angular/router';

import { TdLoadingService, TdDialogService } from '@covalent/core';

import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'qs-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {

  username: string;
  password: string;

  constructor(private _router: Router,
              private _loadingService: TdLoadingService,
              private _dialogService: TdDialogService,
              private viewContainerRef: ViewContainerRef,
              private _authService : AuthService
              ) {}

  login(username, password): void {
    this._loadingService.register('login');
    this._authService.login(username,password)
            .subscribe((result) => {
              this._loadingService.resolve('login');
              this._router.navigate(["reservas"]);
              if( result ){
                this._router.navigate(['reservas']);
              }
            },
            (error) =>{
                this._loadingService.resolve('login');
                this._dialogService.openAlert({
                  message: 'Hubo un problema al intentar iniciar sesión. Intente nuevamente.',
                  disableClose: false, // defaults to false
                  viewContainerRef: this.viewContainerRef, //OPTIONAL
                  title: 'Error', //OPTIONAL, hides if not provided
                  closeButton: 'CERRAR', //OPTIONAL, defaults to 'CLOSE'
                });
            });
  }

  registrar(){
    this._router.navigate(["registrar-empleado"]);
  }

}
