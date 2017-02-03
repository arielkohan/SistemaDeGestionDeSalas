import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { EqualValidator } from '../shared/directives/validate-equal.directive';
import { Router } from '@angular/router';

import { TdLoadingService, TdDialogService } from '@covalent/core';

import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.scss'],
  // directives: [EqualValidator]
})
export class PasswordChangeComponent implements OnInit {

  username: string;
  oldPassword: string;
  newPassword: string; 
  confirmPassword: string;

  constructor(private _router: Router,
              private _loadingService: TdLoadingService,
              private _dialogService: TdDialogService,
              private viewContainerRef: ViewContainerRef,
              private _authService : AuthService) { }

  ngOnInit() {
  }

  changePassword(): void {
     this._loadingService.register('changePassword');
    console.log("changePassword");
    this._authService.changePassword(this.username, this.oldPassword, this.newPassword, this.confirmPassword)
            .subscribe((result) => {
              this._loadingService.resolve('changePassword');
              this._router.navigate(["login"]);
              
            },
            (error) =>{
                this._loadingService.resolve('changePassword');
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
