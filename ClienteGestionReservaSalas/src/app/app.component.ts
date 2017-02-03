import { Component, ViewContainerRef, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { MdIconRegistry } from '@angular/material';

import { TdLoadingService, LoadingType, ILoadingOptions } from '@covalent/core';
import { AuthService } from './shared/services/auth.service';
import { EncuestasService } from './shared/services/encuestas.service';




@Component({
  selector: 'qs-app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit{
  
  routes : Object[];
  loggedIn = false;
  encuestasPendientes = false;

  constructor(private _loadingService: TdLoadingService,
              private _iconRegistry: MdIconRegistry,
              private _domSanitizer: DomSanitizer,
              private viewContainerRef: ViewContainerRef,
              private _authService : AuthService,
              private _router: Router,
              private _encuestasService: EncuestasService) {
      
  }

  ngOnInit() : void{
    this.loggedIn = this._authService.isLoggedIn();
    if ( this.loggedIn ) {
      this._encuestasService.getAll()
          .then(response => this.encuestasPendientes = response.reservasParaResponder.length)
          .catch(err => {});
    }
    this._router.events.subscribe((val) => {
      this.loggedIn = this._authService.isLoggedIn()
      if(! this.loggedIn)
        this.encuestasPendientes = false;
      else
        this._encuestasService.getAll()
          .then(response => this.encuestasPendientes = response.reservasParaResponder.length)
          .catch(err => {});
    });

    let options: ILoadingOptions = {
      name: 'main',
      type: LoadingType.Linear,
    };
    this._loadingService.createOverlayComponent(options, this.viewContainerRef);
    this._iconRegistry.addSvgIconInNamespace('assets', 'teradata',
      this._domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/teradata.svg'));
    this._iconRegistry.addSvgIconInNamespace('assets', 'github',
      this._domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/github.svg'));
    this._iconRegistry.addSvgIconInNamespace('assets', 'covalent',
      this._domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/covalent.svg'));
    this._iconRegistry.addSvgIconInNamespace('assets', 'covalent-mark',
      this._domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/covalent-mark.svg'));
    this._iconRegistry.addSvgIconInNamespace('assets', 'teradata-ux',
      this._domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/teradata-ux.svg'));
    this._iconRegistry.addSvgIconInNamespace('assets', 'appcenter',
      this._domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/appcenter.svg'));
    this._iconRegistry.addSvgIconInNamespace('assets', 'listener',
      this._domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/listener.svg'));
    this._iconRegistry.addSvgIconInNamespace('assets', 'querygrid',
      this._domSanitizer.bypassSecurityTrustResourceUrl('assets/icons/querygrid.svg'));

    //this._loadingService.register("main");

     this.routes = [
      {
        title: "Salas", route: "/salas", icon: "home"
      }, {
        title: "Reservas", route: "/reservas", icon: "library_books"
      }, {
        title: "Encuestas", route: "/encuestas", icon: "color_lens"
      }, {
        title: "Cambiar Contraseña", route: "/change-password", icon: "security"
      },
       
      // {
      //   title: "Layouts", route: "/layouts", icon: "view_quilt"
      // }, 
      // {
      //   title: "Teradata Components", route: "/components", icon: "picture_in_picture"
      // }
    ];
  }



  logout() : void {
    this._authService.logout();
    this._router.navigate(["login"]);
  }


}
