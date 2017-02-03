import { Opcion } from './Opcion';

export class Pregunta {

    public static readonly preguntaDesarrollo = 1;
    public static readonly preguntaCheckbox = 2;

    preguntaID : number;
    enunciado : string;
    tipoPregunta : number;
    required: boolean;
    opciones?: Opcion[];
}
