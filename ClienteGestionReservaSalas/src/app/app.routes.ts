import { Routes, RouterModule } from '@angular/router';

// import { UsersComponent } from './users/users.component';
// import { UsersFormComponent } from './users/+form/form.component';
import { LoginComponent } from './login/login.component';
import { ReservasComponent } from './reservas/reservas.component';
import { FormReservaComponent } from './reservas/form-reserva/form-reserva.component';
import { SalasComponent } from './salas/salas.component';
import { FormSalaComponent } from './salas/form-sala/form-sala.component';
import { EncuestasComponent } from './encuestas/encuestas.component';
import { FormEncuestaComponent } from './encuestas/form-encuesta/form-encuesta.component';
import { LoggedInGuard } from './shared/services/logged-in.guard';
import { PasswordChangeComponent } from './password-change/password-change.component';
import { RegistrarUsuarioComponent } from './registrar-usuario/registrar-usuario.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent },
  {path: 'registrar-empleado', component: RegistrarUsuarioComponent },
  {path: 'change-password', component: PasswordChangeComponent, canActivate: [LoggedInGuard] },
  {path: 'reservas', children:[
    {path: '', component: ReservasComponent, canActivate: [LoggedInGuard] },//, canActivate: [LoggedInGuard]
    {path: 'add', component: FormReservaComponent, canActivate: [LoggedInGuard]},//, canActivate: [LoggedInGuard]
    {path: ':id/edit', component: FormReservaComponent, canActivate: [LoggedInGuard]},
  ]},
  {path: 'salas', children:[
    {path: '' , component: SalasComponent , canActivate: [LoggedInGuard]},
    {path: 'add', component: FormSalaComponent, canActivate: [LoggedInGuard]},
    {path: ':id/edit', component: FormSalaComponent, canActivate: [LoggedInGuard]},
    ]},
     {path: 'encuestas', children:[
        {path: '' , component: EncuestasComponent , canActivate: [LoggedInGuard]},
         {path: 'add/:id', component: FormEncuestaComponent, canActivate: [LoggedInGuard]},
        // {path: ':id/edit', component: FormSalaComponent, canActivate: [LoggedInGuard]},
      ]},
     { path: '**',     component: LoginComponent },
    // {path: 'users', children: [
    //   {path: '', component: UsersComponent},
    //   {path: 'add', component: UsersFormComponent},
    //   {path: ':id/delete', component: UsersFormComponent},
    //   {path: ':id/edit', component: UsersFormComponent},
    // ]},
  // ]},
];

export const appRoutingProviders: any[] = [

];

export const appRoutes: any = RouterModule.forRoot(routes, { useHash: true });
