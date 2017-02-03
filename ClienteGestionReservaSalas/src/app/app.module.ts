import { NgModule, Type } from '@angular/core';
import { BrowserModule, Title }  from '@angular/platform-browser';

import { CovalentCoreModule, CovalentExpansionPanelModule, CovalentLoadingModule } from '@covalent/core';
import { CovalentHttpModule, IHttpInterceptor } from '@covalent/http';
import { CovalentHighlightModule } from '@covalent/highlight';
import { CovalentMarkdownModule } from '@covalent/markdown';
import { CovalentChartsModule } from '@covalent/charts';
import { CovalentDynamicFormsModule } from '@covalent/dynamic-forms';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { appRoutes, appRoutingProviders } from './app.routes';

import { ChartComponent } from '../components/chart/chart.component';

import { RequestInterceptor } from '../config/interceptors/request.interceptor';
import { ReservasComponent } from './reservas/reservas.component';
import { DetalleReservaComponent } from './reservas/detalle-reserva/detalle-reserva.component';
import { SalasComponent } from './salas/salas.component';

import { SalasService } from './shared/services/salas.service';
import { ReservaService } from './shared/services/reserva.service';
import { FormSalaComponent } from './salas/form-sala/form-sala.component';
import { FormReservaComponent } from './reservas/form-reserva/form-reserva.component';
import { AuthService } from './shared/services/auth.service';
import { EncuestasService } from './shared/services/encuestas.service';
import { LoggedInGuard } from './shared/services/logged-in.guard';
import { MinDateTodayValidatorDirective } from './shared/directives/min-date-today.directive';
import { MinHourNowDirective } from './shared/directives/min-hour-now.directive';
import { OneHourAfterTimeDirective } from './shared/directives/one-hour-after-time.directive';
import { FilterSalaByNamePipe } from './shared/pipes/filter-sala-by-name.pipe';
import { FilterReservaByResponsablePipe } from './shared/pipes/filter-reserva-by-responsable.pipe';
import { EncuestasComponent } from './encuestas/encuestas.component';
import { FormEncuestaComponent } from './encuestas/form-encuesta/form-encuesta.component';
import { PasswordChangeComponent } from './password-change/password-change.component';
import { EqualValidator } from './shared/directives/validate-equal.directive';
import { RegistrarUsuarioComponent } from './registrar-usuario/registrar-usuario.component';

const httpInterceptorProviders: Type<IHttpInterceptor>[] = [
  RequestInterceptor,
];

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ChartComponent,
    ReservasComponent,
    DetalleReservaComponent,
    SalasComponent,
    FormSalaComponent,
    FormReservaComponent,
    MinDateTodayValidatorDirective,
    MinHourNowDirective,
    OneHourAfterTimeDirective,
    FilterSalaByNamePipe,
    FilterReservaByResponsablePipe,
    EncuestasComponent,
    FormEncuestaComponent,
    PasswordChangeComponent,
    EqualValidator,
    RegistrarUsuarioComponent,
  ], // directives, components, and pipes owned by this NgModule
  imports: [
    BrowserModule,
    CovalentCoreModule.forRoot(),
    CovalentChartsModule.forRoot(),
    CovalentHttpModule.forRoot({
      inteceptors: [{
        interceptor: RequestInterceptor, paths: ['**'],
      }],
    }),
    CovalentHighlightModule.forRoot(),
    CovalentMarkdownModule.forRoot(),
    CovalentLoadingModule.forRoot(),
    CovalentDynamicFormsModule.forRoot(),
    appRoutes,
  ], // modules needed to run this module
  providers: [
    appRoutingProviders,
    httpInterceptorProviders,
    Title,
    SalasService,
    ReservaService,
    AuthService,
    LoggedInGuard,
    EncuestasService,
  ], // additional providers needed for this module
  entryComponents: [ 
    DetalleReservaComponent,
  ],
  bootstrap: [ AppComponent ],
})
export class AppModule {}
