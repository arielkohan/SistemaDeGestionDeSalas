import { Component, OnInit } from '@angular/core';
import { ITdDynamicElementConfig, TdDynamicElement, TdDynamicType } from '@covalent/dynamic-forms';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MdSnackBar, MdSnackBarRef } from '@angular/material';

import { EncuestasService } from '../../shared/services/encuestas.service';
import { Pregunta } from '../../shared/model/Pregunta';
import { Encuesta } from '../../shared/model/Encuesta';
import { Respuesta } from '../../shared/model/Respuesta';

@Component({
  selector: 'app-form-encuesta',
  templateUrl: './form-encuesta.component.html',
  styleUrls: ['./form-encuesta.component.scss']
})
export class FormEncuestaComponent implements OnInit {

  preguntas: Pregunta[];
  preguntasElements : ITdDynamicElementConfig[] = [];
  reservaID: number = null;
  submit: boolean = false;

  constructor(
    private _encuestasService: EncuestasService,
    private route : ActivatedRoute,
    private _router : Router,
    private _snackBarService: MdSnackBar
  ) { }

  ngOnInit() {
    this._encuestasService.getPreguntas()
        .then((preguntas) =>{ this.preguntas = preguntas; this.parsePreguntas(preguntas)})
        .catch(error => console.log("ERROR AL TRAER LAS PREGUNTAS DE LA BD"));

     this.route
        	.params
			.subscribe(params => {
				this.reservaID = params['id']; // --> Name must match wanted paramter
				}
			);
      
  }

  responderEncuesta(form: any){
    const encuesta = new Encuesta();
    const respuestas: Respuesta[] = [];
    const formElementsValues:any[] = form.form.value;

    if( ! form.valid )
      return console.log("formulario invalido");

    for(let pregunta of this.preguntas){
      let respuesta = new Respuesta();
      respuesta.preguntaID = pregunta.preguntaID;
      respuesta.respuesta = formElementsValues["name-" + pregunta.preguntaID];
      respuestas.push(respuesta);
    }

    encuesta.respuestas = respuestas;

    this._encuestasService.responderEncuesta(this.reservaID, encuesta)
      .then(() => {
        this.submit = true;
        let snackBarRef: MdSnackBarRef<any> = 
              this._snackBarService.open('Encuesta completada exitosamente.', null, {duration:1500});
        snackBarRef.afterDismissed().subscribe(
                () => this._router.navigate(["encuestas"])
              );  
      })
      .catch((error) => {console.error("error en encuesta", error); this._snackBarService.open('Hubo un error. Intente nuevamente.', null, {duration:3500});});

  }

  parsePreguntas(preguntas: Pregunta[]): void {
    const temp : ITdDynamicElementConfig[] = [];
    for (let p of preguntas) {
      let opciones = [];
      if(p.tipoPregunta == Pregunta.preguntaCheckbox)
        for(let o of p.opciones)
          opciones.push(o.descripcion);

      var element: ITdDynamicElementConfig = {
      label: p.enunciado,
      default:"",
      name: "name-" + p.preguntaID.toString(),
      required: p.required ? true : false,
      type: (p.tipoPregunta == Pregunta.preguntaDesarrollo) ? TdDynamicElement.Textarea : TdDynamicElement.Select,
      selections: (opciones) ? opciones : null,
    };
      temp.push(
        element
      );
    }

    this.preguntasElements = temp;
  }





}
