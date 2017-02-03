import { Component, OnInit, AfterViewChecked, ViewChild } from '@angular/core';
import {NgForm, FormControl, FormBuilder} from '@angular/forms';
import { SalasService } from '../../shared/services/salas.service';
import { Sala } from '../../shared/model/Sala';
import { Router, ActivatedRoute, Params } from '@angular/router';
import {  MdSnackBar, MdSnackBarRef } from '@angular/material';
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable'

@Component({
  selector: 'app-form-sala',
  templateUrl: './form-sala.component.html',
  styleUrls: ['./form-sala.component.scss']
})
export class FormSalaComponent implements OnInit, AfterViewChecked {

tipos: any[];
sala : Sala = new Sala();
loading : boolean = true;

tipoSala : any;

esModificacion: boolean = false;
sub; 
id;

  constructor( 
	private _salasService: SalasService,
	private _router : Router,
	private _route: ActivatedRoute,
	// private _dialogService: TdDialogService,
	private _snackBarService: MdSnackBar
	  ) { }

  ngOnInit() {
		
		let p1 = this._salasService.getTipos()
					.then(response => {this.tipos = response; })
					.catch((errorMessage) => console.log(errorMessage));


		this.esModificacion = false;
		
		this.sub = this._route.params.subscribe(params => {
			this.id = +params['id']; // (+) converts string 'id' to a number
			console.log("ID; ", this.id);
			
			if( !! this.id ){
				this.esModificacion = true;
				let p2 = this._salasService.getSala(this.id)
					.then( response => {
						this.sala = response;
						// this.tipoSala = response.tipoSala
					} )
					.catch(error => {
							this._snackBarService.open('Hubo un error al buscar la sala para editar.', null, {duration:3500});
					});
				
				Promise.all([p1,p2]).then(()=>{
					for ( let p of this.tipos){
						if( p.tipoID == this.sala.tipoSalaID){
							this.tipoSala = p;
							break;
						}
					}
					this.loading = false;
				});
			
			} else {
				p1.then(() => this.loading = false);
			}

			}
		);

  }

	onSubmit() : void{
		
		this.sala.tipoSala = null;
		this.sala.tipoSalaID = this.tipoSala.tipoID;
		
		this.loading = true;
		let promise: Promise<void>;
		promise = (this.esModificacion) ? this._salasService.editSala(this.sala) : this._salasService.createSala(this.sala);
		promise.then(response => {
			this.loading = false;
			let snackBarRef: MdSnackBarRef<any> = 
					  this._snackBarService.open(! this.esModificacion ? 'Sala creada exitosamente.' : "Sala modificada exitosamente", null, {duration:1500});
			snackBarRef.afterDismissed().subscribe(
						  () => this._router.navigate(["salas"])
					  );
		}).catch(errorMessage =>{
			this.loading = false;
			console.error(errorMessage);
			this._snackBarService.open('Hubo un error. Intente nuevamente.', null, {duration:3500});
		});

	}

	doSomething():void{
	}


	ngAfterViewChecked() {
		this.formChanged();
	}

	salaForm: NgForm;
	@ViewChild('salaForm') currentForm: NgForm;

	formChanged() {
		if (this.currentForm === this.salaForm) {  return; }
			this.salaForm = this.currentForm;
		if (this.salaForm) {
			this.salaForm.valueChanges
			.subscribe(data => {this.onValueChanged(data)}); // this.updateHoraEgresoValidator()
		}

	}


	onValueChanged(data?: any) {

		if (!this.salaForm) { return; }
		const form = this.salaForm.form;
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



	formErrors = {
		'nombreSala': '',
		'capacidadSala': '',
		'tipoSala': '',
		'ubicacionSala': '',
		//demas
	};

	validationMessages = {
		'nombreSala':{
			'required': 'El nombre de sala es requerido.'
		},
		'capacidadSala':{
			'required': 'Cantidad de personas requerido.',
			'min': "La cantidad de personas debe ser un valor mayor a uno."
		},
		 'ubicacionSala':{
			 'required': 'La ubicación de la sala es requerida.',
		 },
		 'tipoSala':{
		 	'required': 'El tipo de sala es requerido.',
		 },
	};



  MOCK_TIPOS():void{
  	this.tipos = [
  		{
  			tipoID: 1,
  			descripcion: "Reunion"
  		},
  		{
  			tipoID: 1,
  			descripcion: "Capacitación"
  		},
  		{
  			tipoID: 3,
  			descripcion: "Auditorio"
  		}
  	]
  }
}
