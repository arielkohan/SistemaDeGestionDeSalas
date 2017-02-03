import { Component, OnInit, AfterViewChecked, ViewChild } from '@angular/core';
import {NgForm, FormControl, FormBuilder} from '@angular/forms';
import { Sala } from '../../shared/model/Sala';
import { SalasService } from '../../shared/services/salas.service';
import { ReservaService } from '../../shared/services/reserva.service';
import { Router, ActivatedRoute } from '@angular/router';
import { StepState } from '@covalent/core';

import { MdDialog, MdDialogRef, MdDialogConfig, MdSnackBar, MdSnackBarRef } from '@angular/material';
import { Reserva } from '../../shared/model/Reserva';

@Component({
  selector: 'app-form-reserva',
  templateUrl: './form-reserva.component.html',
  styleUrls: ['./form-reserva.component.scss']
})
export class FormReservaComponent implements OnInit, AfterViewChecked {

	reservaForm: NgForm;
	@ViewChild('reservaForm') currentForm: NgForm;

	stepTwoState: StepState = StepState.None;
	stepOneState: StepState = StepState.None;

	today : Date ;

	formErrors = {
		'fechaReserva': '',
		'cantidadPersonas': '',
		'horaEgreso': '',
		'horaIngreso': '',
		'motivo': '',
		'tipoSala': '',
		//demas
	};

	validationMessages = {
		'fechaReserva':{
			'required': 'Fecha de reserva es requerida.',
			'minDateToday': "La fecha debe ser hoy o un día posterior."
		},
		'cantidadPersonas':{
			'required': 'Cantidad de personas requerido.',
			'min': "La cantidad de personas debe ser un valor mayor a uno."
		},
		 'horaEgreso':{
		 	'oneHourAfterTime': "El egreso debe ser por lo menos una hora mas tarde que el ingreso.",
			 'required': 'La hora de egreso es requerida.',
		 },
		 'horaIngreso':{
		 	'required': 'La hora de ingreso es requerida.',
		 },
		 'tipoSala':{
			 'required': 'El tipo de sala es requerido.',
		 },
		 'motivo':{
			 'required': 'El motivo es requerido.',
		 },
	};


	salas: Sala[];
	esEditar : boolean = false;
	reserva : Reserva = null; //CAMPO QUE SE LLENARÁ CON LA RESERVA SI ES EDITAR.
	loading : boolean = true;
	tipos: any[];
	sala: Sala = null;

	//elementos de busqueda y reserva
	fechaReserva: string;
	horaIngreso: string;
	horaEgreso: string;
	cantidadPersonas: number;
	motivo: string;
	servicio: boolean = false;
	almuerzo: boolean = false;
	proyector: boolean = false;
	tipoSala: any;

	

	constructor(
				private _salasService: SalasService,
				private _reservasService: ReservaService,
				private route : ActivatedRoute,
				 private _router : Router,
				// private _dialogService: TdDialogService,
				private _snackBarService: MdSnackBar
				 ) { }

	ngOnInit() {
		this.today = new Date();
		this.today.setHours(0,0,0,0);


		let p1 = this._salasService.getTipos()
					.then(response => {this.tipos = response; })
					.catch((errorMessage) => {console.log(errorMessage); });


		 this.route
        	.params
			.subscribe(params => {
				this.esEditar = !!params['id']; // --> Name must match wanted paramter
				let paramID = +params['id'];
				//TODO: communicate one componento with the other or do a get
				if( this.esEditar ){
					let p2 = this._reservasService.getReserva(paramID)
						.then(response => {
							this.reserva = response;

							const dateInicio = new Date(response.fechaInicio);
							const dateFin = new Date(response.fechaFin);

							this.fechaReserva = dateInicio.toISOString().slice(0,10);

							this.horaIngreso = dateInicio.getHours() < 10 ? "0" + dateInicio.toLocaleTimeString() : dateInicio.toLocaleTimeString();
							this.horaEgreso = dateFin.getHours() < 10 ? "0" + dateFin.toLocaleTimeString() : dateFin.toLocaleTimeString();
							this.cantidadPersonas = this.reserva.cantidadPersonas;
							this.motivo = this.reserva.motivo;
							this.servicio = this.reserva.servicio;
							this.almuerzo = this.reserva.almuerzo;
							this.proyector = this.reserva.proyector;
							this.sala = this.reserva.sala;
							this.salas = [this.sala];
						})
						.catch((error) => console.log("ERROR BUSCANDO LA RESERVA A EDITAR:", error));


					Promise.all([p1,p2]).then(()=>{
						for ( let p of this.tipos){
							if( p.tipoID == this.reserva.sala.tipoSalaID){
								this.tipoSala = p;
								break;
							}
						}
						this.loading = false;
					});

				} else {
				p1.then(() => this.loading = false);
			}

				
			});

		

		
	}

	ngAfterViewChecked() {
		this.formChanged();
	}

	formChanged() {
		if (this.currentForm === this.reservaForm) {  return; }
			this.reservaForm = this.currentForm;
		if (this.reservaForm) {
			this.reservaForm.valueChanges
			.subscribe(data => {this.onValueChanged(data)}); // this.updateHoraEgresoValidator()
		}

	}

	onValueChanged(data?: any) {
		
		if (!this.reservaForm) { return; }
		const form = this.reservaForm.form;
		
		if( this.sala && form.valid)
		{	
			this.salas = [];
			this.stepTwoRequired();
			this.sala = null; //PARA DESACTIVAR EL BOTON DE SUBMIT Y QUE EL USUARIO DEBA ELEGIR OTRA SALA
		}

		for (const field in this.formErrors) {
			// clear previous error message (if any)
			this.formErrors[field] = '';
			const control = form.get(field);
			if (control && control.dirty && !control.valid) { 
				const messages = this.validationMessages[field];
			
				for (const key in control.errors) {
					this.formErrors[field] += messages[key] + ' ';

				}
			}
		}
	}
	stepTwoRequired(){
		this.stepTwoState = StepState.Required;
	}
	stepTwoComplete(){
		this.stepTwoState = StepState.Complete;
	}
	stepOneComplete(){
		this.stepOneState = StepState.Complete;
	}

	buscarSalas(reservaForm : NgForm): void {

		if(reservaForm.form.valid){
			let dateTimeIngreso = new Date(this.fechaReserva + " " + this.horaIngreso);
			let dateTimeEgreso = new Date(this.fechaReserva + " " + this.horaEgreso);
			let tipoSalaID =  this.tipoSala.tipoID;
			this.loading = true;
			this._reservasService.getSalasParaReservar(dateTimeIngreso, dateTimeEgreso, tipoSalaID, this.cantidadPersonas)
				.then(response => {this.salas = response; this.loading = false;})
				.catch((errorMessage) => {console.log(errorMessage); this.loading = false;});
		}
	}

	reservar(): void {
		let reserva : Reserva = new Reserva();
		reserva.almuerzo = this.almuerzo;
		reserva.cantidadPersonas = this.cantidadPersonas;
		reserva.encuestaID = null;
		reserva.fechaInicio = new Date(this.fechaReserva + " " + this.horaIngreso);
		reserva.fechaFin = new Date(this.fechaReserva + " " + this.horaEgreso);
		reserva.motivo = this.motivo;
		reserva.proyector = this.proyector;
		reserva.salaID = this.sala.salaID;
		reserva.servicio = this.servicio;
		if( this.esEditar ) reserva.reservaID = this.reserva.reservaID;
		
		let p = this.esEditar ? this._reservasService.editReserva(reserva) : this._reservasService.reservar(reserva);

		p.then(() =>{
			this.reservaForm.reset();
			let snackBarRef: MdSnackBarRef<any> = 
					  this._snackBarService.open(this.esEditar ? 'Reserva modificada exitosamente.':'Reserva creada exitosamente.', null, {duration:1500});
			snackBarRef.afterDismissed().subscribe(
						  () => this._router.navigate(["reservas"])
					  );
		})
		.catch((error) => {console.error("error en reserva", error); this._snackBarService.open('Hubo un error. Intente nuevamente.', null, {duration:3500});});
	}

	// _MOCKSALAS(){

	// 	let _salas : Sala[] = [
	// 		new Sala(),
	// 		new Sala(),
	// 		new Sala(),
	// 		new Sala(),
	// 	];
	// 	let i = 0 ;
	// 	for(var s of _salas){
	// 		s.nombre = "asd";
	// 		s.ubicacion = "por aca";
	// 		s.salaID = i++;
	// 	}
	// 	this.salas = _salas;
	// }
}
